using UnityEngine;
using System;
using System.Collections.Generic;
using GameDataFrame;
using LitJson;
namespace GameDataFrame
{
    public class CityDataSet : CallbackScriptableObject
    {
        
        public List<CityData> CityDataList;
        public Dictionary<int, CityData> CityDataDict;

        public CityDataSet Load()
        {
            CityDataSet dataSet = null;
            if (GameDataManager.Instance.dataType == DataType.Json)
            {
                string content = GameDataManager.Instance.GetDataSet<string>("CityDataSet");
                CityDataList = JsonMapper.ToObject<List<CityData>>(content);
                dataSet = this;
             }
            else if(GameDataManager.Instance.dataType == DataType.UnityAsset)
            {
                dataSet = GameDataManager.Instance.GetDataSet<CityDataSet>("CityDataSet");
             }
            dataSet.OnLoadFinished();
            return dataSet;
         }




        public override void OnLoadFinished()
        {
            CityDataDict = new Dictionary<int, CityData>();
            foreach (CityData data in CityDataList)
            {
                CityDataDict[data.id] = data;
            }
        }
        public override void Release()
        {
            CityDataList.Clear();
            ClearDictionary(CityDataDict);
        }

        public CityData GetCityDataByID(int ID)
        {
            if (CityDataDict.ContainsKey(ID))
                return CityDataDict[ID];
            else
            {
                return null;
            }
        }
    }
}