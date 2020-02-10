using UnityEngine;
using System;
using System.Collections.Generic;
using GameDataFrame;
using LitJson;
namespace GameDataFrame
{
    public class OptionDataSet : CallbackScriptableObject
    {
        
        public List<OptionData> OptionDataList;
        public Dictionary<int, OptionData> OptionDataDict;

        public OptionDataSet Load()
        {
            OptionDataSet dataSet = null;
            if (GameDataManager.Instance.dataType == DataType.Json)
            {
                string content = GameDataManager.Instance.GetDataSet<string>("OptionDataSet");
                OptionDataList = JsonMapper.ToObject<List<OptionData>>(content);
                dataSet = this;
             }
            else if(GameDataManager.Instance.dataType == DataType.UnityAsset)
            {
                dataSet = GameDataManager.Instance.GetDataSet<OptionDataSet>("OptionDataSet");
             }
            dataSet.OnLoadFinished();
            return dataSet;
         }




        public override void OnLoadFinished()
        {
            OptionDataDict = new Dictionary<int, OptionData>();
            foreach (OptionData data in OptionDataList)
            {
                OptionDataDict[data.id] = data;
            }
        }
        public override void Release()
        {
            OptionDataList.Clear();
            ClearDictionary(OptionDataDict);
        }

        public OptionData GetOptionDataByID(int ID)
        {
            if (OptionDataDict.ContainsKey(ID))
                return OptionDataDict[ID];
            else
            {
                return null;
            }
        }
    }
}