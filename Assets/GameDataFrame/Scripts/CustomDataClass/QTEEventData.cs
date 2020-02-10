using UnityEngine;
using System;
using System.Collections.Generic;

using GameDataFrame;

namespace GameDataFrame
{
    [System.Serializable]
    public class QTEEventData
	{
		public string id;
		public int type;
		public float timeLimit;
		public int numLimit;
		public int keyNum;
		public string successNextVideoID;
		public string failNextVideoID;
		public string desc;
	}
}
