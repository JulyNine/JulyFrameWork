using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using com.ootii.Messages;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
/// <summary>
/// 资源管理
/// </summary>
public class ResourceManager
{
    // 获取单例
    private static ResourceManager m_instance = null;   // 单例
    public static ResourceManager Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new ResourceManager();
            }
            return m_instance;
        }
    }

   // private DataVersion serverDataVersion;
   // private DataVersion localDataVersion;
   // //public string serverDataVersionPath = Application.streamingAssetsPath + "/Datas/XMLData/Version/AssetDataVersion.xml";
   // //public string serverDataVersionPath = "http://47.92.0.165" + "/GameData/AssetDataVersion.xml";
   // public string localDataVersionPath = GlobalSystemData.TEMP_PATH + "/AssetDataVersion.xml";

   // public string serverDataVersionPath
   // {
   //     get
   //     {
   //        if(GameMain.Instance.loadLocalAsset)
   //         {
   //             return Application.streamingAssetsPath + "/Datas/XMLData/Version/AssetDataVersion.xml";
   //         }
   //        else
   //             return "http://47.92.0.165" + "/GameData/AssetDataVersion.xml";
   //     }
   // }


   // public void Init()
   // {
   //     m_instance = this;
   //     MessageDispatcher.AddListener(GameEvent.LOADDATA_FINISH, LoadDataFinish);
   //     MessageDispatcher.AddListener(GameEvent.LOADXML_FINISH, LoadXMLFinish, true);
   // }
   // /// <summary>
   // /// 下载资源
   // /// </summary>
   // public void DownLoadResource()
   // {
   //     CheckResource();
   // }
   // /// <summary>
   // /// 检查资源版本
   // /// </summary>
   // public void CheckResource()
   // {
   //     //检查数据和资源版本号，需要更新则下载
   //     //string dataVersionPath = Application.dataPath + "/Datas/XMLData/Version/AssetDataVersion.xml";

   //     //加载服务器数据版本信息
   //     Debug.Log("dataVersionPath" + serverDataVersionPath);
   //     DataVersion.Load(serverDataVersionPath);
   // }
   // /// <summary>
   // /// 加载XML完成
   // /// </summary>
   // /// <param name="rMessage"></param>
   // public void LoadXMLFinish(IMessage rMessage)
   // {
   //     rMessage.IsHandled = true;
   //     serverDataVersion = rMessage.Data as DataVersion;
   //     DownLoadAllAssetBundle();
   //     /*
   //     if (serverDataVersion == null)
   //     {
   //         serverDataVersion = rMessage.Data as DataVersion;
   //         //加载本地数据版本信息       
   //         FileInfo file = new FileInfo(localDataVersionPath);
   ////         if (!file.Exists)
   //         {
   //             DownLoadAllAssetBundle();
   //         }
   //   //      else
   //         {
   //  //           DataVersion.Load(localDataVersionPath);
   //         }
   //     }
   //     else
   //     {
   //         localDataVersion = rMessage.Data as DataVersion;
   //         DownLoadAssetBundle();
   //     }
   //     OpenResourceUpdateWindow();
   //     */
   // }
   // private void OpenResourceUpdateWindow()
   // {
   //     //Message lMessage = new Message();
   //     //lMessage.Type = GameEvent.OPEN_UIWINDOW;
   //     //lMessage.Sender = this;
   //     //OpenUIWindowArgs args = new OpenUIWindowArgs() { windowKey = UIWindows.ResourceUpdateWindow };
   //     //lMessage.Data = args;
   //     //lMessage.Delay = EnumMessageDelay.IMMEDIATE;
   //     //MessageDispatcher.SendMessage(lMessage);
   // }

   // private void DownLoadAllAssetBundle()
   // {
   //     Debug.Log("DownLoadAllAssetBundle");
   //     //DataVersion.UpdateLocalData(serverDataVersionPath);
   //     for (int i = 0; i < serverDataVersion._dataList.Count; i++)
   //     {
   //         string[] sArray = serverDataVersion._dataList[i].data.Split('_');
   //         if (sArray.Length > 2)
   //         {
   //             string bundleLanguage = sArray[0];
   //             string bundleType = sArray[1];
   //             if (!bundleLanguage.Equals(Enum.GetName(typeof(GameLanguage), GameMain.Instance.curLanguage), StringComparison.CurrentCultureIgnoreCase))
   //                 continue;
   //             AssetBundleManager.Instance.AddDownloadTask(serverDataVersion._dataList[i], bundleType);
   //             //Debug.Log(version._dataList[i].data);
   //         }
   //         else
   //         {
   //             string bundleType = sArray[0];
   //             AssetBundleManager.Instance.AddDownloadTask(serverDataVersion._dataList[i], bundleType);
   //         }
   //     }
   //     MessageDispatcher.SendMessage(GameEvent.DOWNLOAD_ASSETBUNDLES);
   // }

   // private void DownLoadAssetBundle()
   // {
   //     if(serverDataVersion.gameVersion != localDataVersion.gameVersion)
   //     {
   //         for (int i = 0; i < serverDataVersion._dataList.Count; i++)
   //         {
   //             bool needUpdate = true;
   //             for (int j = 0; j < localDataVersion._dataList.Count; j++)
   //             {
   //                 if (localDataVersion._dataList[j].keyName == serverDataVersion._dataList[i].keyName && localDataVersion._dataList[j].version == serverDataVersion._dataList[i].version)
   //                 {
   //                     needUpdate = false;
   //                     break;
   //                 }
   //             }
   //             if (needUpdate)
   //             {
   //                 string[] sArray = serverDataVersion._dataList[i].data.Split('_');
   //                 if (sArray.Length > 2)
   //                 {
   //                     string bundleLanguage = sArray[0];
   //                     string bundleType = sArray[1];
   //                     if (!bundleLanguage.Equals(Enum.GetName(typeof(GameLanguage), GameMain.Instance.curLanguage), StringComparison.CurrentCultureIgnoreCase))
   //                         continue;
   //                     AssetBundleManager.Instance.AddDownloadTask(serverDataVersion._dataList[i], bundleType);
   //                     //Debug.Log(version._dataList[i].data);
   //                 }
   //                 else
   //                 {
   //                     string bundleType = sArray[0];
   //                     AssetBundleManager.Instance.AddDownloadTask(serverDataVersion._dataList[i], bundleType);
   //                 }
   //             }
   //         }
   //     }
   //     MessageDispatcher.SendMessage(GameEvent.DOWNLOAD_ASSETBUNDLES);
   // }
   // /// <summary>
   // /// 加载资源完成
   // /// </summary>
   // /// <param name="rMessage"></param>
   // public void LoadDataFinish(IMessage rMessage)
   // {
   //     rMessage.IsHandled = true;
   //     if (LanguageManager.Instance.isLoaded && DataManager.Instance.isLoaded )
   //     {

   //         Message IMessage = new Message();
   //         IMessage.Type = GameEvent.OPEN_UIWINDOW;
   //         IMessage.Sender = this;
   //         OpenUIWindowArgs args = new OpenUIWindowArgs() { windowKey = UIWindows.CityMenuWindow };
   //         IMessage.Data = args;
   //         //IMessage.Delay = enum
   //         MessageDispatcher.SendMessage(IMessage);


   //         //  CityManager.Instance.GameInit();
   //         //Message lMessage = new Message();
   //         //lMessage.Type = GameEvent.OPEN_UIWINDOW;
   //         //lMessage.Sender = this;
   //         //OpenUIWindowArgs args = new OpenUIWindowArgs() { windowKey = UIWindows.OwnerLandsWindow };
   //         //lMessage.Data = args;
   //         ////lMessage.Delay = EnumMessageDelay.IMMEDIATE;
   //         //MessageDispatcher.SendMessage(lMessage);
   //     }
   // }


   // public void InitLanguageData()
   // {
   //     /*
   //     string languageName = Enum.GetName(typeof(GameLanguage), GameMain.Instance.gameLanguage);
   //     string assetName = ASSET_FOLDER_PATH + languageName;
   //     languageDataSet = (LanguageDataSet)Resources.Load(assetName, typeof(LanguageDataSet));
   //     */
   // }
   // public void InitDatas()
   // {
   //     /*
   //     AssetBundle bundle = AssetBundleManager.Instance.getAssetBundle(ATLAS_ASSET_NAME, 1);
   //     atlasDataSet = (AtlasDataSet)bundle.LoadAsset(ATLAS_ASSET_NAME, typeof(AtlasDataSet));
   //     atlasDataSet.OnLoadFinished();
   //     dictData.Add(ATLAS_ASSET_NAME, atlasDataSet);

   //     bundle.Unload(false);
   //     */
   //     /*
   //     atlasDataSet = ScriptableObject.CreateInstance<AtlasDataSet>();
   //     InitSpriteDatas(ATLAS_ASSET_NAME_BUTTON, 1);
   //     InitSpriteDatas(ATLAS_ASSET_NAME_GOODS, 1);
   //     dictData.Add("AtlasData", atlasDataSet);
   //     */
   //     /*
   //     bundle = AssetBundleManager.Instance.getAssetBundle(LANGUAGE_ASSET_NAME, 1);
   //     languageDataSet = (LanguageDataSet)bundle.LoadAsset(LANGUAGE_ASSET_NAME, typeof(LanguageDataSet));
   //     languageDataSet.OnLoadFinished();
   //     dictData.Add(LANGUAGE_ASSET_NAME, languageDataSet);

   //     bundle.Unload(false);

   //     bundle = AssetBundleManager.Instance.getAssetBundle(GOODS_ASSET_NAME, 1);
   //     goodsDataSet = (GoodsDataSet)bundle.LoadAsset(GOODS_ASSET_NAME, typeof(GoodsDataSet));
   //     goodsDataSet.OnLoadFinished();
   //     dictData.Add(GOODS_ASSET_NAME,goodsDataSet);
   //     bundle.Unload(false);

   //     bundle = AssetBundleManager.Instance.getAssetBundle(CITY_ASSET_NAME, 1);
   //     cityDataSet = (CityDataSet)bundle.LoadAsset(CITY_ASSET_NAME, typeof(CityDataSet));
   //     cityDataSet.OnLoadFinished();
   //     dictData.Add(CITY_ASSET_NAME, cityDataSet);
   //     bundle.Unload(false);
   //     */
   // }
   // public void InitSpriteDatas(string name,int version)
   // {
   //     /*
   //     AssetBundle bundle = AssetBundleManager.Instance.getAssetBundle(name, version);
   //     string[] spriteNames = bundle.GetAllAssetNames();
   //     spriteNames = bundle.GetAllAssetNames();
   //     for (int i = 0; i < spriteNames.Length; i++)
   //     {
   //         string[] tmp = spriteNames[i].Split(new char[] { '/' });
   //         string pngName = tmp[tmp.Length - 1];
   //         tmp = pngName.Split(new char[] { '.' });
   //         string realName = tmp[0];
   //         atlasDataSet.spritesDataDict.Add(realName, (Sprite)bundle.LoadAsset(realName, typeof(Sprite)));
   //     }
   //     bundle.Unload(false);
   //     */
   // }



   // public void Release()
   // {


   //     m_instance = null;
   // }
   // /*
   // // 成员变量
   // private Dictionary<string, CallbackScriptableObject> dictData = new Dictionary<string, CallbackScriptableObject>(); // 数据

   // // 获取数据
   // public CallbackScriptableObject GetData(string assetFileName)
   // {
   //     // 若已经加载
   //     if (dictData.ContainsKey(assetFileName))
   //         return dictData[assetFileName];

   //     // 若还没有加载
   //     CallbackScriptableObject temp =  Resources.Load("GameData/" + assetFileName, typeof(GameObject)) as CallbackScriptableObject;
   //     //CallbackScriptableObject temp = ResourceLoader.GetObjectDirectly<CallbackScriptableObject>("GameData/" + assetFileName);//(CallbackScriptableObject)Resources.Load("GameData/" + assetFileName, typeof(CallbackScriptableObject));
   //     if (temp != null)
   //     {
   //         // 注：此处暂时不实例化，因此配置表数据各项只读
   //         dictData[assetFileName] = temp;

   //         // 加载完毕回调
   //         dictData[assetFileName].OnLoadFinished();
   //     }
   //     else
   //     {

   //     }
   //     return dictData[assetFileName];
   // }
   // */
   
   public void CreatePrefabObjectAsync(string name,  Action<GameObject> callback)
   {
       AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync(name);
       handle.Completed += (op) =>
       {
           if (op.Status == AsyncOperationStatus.Succeeded)
           {
               callback(handle.Result);
           }
       };
   }

   public void ReleasePrefebObject(GameObject obj)
   {
       Addressables.ReleaseInstance(obj);
   }
   
   
   
   
   
   
   
   
   
   
   
   
   
}





