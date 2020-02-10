using UnityEngine;
using System;
using System.Collections.Generic;
using GameDataFrame;
using LitJson;
namespace GameDataFrame
{
    public class ExtraVideoDataSet : CallbackScriptableObject
    {
        
        public List<ExtraVideoData> ExtraVideoDataList;
        public Dictionary<int, ExtraVideoData> ExtraVideoDataDict;

        public ExtraVideoDataSet Load()
        {
            ExtraVideoDataSet dataSet = null;
            if (GameDataManager.Instance.dataType == DataType.Json)
            {
                string content = GameDataManager.Instance.GetDataSet<string>("ExtraVideoDataSet");
                ExtraVideoDataList = JsonMapper.ToObject<List<ExtraVideoData>>(content);
                dataSet = this;
             }
            else if(GameDataManager.Instance.dataType == DataType.UnityAsset)
            {
                dataSet = GameDataManager.Instance.GetDataSet<ExtraVideoDataSet>("ExtraVideoDataSet");
             }
            dataSet.OnLoadFinished();
            return dataSet;
         }




        public override void OnLoadFinished()
        {
            ExtraVideoDataDict = new Dictionary<int, ExtraVideoData>();
            foreach (ExtraVideoData data in ExtraVideoDataList)
            {
                ExtraVideoDataDict[data.id] = data;
            }
        }
        public override void Release()
        {
            ExtraVideoDataList.Clear();
            ClearDictionary(ExtraVideoDataDict);
        }

        public ExtraVideoData GetExtraVideoDataByID(int ID)
        {
            if (ExtraVideoDataDict.ContainsKey(ID))
                return ExtraVideoDataDict[ID];
            else
            {
                return null;
            }
        }
    }
}