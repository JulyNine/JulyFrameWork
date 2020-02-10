using UnityEngine;
using System;
using System.Collections.Generic;
using GameDataFrame;
namespace GameDataFrame
{
    public class SelectEventDataSet : CallbackScriptableObject
    {
        
        public List<SelectEventData> SelectEventDataList;
        public Dictionary<string, SelectEventData> SelectEventDataDict;
        public override void OnLoadFinished()
        {
            SelectEventDataDict = new Dictionary<string, SelectEventData>();
            foreach (SelectEventData data in SelectEventDataList)
            {
                SelectEventDataDict[data.id] = data;
            }
        }
        public override void Release()
        {
            SelectEventDataList.Clear();
            ClearDictionary(SelectEventDataDict);
        }

        public SelectEventData GetSelectEventDataByID(string ID)
        {
            if (SelectEventDataDict.ContainsKey(ID))
                return SelectEventDataDict[ID];
            else
            {
                return null;
            }
        }
    }
}