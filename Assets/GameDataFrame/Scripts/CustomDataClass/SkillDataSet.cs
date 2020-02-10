using UnityEngine;
using System;
using System.Collections.Generic;
using GameDataFrame;
namespace GameDataFrame
{
    public class SkillDataSet : CallbackScriptableObject
    {
        
        public List<SkillData> SkillDataList;
        public Dictionary<string, SkillData> SkillDataDict;
        public override void OnLoadFinished()
        {
            SkillDataDict = new Dictionary<string, SkillData>();
            foreach (SkillData data in SkillDataList)
            {
                SkillDataDict[data.id] = data;
            }
        }
        public override void Release()
        {
            SkillDataList.Clear();
            ClearDictionary(SkillDataDict);
        }

        public SkillData GetSkillDataByID(string ID)
        {
            if (SkillDataDict.ContainsKey(ID))
                return SkillDataDict[ID];
            else
            {
                return null;
            }
        }
    }
}