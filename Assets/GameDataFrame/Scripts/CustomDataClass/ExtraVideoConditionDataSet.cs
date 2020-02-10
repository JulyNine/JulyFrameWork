using UnityEngine;
using System;
using System.Collections.Generic;
using GameDataFrame;
using LitJson;
namespace GameDataFrame
{
    public class ExtraVideoConditionDataSet : CallbackScriptableObject
    {
        
        public List<ExtraVideoConditionData> ExtraVideoConditionDataList;
        public Dictionary<int, ExtraVideoConditionData> ExtraVideoConditionDataDict;

        public ExtraVideoConditionDataSet Load()
        {
            ExtraVideoConditionDataSet dataSet = null;
            if (GameDataManager.Instance.dataType == DataType.Json)
            {
                string content = GameDataManager.Instance.GetDataSet<string>("ExtraVideoConditionDataSet");
                ExtraVideoConditionDataList = JsonMapper.ToObject<List<ExtraVideoConditionData>>(content);
                dataSet = this;
             }
            else if(GameDataManager.Instance.dataType == DataType.UnityAsset)
            {
                dataSet = GameDataManager.Instance.GetDataSet<ExtraVideoConditionDataSet>("ExtraVideoConditionDataSet");
             }
            dataSet.OnLoadFinished();
            return dataSet;
         }




        public override void OnLoadFinished()
        {
            ExtraVideoConditionDataDict = new Dictionary<int, ExtraVideoConditionData>();
            foreach (ExtraVideoConditionData data in ExtraVideoConditionDataList)
            {
                ExtraVideoConditionDataDict[data.id] = data;
            }
        }
        public override void Release()
        {
            ExtraVideoConditionDataList.Clear();
            ClearDictionary(ExtraVideoConditionDataDict);
        }

        public ExtraVideoConditionData GetExtraVideoConditionDataByID(int ID)
        {
            if (ExtraVideoConditionDataDict.ContainsKey(ID))
                return ExtraVideoConditionDataDict[ID];
            else
            {
                return null;
            }
        }
    }
}