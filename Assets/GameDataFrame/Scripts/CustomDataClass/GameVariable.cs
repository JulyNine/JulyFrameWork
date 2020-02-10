using UnityEngine;
using System;
using System.Collections.Generic;

using GameDataFrame;

namespace GameDataFrame
{
    [System.Serializable]
    public class GameVariable
	{
		public int id;
		public string name;
		public string desc;
		public int type;
		public List<string> path;
	}
}
