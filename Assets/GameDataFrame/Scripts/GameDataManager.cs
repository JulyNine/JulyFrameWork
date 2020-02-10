using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;
using UnityEngine.Networking;

namespace GameDataFrame
{
    /// <summary>
    /// 数据控制类
    /// </summary>
    public class GameDataManager : MonoSingleton<GameDataManager>
    {
        // 数据格式
        public DataType dataType;
        public GameDataVersion gameDataVersion;
        // 数据加载模式
        public LoadType loadType;
        public string URLPath = "http://223.223.179.220/FMVGame/JsonData/";
        public delegate void LoadDataFinish(); 
        public event LoadDataFinish EventLoadDataFinish;
        [HideInInspector] public bool dataIsReady;
        
        private readonly Dictionary<string, CallbackScriptableObject> dataSetDict =
            new Dictionary<string, CallbackScriptableObject>();
        private readonly Dictionary<string, string> dataSetJsonStringDict = new Dictionary<string, string>();
        private readonly Dictionary<string, bool> dataSetLoadedDict = new Dictionary<string, bool>();
        public void Init()
        {
            if (loadType == LoadType.LocalResource)
            {
                var versionAsset = Resources.Load<TextAsset>("DataVersion");
                var content = versionAsset.text;
                gameDataVersion = JsonMapper.ToObject<GameDataVersion>(content);
                LoadAllDataSet();
            }
            else if (loadType == LoadType.URL) //URL 网络模式暂时只支持json
            {
                DownloadVersionFile();
            }
        }
        /// <summary>
        /// 加载全部数据
        /// </summary>
        public void LoadAllDataSet()
        {
            dataSetLoadedDict.Clear();
            foreach (var dataSetInfo in gameDataVersion.dataVersionDict) dataSetLoadedDict.Add(dataSetInfo.Key, false);
            foreach (var dataSetInfo in gameDataVersion.dataVersionDict) LoadDataSet(dataSetInfo.Key);
        }
        /// <summary>
        /// 加载某类数据
        /// </summary>
        /// <param name="dataSetName"></param>
        public void LoadDataSet(string dataSetName)
        {
            if (loadType == LoadType.LocalResource)
            {
                if (dataType == DataType.UnityAsset)
                {
                    var dataSet = Resources.Load<CallbackScriptableObject>("AssetData/" + dataSetName);
                    CacheDataSet(dataSetName, dataSet);
                    dataSetLoadedDict[dataSetName] = true;
                    dataIsReady = CheckDataSetReady();
                }
                else if (dataType == DataType.Json)
                {
                    var dataAsset = Resources.Load<TextAsset>("JsonData/" + dataSetName);
                    var content = dataAsset.text;
                    CacheDataSet(dataSetName, content);
                    dataSetLoadedDict[dataSetName] = true;
                    dataIsReady = CheckDataSetReady();
                }
            }
            else if (loadType == LoadType.URL) //URL 网络模式暂时只支持json
            {
                DownloadDataSet(dataSetName);
            }
        }
        /// <summary>
        /// 加载某类数据
        /// </summary>
        /// <param name="dataSetName"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T LoadDataSet<T>(string dataSetName) where T : CallbackScriptableObject
        {
            if (dataType == DataType.UnityAsset)
            {
                var dataSet = Resources.Load<T>("AssetData/" + dataSetName);
                CacheDataSet(dataSetName, dataSet);
                return dataSet;
            }

            if (dataType == DataType.Json)
            {
                var dataAsset = Resources.Load<TextAsset>("dataSetName");
                var content = dataAsset.text;
                var dataSet = JsonMapper.ToObject<T>(content);
                CacheDataSet(dataSetName, dataSet);
                return dataSet;
            }

            return null;
        }

        public void CacheDataSet(string dataSetName, CallbackScriptableObject dataSet)
        {
            if (dataSetDict.ContainsKey(dataSetName))
                dataSetDict[dataSetName] = dataSet;
            else
                dataSetDict.Add(dataSetName, dataSet);
        }

        public void CacheDataSet(string dataSetName, string jsonString)
        {
            if (dataSetJsonStringDict.ContainsKey(dataSetName))
                dataSetJsonStringDict[dataSetName] = jsonString;
            else
                dataSetJsonStringDict.Add(dataSetName, jsonString);
        }

        public CallbackScriptableObject GetDataSet(string dataSetName)
        {
            if (dataSetDict.ContainsKey(dataSetName))
                return dataSetDict[dataSetName];
            return null;
        }

        public T GetDataSet<T>(string dataSetName)
        {
            T res = default;
            if (dataType == DataType.UnityAsset)
            {
                if (dataSetDict.ContainsKey(dataSetName))
                    res = (T) Convert.ChangeType(dataSetDict[dataSetName], typeof(T));
            }
            else if (dataType == DataType.Json)
            {
                if (dataSetJsonStringDict.ContainsKey(dataSetName))
                    res = (T) Convert.ChangeType(dataSetJsonStringDict[dataSetName], typeof(T));
            }

            return res;
        }

        public void DownloadDataSet(string dataSetName)
        {
            StartCoroutine(DownloadDataSetCoroutine(dataSetName));
        }

        private IEnumerator DownloadDataSetCoroutine(string dataSetName)
        {
            var url = URLPath + dataSetName + ".json";
            Debug.Log("DownloadDataSet：" + url);
            var webRequest = UnityWebRequest.Get(url);
            yield return webRequest.SendWebRequest();
            //异常处理，
            if (webRequest.isHttpError || webRequest.isNetworkError)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                CacheDataSet(dataSetName, webRequest.downloadHandler.text);
                dataSetLoadedDict[dataSetName] = true;
                dataIsReady = CheckDataSetReady();
                Debug.Log(webRequest.downloadHandler.text);
            }
        }


        public void DownloadVersionFile()
        {
            StartCoroutine(DownloadVersionFileCoroutine());
        }

        private IEnumerator DownloadVersionFileCoroutine()
        {
            var url = URLPath + "DataVersion.json";
            var webRequest = UnityWebRequest.Get(url);
            yield return webRequest.SendWebRequest();
            //异常处理
            if (webRequest.isHttpError || webRequest.isNetworkError)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                var content = webRequest.downloadHandler.text;
                gameDataVersion = JsonMapper.ToObject<GameDataVersion>(content);
                LoadAllDataSet();
                Debug.Log(webRequest.downloadHandler.text);
            }
        }

        public bool CheckDataSetReady()
        {
            var res = true;
            foreach (var loaded in dataSetLoadedDict)
                if (loaded.Value == false)
                    res = false;
            if (res)
            {
                if (EventLoadDataFinish != null)
                    EventLoadDataFinish();
                return true;
            }

            return false;
        }

        //public static IEnumerator LoadFile(string path)
        //{
        //    string localPath;
        //    if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
        //    {
        //        if (path.Contains("http"))
        //            localPath = path;
        //        else
        //            localPath = "file:///" + path;//在Windows中实例化WWW必须要在路径前面加"file://"
        //    }
        //    else if (Application.platform == RuntimePlatform.Android)
        //    {
        //        if (path.Contains("jar:file") || path.Contains("http"))
        //            localPath = path;
        //        else
        //            localPath = "file://" + path; //在Android中实例化WWW不能在路径前面加"file://"
        //    }
        //    else
        //    {
        //        localPath = "file://" + path; //在Android中实例化WWW不能在路径前面加"file://"
        //    }
        //    WWW www = new WWW(localPath);
        //    while (!www.isDone)
        //    {
        //        yield return www;
        //    }
        //    if (www.error != null)
        //    {
        //        Debug.LogError("Local Load www.error: " + www.error);
        //    }
        //    else
        //    {
        //        Debug.LogError("ParseXml: " + www.text);
        //        //ParseXml(www);
        //    }
        //}
    }
    
    /// <summary>
    /// 数据资源加载来源
    /// </summary>

    public enum LoadType
    {
        LocalResource,
        URL
    }
    /// <summary>
    /// 数据资源格式
    /// </summary>
    public enum DataType
    {
        UnityAsset,
        Json,
        AssetBundle
    }
}