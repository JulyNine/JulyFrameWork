using UnityEngine;
using System;
using System.Collections.Generic;
using GameDataFrame;
using LitJson;
namespace GameDataFrame
{
    public class ChapterDataSet : CallbackScriptableObject
    {
        
        public List<ChapterData> ChapterDataList;
        public Dictionary<int, ChapterData> ChapterDataDict;

        public ChapterDataSet Load()
        {
            ChapterDataSet dataSet = null;
            if (GameDataManager.Instance.dataType == DataType.Json)
            {
                string content = GameDataManager.Instance.GetDataSet<string>("ChapterDataSet");
                ChapterDataList = JsonMapper.ToObject<List<ChapterData>>(content);
                dataSet = this;
             }
            else if(GameDataManager.Instance.dataType == DataType.UnityAsset)
            {
                dataSet = GameDataManager.Instance.GetDataSet<ChapterDataSet>("ChapterDataSet");
             }
            dataSet.OnLoadFinished();
            return dataSet;
         }




        public override void OnLoadFinished()
        {
            ChapterDataDict = new Dictionary<int, ChapterData>();
            foreach (ChapterData data in ChapterDataList)
            {
                ChapterDataDict[data.id] = data;
            }
        }
        public override void Release()
        {
            ChapterDataList.Clear();
            ClearDictionary(ChapterDataDict);
        }

        public ChapterData GetChapterDataByID(int ID)
        {
            if (ChapterDataDict.ContainsKey(ID))
                return ChapterDataDict[ID];
            else
            {
                return null;
            }
        }
    }
}