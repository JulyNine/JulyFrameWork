using UnityEngine;
using System;
using System.Collections.Generic;

using GameDataFrame;

namespace GameDataFrame
{
    [System.Serializable]
    public class QTEActionData
	{
		public int id;
		public int type;
		public string prefabName;
		public int limitTime;
		public int limitNum;
		public int key;
		public int totalPressTime;
		public int slideDirection;
		public bool isPause;
		public bool isVideoEnd;
		public string audio;
		public List<int> sucVariableIDList;
		public List<int> sucValueList;
		public List<int> sucPathIDList;
		public List<int> failVariableIDList;
		public List<int> failValueList;
		public List<int> failPathIDList;
		public int sourceDataID;
		public List<int> danceBeatTimeList;
		public List<int> danceLevelList;
		public List<int> buttonPosition;
		public bool needWarning;
        public string desc;
	}
}
