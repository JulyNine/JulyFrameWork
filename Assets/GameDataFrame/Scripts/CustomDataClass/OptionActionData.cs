using UnityEngine;
using System;
using System.Collections.Generic;

using GameDataFrame;

namespace GameDataFrame
{
    [System.Serializable]
    public class OptionActionData
	{
		public int id;
		public int type;
		public int limitTime;
		public List<int> options;
		public bool isPause;
		public bool isVideoEnd;
		public string audio;
		public string desc;
		public int UIType;
        public bool needWarning;
    }
}
