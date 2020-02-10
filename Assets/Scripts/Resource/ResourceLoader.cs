using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
//using SimpleObjectPool;
//using EasyMotion2D;
//using LitJson;

/// <summary>
/// 资源加载类
/// </summary>
public class ResourceLoader : MonoBehaviour
{

    //单例
    private static ResourceLoader instance = null;
    public static ResourceLoader Instance
    {
        get
        {
            return instance;
        }
    }

    private static Dictionary<string, Dictionary<string, UnityEngine.Object>> objectMaps = new Dictionary<string, Dictionary<string, UnityEngine.Object>>();

    public static T GetObjectDirectly<T>(string name) where T : UnityEngine.Object
    {
        name = name.ToLower();
        T reObject = null;
        Type type = typeof(T);
        UnityEngine.Object obj;
        if (GetValue(name, type, out obj))
        {
            if (obj != null)
            {
                reObject = obj as T;
            }
        }

        if (reObject != null)
        {
            return reObject;
        }

        //if (useBundle)
        //{
        //    // 从bundle获取
        //    Meteor.Log.LogSystem.Debug("从bundle加载：" + name);
        //    reObject = (T)GetDirectlyFromBundle(name, type);
        //}
        // 从Resource获取
        if (reObject == null)
        {
            reObject = Resources.Load<T>(name) as T;
        }

        if (reObject != null)
        {
            SetValue(name, type, reObject);
            return reObject;
        }
        LogManager.Log("无法加载：" + name);
        return null;
    }



    public static bool GetValue(string name, Type type, out UnityEngine.Object obj)
    {
        obj = null;
        Dictionary<string, UnityEngine.Object> objectMap;
        if (objectMaps.TryGetValue(name, out objectMap))
        {
            if (objectMap.TryGetValue(type.FullName, out obj))
            {
                return true;
            }
        }

        return false;
    }

    public static void SetValue(string name, Type type, UnityEngine.Object obj)
    {
        //暂时不启用缓存机制
        return;
        Dictionary<string, UnityEngine.Object> objectMap = null;
        if (objectMaps.ContainsKey(name))
        {
            objectMap = objectMaps[name];
        }

        if (objectMap == null)
        {
            objectMap = new Dictionary<string, UnityEngine.Object>();
        }

        objectMap[type.FullName] = obj;
        objectMaps[name] = objectMap;
    }






    /*
	public static bool useBundle = false;
	public static string targetVersion;
	private static string BaseUrl = "http://192.168.0.89/yzr2/bundle";
	private static string PathPrefix = "file://";
	private static string MainBundle = "AssetBundles";
	private static string BundleSuffix = ".bundle";

	public static string SizeDataFile = "Extra/sizedata/bundlesizedata";
	public static string PersistentDataPath = ApplicationHelper.persistentDataPath + "/" + MainBundle + "/";
	public static string StreamingAssetsPath = //Application.streamingAssetsPath + "/AssetBundles/";
#if UNITY_EDITOR
			Application.streamingAssetsPath + "/" + MainBundle + "/";
#elif UNITY_IPHONE
			Application.streamingAssetsPath + "/" + MainBundle + "/";
#elif UNITY_ANDROID
			Application.dataPath + "!assets/" + MainBundle + "/";
#elif UNITY_STANDALONE_WIN
            Application.streamingAssetsPath + "/" + MainBundle + "/";
#else
			string.Empty;
#endif

	private static AssetBundleManifest assetBundleManifest;
	public static AssetBundleManifest LoadedManifest { get { return assetBundleManifest; } }
	private static Dictionary<string, AssetBundle> dependedBundle = new Dictionary<string, AssetBundle>();
	private static Dictionary<string, SpriteAnimationClip[]> loadedAnimationObjects = new Dictionary<string, SpriteAnimationClip[]>();
	private static Dictionary<string, Dictionary<string, UnityEngine.Object>> objectMaps = new Dictionary<string, Dictionary<string, UnityEngine.Object>>();
	private static List<string> allBundleNames = new List<string>();
	public static DownloadManager downloadMgr = new DownloadManager();
	//private static Dictionary<string, int> dependedCount = new Dictionary<string, int>();

	//单例
	private static ResourceLoader instance = null;
	public static ResourceLoader Instance
	{
		get
		{
			return instance;
		}
	}

    public void Init()
    {
        instance = this;

		useBundle = GameEngine.Instance.UseBundle;
        targetVersion = null;
		assetBundleManifest = null;
        dependedBundle = new Dictionary<string, AssetBundle>();
        loadedAnimationObjects = new Dictionary<string, SpriteAnimationClip[]>();
        objectMaps = new Dictionary<string, Dictionary<string, UnityEngine.Object>>();
		allBundleNames = new List<string>();
        downloadMgr = new DownloadManager();
		//dependedCount = new Dictionary<string, int>();

		InitPackageVersion();
		InitManifest();
    }

    public void Release()
    {
        instance = null;
        if (ObjectPoolManager.Instance != null)
        {
            ObjectPoolManager.Instance.Release();
        }

		//dependedCount.Clear();
        objectMaps.Clear();
        loadedAnimationObjects.Clear();
        
		allBundleNames.Clear();
        downloadMgr.Clear();

        ClearBundleCacheAll();
        dependedBundle.Clear();
    }

	private void InitPackageVersion()
	{
		JsonData packageVersionData = null;
		string localFileName = ApplicationHelper.persistentDataPath + "/PackageVersion.data";
		// 加载本地数据
		try
		{
			packageVersionData = (JsonData)SaveHelper.GetData<JsonData>(localFileName);
		}
		catch (Exception e)
		{
			packageVersionData = new JsonData();
		}
		finally
		{
			if (packageVersionData.Keys.Count <= 0 || (string)packageVersionData["PackageVersion"] != GameEngine.Instance.packageVersion)
			{
				//删除persistent bundle path
				if(Directory.Exists(PersistentDataPath))
					Directory.Delete(PersistentDataPath, true);
				//保存包版本信息
				packageVersionData["PackageVersion"] = GameEngine.Instance.packageVersion;
				SaveHelper.SetData(localFileName, packageVersionData);
			}
		}
	}


	public static void InitManifest()
	{
		if (!useBundle)
			return;

		AssetBundle bundle = GetAssetBundle(MainBundle);

		if (bundle != null)
		{
			assetBundleManifest = (AssetBundleManifest)bundle.LoadAsset("AssetBundleManifest");
			bundle.Unload(false);
		}

		if (assetBundleManifest != null)
		{
			string[] bundleNames = assetBundleManifest.GetAllAssetBundles();
			for (int i = 0; i < bundleNames.Length; i++)
			{
				if (string.IsNullOrEmpty(bundleNames[i]))
					continue;

				allBundleNames.Add(bundleNames[i]);
				//string[] dps = assetBundleManifest.GetAllDependencies(bundleNames[i]);
				//for (int j = 0; j < dps.Length; j++)
				//{
				//	if (string.IsNullOrEmpty(dps[j]))
				//		continue;

					//if (!dependedCount.ContainsKey(dps[j]))
					//{
					//	dependedCount[dps[j]] = 0;
					//}
				//}
			}
		}
	}

	public static void BatchLoad()
	{
		AssetBundle bundle = GetAssetBundle(MainBundle);

		if (bundle != null)
		{
			assetBundleManifest = (AssetBundleManifest)bundle.LoadAsset("AssetBundleManifest");
			bundle.Unload(false);
		}

		if (assetBundleManifest != null)
		{
			string[] bundleNames = assetBundleManifest.GetAllAssetBundles();
			for (int i = 0; i < bundleNames.Length; i++)
			{
				if (string.IsNullOrEmpty(bundleNames[i]))
					continue;

				string[] dps = assetBundleManifest.GetAllDependencies(bundleNames[i]);
				for (int j = 0; j < dps.Length; j++)
				{
					if (string.IsNullOrEmpty(dps[j]))
						continue;

					if (dependedBundle.ContainsKey(dps[j]) && dependedBundle[dps[j]] != null)
						continue;

					AssetBundle dBundle = GetAssetBundle(dps[j]);
					dependedBundle[dps[j]] = dBundle;
				}
			}
		}
	}

	public static void Install()
	{
		if (downloadMgr == null)
			Meteor.Log.LogSystem.Debug("木有下载manager");

		downloadMgr.Clear(); // Clear all download data
		downloadMgr.StartVerify(StreamingAssetsPath); // Start Verification
	}

	public static void VerifyResource()
	{
		BaseUrl = GameEngine.Instance.DomainData.cdnUrl;
		string cdnUrl = string.Empty;

		if (Application.platform == RuntimePlatform.IPhonePlayer)
			cdnUrl = BaseUrl + "/ios/resource/" + targetVersion + "/AssetBundles/";
		else if (Application.platform == RuntimePlatform.Android)
			cdnUrl = BaseUrl + "/android/resource/" + targetVersion + "/AssetBundles/";
		else
			cdnUrl = BaseUrl + "/editor/resource/" + targetVersion + "/AssetBundles/";

		if (downloadMgr == null)
			Meteor.Log.LogSystem.Debug("木有下载manager");

		downloadMgr.Clear(); // Clear all download data

		UnityEngine.Object bundleSize = null;
		AssetBundle ab = GetAssetBundle(SizeDataFile);
		if (ab != null)
		{
			bundleSize = ab.LoadAsset<UnityEngine.Object>("bundlesizedata");
			ab.Unload(false);
		}
		if (bundleSize != null)
		{
			downloadMgr.OnLoadBundleSizeInfo(bundleSize, false); // Set old resource Dictionary
		}
		downloadMgr.StartVerify(cdnUrl); // Start Verification
	}

	public static void DownloadResource()
	{
		if (downloadMgr == null)
			Meteor.Log.LogSystem.Debug("木有下载manager");

		downloadMgr.StartDownLoad();
	}

	public static void VerifyData(string version)
	{
        Meteor.Log.LogSystem.Debug("服务器版本： " + version);
		BaseUrl = GameEngine.Instance.DomainData.cdnUrl;
		string cdnUrl = string.Empty;

		if(Application.platform == RuntimePlatform.IPhonePlayer)
			cdnUrl = BaseUrl + "/ios/data/" + version + "/DataBundles/";
		else if (Application.platform == RuntimePlatform.Android)
			cdnUrl = BaseUrl + "/android/data/" + version + "/DataBundles/";
		else
			cdnUrl = BaseUrl + "/editor/data/" + version + "/DataBundles/";

		if (downloadMgr == null)
			Meteor.Log.LogSystem.Debug("木有下载manager");

		downloadMgr.Clear(); // Clear all download data
		downloadMgr.StartVerify(cdnUrl); // Start Verification
	}

	public static void DownloadData()
	{
		downloadMgr.StartDownLoad();
	}

	public static void ClearVersionData()
	{
		string versionFile = "gamedata/versiondata";
		string versionBundle = versionFile + ".bundle";
		if (objectMaps.ContainsKey(versionFile))
		{
			objectMaps[versionFile].Clear();
			objectMaps.Remove(versionFile);
		}

		if (dependedBundle.ContainsKey(versionBundle))
		{
			dependedBundle[versionBundle].Unload(false);
			dependedBundle.Remove(versionBundle);
		}
	}

	public static void ClearBundleCache()
	{
        Dictionary<string, AssetBundle> dataBundle = new Dictionary<string, AssetBundle>();

		var itr = dependedBundle.GetEnumerator();
		while (itr.MoveNext())
		{
            if (itr.Current.Value != null)
            {
                if (itr.Current.Key.StartsWith("res") || itr.Current.Key.Equals("res/ui_res/ui_pic/ui.bundle") || itr.Current.Key.Contains("font") || itr.Current.Key.Equals("res/ui_res/ui_pic/skill.bundle") || itr.Current.Key.StartsWith("data") || itr.Current.Key.StartsWith("gamedata") | itr.Current.Key.StartsWith("ai"))
                {
                    dataBundle.Add(itr.Current.Key, itr.Current.Value);
                }
                else
                {
                    itr.Current.Value.Unload(false);
                }
            }
		}
		dependedBundle.Clear();

        dependedBundle = dataBundle;
	}

    public static void ClearBundleCacheWithoutTexture()
    {
        Dictionary<string, AssetBundle> dataBundle = new Dictionary<string, AssetBundle>();

        var itr = dependedBundle.GetEnumerator();
        while (itr.MoveNext())
        {
            if (itr.Current.Value != null)
            {
                if (itr.Current.Key.StartsWith("res") || itr.Current.Key.StartsWith("animation") || itr.Current.Key.StartsWith("live2d") || itr.Current.Key.Equals("res/ui_res/ui_pic/ui.bundle") || itr.Current.Key.Contains("font") || itr.Current.Key.Equals("res/ui_res/ui_pic/skill.bundle") || itr.Current.Key.StartsWith("data") || itr.Current.Key.StartsWith("gamedata") | itr.Current.Key.StartsWith("ai"))
                {
                    dataBundle.Add(itr.Current.Key, itr.Current.Value);
                }
                else
                {
                    itr.Current.Value.Unload(false);
                }
            }
        }
        dependedBundle.Clear();

        dependedBundle = dataBundle;
    }

    public static void ClearBundleCacheAfterLoad()
    {
        Dictionary<string, AssetBundle> dataBundle = new Dictionary<string, AssetBundle>();

        var itr = dependedBundle.GetEnumerator();
        while (itr.MoveNext())
        {
            if (itr.Current.Value != null)
            {
                if (itr.Current.Key.Equals("res/ui_res/ui_pic/ui.bundle") || itr.Current.Key.Contains("font"))
                {
                    dataBundle.Add(itr.Current.Key, itr.Current.Value);
                }
                else
                {
                    itr.Current.Value.Unload(false);
                }
            }
        }
        dependedBundle.Clear();

        dependedBundle = dataBundle;
    }

    public static void ClearBundleCacheFalse()
    {
        var itr = dependedBundle.GetEnumerator();
        while (itr.MoveNext())
        {
            if (itr.Current.Value != null)
            {
                {
                    itr.Current.Value.Unload(false);
                }
            }
        }
        dependedBundle.Clear();
    }

    public static void ClearBundleCacheAll()
    {
        AssetBundle uiBundle;
        if (dependedBundle.TryGetValue("res/ui_res/ui_pic/ui.bundle", out uiBundle) == true)
        {
            uiBundle.Unload(true);
        }

        var itr = dependedBundle.GetEnumerator();
        while (itr.MoveNext())
        {
            if (itr.Current.Value != null)
            {
                itr.Current.Value.Unload(false);
            }
        }
        dependedBundle.Clear();
    }

	public static void ClearCache()
	{
		var itr = objectMaps.Values.GetEnumerator();
		while (itr.MoveNext())
		{
			itr.Current.Clear();
		}
		objectMaps.Clear();

		loadedAnimationObjects.Clear();

		//Resources.UnloadUnusedAssets();
	}

	public static void UnloadObjects(List<string> objs)
	{
		for (int i = 0; i < objs.Count; i++)
		{
			// 删除实例
			if (objectMaps.ContainsKey(objs[i]))
			{
				objectMaps[objs[i]].Clear();
				objectMaps.Remove(objs[i]);
			}
			else if (loadedAnimationObjects.ContainsKey(objs[i]))
			{
				loadedAnimationObjects.Remove(objs[i]);
			}
			else
			{
				Meteor.Log.LogSystem.Debug("Resource can't be recognized in ObjectMaps either in loadedAnimationObjects, name: " + objs[i]);
			}

			// 去除依赖
			//if (assetBundleManifest != null)
			//{
			//	string bundleName = objs[i];
			//	if (bundleName.EndsWith(".bundle") == false)
			//	{
			//		bundleName += ".bundle";
			//	}
			//	string[] dps = assetBundleManifest.GetAllDependencies(bundleName.ToLower());
			//	for (int j = 0; j < dps.Length; j++)
			//	{
			//		if (string.IsNullOrEmpty(dps[j]))
			//			continue;

			//		if (dependedBundle.ContainsKey(dps[j]) && dependedBundle[dps[j]] != null)
			//		{
			//			dependedCount[dps[j]] -= 1;
			//			if (dependedCount[dps[j]] <= 0)
			//			{
			//				dependedCount[dps[j]] = 0;
			//				dependedBundle[dps[j]].Unload(false);
			//				dependedBundle.Remove(dps[j]);
			//			}
			//		}
			//	}

			//	if (dependedBundle.ContainsKey(bundleName) && dependedBundle[bundleName] != null)
			//	{
			//		dependedCount[bundleName] -= 1;
			//		if (dependedCount[bundleName] <= 0)
			//		{
			//			dependedCount[bundleName] = 0;
			//			dependedBundle[bundleName].Unload(false);
			//			dependedBundle.Remove(bundleName);
			//		}
			//	}
			//}
		}
		//Resources.UnloadUnusedAssets();
	}

	public static void UnloadObjectsWithoutGC(List<string> objs)
	{
		for (int i = 0; i < objs.Count; i++)
		{
			// 删除实例
			if (objectMaps.ContainsKey(objs[i]))
			{
				Dictionary<string, UnityEngine.Object> dict = objectMaps[objs[i]];
				var itr = dict.Keys.GetEnumerator();
				while(itr.MoveNext())
				{
					Resources.UnloadAsset(dict[itr.Current]);
				}

				objectMaps[objs[i]].Clear();
				objectMaps.Remove(objs[i]);
			}
			else if (loadedAnimationObjects.ContainsKey(objs[i]))
			{
				loadedAnimationObjects.Remove(objs[i]);
			}
			else
			{
				Meteor.Log.LogSystem.Debug("Resource can't be recognized in ObjectMaps either in loadedAnimationObjects, name: " + objs[i]);
			}

			// 去除依赖
			//if (assetBundleManifest != null)
			//{
			//	string bundleName = objs[i];
			//	if (bundleName.EndsWith(".bundle") == false)
			//	{
			//		bundleName += ".bundle";
			//	}
			//	string[] dps = assetBundleManifest.GetAllDependencies(bundleName.ToLower());
			//	for (int j = 0; j < dps.Length; j++)
			//	{
			//		if (string.IsNullOrEmpty(dps[j]))
			//			continue;

			//		if (dependedBundle.ContainsKey(dps[j]) && dependedBundle[dps[j]] != null)
			//		{
			//			dependedCount[dps[j]] -= 1;
			//			if (dependedCount[dps[j]] <= 0)
			//			{
			//				dependedCount[dps[j]] = 0;
			//				dependedBundle[dps[j]].Unload(false);
			//				dependedBundle.Remove(dps[j]);
			//			}
			//		}
			//	}

			//	if (dependedBundle.ContainsKey(bundleName) && dependedBundle[bundleName] != null)
			//	{
			//		dependedCount[bundleName] -= 1;
			//		if (dependedCount[bundleName] <= 0)
			//		{
			//			dependedCount[bundleName] = 0;
			//			dependedBundle[bundleName].Unload(false);
			//			dependedBundle.Remove(bundleName);
			//		}
			//	}
			//}
		}
	}

	public static bool GetValue(string name, Type type, out UnityEngine.Object obj)
	{
		obj = null;
		Dictionary<string, UnityEngine.Object> objectMap;
		if (objectMaps.TryGetValue(name, out objectMap))
		{
			if (objectMap.TryGetValue(type.FullName, out obj))
			{
				return true;
			}
		}

		return false;
	}

	public static void SetValue(string name, Type type, UnityEngine.Object obj)
	{
		Dictionary<string, UnityEngine.Object> objectMap = null;
		if (objectMaps.ContainsKey(name))
		{
			objectMap = objectMaps[name];
		}

		if (objectMap == null)
		{
			objectMap = new Dictionary<string, UnityEngine.Object>();
		}

		objectMap[type.FullName] = obj;
		objectMaps[name] = objectMap;
	}

	public static SpriteAnimationClip[] GetAnimation(string name)
	{
		name = name.ToLower();
		SpriteAnimationClip[] reClips = null;
		if (loadedAnimationObjects.TryGetValue(name, out reClips))
		{
			if (reClips != null)
			{
				return reClips;
			}
		}
		else
		{
			if (useBundle)
			{
				// 从Bundle获取
				reClips = GetAnimationFromBundle(name);
			}
			// 从Resource获取
			if (reClips == null)
			{
				reClips = Resources.LoadAll<SpriteAnimationClip>(name);
			}
			if (reClips != null)
			{
				OnGetAnimation(name, reClips);
				return reClips;
			}
		}
		Meteor.Log.LogSystem.Debug("无法加载：" + name);
		return null;
	}

    public static T GetObjectDirectly<T>(string name) where T : UnityEngine.Object
    {
		name = name.ToLower();
        T reObject = null;
		Type type = typeof(T);
		UnityEngine.Object obj;
		if (GetValue(name, type, out obj))
		{
			if (obj != null)
			{
				reObject = obj as T;
			}
		}

		if (reObject != null)
		{
			return reObject;
		}

		if (useBundle)
		{
			// 从bundle获取
			Meteor.Log.LogSystem.Debug("从bundle加载：" + name);
			reObject = (T)GetDirectlyFromBundle(name, type);
		}
		// 从Resource获取
		if (reObject == null)
		{
			Meteor.Log.LogSystem.Debug("从Resource加载：" + name);
			reObject = Resources.Load<T>(name) as T;
		}

		if (reObject != null)
		{
			SetValue(name, type, reObject);
			return reObject;
		}
		Meteor.Log.LogSystem.Debug("无法加载：" + name);
        return null;
    }

	private static UnityEngine.Object GetDirectlyFromBundle(string name, Type type)
	{
		string bundleName = "";
		string shortName = "";
		if (name.StartsWith("animation"))
		{
			string[] temp = name.Split('/');
			bundleName = string.Format("{0}/{1}",temp[0],temp[1]);
			if (type == typeof(Texture))
			{
				shortName = "assets/resources/" + name + ".png";
			}
			else
			{
				shortName = "assets/resources/" + name + ".asset";
			}
		}
		else
		{
			bundleName = name;
			shortName = name.Substring(name.LastIndexOf("/") + 1);
		}

		if (bundleName.EndsWith(".bundle") == false)
		{
			bundleName += ".bundle";
		}

		return GetDirectlyFromBundle(shortName, bundleName, type);
	}

	private static UnityEngine.Object GetDirectlyFromBundle(string name, string bundleName, Type type)
	{
		if (!allBundleNames.Contains(bundleName))
			return null;

		// 加载依赖资源项
		if (assetBundleManifest != null)
		{
			string[] dps = assetBundleManifest.GetAllDependencies(bundleName.ToLower());
			for (int i = 0; i < dps.Length; i++)
			{
				if (string.IsNullOrEmpty(dps[i]))
					continue;

				if (dependedBundle.ContainsKey(dps[i]) && dependedBundle[dps[i]] != null)
				{
					continue;
				}

				AssetBundle dBundle = GetAssetBundle(dps[i]);
				dependedBundle[dps[i]] = dBundle;
			}
		}
		else
		{
			Meteor.Log.LogSystem.Error("assetBundleManifest不存在");
		}

		// 加载本体
		AssetBundle ab = null;
		if (dependedBundle.ContainsKey(bundleName) && dependedBundle[bundleName] != null) // 属于bundle已被加载
		{
			ab = dependedBundle[bundleName];
		}
		else
		{
			ab = GetAssetBundle(bundleName);
			dependedBundle[bundleName] = ab;
		}

		if (ab == null)
		{
			return null;
		}

		UnityEngine.Object gobj;
		if (name == "UI.dll" || type == typeof(TextAsset))
		{
			UnityEngine.Object[] objs = ab.LoadAllAssets();
			gobj = objs[0];
		}
		else
		{
			gobj = ab.LoadAsset(name, type) as UnityEngine.Object;
            ClearBundleCacheWithoutTexture();
		}
		//if (gobj != null)
		//{
		//	gobj.name = name;
		//}

		return gobj;
	}

	private static SpriteAnimationClip[] GetAnimationFromBundle(string name)
	{
		string realName = name;
		if (realName.EndsWith(".bundle") == false)
		{
			realName += ".bundle";
		}

		// 加载依赖资源项
		if (assetBundleManifest != null)
		{
			string[] dps = assetBundleManifest.GetAllDependencies(realName.ToLower());
			for (int i = 0; i < dps.Length; i++)
			{
				if (string.IsNullOrEmpty(dps[i]))
					continue;

				if (dependedBundle.ContainsKey(dps[i]) && dependedBundle[dps[i]] != null)
				{
					continue;
				}

				AssetBundle dBundle = GetAssetBundle(dps[i]);
				dependedBundle[dps[i]] = dBundle;
			}
		}
		else
		{
			Meteor.Log.LogSystem.Error("assetBundleManifest不存在");
		}

		// 加载本体
		AssetBundle ab = null;
		if (dependedBundle.ContainsKey(realName) && dependedBundle[realName] != null) // 属于bundle已被加载
		{
			ab = dependedBundle[realName];
		}
		else
		{
			ab = GetAssetBundle(realName);
			dependedBundle[realName] = ab;
		}

		if (ab == null)
		{
			return null;
		}

		SpriteAnimationClip[] clips = ab.LoadAllAssets<SpriteAnimationClip>();
		return clips;
	}

	public static AssetBundle GetAssetBundle(string name)
	{
		AssetBundle dBundle = GetAssetBundle(name, PersistentDataPath); // 先从PersistentDataPath加载
		if (dBundle == null) dBundle = GetAssetBundle(name, StreamingAssetsPath); // 如果没有从StreamingAssetsPath加载
		if (dBundle == null)
		{
			Meteor.Log.LogSystem.Debug("Bundle load error! filename : " + name);
		}

		return dBundle;
	}

	public static AssetBundle GetAssetBundle(string name, string path)
	{
		if (name.EndsWith("AssetBundles") == false && name.EndsWith(".bundle") == false)
		{
			name += ".bundle";
		}
		string url = path + name;
		Meteor.Log.LogSystem.Debug("url:" + url);

		if (path == PersistentDataPath && !File.Exists(url))
		{
			Meteor.Log.LogSystem.Debug("目标文件不存在，url:" + url);
			return null;
		}

		AssetBundle bundle = AssetBundle.LoadFromFile(url);
		return bundle;
	}

	// 读取某个资源
	private IEnumerator StartLoadGameObject(string name, string path)
	{
		string realName = name;
		if (realName.EndsWith(".bundle") == false)
		{
			realName += ".bundle";
		}
		string shortName = name.Substring(name.LastIndexOf("/") + 1);

		// 加载AssetBunles文件
		string mUrl = path + "AssetBundles";
		Meteor.Log.LogSystem.Debug("url:"+ mUrl);
		WWW mwww = new WWW(mUrl);//WWW.LoadFromCacheOrDownload(mUrl, 0);
		yield return mwww;
		if (!string.IsNullOrEmpty(mwww.error))
		{
			throw new Exception("mWWW download had an error:" + mwww.error);
		}

		AssetBundle bundle = mwww.assetBundle;
		AssetBundleManifest manifest = (AssetBundleManifest)bundle.LoadAsset("AssetBundleManifest");
		bundle.Unload(false);

		// 加载依赖资源项
		string[] dps = manifest.GetAllDependencies(realName);
		AssetBundle[] abs = new AssetBundle[dps.Length];
		for (int i = 0; i < dps.Length; i++)
		{
			string dUrl = path + dps[i];
			WWW dwww = new WWW(dUrl);//WWW.LoadFromCacheOrDownload(dUrl, manifest.GetAssetBundleHash(dps[i]));
			yield return dwww;
			if (!string.IsNullOrEmpty(dwww.error))
			{
				Meteor.Log.LogSystem.Debug(dwww.error);
			}
			abs[i] = dwww.assetBundle;
		}

		// 加载本体
		string url = path + realName;
		WWW www = new WWW(url);
		yield return www;
		if (!string.IsNullOrEmpty(www.error))
		{
			Meteor.Log.LogSystem.Debug(www.error);
		}
		else
		{
			AssetBundle ab = www.assetBundle;
			UnityEngine.Object obj = ab.LoadAsset(shortName) as UnityEngine.Object;
			if (obj != null)
			{
				UnityEngine.Object objInstance = obj;//Instantiate(gobj);
				objInstance.name = shortName;
				OnLoadGameObject(name, objInstance);
			}
			ab.Unload(false);
		}
		for (int i = 0; i < abs.Length; i++)
		{
			abs[i].Unload(false);
		}
	}

	// Helper method
	private void OnLoadGameObject(string name, UnityEngine.Object obj)
	{
		SetValue(name, typeof(UnityEngine.Object), obj);
		ObjectPoolManager.Instance.Load(obj); // add to pool
	}

	private static void OnGetAnimation(string name, SpriteAnimationClip[] clips)
	{
		loadedAnimationObjects[name] = clips;
	}
    */


}
