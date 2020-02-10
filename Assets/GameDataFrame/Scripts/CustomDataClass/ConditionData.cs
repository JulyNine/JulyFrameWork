using UnityEngine;
using System;
using System.Collections.Generic;

using GameDataFrame;

namespace GameDataFrame
{
    [System.Serializable]
    public class ConditionData
	{
		public int id;
		public List<int> variableType;
		public List<int> variableValue;
		public List<int> judgeType;
		public List<int> judgeValues;
	}
}
