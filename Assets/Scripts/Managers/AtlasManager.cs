using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using com.ootii.Messages;
using UnityEngine.UI;
using GameFramework.Localisation;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.U2D;

/// <summary>
/// 图片资源管理
/// </summary>
public class AtlasManager : MonoSingleton<AtlasManager>
{
   
    //图集列表
    public List<string> atlasNameList = new List<string>(){
        "Cover",
      //  "QTE"
    };

    private  Dictionary<string, SpriteAtlas> _cachedAtlasDict = new Dictionary<string, SpriteAtlas>();


    private int _downloadedCount;
    public void Init()
    {
        _downloadedCount = 0;
        for (int i = 0; i < atlasNameList.Count; i++)
        {
            AsyncOperationHandle handle = Addressables.DownloadDependenciesAsync(atlasNameList[i]);
            
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
        if (_downloadedCount == atlasNameList.Count)
        {
            MessageDispatcher.SendMessage(GameEvent.DOWNLOAD_ASSETBUNDLES_FINISH);
            
            
            //LoadAtlas("QTE");
        }
    }
    
    public void SetSprite(Image image, string atlasName, string imageName)
    {
        if (_cachedAtlasDict.ContainsKey(atlasName))
        {
            image.sprite = _cachedAtlasDict[atlasName].GetSprite(imageName);
        }
        else
        {
	        AsyncOperationHandle<SpriteAtlas> handle = Addressables.LoadAssetAsync<SpriteAtlas>(atlasName);
	       // AsyncOperationHandle<Sprite[]> handle = Addressables.LoadAssetAsync<Sprite[]>(atlasName);
            handle.Completed += (op) =>
            {
                if (op.Status == AsyncOperationStatus.Succeeded)
                {
	                //image.sprite = handle.Result[0];
	                if (!_cachedAtlasDict.ContainsKey(atlasName))
						_cachedAtlasDict.Add(atlasName, handle.Result);
	                image.sprite = _cachedAtlasDict[atlasName].GetSprite(imageName);
                }
            };
        }
    }
    public void LoadAtlas(string atlasName)
    {

		    AsyncOperationHandle<SpriteAtlas> handle = Addressables.LoadAssetAsync<SpriteAtlas>(atlasName);
		    // AsyncOperationHandle<Sprite[]> handle = Addressables.LoadAssetAsync<Sprite[]>(atlasName);
		    handle.Completed += (op) =>
		    {
			    if (op.Status == AsyncOperationStatus.Succeeded)
			    {
				    //image.sprite = handle.Result[0];
				    if (!_cachedAtlasDict.ContainsKey(atlasName))
					    _cachedAtlasDict.Add(atlasName, handle.Result);
				    else
				    {
					    _cachedAtlasDict[atlasName] = handle.Result;
				    }
			    }
		    };
    }
    public void ReleaseAtlas(string atlasName)
    {
	    if (_cachedAtlasDict.ContainsKey(atlasName))
	    {
		    SpriteAtlas atlas = _cachedAtlasDict[atlasName];
		    _cachedAtlasDict.Remove(atlasName);
		    Addressables.Release(atlas);
	    }
    }
    
    public void ReleaseAtlas(SpriteAtlas atlas)
    {
	    Addressables.Release(atlas);
    }
    
 //    
 //    
 //    
 //    //图片资源缓存字典
 //    public Dictionary<string,Dictionary<string, Sprite>> atlasSpritesDict;
 //
 //    //是否加载完毕
 //    public bool isLoaded = false;
 //
	// //需要常驻内存的图集
	// public List<string> commonAltasList = new List<string>(){
	// 	"atlas_common"
	// };
 //
	// //需要登录前预加载的图集
	// public List<string> preLoadAltasList = new List<string>(){
	// 	"atlas_maincity",
	// };
 //
 //    // public void Init()
 //    // {
 //    //     if(UIManager.Instance.useSpriteBundle)
 //    //     {
 //    //         //spritesDict = new Dictionary<string, Sprite>();
 //    //         atlasSpritesDict = new Dictionary<string, Dictionary<string, Sprite>>();
 //    //     }
 //    //        // spritesDict = new Dictionary<string, Sprite>();
 //    //
 //    //     MessageDispatcher.AddListener(GameEvent.DOWNLOAD_ASSETBUNDLES_FINISH, LoadData);
 //    // }
 //
	// public void Clear()
	// {
	// 	atlasSpritesDict.Clear();
	// 	// spritesDict.Clear();
	// 	// atlasDataSet = null;
	// 	isLoaded = false;
	// }
 //
 //    /// <summary>
 //    /// 加载图片资源到内存缓存
 //    /// </summary>
 //    /// <param name="rMessage"></param>
 //    public void LoadData(IMessage rMessage)
 //    {
 //        rMessage.IsHandled = true;
	// 	if (!UIManager.Instance.useSpriteBundle) {
 //            //不采用Bundle的情况下
 //            isLoaded = true;
 //            MessageDispatcher.SendMessage (GameEvent.LOADDATA_FINISH);
	// 	} else {
 //
 //            //清空加载的AtlasBundle
 //            List<string> keys = new List<string>(AssetBundleManager.Instance.atlasAssetBundleDict.Keys);
 //            for (int i = 0; i < keys.Count; i++)
 //            {
 //                AssetBundleManager.Instance.Unload(keys[i], BundleType.Atlas, false);
 //            }
 //
 //            //采用Bundle加载时，先载入需要常驻和需要预载Bundle
 //            for (int i = 0; i < commonAltasList.Count; i++)
 //            {
 //               StartCoroutine(LoadSpriteFromBundleAync(commonAltasList[i]));
 //            }
 //            for (int i = 0; i < preLoadAltasList.Count; i++)
 //            {
	// 		   StartCoroutine(LoadSpriteFromBundleAync(preLoadAltasList[i]));
 //            }
 //            isLoaded = true;
	// 		MessageDispatcher.SendMessage(GameEvent.LOADDATA_FINISH);
	// 	}
 //
 //    }
 //
 //
 //    public void ClearBundles()
 //    {
 //        //清空加载的AtlasBundle
 //        List<string> keys = new List<string>(AssetBundleManager.Instance.atlasAssetBundleDict.Keys);
 //        for (int i = 0; i < keys.Count; i++)
 //        {
 //            AssetBundleManager.Instance.Unload(keys[i], BundleType.Atlas, false);
 //        }
 //    }
 //
	// /// <summary>
	// /// 从内存卸载指定图集
	// /// </summary>
	// /// <param name="atlasName">图集名</param>
	// public void UnloadAtlas(string atlasName)
	// {
	// 	if(atlasSpritesDict.ContainsKey(atlasName))
	// 	{
	// 		Dictionary<string, Sprite> dict = atlasSpritesDict[atlasName];
	// 		var itr = dict.Keys.GetEnumerator();
	// 		while(itr.MoveNext())
	// 		{
	// 			Resources.UnloadAsset(dict[itr.Current]);
	// 		}
 //
	// 		atlasSpritesDict.Remove (atlasName);
	// 		LogManager.Log("Atlas unload :" + atlasName);
	// 	}
	// }
 //
 //    //从指定Bundle中获取包含的图片
 //    private IEnumerator LoadSpriteFromBundleAync(string bundleName)
 //    {
 //        AssetBundle bundle = AssetBundleManager.Instance.GetAssetBundle(bundleName, BundleType.Atlas);
 //        if (bundle == null)
 //        {
 //            LogManager.LogError("Atlas bundle not found:" + bundleName);
 //            yield return null;
 //        }
 //        else
 //        {
 //            AssetBundleRequest request = bundle.LoadAllAssetsAsync<Sprite>();
 //            yield return request;
 //            UnityEngine.Object[] sprites = request.allAssets;
 //            //Sprite[] sprites = bundle.LoadAllAssetsAsync<Sprite>();
 //
 //            Dictionary<string, Sprite> spritesDict = new Dictionary<string, Sprite>();
 //            for (int i = 0; i < sprites.Length; i++)
 //            {
 //                spritesDict[sprites[i].name] = (Sprite)sprites[i];
 //            }
 //            sprites = null;
 //            atlasSpritesDict[bundleName] = spritesDict;
 //            AssetBundleManager.Instance.Unload(bundleName, BundleType.Atlas, false);
 //        }
 //       
 //    }
 //    private IEnumerator LoadSpriteFromBundleAync(Image image, string bundleName, string imageName)
 //    {
 //        if (string.IsNullOrEmpty(bundleName))
 //        {
 //            LogManager.LogError("图集名称为空：" + image.gameObject.name);
 //            yield break;
 //        }
 //        if (string.IsNullOrEmpty(imageName))
 //        {
 //            LogManager.LogError("图片名称为空：" + image.gameObject.name);
 //            yield break;
 //        }
 //
 //        AssetBundle bundle = AssetBundleManager.Instance.GetAssetBundle(bundleName, BundleType.Atlas);
 //        if (bundle == null)
 //        {
 //            LogManager.LogError("Atlas bundle not found:" + bundleName);
 //            yield return null;
 //        }
 //        AssetBundleRequest request = bundle.LoadAllAssetsAsync<Sprite>();
 //        yield return request;
 //        UnityEngine.Object[] sprites = request.allAssets;
 //        //  Sprite[] sprites = bundle.LoadAllAssetsAsync<Sprite>();
 //
 //        Dictionary<string, Sprite> spritesDict = new Dictionary<string, Sprite>();
 //        for (int i = 0; i < sprites.Length; i++)
 //        {
 //            spritesDict[sprites[i].name] = sprites[i] as Sprite;
 //        }
 //        sprites = null;
 //        atlasSpritesDict[bundleName] = spritesDict;
 //        AssetBundleManager.Instance.Unload(bundleName, BundleType.Atlas, false);
 //        SetSprite(image, bundleName, imageName);
 //    }
 //
 //
 //
 //    private void LoadSpriteFromBundle(Image image, string bundleName, string imageName)
 //    {
 //        if (string.IsNullOrEmpty(bundleName))
 //        {
 //            LogManager.LogError("图集名称为空：" + image.gameObject.name);
 //        }
 //        if (string.IsNullOrEmpty(imageName))
 //        {
 //            LogManager.LogError("图片名称为空：" + image.gameObject.name);
 //        }
 //
 //        AssetBundle bundle = AssetBundleManager.Instance.GetAssetBundle(bundleName, BundleType.Atlas);
 //        if (bundle == null)
 //        {
 //            LogManager.LogError("Atlas bundle not found:" + bundleName);
 //            return;
 //        }
 //      //  AssetBundleRequest request = bundle.LoadAllAssetsAsync<Sprite>();
 //      //  yield return request;
 //       // UnityEngine.Object[] sprites = request.allAssets;
 //        Sprite[] sprites = bundle.LoadAllAssets<Sprite>();
 //
 //        Dictionary<string, Sprite> spritesDict = new Dictionary<string, Sprite>();
 //        for (int i = 0; i < sprites.Length; i++)
 //        {
 //            spritesDict[sprites[i].name] = sprites[i] as Sprite;
 //        }
 //        sprites = null;
 //        atlasSpritesDict[bundleName] = spritesDict;
 //       // AssetBundleManager.Instance.Unload(bundleName, BundleType.Atlas, false);
 //        SetSprite(image, bundleName, imageName);
 //    }
 //
 //
 //
 //    private void SetSprite(Image image, string atlasName, string imageName)
 //    {
 //        if (atlasSpritesDict.ContainsKey(atlasName))
 //        {
 //            if (atlasSpritesDict[atlasName].ContainsKey(imageName))
 //            {
 //                if(image != null)
 //                    image.sprite = atlasSpritesDict[atlasName][imageName];
 //            }
 //            else
 //            {
 //                LogManager.LogError("The imageName not found:" + atlasName + " / " + imageName +  " / " + image.gameObject.name);
 //                image.sprite = null;
 //            }
 //        }
 //    }
 //
 //
 //    //public void GetSprite(Image image)
 //    //{
 //    //    image.sprite = AtlasManager.Instance.atlasSpritesDict["atlas_common"]["logo"];
 //    //}
 //
 //    //获取图片资源
 //    public void GetSprite(Image image, string atlasName, string imageName)
 //    {
 //        GetSprite(image, atlasName, imageName, false);
 //    }
 //    ////获取图片资源
 //    //public Sprite GetSprite(string atlasName, string imageName)
 //    //{
 //    //    return null;
 //    //}
 //    //获取图片资源
 //    //public Sprite GetSprite(string atlasName)
 //    //{
 //    //    return null;
 //    //}
 //    //获取图片资源
 //    public void GetSprite(Image image, string atlasName, string imageName,bool needTraslation)
 //    {
	// 	//转换多语言版本图集
 //        if(needTraslation)
 //        {
 //            //atlasName = GameMain.Instance.curLanguageString + "_" + atlasName;
 //            atlasName = GlobalLocalisation.Language + "_" + atlasName;
 //        }
 //
	// 	//如果没有加载Bundle时则先加载Bundle(需同步加载,如需异步则需要改动)
 //        if (!atlasSpritesDict.ContainsKey(atlasName))
 //        {
 //            //GameMain.Instance.StartCoroutine(LoadSpriteFromBundle(image, atlasName, imageName));
 //            LoadSpriteFromBundle(image, atlasName, imageName);
 //        }
 //        else
 //        {
 //            SetSprite(image, atlasName, imageName);
 //        }	
 //    }
 //
 //    //获取图片资源
 //    public void GetSprite(Image image, string atlasImageName, bool needTraslation = false)
 //    {
 //        string[] sArray = atlasImageName.Split('/');
 //        if(sArray.Length > 1)
 //        {
 //            GetSprite(image, sArray[0], sArray[1], needTraslation);
 //        }
 //        else
 //        {
 //            LogManager.LogError("The imageName error!" + atlasImageName);
 //            image.sprite = null;
 //        }
 //        /*
 //        if (spritesDict != null && spritesDict.ContainsKey(buttonImageName))
 //            return spritesDict[buttonImageName];
 //        else
 //        {
 //
 //           // (LanguageDataSet)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Datas/AssetData/LanguageDataSet.asset", typeof(LanguageDataSet));
 //
 //            return Resources.Load("Pic/UI/" + buttonImageName,typeof(Sprite)) as Sprite;
 //
 //            //以后可以从bundle等其他方式加载
 //           // return null;
 //        } 
 //        */       
 //    }
}





