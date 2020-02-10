using UnityEngine;
using System;
using System.Collections.Generic;
using GameDataFrame;
using LitJson;
namespace GameDataFrame
{
    public class GameVariableSet : CallbackScriptableObject
    {
        
        public List<GameVariable> GameVariableList;
        public Dictionary<int, GameVariable> GameVariableDict;

        public GameVariableSet Load()
        {
            GameVariableSet dataSet = null;
            if (GameDataManager.Instance.dataType == DataType.Json)
            {
                string content = GameDataManager.Instance.GetDataSet<string>("GameVariableSet");
                GameVariableList = JsonMapper.ToObject<List<GameVariable>>(content);
                dataSet = this;
             }
            else if(GameDataManager.Instance.dataType == DataType.UnityAsset)
            {
                dataSet = GameDataManager.Instance.GetDataSet<GameVariableSet>("GameVariableSet");
             }
            dataSet.OnLoadFinished();
            return dataSet;
         }




        public override void OnLoadFinished()
        {
            GameVariableDict = new Dictionary<int, GameVariable>();
            foreach (GameVariable data in GameVariableList)
            {
                GameVariableDict[data.id] = data;
            }
        }
        public override void Release()
        {
            GameVariableList.Clear();
            ClearDictionary(GameVariableDict);
        }

        public GameVariable GetGameVariableByID(int ID)
        {
            if (GameVariableDict.ContainsKey(ID))
                return GameVariableDict[ID];
            else
            {
                return null;
            }
        }
    }
}