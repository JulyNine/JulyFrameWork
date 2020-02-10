using UnityEngine;
using System;
using System.Collections.Generic;
using GameDataFrame;
using LitJson;
namespace GameDataFrame
{
    public class VideoSourceDataSet : CallbackScriptableObject
    {
        
        public List<VideoSourceData> VideoSourceDataList;
        public Dictionary<int, VideoSourceData> VideoSourceDataDict;

        public VideoSourceDataSet Load()
        {
            VideoSourceDataSet dataSet = null;
            if (GameDataManager.Instance.dataType == DataType.Json)
            {
                string content = GameDataManager.Instance.GetDataSet<string>("VideoSourceDataSet");
                VideoSourceDataList = JsonMapper.ToObject<List<VideoSourceData>>(content);
                dataSet = this;
             }
            else if(GameDataManager.Instance.dataType == DataType.UnityAsset)
            {
                dataSet = GameDataManager.Instance.GetDataSet<VideoSourceDataSet>("VideoSourceDataSet");
             }
            dataSet.OnLoadFinished();
            return dataSet;
         }




        public override void OnLoadFinished()
        {
            VideoSourceDataDict = new Dictionary<int, VideoSourceData>();
            foreach (VideoSourceData data in VideoSourceDataList)
            {
                VideoSourceDataDict[data.id] = data;
            }
        }
        public override void Release()
        {
            VideoSourceDataList.Clear();
            ClearDictionary(VideoSourceDataDict);
        }

        public VideoSourceData GetVideoSourceDataByID(int ID)
        {
            if (VideoSourceDataDict.ContainsKey(ID))
                return VideoSourceDataDict[ID];
            else
            {
                return null;
            }
        }
    }
}