using UnityEngine;
using System;
using System.Collections.Generic;

using GameDataFrame;

namespace GameDataFrame
{
    [System.Serializable]
    public class OptionData
	{
		public int id;
		public int optionActionID;
		public string content;
		public List<int> variableIDList;
		public List<int> valueList;
		public List<int> pathIDList;
		public string picURL;
		public List<int> position;
	}
}
