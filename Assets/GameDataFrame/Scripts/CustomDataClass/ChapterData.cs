using UnityEngine;
using System;
using System.Collections.Generic;

using GameDataFrame;

namespace GameDataFrame
{
    [System.Serializable]
    public class ChapterData
	{
		public int id;
		public string title;
		public string cover;
		public string desc;
		public int startVideoID;
		public int sourceDataID;
		public int videoNums;
		public int endingNums;
		public int keyCharacter;
		public List<int> progressAwards;
	}
}
