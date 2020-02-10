using UnityEngine;
using System;
using System.Collections.Generic;
using GameDataFrame;
using LitJson;
namespace GameDataFrame
{
    public class CharacterDataSet : CallbackScriptableObject
    {
        
        public List<CharacterData> CharacterDataList;
        public Dictionary<int, CharacterData> CharacterDataDict;

        public CharacterDataSet Load()
        {
            CharacterDataSet dataSet = null;
            if (GameDataManager.Instance.dataType == DataType.Json)
            {
                string content = GameDataManager.Instance.GetDataSet<string>("CharacterDataSet");
                CharacterDataList = JsonMapper.ToObject<List<CharacterData>>(content);
                dataSet = this;
             }
            else if(GameDataManager.Instance.dataType == DataType.UnityAsset)
            {
                dataSet = GameDataManager.Instance.GetDataSet<CharacterDataSet>("CharacterDataSet");
             }
            dataSet.OnLoadFinished();
            return dataSet;
         }




        public override void OnLoadFinished()
        {
            CharacterDataDict = new Dictionary<int, CharacterData>();
            foreach (CharacterData data in CharacterDataList)
            {
                CharacterDataDict[data.id] = data;
            }
        }
        public override void Release()
        {
            CharacterDataList.Clear();
            ClearDictionary(CharacterDataDict);
        }

        public CharacterData GetCharacterDataByID(int ID)
        {
            if (CharacterDataDict.ContainsKey(ID))
                return CharacterDataDict[ID];
            else
            {
                return null;
            }
        }
    }
}