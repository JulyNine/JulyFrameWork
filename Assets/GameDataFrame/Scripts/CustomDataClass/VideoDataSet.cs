using UnityEngine;
using System;
using System.Collections.Generic;
using GameDataFrame;
using LitJson;
namespace GameDataFrame
{
    public class VideoDataSet : CallbackScriptableObject
    {
        
        public List<VideoData> VideoDataList;
        public Dictionary<int, VideoData> VideoDataDict;

        public VideoDataSet Load()
        {
            VideoDataSet dataSet = null;
            if (GameDataManager.Instance.dataType == DataType.Json)
            {
                string content = GameDataManager.Instance.GetDataSet<string>("VideoDataSet");
                VideoDataList = JsonMapper.ToObject<List<VideoData>>(content);
                dataSet = this;
             }
            else if(GameDataManager.Instance.dataType == DataType.UnityAsset)
            {
                dataSet = GameDataManager.Instance.GetDataSet<VideoDataSet>("VideoDataSet");
             }
            dataSet.OnLoadFinished();
            return dataSet;
         }




        public override void OnLoadFinished()
        {
            VideoDataDict = new Dictionary<int, VideoData>();
            foreach (VideoData data in VideoDataList)
            {
                VideoDataDict[data.id] = data;
            }
        }
        public override void Release()
        {
            VideoDataList.Clear();
            ClearDictionary(VideoDataDict);
        }

        public VideoData GetVideoDataByID(int ID)
        {
            if (VideoDataDict.ContainsKey(ID))
                return VideoDataDict[ID];
            else
            {
                return null;
            }
        }
    }
}