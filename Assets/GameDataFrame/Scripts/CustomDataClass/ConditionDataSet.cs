using UnityEngine;
using System;
using System.Collections.Generic;
using GameDataFrame;
using LitJson;
namespace GameDataFrame
{
    public class ConditionDataSet : CallbackScriptableObject
    {
        
        public List<ConditionData> ConditionDataList;
        public Dictionary<int, ConditionData> ConditionDataDict;

        public ConditionDataSet Load()
        {
            ConditionDataSet dataSet = null;
            if (GameDataManager.Instance.dataType == DataType.Json)
            {
                string content = GameDataManager.Instance.GetDataSet<string>("ConditionDataSet");
                ConditionDataList = JsonMapper.ToObject<List<ConditionData>>(content);
                dataSet = this;
             }
            else if(GameDataManager.Instance.dataType == DataType.UnityAsset)
            {
                dataSet = GameDataManager.Instance.GetDataSet<ConditionDataSet>("ConditionDataSet");
             }
            dataSet.OnLoadFinished();
            return dataSet;
         }




        public override void OnLoadFinished()
        {
            ConditionDataDict = new Dictionary<int, ConditionData>();
            foreach (ConditionData data in ConditionDataList)
            {
                ConditionDataDict[data.id] = data;
            }
        }
        public override void Release()
        {
            ConditionDataList.Clear();
            ClearDictionary(ConditionDataDict);
        }

        public ConditionData GetConditionDataByID(int ID)
        {
            if (ConditionDataDict.ContainsKey(ID))
                return ConditionDataDict[ID];
            else
            {
                return null;
            }
        }
    }
}