using UnityEngine;
using System;
using com.ootii.Messages;
using GameDataFrame;
//语言文字控制类
public class LanguageManager
{
	
	private static LanguageManager s_Instance;
	public LanguageManager() { s_Instance = this; }
    public static LanguageManager Instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = new LanguageManager();
            }
            return s_Instance;
        }
    }
    //语言文字数据库
    LanguageDataSet languageDataSet;

    public bool isLoaded = false;

    public void Init()
    {
        MessageDispatcher.AddListener(GameEvent.DOWNLOAD_ASSETBUNDLES_FINISH, LoadData);
        ConfigSettings.Instance.curLanguageString = ConfigSettings.Instance.curLanguage.ToString().ToLower();
    }
    //加载文字数据库
    public void LoadData(IMessage rMessage)
	{
        rMessage.IsHandled = true;
        string assetName = "LanguageDataSet";
        AssetBundle bundle = AssetBundleManager.Instance.GetAssetBundle("GameData_" + assetName, BundleType.GameData);
        languageDataSet = (LanguageDataSet)bundle.LoadAsset(assetName, typeof(LanguageDataSet));
        languageDataSet.OnLoadFinished();
        AssetBundleManager.Instance.Unload(assetName, BundleType.GameData,false);
        isLoaded = true;

        Debug.Log("xxxxx" + GetCommonLanguageString("LOGIN_FAILED"));
        MessageDispatcher.SendMessage(GameEvent.LOADDATA_FINISH);
    }

    //Editor转数据时使用
    public void LoadData()
    {

#if UNITY_EDITOR
        languageDataSet = (LanguageDataSet)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Datas/AssetData/LanguageDataSet.asset", typeof(LanguageDataSet));
        languageDataSet.OnLoadFinished();
#endif
    }

    public void Clear()
    {
        languageDataSet = null;
        isLoaded = false;
    }

    //获取游戏内
	public string GetCommonLanguageString(string key)
	{
        LanguageData data;
        if (languageDataSet.CommonLanguageDict.ContainsKey(key))
        {
            data = languageDataSet.CommonLanguageDict[key];
            return data.content.Replace("\\n", "\n");
        }
        else
        {
            LogManager.Log("dict no data for key :" + key);
            return key;
        }
	}
    //获取UI摆放的文字
    public string GetUILanguageString(string key)
    {
        LanguageData data;
        if (languageDataSet == null)
            return null;
        if(languageDataSet.UILanguageDict.ContainsKey(key))
        {
            data = languageDataSet.UILanguageDict[key];
            return data.content.Replace("\\n", "\n");
        }
        else
        {
            LogManager.Log("dict no data for key :" + key);
            return key;
        }
    }
    //获取Excel中文字
    public string GetExcelLanguageString(string key)
    {
        LanguageData data;
        if (languageDataSet.ExcelLanguageDict.ContainsKey(key))
        {
            data = languageDataSet.ExcelLanguageDict[key];
            return data.content.Replace("\\n", "\n");
        }
        else
        {
      //      LogManager.Log("dict no data for key :" + key);
            return key;
        }
    }

    //获取ErrorCode
    public string GetErrorCodeLanguageString(string key)
    {
        LanguageData data;
        if (languageDataSet.ErrorCodeLanguageDict.ContainsKey(key))
        {
            data = languageDataSet.ErrorCodeLanguageDict[key];
            return data.content.Replace("\\n", "\n");
        }
        else
        {
            LogManager.Log("errorcode dict no data for key :" + key);
            return key;
        }
    }


}
