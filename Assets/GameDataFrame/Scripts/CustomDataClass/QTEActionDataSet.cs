using UnityEngine;
using System;
using System.Collections.Generic;
using GameDataFrame;
using LitJson;
namespace GameDataFrame
{
    public class QTEActionDataSet : CallbackScriptableObject
    {
        
        public List<QTEActionData> QTEActionDataList;
        public Dictionary<int, QTEActionData> QTEActionDataDict;

        public QTEActionDataSet Load()
        {
            QTEActionDataSet dataSet = null;
            if (GameDataManager.Instance.dataType == DataType.Json)
            {
                string content = GameDataManager.Instance.GetDataSet<string>("QTEActionDataSet");
                QTEActionDataList = JsonMapper.ToObject<List<QTEActionData>>(content);
                dataSet = this;
             }
            else if(GameDataManager.Instance.dataType == DataType.UnityAsset)
            {
                dataSet = GameDataManager.Instance.GetDataSet<QTEActionDataSet>("QTEActionDataSet");
             }
            dataSet.OnLoadFinished();
            return dataSet;
         }




        public override void OnLoadFinished()
        {
            QTEActionDataDict = new Dictionary<int, QTEActionData>();
            foreach (QTEActionData data in QTEActionDataList)
            {
                QTEActionDataDict[data.id] = data;
            }
        }
        public override void Release()
        {
            QTEActionDataList.Clear();
            ClearDictionary(QTEActionDataDict);
        }

        public QTEActionData GetQTEActionDataByID(int ID)
        {
            if (QTEActionDataDict.ContainsKey(ID))
                return QTEActionDataDict[ID];
            else
            {
                return null;
            }
        }
    }
}