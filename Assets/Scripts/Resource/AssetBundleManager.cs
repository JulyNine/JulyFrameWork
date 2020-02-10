using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.ootii.Messages;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
/// <summary>
/// bundle控制类
/// </summary> 
public class AssetBundleManager : MonoSingleton<AssetBundleManager>
{
    
    //
    public List<string> bundleNameList = new List<string>(){
      //  "QTE",
        "QTEPrefabs",
        "PlayVideoWindow"
    };

    
    
    private int _downloadedCount;
    
    public void Init()
    {
        _downloadedCount = 0;
       // Clear();
       // MessageDispatcher.AddListener(GameEvent.DOWNLOAD_ASSETBUNDLES, DownloadAssetBundles, true);
        //MessageDispatcher.AddListener(GameEvent.DOWNLOAD_ASSETBUNDLE_SUCCESS, DownloadAssetBundleSuccess);
        for (int i = 0; i < bundleNameList.Count; i++)
        {
            AsyncOperationHandle handle = Addressables.DownloadDependenciesAsync(bundleNameList[i]);
            
            handle.Completed += DownloadCompleted;
        }
        
        
        
    }
    
    private void DownloadCompleted(AsyncOperationHandle handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            _downloadedCount++;
            CheckDownloadFinish();
        }
    }
    
    private void CheckDownloadFinish()
    {
        if (_downloadedCount == bundleNameList.Count)
        {
            MessageDispatcher.SendMessage(GameEvent.DOWNLOAD_ASSETBUNDLES_FINISH);
            
            
            //LoadAtlas("QTE");
        }
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    //ͼƬbwundle�����ֵ��
    public Dictionary<string, AssetBundle> atlasAssetBundleDict = new Dictionary<string, AssetBundle>();

    //bundle���� �����б�
    public List<BundleDownloadTask> bundleDownloadTaskList = new List<BundleDownloadTask>();

    private BundleType currentBundleType;

    //����bundle�����ֵ��
    public Dictionary<string, AssetBundle> gameDataAssetBundleDict = new Dictionary<string, AssetBundle>();
    public bool loadLocalAsset = true;
    public Platform platform;

    public WWW wwwProgress;

    /// <summary>
    ///     ��ȡbudnle���� URL
    /// </summary>
    public string AssetbundlePathURL
    {
        get
        {
            if (loadLocalAsset)
            {
                //StringBuilder DOWNLOADASSETPATH = new StringBuilder(Application.streamingAssetsPath + "/Datas/BundleData/Android");
                if (Application.platform == RuntimePlatform.Android)
                    return new StringBuilder(Application.streamingAssetsPath + "/Datas/BundleData/Android/").ToString();

                if (Application.platform == RuntimePlatform.IPhonePlayer)
                    return "file://" + new StringBuilder(Application.streamingAssetsPath + "/Datas/BundleData/IOS/");

                if (Application.platform == RuntimePlatform.WindowsEditor)
                {
                    if (platform == Platform.Android)
                        return string.Format("{0}\\{1}", Application.dataPath,
                            "\\StreamingAssets\\Datas\\BundleData\\Android\\");
                    if (platform == Platform.IOS)
                        return string.Format("{0}\\{1}", Application.dataPath,
                            "\\StreamingAssets\\Datas\\BundleData\\IOS\\");
                    if (platform == Platform.WebGL)
                        return string.Format("{0}\\{1}", Application.dataPath,
                            "\\StreamingAssets\\Datas\\BundleData\\WebGL\\");
                    return string.Format("{0}\\{1}", Application.dataPath,
                        "\\StreamingAssets\\Datas\\BundleData\\Editor\\");
                }

                if (Application.platform == RuntimePlatform.OSXEditor)
                    return "file://" + new StringBuilder(Application.streamingAssetsPath + "/Datas/BundleData/IOS/");
                //if(GameMain.Instance.assetDefautPlatform == Platform.Android)
                //	return string.Format("{0}\\{1}", Application.dataPath, "\\StreamingAssets\\Datas\\BundleData\\Android\\");
                //else if (GameMain.Instance.assetDefautPlatform == Platform.IOS)
                //	return string.Format("{0}\\{1}", Application.dataPath, "\\StreamingAssets\\Datas\\BundleData\\IOS\\");
                //else
                //return string.Format("{0}\\{1}", Application.dataPath, "\\StreamingAssets\\Datas\\BundleData\\Editor\\");
                if (Application.platform == RuntimePlatform.WebGLPlayer)
                    return string.Format("{0}\\{1}", Application.dataPath,
                        "\\StreamingAssets\\Datas\\BundleData\\WebGL\\");
                //if(GameMain.Instance.assetDefautPlatform == Platform.Android)
                //	return string.Format("{0}\\{1}", Application.dataPath, "\\StreamingAssets\\Datas\\BundleData\\Android\\");
                //else if (GameMain.Instance.assetDefautPlatform == Platform.IOS)
                //	return string.Format("{0}\\{1}", Application.dataPath, "\\StreamingAssets\\Datas\\BundleData\\IOS\\");
                //else
                //return string.Format("{0}\\{1}", Application.dataPath, "\\StreamingAssets\\Datas\\BundleData\\Editor\\");
                return string.Format("{0}\\{1}", Application.dataPath,
                    "\\StreamingAssets\\Datas\\BundleData\\Editor\\");

                //return new StringBuilder(Application.streamingAssetsPath + "\\Datas\\BundleData\\Editor").ToString();
            }

            var DOWNLOADASSETPATH = new StringBuilder("http://47.92.0.165/GameData/");
            switch (ConfigSettings.Instance.server)
            {
                default:
                    DOWNLOADASSETPATH.Append("");
                    break;
            }

            if (Application.platform == RuntimePlatform.Android)
                DOWNLOADASSETPATH.Append("Android/");
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
                DOWNLOADASSETPATH.Append("IOS/");
            else if (Application.platform == RuntimePlatform.WindowsEditor)
                DOWNLOADASSETPATH.Append("Editor/");
            else if (Application.platform == RuntimePlatform.WindowsPlayer)
                DOWNLOADASSETPATH.Append("Editor/");
            else
                DOWNLOADASSETPATH.Append("Android/");

            //switch (GameMain.Instance.server)
            //{
            //    default:
            //        DOWNLOADASSETPATH.Append(GameMain.Instance.gameVer);
            //        DOWNLOADASSETPATH.Append(".");
            //        DOWNLOADASSETPATH.Append(GameMain.Instance.resourceVer);
            //        DOWNLOADASSETPATH.Append("/");
            //        //DOWNLOADASSETPATH += GameMain.Instance.gameVer + "." + GameMain.Instance.resourceVer + "/"; ;
            //        break;
            //}
            return DOWNLOADASSETPATH.ToString();
        }
    }


    /// <summary>
    ///     ��ȡbudnle���� URL
    /// </summary>
    public string AssetbundleLocalPathURL
    {
        get
        {
            if (loadLocalAsset)
            {
                //StringBuilder DOWNLOADASSETPATH = new StringBuilder(Application.streamingAssetsPath + "/Datas/BundleData/Android");
                if (Application.platform == RuntimePlatform.Android)
                    return new StringBuilder(Application.streamingAssetsPath + "/Datas/BundleData/Android/").ToString();

                if (Application.platform == RuntimePlatform.IPhonePlayer)
                    return new StringBuilder(Application.streamingAssetsPath + "/Datas/BundleData/IOS/").ToString();

                if (Application.platform == RuntimePlatform.WindowsEditor)
                {
                    if (platform == Platform.Android)
                        return string.Format("{0}\\{1}", Application.dataPath,
                            "\\StreamingAssets\\Datas\\BundleData\\Android\\");
                    if (platform == Platform.IOS)
                        return string.Format("{0}\\{1}", Application.dataPath,
                            "\\StreamingAssets\\Datas\\BundleData\\IOS\\");
                    return string.Format("{0}\\{1}", Application.dataPath,
                        "\\StreamingAssets\\Datas\\BundleData\\Editor\\");
                }

                if (Application.platform == RuntimePlatform.OSXEditor)
                    return new StringBuilder(Application.streamingAssetsPath + "/Datas/BundleData/IOS/").ToString();
                //if(GameMain.Instance.assetDefautPlatform == Platform.Android)
                //	return string.Format("{0}\\{1}", Application.dataPath, "\\StreamingAssets\\Datas\\BundleData\\Android\\");
                //else if (GameMain.Instance.assetDefautPlatform == Platform.IOS)
                //	return string.Format("{0}\\{1}", Application.dataPath, "\\StreamingAssets\\Datas\\BundleData\\IOS\\");
                //else
                //return string.Format("{0}\\{1}", Application.dataPath, "\\StreamingAssets\\Datas\\BundleData\\Editor\\");
                if (Application.platform == RuntimePlatform.WebGLPlayer)
                    return new StringBuilder(Application.streamingAssetsPath + "/Datas/BundleData/WebGL/").ToString();
                //if(GameMain.Instance.assetDefautPlatform == Platform.Android)
                //	return string.Format("{0}\\{1}", Application.dataPath, "\\StreamingAssets\\Datas\\BundleData\\Android\\");
                //else if (GameMain.Instance.assetDefautPlatform == Platform.IOS)
                //	return string.Format("{0}\\{1}", Application.dataPath, "\\StreamingAssets\\Datas\\BundleData\\IOS\\");
                //else
                //return string.Format("{0}\\{1}", Application.dataPath, "\\StreamingAssets\\Datas\\BundleData\\Editor\\");
                return string.Format("{0}\\{1}", Application.dataPath,
                    "\\StreamingAssets\\Datas\\BundleData\\Editor\\");

                //return new StringBuilder(Application.streamingAssetsPath + "\\Datas\\BundleData\\Editor").ToString();
            }

            var DOWNLOADASSETPATH = new StringBuilder("http://47.92.0.165/GameData/");
            switch (ConfigSettings.Instance.server)
            {
                default:
                    DOWNLOADASSETPATH.Append("");
                    break;
            }

            if (Application.platform == RuntimePlatform.Android)
                DOWNLOADASSETPATH.Append("Android/");
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
                DOWNLOADASSETPATH.Append("IOS/");
            else if (Application.platform == RuntimePlatform.WindowsEditor)
                DOWNLOADASSETPATH.Append("Editor/");
            else if (Application.platform == RuntimePlatform.WindowsPlayer)
                DOWNLOADASSETPATH.Append("Editor/");
            else
                DOWNLOADASSETPATH.Append("Android/");

            //switch (GameMain.Instance.server)
            //{
            //    default:
            //        DOWNLOADASSETPATH.Append(GameMain.Instance.gameVer);
            //        DOWNLOADASSETPATH.Append(".");
            //        DOWNLOADASSETPATH.Append(GameMain.Instance.resourceVer);
            //        DOWNLOADASSETPATH.Append("/");
            //        //DOWNLOADASSETPATH += GameMain.Instance.gameVer + "." + GameMain.Instance.resourceVer + "/"; ;
            //        break;
            //}
            return DOWNLOADASSETPATH.ToString();
        }
    }

    // ��ȡ����
    // public void Init()
    // {
    //     Clear();
    //     MessageDispatcher.AddListener(GameEvent.DOWNLOAD_ASSETBUNDLES, DownloadAssetBundles, true);
    //     MessageDispatcher.AddListener(GameEvent.DOWNLOAD_ASSETBUNDLE_SUCCESS, DownloadAssetBundleSuccess);
    // }
    /// <summary>
    /// �������bundle����
    /// </summary>
    /// <param name="dataVersionInfo"></param>
    /// <param name="bundleType"></param>
    //public void AddDownloadTask(DataVersionInfo dataVersionInfo,string bundleType)
    //{
    //    BundleType type = (BundleType)Enum.Parse(typeof(BundleType), bundleType, true);
    //    if(type == BundleType.Atlas)
    //    {
    //        if (!GameMain.Instance.useSpriteBundle)
    //            return;
    //    }
    //    bundleDownloadTaskList.Add(new BundleDownloadTask(dataVersionInfo.data, dataVersionInfo.keyName, dataVersionInfo.version, type));
    //}

    /// <summary>
    ///     ����bundle
    /// </summary>
    /// <param name="rMessage"></param>
    public void DownloadAssetBundles(IMessage rMessage)
    {
        rMessage.IsHandled = true;
        //if(bundleDownloadTaskList.Count == 0)
        //{
        //    CheckDownloadStatus();
        //}
        //else
        {
            for (var i = 0; i < bundleDownloadTaskList.Count; i++)
                //  int dataVersion = 1;
                if (bundleDownloadTaskList[i].isDownloaded == false)
                {
                    Debug.Log(i + "bundleDownloadTaskList.Count" + bundleDownloadTaskList.Count);
                    if (loadLocalAsset)
                        StartCoroutine(LoadLocalAssetBundle(bundleDownloadTaskList[i]));
                    else
                        StartCoroutine(DownloadAssetBundle(bundleDownloadTaskList[i]));
                    return;
                }

            LogManager.Log("Download Bundle Success Finish");
            MessageDispatcher.SendMessage(GameEvent.DOWNLOAD_ASSETBUNDLES_FINISH);
            // DataVersion.UpdateLocalData(ResourceManager.Instance.serverDataVersionPath);
        }
    }

    /// <summary>
    ///     ����bundle
    /// </summary>
    /// <param name="rMessage"></param>
    public void DownloadAssetBundleSuccess(IMessage rMessage)
    {
        DownloadAssetBundles(rMessage);
    }

    public void DownloadAssetBundles(BundleType type)
    {
        /*
        currentBundleType = type;
        if (currentBundleType == BundleType.Data)
            InitStatusDict(ResourceManager.dataBundleNameList);
        if (currentBundleType == BundleType.Atlas)
            InitStatusDict(ResourceManager.Instance.atlasDataSet.atlasBundleNameList);

        List<string> keys = new List<string>(statusDict.Keys);
        for (int i = 0; i < keys.Count; i++)
        {
            int dataVersion = 1;
            if (GameMain.Instance.loadLocalAsset)
                GameMain.Instance.StartCoroutine(LoadLocalAssetBundle(keys[i], dataVersion));
            else
                GameMain.Instance.StartCoroutine(downloadAssetBundle(keys[i], dataVersion));
        }
        */
    }

    /*
    public void LoadLocalAssetBundle(string name, int version)
    {
        string keyName = name + version.ToString();
        if (dictAssetBundleRefs.ContainsKey(keyName))
        {
             return;
        }
        AssetBundleRef abRef = new AssetBundleRef(name, version);
        string bundleName = name + ".unity3d";
        abRef.assetBundle = (AssetBundle)Resources.Load(bundleName, typeof(AssetBundle));
        dictAssetBundleRefs.Add(keyName, abRef);
        statusDict[name].isDownloaded = true;
        Debug.Log("Local Load Success!" + name);
        CheckDownloadStatus();
    }
    */
    /// <summary>
    ///     �ӱ��ؼ���bundle
    /// </summary>
    /// <param name="task"></param>
    /// <returns></returns>
    private IEnumerator LoadLocalAssetBundle(BundleDownloadTask task)
    {
        //AssetBundle bundle = GetAssetBundle(task.keyName, task.bundleType);
        //if (bundle != null)
        //{
        //    AddBundleToDict(task, bundle);
        //    yield return null;
        //}
        //else
        {
            string bundleName;
            if (Application.platform != RuntimePlatform.Android)
            {
                //bundleName = "file://" + string.Format("{0}/{1}", AssetbundlePathURL, task.name.ToLower());

                if (AssetbundlePathURL.Contains("file") || AssetbundlePathURL.Contains("http"))
                    bundleName = AssetbundlePathURL + task.name.ToLower();
                else
                    bundleName = "file://" + string.Format("{0}/{1}", AssetbundlePathURL, task.name.ToLower());
            }
            else
            {
                bundleName = AssetbundlePathURL + task.name.ToLower();
            }

            LogManager.Log("LoadLocalAssetBundle" + bundleName);
            task.reDownloadTimesLeft--;
            task.startDownloadTime = DateTime.Now;
            //using (WWW www = WWW.LoadFromCacheOrDownload(bundleName, task.version))
            using (var www = new WWW(bundleName))
            {
                // abRef.www.threadPriority = ThreadPriority.Low;
                while (!www.isDone)
                    if (DateTime.Now > task.startDownloadTime.AddSeconds(15) && task.reDownloadTimesLeft > 0)
                        StartCoroutine(LoadLocalAssetBundle(task));
                    //LoadLocalAssetBundle(task);
                    else
                        yield return null;

                if (www.error != null)
                {
                    LogManager.LogError("Local Load www.error: " + www.error);
                    if (task.reDownloadTimesLeft > 0)
                        StartCoroutine(LoadLocalAssetBundle(task));
                    // LoadLocalAssetBundle(task);
                }
                else
                {
                    //  Tools.CreateFile(task.name, www.bytes);
                    AddBundleToDict(task, www.assetBundle);
                }
            }
        }
    }

    /// <summary>
    ///     �����غõ�bundle���浽�ڴ��ֵ�ȴ�����
    /// </summary>
    /// <param name="task"></param>
    /// <param name="bundle"></param>
    public void AddBundleToDict(BundleDownloadTask task, AssetBundle bundle)
    {
        switch (task.bundleType)
        {
            case BundleType.GameData:
                gameDataAssetBundleDict.Add(task.name, bundle);
                break;

            case BundleType.Atlas:
                atlasAssetBundleDict.Add(task.name, bundle);
                break;
        }

        LogManager.Log("DownLoad bundle Success!" + task.name);
        task.isDownloaded = true;
        var lMessage = new Message();
        lMessage.Type = GameEvent.DOWNLOAD_ASSETBUNDLE_SUCCESS;
        lMessage.Data = new DownloadAssetbundleSuccessArgs {task = task};
        lMessage.Sender = this;
        MessageDispatcher.SendMessage(lMessage);
        //CheckDownloadStatus();
    }

    public IEnumerator DownloadAssetBundle(BundleDownloadTask task)
    {
        string bundleName;
        //if (Application.platform == RuntimePlatform.WindowsEditor)
        //{
        //    bundleName = string.Format("file://{0}/{1}", AssetbundlePathURL, task.name.ToLower());
        //}
        //else
        bundleName = AssetbundlePathURL + task.name.ToLower();
        LogManager.Log("DownLoadAssetBundle" + bundleName);
        //using (WWW www = WWW.LoadFromCacheOrDownload(bundleName, task.version))
        using (var www = new WWW(bundleName))
        {
            // abRef.www.threadPriority = ThreadPriority.Low;
            while (!www.isDone) yield return null;

            if (www.error != null)
                LogManager.LogError("Load www.error: " + www.error);
            else
                //   Tools.CreateFile(task.name, www.bytes);
                AddBundleToDict(task, www.assetBundle);
        }

        //return null;
    }

    /*
    // Download an AssetBundle
    public IEnumerator downloadAssetBundle(string name, int version)
    {
        string keyName = name + version.ToString();
		//Debug.Log("downloadAssetBundle:" + keyName);
        if (dictAssetBundleRefs.ContainsKey(keyName))
		{
			//Debug.Log("has keyName:" + keyName);
			//Debug.Log("dictAssetBundleRefs ContainsKey:" + dictAssetBundleRefs.Count);
			yield return null;
		}
        else
        {
			AssetBundleRef abRef = new AssetBundleRef(name, version);
			using (abRef.www = WWW.LoadFromCacheOrDownload(AssetbundlePathURL + name + ".unity.3d", version))
			{
				abRef.www.threadPriority = ThreadPriority.Low;
				while (!abRef.www.isDone)
				{
					
                    MessageEventArgs msg2 = new MessageEventArgs();
                    msg2.AddMessage("resourceName", abRef.url);
                    msg2.AddMessage("progress", abRef.www.progress.ToString());
                    EventManager.Instance.PostEvent(EventDefine.UpdateDownloadProgress, msg2);
                    
					yield return null;
				}
				//Debug.Log("abRef.www.isDonectAssetBundleRefs" + abRef.www.isDone);
				//yield return abRef.www;
				if (abRef.www.error != null)
				{
                    //statusDict[name].reDownloadTimes++;
                   // if(statusDict[name].reDownloadTimes < 10)
                    {
                        GameMain.Instance.StartCoroutine(downloadAssetBundle(name, version));
                    }
                  //  else
                    {
                        LogManager.Log("WWW download:" + abRef.www.error + "at path" + name);
                    }
                    //Debug.Log("WWW download:" + abRef.www.error + "at path" + url);

                    
                    MessageEventArgs msgError = new MessageEventArgs();
                    msgError.AddMessage("resourceName", url);
                    EventManager.Instance.PostEvent(EventDefine.DownloadError, msgError);
                    
                }
				else
				{
					abRef.assetBundle = abRef.www.assetBundle;
					//Debug.Log("1 Add dictAssetBundleRefs" + keyName);
					dictAssetBundleRefs.Add(keyName, abRef);
                   // statusDict[name].isDownloaded = true;
                }
                LogManager.Log("success!" + name);
                CheckDownloadStatus();
            }
        }
    }
	*/

    // Download an AssetBundle
    public WWW DownloadAssetBundle(string url, int version)
    {
        return null;
        /*
        string keyName = url + version.ToString();
        if (dictAssetBundleRefs.ContainsKey(keyName))
            return null;
        else
		{
			
			AssetBundleRef abRef = new AssetBundleRef(url, version);
			
			abRef.www = WWW.LoadFromCacheOrDownload(url, version);

			abRef.assetBundle = abRef.www.assetBundle;
            dictAssetBundleRefs.Add(keyName, abRef);
			//Debug.Log("2 Add dictAssetBundleRefs" + keyName);
 
			MessageEventArgs msg = new MessageEventArgs();
			msg.AddMessage("resourceName", url);
			EventManager.Instance.PostEvent(EventDefine.LoadAssetFinish, msg);

			return abRef.www;
        }
        */
    }

    // Get an AssetBundle
    public AssetBundle GetAssetBundle(string keyName, BundleType type)
    {
        AssetBundle bundle = null;

        switch (type)
        {
            case BundleType.GameData:
                gameDataAssetBundleDict.TryGetValue(keyName, out bundle);
                if (bundle == null)
                {
                    bundle = GetAssetBundleFromLocal(keyName);
                    if (bundle != null)
                        gameDataAssetBundleDict.Add(keyName, bundle);
                    else
                        LogManager.LogError("bundle not exist" + keyName);
                }

                break;
            case BundleType.Atlas:
                atlasAssetBundleDict.TryGetValue(keyName, out bundle);
                if (bundle == null)
                {
                    bundle = GetAssetBundleFromLocal(keyName);
                    if (bundle != null)
                        atlasAssetBundleDict.Add(keyName, bundle);
                    else
                        LogManager.LogError("bundle not exist" + keyName);
                }

                break;
        }

        return bundle;
    }

    private AssetBundle GetAssetBundleFromLocal(string keyName)
    {
        //Debug.Log("GetAssetBundleFromLocal" + keyName);
        //  AssetBundle bundle = AssetBundle.LoadFromFile(GlobalSystemData.TEMP_PATH + "//" + keyName);
        //Debug.Log("GetAssetBundleFromLocal:" + AssetbundleLocalPathURL + "/" + keyName);
        var bundle = AssetBundle.LoadFromFile(AssetbundleLocalPathURL + keyName);
        return bundle;
    }

    // Unload an AssetBundle
    public void Unload(string keyName, BundleType type, bool all)
    {
        switch (type)
        {
            case BundleType.GameData:
                if (gameDataAssetBundleDict.ContainsKey(keyName))
                {
                    gameDataAssetBundleDict[keyName].Unload(all);
                    gameDataAssetBundleDict.Remove(keyName);
                }

                break;
            case BundleType.Atlas:
                if (atlasAssetBundleDict.ContainsKey(keyName))
                {
                    atlasAssetBundleDict[keyName].Unload(all);
                    atlasAssetBundleDict.Remove(keyName);
                }

                break;
        }
    }

    public void Clear()
    {
        gameDataAssetBundleDict.Clear();
        atlasAssetBundleDict.Clear();
        bundleDownloadTaskList.Clear();
        //assetBundleDict.Clear();
    }

    /// <summary>
    ///     ����Ƿ�����bundle�����������
    /// </summary>
    public void CheckDownloadStatus()
    {
        for (var i = 0; i < bundleDownloadTaskList.Count; i++)
            if (bundleDownloadTaskList[i].isDownloaded == false)
            {
                LogManager.Log("Waiting for Downloading : " + bundleDownloadTaskList[i].name);
                return;
            }

        MessageDispatcher.SendMessage(GameEvent.DOWNLOAD_ASSETBUNDLES_FINISH);
        //  GameDataVersion.UpdateLocalData(ResourceManager.Instance.serverDataVersionPath);
        LogManager.Log("Download Bundle Success Finish");
    }
}

/// <summary>
///     bundle ����
/// </summary>
public enum BundleType
{
    GameData,
    Atlas,
    Effect
}

/// <summary>
///     ����bundle����
/// </summary>
public class BundleDownloadTask
{
    public BundleType bundleType;
    public bool isDownloaded;
    public string keyName;
    public string name;
    public int reDownloadTimesLeft;
    public DateTime startDownloadTime;
    public int version;

    public BundleDownloadTask()
    {
        isDownloaded = false;
        reDownloadTimesLeft = 10;
    }

    public BundleDownloadTask(string _name, string _keyName, int _version, BundleType _bundleType)
    {
        name = _name;
        keyName = _keyName;
        version = _version;
        bundleType = _bundleType;
        isDownloaded = false;
        reDownloadTimesLeft = 10;
    }
}

/// <summary>
///     ����bundle�ɹ�����
/// </summary>
public class DownloadAssetbundleSuccessArgs
{
    public BundleDownloadTask task;
}


public partial class GameEvent
{
    //���MultiSelecteButton
    public static string MULTISELECTEBUTTON_CLICK = "MultiSelecteButtonClick";
    public static string MULTISELECTEBUTTON_CLICK_ACK = "MultiSelecteButtonClickAck";
}