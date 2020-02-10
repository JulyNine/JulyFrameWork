using UnityEngine;
using System;
using System.Collections.Generic;

using GameDataFrame;

namespace GameDataFrame
{
    [System.Serializable]
    public class ExtraVideoData
	{
		public int id;
		public string title;
		public string cover;
		public List<int> unlockCondition;
	}
}
