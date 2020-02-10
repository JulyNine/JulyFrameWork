using UnityEngine;
using System;
using System.Collections.Generic;
using GameDataFrame;
namespace GameDataFrame
{
    public class QTEEventDataSet : CallbackScriptableObject
    {
        
        public List<QTEEventData> QTEEventDataList;
        public Dictionary<string, QTEEventData> QTEEventDataDict;
        public override void OnLoadFinished()
        {
            QTEEventDataDict = new Dictionary<string, QTEEventData>();
            foreach (QTEEventData data in QTEEventDataList)
            {
                QTEEventDataDict[data.id] = data;
            }
        }
        public override void Release()
        {
            QTEEventDataList.Clear();
            ClearDictionary(QTEEventDataDict);
        }

        public QTEEventData GetQTEEventDataByID(string ID)
        {
            if (QTEEventDataDict.ContainsKey(ID))
                return QTEEventDataDict[ID];
            else
            {
                return null;
            }
        }
    }
}