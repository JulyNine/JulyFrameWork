using UnityEngine;
using System;
using System.Collections.Generic;

using GameDataFrame;

namespace GameDataFrame
{
    [System.Serializable]
    public class VideoData
	{
		public int id;
		public int type;
		public string title;
		public string cover;
		public string endingImage;
		public int sourceDataID;
		public bool isEnding;
		public List<int> times;
		public List<int> conditions;
		public List<int> actions;
		public List<int> actionTypes;
		public int chapterID;
		public List<int> childVideoConditions;
		public List<int> childVideoIDList;
		public bool isLoopOptionVideo;
		public bool isChapterEnding;
	}
}
