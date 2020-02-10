using UnityEngine;
using System;
using System.Collections.Generic;
using GameDataFrame;
namespace GameDataFrame
{
    /// <summary>
    /// 语言数据管理
    /// </summary>
    public class LanguageDataSet : CallbackScriptableObject
    {
        //游戏内普通文字
        public List<LanguageData> CommonLanguageList = new List<LanguageData>();
        //游戏UI摆放的文字
        public List<LanguageData> UILanguageList = new List<LanguageData>();
        //游戏Excel数据中文字
        public List<LanguageData> ExcelLanguageList = new List<LanguageData>();
        //错误码
        public List<LanguageData> ErrorCodeLanguageList = new List<LanguageData>();

        public Dictionary<string, LanguageData> CommonLanguageDict = new Dictionary<string, LanguageData>();
        public Dictionary<string, LanguageData> UILanguageDict = new Dictionary<string, LanguageData>();
        public Dictionary<string, LanguageData> ExcelLanguageDict = new Dictionary<string, LanguageData>();
        public Dictionary<string, LanguageData> ErrorCodeLanguageDict = new Dictionary<string, LanguageData>();


        public override void OnLoadFinished()
        {
            CommonLanguageDict.Clear();
            for (int i = 0; i < CommonLanguageList.Count; i++)
            {
                LanguageData data = CommonLanguageList[i];
                CommonLanguageDict.Add(data.key, data);
            }
            CommonLanguageList = null;
            UILanguageDict.Clear();
            for (int i = 0; i < UILanguageList.Count; i++)
            {
                LanguageData data = UILanguageList[i];
                if (!UILanguageDict.ContainsKey(data.key))
                    UILanguageDict.Add(data.key, data);
            }
            UILanguageList = null;
            ExcelLanguageDict.Clear();
            for (int i = 0; i < ExcelLanguageList.Count; i++)
            {
                LanguageData data = ExcelLanguageList[i];
                if (!ExcelLanguageDict.ContainsKey(data.key))
                    ExcelLanguageDict.Add(data.key, data);
            }
            ExcelLanguageList = null;

            ErrorCodeLanguageDict.Clear();
            for (int i = 0; i < ErrorCodeLanguageList.Count; i++)
            {
                LanguageData data = ErrorCodeLanguageList[i];
                if (!ErrorCodeLanguageDict.ContainsKey(data.key))
                    ErrorCodeLanguageDict.Add(data.key, data);
            }
            ErrorCodeLanguageList = null;
        }

        public override void Release()
        {
            CommonLanguageList.Clear();
            UILanguageList.Clear();
            ExcelLanguageList.Clear();
            ErrorCodeLanguageList.Clear();
            CommonLanguageDict.Clear();
            UILanguageDict.Clear();
            ExcelLanguageDict.Clear();
            ErrorCodeLanguageDict.Clear();
        }
    }
}