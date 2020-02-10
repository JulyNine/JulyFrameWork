using UnityEngine;
using System;
using System.Collections.Generic;
using GameDataFrame;
using LitJson;
namespace GameDataFrame
{
    public class OptionActionDataSet : CallbackScriptableObject
    {
        
        public List<OptionActionData> OptionActionDataList;
        public Dictionary<int, OptionActionData> OptionActionDataDict;

        public OptionActionDataSet Load()
        {
            OptionActionDataSet dataSet = null;
            if (GameDataManager.Instance.dataType == DataType.Json)
            {
                string content = GameDataManager.Instance.GetDataSet<string>("OptionActionDataSet");
                OptionActionDataList = JsonMapper.ToObject<List<OptionActionData>>(content);
                dataSet = this;
             }
            else if(GameDataManager.Instance.dataType == DataType.UnityAsset)
            {
                dataSet = GameDataManager.Instance.GetDataSet<OptionActionDataSet>("OptionActionDataSet");
             }
            dataSet.OnLoadFinished();
            return dataSet;
         }




        public override void OnLoadFinished()
        {
            OptionActionDataDict = new Dictionary<int, OptionActionData>();
            foreach (OptionActionData data in OptionActionDataList)
            {
                OptionActionDataDict[data.id] = data;
            }
        }
        public override void Release()
        {
            OptionActionDataList.Clear();
            ClearDictionary(OptionActionDataDict);
        }

        public OptionActionData GetOptionActionDataByID(int ID)
        {
            if (OptionActionDataDict.ContainsKey(ID))
                return OptionActionDataDict[ID];
            else
            {
                return null;
            }
        }
    }
}