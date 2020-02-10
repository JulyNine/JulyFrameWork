using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using com.ootii.Messages;
using GameDataFrame;
using LitJson;
using Newtonsoft.Json;

/// <summary>
/// 临时全局数据资源管理
/// </summary>
public class NetDataManager
{
    // 获取单例
    private static NetDataManager m_instance = null; // 单例

    public static NetDataManager Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new NetDataManager();
            }

            return m_instance;
        }
    }

    public UserData userData;

    public Dictionary<int, int> videoStatusDict = new Dictionary<int, int>();
    public Dictionary<int, int> optionStatusDict = new Dictionary<int, int>();
    public Dictionary<int, int> QTEStatusDict = new Dictionary<int, int>();
    public Dictionary<int, int> chapterStatusDict = new Dictionary<int, int>();
    public Dictionary<int, int> extraVideoStatusDict = new Dictionary<int, int>();

    //public List<int> variableList = new List<int>();

    // public List<int> conditionVariableList = new List<int>();

    public Dictionary<int, Dictionary<int, Dictionary<int, int>>> gameVariableDict =
        new Dictionary<int, Dictionary<int, Dictionary<int, int>>>();

    public int lastVideoID = 0;
    //  public Dictionary<int, List<int>> historyVariablesDict = new Dictionary<int, List<int>>();//老版本数值系统使用

    public List<int> historyActionList = new List<int>();

    //public  Dictionary<int, int> historyActionResDict = new Dictionary<int, int>();

    //public Dictionary<int, Dictionary<int, int>> historyOptionsDict = new Dictionary<int, Dictionary<int, int>>();

    // public void Init()
    // {
    //     GameVariableSet gameVariableSet = new GameVariableSet();
    //     gameVariableSet = gameVariableSet.Load();
    //     //GameVariableSet gameVariableSet = (GameVariableSet)GameDataManager.Instance.GetDataSet("GameVariableSet");
    //     LogManager.Log("Init NetData:  variable num : " + gameVariableSet.GameVariableList.Count);
    //     // variableList = new List<int>();
    //     // for (int i = 0; i < gameVariableSet.GameVariableList.Count; i++)
    //     // {
    //     //     variableList.Add(0);
    //     // }
    //
    //     if (!chapterStatusDict.ContainsKey(1)) //初始解锁序章
    //         chapterStatusDict.Add(1, 1);
    // }


    public void LoadLocalData()
    {
        videoStatusDict = ES3.Load<Dictionary<int, int>>("videoStatusDict", new Dictionary<int, int>());
        optionStatusDict = ES3.Load<Dictionary<int, int>>("optionStatusDict", new Dictionary<int, int>());
        QTEStatusDict = ES3.Load<Dictionary<int, int>>("QTEStatusDict", new Dictionary<int, int>());
        chapterStatusDict = ES3.Load<Dictionary<int, int>>("chapterStatusDict", new Dictionary<int, int>());
        //historyVariablesDict = ES3.Load<Dictionary<int, List<int>>>("historiyVariablesDict", new Dictionary<int, List<int>>());
        lastVideoID = ES3.Load<int>("lastVideoID", 0);
        // variableList = ES3.Load<List<int>>("variableList");
        //  conditionVariableList = ES3.Load<List<int>>("conditionVariableList");

        gameVariableDict = ES3.Load<Dictionary<int, Dictionary<int, Dictionary<int, int>>>>("gameVariableDict",
            new Dictionary<int, Dictionary<int, Dictionary<int, int>>>());

        historyActionList = ES3.Load<List<int>>("historyActionList", new List<int>());
    }


    public void GetServerDatas()
    {
        MessageDispatcher.SendMessage(GameEvent.GET_CHAPTER_REQ);
        MessageDispatcher.SendMessage(GameEvent.GET_QTE_REQ);
        MessageDispatcher.SendMessage(GameEvent.GET_Variables_REQ);
        MessageDispatcher.SendMessage(GameEvent.GET_ActionHistory_REQ);
        MessageDispatcher.SendMessage(GameEvent.GET_VIDEO_REQ);
        MessageDispatcher.SendMessage(GameEvent.GET_OPTION_REQ);

    }
    
    public void ModifyTempVariableValue(int index, int value)
    {
        // if (index >= 0 && index < variableList.Count)
        //     variableList[index] += value;
        // else
        //     LogManager.LogError("Variable Index exceed!");
        //
        // LogManager.LogWarning("variable index: " + index + "value : " + variableList[index]);
    }

    // public void ModifyTempVariableValues(VideoData currentVideoData)
    // {
    //     for (int i = 0; i < currentVideoData.variableIDList.Count; i++)
    //     {
    //         int index = currentVideoData.variableIDList[i];
    //         int value = currentVideoData.valueList[i];
    //         ModifyTempVariableValue(index, value);
    //     }
    // }
    //
    // public void ModifyTempVariableValues(QTEActionData currentQTEActionData, int res)
    // {
    //     if (res == 1)
    //     {
    //         for (int i = 0; i < currentQTEActionData.sucVariableIDList.Count; i++)
    //         {
    //             int index = currentQTEActionData.sucVariableIDList[i];
    //             int value = currentQTEActionData.sucValueList[i];
    //             ModifyTempVariableValue(index, value);
    //         }
    //     }
    //     else
    //     {
    //         for (int i = 0; i < currentQTEActionData.failVariableIDList.Count; i++)
    //         {
    //             int index = currentQTEActionData.failVariableIDList[i];
    //             int value = currentQTEActionData.failValueList[i];
    //             ModifyTempVariableValue(index, value);
    //         }
    //     }
    // }

    // public void ConfigVariablesList(VideoData currentVideoData)
    // {
    //     int num = currentVideoData.childVideoIDList.Count;
    //     if (currentVideoData.isLoopOptionVideo) //循环视频特殊处理，只修改真正的子视频数据
    //         num = 1;
    //
    //     for (int i = 0; i < num; i++)
    //     {
    //         List<int> variableListTemp = new List<int>();
    //         for (int j = 0; j < variableList.Count; j++)
    //         {
    //             variableListTemp.Add(variableList[j]);
    //         }
    //
    //
    //         int childVideoID = currentVideoData.childVideoIDList[i];
    //         if (historyVariablesDict.ContainsKey(childVideoID))
    //         {
    //             historyVariablesDict[childVideoID] = variableListTemp;
    //         }
    //         else
    //             historyVariablesDict.Add(childVideoID, variableListTemp);
    //     }

    //Debug
    // foreach (var VARIABLE in historyVariablesDict)
    // {
    //     Debug.Log("VARIABLE.Value key" + VARIABLE.Key);
    //     for (int i = 0; i < VARIABLE.Value.Count; i++)
    //     {
    //         Debug.Log(VARIABLE.Value[i]);
    //         
    //     }
    //     
    // }
    //}


    // public void ModifyTempVariableValues(OptionData optionData)
    // {
    //     for (int i = 0; i < optionData.variableIDList.Count; i++)
    //     {
    //         int index = optionData.variableIDList[i];
    //         int value = optionData.valueList[i];
    //         ModifyTempVariableValue(index, value);
    //     }
    // }
/*
    private void ModifyVariableValue(List<int> IDList, List<int> ValueList, int actionID, List<int> pathIDList)
    {
        for (int i = 0; i < IDList.Count; i++)
        {
            int index = IDList[i];
            int value = ValueList[i];
            GameVariable gameVariable = GameDataCache.Instance.gameVariableSet.GetGameVariableByID(index);
            if ((GameVariableType) gameVariable.type == GameVariableType.ConditonVariable)
            {
                for (int j = 0; j < pathIDList.Count; j++)
                {
                    int pathID = pathIDList[j];
                    if (gameVariableDict.ContainsKey(index))
                    {
                        Dictionary<int, Dictionary<int, int>> savedData = gameVariableDict[index];
                        if (savedData.ContainsKey(pathID))
                        {
                            Dictionary<int, int> savedPathData = savedData[pathID];
                            if (savedPathData.ContainsKey(actionID))
                            {
                                savedPathData[actionID] = value;
                            }
                            else
                            {
                                savedPathData.Add(actionID, value);
                            }
                        }
                        else
                        {
                            Dictionary<int, int> savedPathData = new Dictionary<int, int>();
                            savedPathData.Add(actionID, value);
                            savedData.Add(pathID, savedPathData);
                        }
                    }
                    else
                    {
                        Dictionary<int, Dictionary<int, int>> savedData = new Dictionary<int, Dictionary<int, int>>();
                        Dictionary<int, int> savedPathData = new Dictionary<int, int>();
                        savedPathData.Add(actionID, value);
                        savedData.Add(pathID, savedPathData);
                        gameVariableDict.Add(index, savedData);
                    }
                }
            }
            else if ((GameVariableType) gameVariable.type == GameVariableType.LoveVariable)
            {
                if (gameVariableDict.ContainsKey(index))
                {
                    Dictionary<int, Dictionary<int, int>> savedData = gameVariableDict[index];
                    if (savedData.ContainsKey(0))
                    {
                        Dictionary<int, int> savedPathData = savedData[0];
                        if (savedPathData.ContainsKey(actionID))
                        {
                            savedPathData[actionID] = value;
                        }
                        else
                        {
                            savedPathData.Add(actionID, value);
                        }
                    }
                    else
                    {
                        Dictionary<int, int> savedPathData = new Dictionary<int, int>();
                        savedPathData.Add(actionID, value);
                        savedData.Add(0, savedPathData);
                    }
                }
                else
                {
                    Dictionary<int, Dictionary<int, int>> savedData = new Dictionary<int, Dictionary<int, int>>();
                    Dictionary<int, int> savedPathData = new Dictionary<int, int>();
                    savedPathData.Add(actionID, value);
                    savedData.Add(0, savedPathData);
                    gameVariableDict.Add(index, savedData);
                }
            }
        }
    }
*/
    private void ModifyVariableValues(List<int> IDList, List<int> valueList, int actionID, List<int> pathIDList)
    {
        ModifyConditionVariableValue(IDList, valueList, actionID, pathIDList);
        ModifyLoveVariableValue(IDList, valueList, actionID);

        SaveVariableData();

    }

    private void ModifyConditionVariableValue(List<int> IDList, List<int> valueList, int actionID, List<int> pathIDList)
    {
         for (int i = 0; i < IDList.Count; i++)
        {
            int index = IDList[i];
            int value = valueList[i];
            GameVariable gameVariable = GameDataCache.Instance.gameVariableSet.GetGameVariableByID(index);
            if ((GameVariableType) gameVariable.type == GameVariableType.ConditonVariable)
            {
                for (int j = 0; j < pathIDList.Count; j++)
                {
                    int pathID = pathIDList[j];
                    if (gameVariableDict.ContainsKey(index))
                    {
                        Dictionary<int, Dictionary<int, int>> savedData = gameVariableDict[index];
                        if (savedData.ContainsKey(pathID))
                        {
                            Dictionary<int, int> savedPathData = savedData[pathID];
                            if (savedPathData.ContainsKey(actionID))
                            {
                                savedPathData[actionID] = value;
                            }
                            else
                            {
                                savedPathData.Add(actionID, value);
                            }
                        }
                        else
                        {
                            Dictionary<int, int> savedPathData = new Dictionary<int, int>();
                            savedPathData.Add(actionID, value);
                            savedData.Add(pathID, savedPathData);
                        }
                    }
                    else
                    {
                        Dictionary<int, Dictionary<int, int>> savedData = new Dictionary<int, Dictionary<int, int>>();
                        Dictionary<int, int> savedPathData = new Dictionary<int, int>();
                        savedPathData.Add(actionID, value);
                        savedData.Add(pathID, savedPathData);
                        gameVariableDict.Add(index, savedData);
                    }
                }
            }
        }
    }

    private void ModifyLoveVariableValue(List<int> IDList, List<int> valueList, int actionID)
    {
        for (int i = 0; i < IDList.Count; i++)
        {
            int index = IDList[i];
            int value = valueList[i];
            GameVariable gameVariable = GameDataCache.Instance.gameVariableSet.GetGameVariableByID(index);
            if ((GameVariableType) gameVariable.type == GameVariableType.LoveVariable)
            {
                if (gameVariableDict.ContainsKey(index))
                {
                    Dictionary<int, Dictionary<int, int>> savedData = gameVariableDict[index];
                    if (savedData.ContainsKey(0))
                    {
                        Dictionary<int, int> savedPathData = savedData[0];
                        if (savedPathData.ContainsKey(actionID))
                        {
                            savedPathData[actionID] = value;
                        }
                        else
                        {
                            savedPathData.Add(actionID, value);
                        }
                    }
                    else
                    {
                        Dictionary<int, int> savedPathData = new Dictionary<int, int>();
                        savedPathData.Add(actionID, value);
                        savedData.Add(0, savedPathData);
                    }
                }
                else
                {
                    Dictionary<int, Dictionary<int, int>> savedData = new Dictionary<int, Dictionary<int, int>>();
                    Dictionary<int, int> savedPathData = new Dictionary<int, int>();
                    savedPathData.Add(actionID, value);
                    savedData.Add(0, savedPathData);
                    gameVariableDict.Add(index, savedData);
                }
            }
        }
    }
    
    
    
    //互动操作对游戏中条件变量的影响 和 互动操作对游戏中恋爱度变量的影响
    public void ModifyVariableValues(OptionData optionData)
    {
        List<int> IDList = optionData.variableIDList;
        List<int> valueList = optionData.valueList;
        ModifyVariableValues(IDList, valueList, optionData.optionActionID, optionData.pathIDList);
       // ModifyConditionVariableValue(IDList, valueList, optionData.optionActionID, optionData.pathIDList);
      //  ModifyLoveVariableValue(IDList, valueList, optionData.optionActionID);
        
        

        /*
        for (int i = 0; i < optionData.variableIDList.Count; i++)
        {
            int index = optionData.variableIDList[i];
            int value = optionData.valueList[i];
            GameVariable gameVariable = GameDataCache.Instance.gameVariableSet.GetGameVariableByID(index);
            if ((GameVariableType) gameVariable.type == GameVariableType.ConditonVariable)
            {
                for (int j = 0; j < optionData.pathIDList.Count; j++)
                {
                    int pathID = optionData.pathIDList[j];
                    if (gameVariableDict.ContainsKey(index))
                    {
                        Dictionary<int, Dictionary<int, int>> savedData = gameVariableDict[index];
                        if (savedData.ContainsKey(pathID))
                        {
                            Dictionary<int, int> savedPathData = savedData[pathID];
                            if (savedPathData.ContainsKey(optionData.optionActionID))
                            {
                                savedPathData[optionData.optionActionID] = value;
                            }
                            else
                            {
                                savedPathData.Add(optionData.optionActionID, value);
                            }
                        }
                        else
                        {
                            Dictionary<int, int> savedPathData = new Dictionary<int, int>();
                            savedPathData.Add(optionData.optionActionID, value);
                            savedData.Add(pathID, savedPathData);
                        }
                    }
                    else
                    {
                        Dictionary<int, Dictionary<int, int>> savedData = new Dictionary<int, Dictionary<int, int>>();
                        Dictionary<int, int> savedPathData = new Dictionary<int, int>();
                        savedPathData.Add(optionData.optionActionID, value);
                        savedData.Add(pathID, savedPathData);
                        gameVariableDict.Add(index, savedData);
                    }
                }
            }
            else if ((GameVariableType) gameVariable.type == GameVariableType.LoveVariable)
            {
                if (gameVariableDict.ContainsKey(index))
                {
                    Dictionary<int, Dictionary<int, int>> savedData = gameVariableDict[index];
                    if (savedData.ContainsKey(0))
                    {
                        Dictionary<int, int> savedPathData = savedData[0];
                        if (savedPathData.ContainsKey(optionData.optionActionID))
                        {
                            savedPathData[optionData.optionActionID] = value;
                        }
                        else
                        {
                            savedPathData.Add(optionData.optionActionID, value);
                        }
                    }
                    else
                    {
                        Dictionary<int, int> savedPathData = new Dictionary<int, int>();
                        savedPathData.Add(optionData.optionActionID, value);
                        savedData.Add(0, savedPathData);
                    }
                }
                else
                {
                    Dictionary<int, Dictionary<int, int>> savedData = new Dictionary<int, Dictionary<int, int>>();
                    Dictionary<int, int> savedPathData = new Dictionary<int, int>();
                    savedPathData.Add(optionData.optionActionID, value);
                    savedData.Add(0, savedPathData);
                    gameVariableDict.Add(index, savedData);
                }
            }
            
        }
        */
        SaveVariableData();
    }

    public void ModifyVariableValues(QTEActionData qteActionDataData, int res)
    {
        List<int> IDList;
        List<int> valueList;
        List<int> pathIDList;
        if (res == 1)
        {
            IDList = qteActionDataData.sucVariableIDList;
            valueList = qteActionDataData.sucValueList;
            pathIDList = qteActionDataData.sucPathIDList;
        }
        else
        {
            IDList = qteActionDataData.failVariableIDList;
            valueList = qteActionDataData.failValueList;
            pathIDList = qteActionDataData.failPathIDList;
        }

        if (IDList != null && valueList != null)  
            ModifyVariableValues(IDList, valueList, qteActionDataData.id, pathIDList);
       // ModifyConditionVariableValue(IDList, valueList, qteActionDataData.id, pathIDList);
        //ModifyLoveVariableValue(IDList, valueList, qteActionDataData.id);
    }



    public void ModifyVariableValues(VideoData videoData)
    {
        // List<int> IDList;
        // List<int> valueList;
        // ModifyLoveVariableValue(IDList, valueList, qteActionDataData.id);
        
        
        
    }
    public int GetConditionVariableValue(int id)
    {
        int res = 0;
        if (gameVariableDict.ContainsKey(id))
        {
            // Dictionary<int,Dictionary<int, int>> savedData = gameVariableDict[id];
            GameVariable gameVariable = GameDataCache.Instance.gameVariableSet.GetGameVariableByID(id);


            List<string> pathStrList = gameVariable.path;
            List<List<int>> pathList = new List<List<int>>();
            for (int i = 0; i < pathStrList.Count; i++)
            {
                string[] pathStr = pathStrList[i].Split(',');
                List<int> path = new List<int>();
                for (int j = 0; j < pathStr.Length; j++)
                {
                    path.Add(int.Parse(pathStr[j]));
                }

                pathList.Add(path);
            }


            for (int i = historyActionList.Count - 1; i >= 0; i--)
            {
                int actionID = historyActionList[i];


                List<List<int>> containedPathList = CalulatePath(pathList, actionID);

                if (containedPathList.Count == 0)
                {
                    continue;
                }
                else if (containedPathList.Count == 1)
                {
                    int pathID = pathList.FindIndex(a => a == containedPathList[0]);
                    res = CalculatePathValue(containedPathList[0], pathID, id, actionID);
                    LogManager.Log("Variable id: " + id + " value: " + res);
                    return res;
                }
                else if (containedPathList.Count > 1)
                {
                    List<List<int>> completePathList = new List<List<int>>();
                    for (int j = 0; j < containedPathList.Count; j++)
                    {
                        int actionIndex = containedPathList[j].FindIndex(a => a == actionID);
                        int k = actionIndex;
                        if (actionIndex >= 1)
                        {
                            for (k = actionIndex - 1; k >= 0; k--)
                            {
                                int tempActionID = containedPathList[j][k];
                                if (!historyActionList.Contains(tempActionID))
                                {
                                    break;
                                }
                            }
                        }

                        if (k <= 0)
                        {
                            completePathList.Add(containedPathList[j]);
                        }
                    }

                    if (completePathList.Count == 0)
                    {
                        LogManager.LogError("no path !!!");
                    }
                    else if (completePathList.Count == 1)
                    {
                        LogManager.Log("Variable id: " + id + " value: " + res);
                        int pathID = pathList.FindIndex(a => a == containedPathList[0]);
                        res = CalculatePathValue(completePathList[0], pathID, id, actionID);
                        return res;
                    }
                    else
                    {
                        List<List<int>> containedPrePathList = completePathList;

                        while (containedPrePathList.Count != 1)
                        {
                            i--;
                            if (i < 0)
                            {
                                break;
                                //LogManager.LogError("Data Error! no avaliable data");
                                //return res;
                            }

                            int preActionID = historyActionList[i];
                            if (preActionID < actionID)
                                containedPrePathList = CalulatePath(containedPrePathList, preActionID);
                        }

                        if (containedPrePathList.Count > 1)
                        {
                            int optionIndex = optionStatusDict[actionID] - 1;
                            OptionActionData optionActionData =
                                GameDataCache.Instance.optionActionDataSet.GetOptionActionDataByID(actionID);
                            int optionID = optionActionData.options[optionIndex];
                            OptionData optionData = GameDataCache.Instance.optionDataSet.GetOptionDataByID(optionID);
                            for (int j = 0; j < containedPrePathList.Count; j++)
                            {
                                int pathID = pathList.FindIndex(a => a == containedPrePathList[j]);

                                if (optionData.pathIDList.Contains(pathID))
                                {
                                    res = CalculatePathValue(containedPrePathList[j], pathID, id, actionID);
                                    LogManager.Log("Variable id: " + id + " value: " + res);
                                    return res;
                                }
                            }
                        }
                        else if (containedPrePathList.Count == 1)
                        {
                            int pathID = pathList.FindIndex(a => a == containedPrePathList[0]);
                            res = CalculatePathValue(containedPrePathList[0], pathID, id, actionID);
                            LogManager.Log("Variable id: " + id + " value: " + res);
                            return res;
                        }
                    }
                }
            }
        }

        return res;
    }

    /*
       public int GetConditionVariableValue(int id) 
       {
           int res = 0;
           if (gameVariableDict.ContainsKey(id))
           {
              // Dictionary<int,Dictionary<int, int>> savedData = gameVariableDict[id];
               GameVariable gameVariable = GameDataCache.Instance.gameVariableSet.GetGameVariableByID(id);
   
   
               List<string> pathStrList = gameVariable.path;
               List<List<int>> pathList = new List<List<int>>();
               for (int i = 0; i < pathStrList.Count; i++)
               {
                   string[] pathStr = pathStrList[i].Split(',');
                   List<int> path = new List<int>();
                   for (int j = 0; j < pathStr.Length; j++)
                   {
                       path.Add(int.Parse(pathStr[j]));
                   }
                   pathList.Add(path);
               }
   
   
               for (int i = historyActionList.Count - 1; i >= 0; i--)
               {
                   int actionID = historyActionList[i]; 
   
   
                   List<List<int>> containedPathList = CalulatePath(pathList, actionID);
   
                   if (containedPathList.Count == 0)
                   {
                       continue;
                   }
                   else if (containedPathList.Count == 1)
                   {
                       res = CalculatePathValue(containedPathList[0], id);
                       LogManager.Log("Variable id: " + id + " value: " + res);
                       return res;
                   }
                   else if (containedPathList.Count > 1)
                   {
                       List<List<int>> completePathList = new List<List<int>>();
                       for (int j = 0; j < containedPathList.Count; j++)
                       {
                           int actionIndex = containedPathList[j].FindIndex(a=> a == actionID);
                           int k;
                           for (k = actionIndex - 1; k >= 0; k--)
                           {
                               int tempActionID = containedPathList[j][k];
                               if (!historyActionList.Contains(tempActionID))
                               {
                                   break;
                               }
                           }
   
                           if (k == 0)
                           {
                               completePathList.Add(containedPathList[j]);
                           }
                       }
                       if (completePathList.Count == 0)
                       {
                           LogManager.LogError("no path !!!");
                       }
                       else if (completePathList.Count == 1)
                       {
                           LogManager.Log("Variable id: " + id + " value: " + res);
                           res = CalculatePathValue(completePathList[0], id);
                           return res;
                       }
                       else
                       {
                           List<List<int>> containedPrePathList  = new List<List<int>>();
   
                           while (containedPrePathList.Count != 1)
                           {
                               i--;
                               if (i < 0)
                               {
                                   LogManager.LogError("Data Error! no avaliable data");
                                   return res;
                               }
                               int preActionID = historyActionList[i];
                               containedPrePathList = CalulatePath(completePathList, preActionID);
                           }
                           res = CalculatePathValue(containedPrePathList[0], id);
                           LogManager.Log("Variable id: " + id + " value: " + res);
                           return res;
                       }
                   }
               }
           }
           return res;
       }
   
   */
    private List<List<int>> CalulatePath(List<List<int>> pathList, int index)
    {
        List<List<int>> containedPathList = new List<List<int>>();
        List<int> pathIDList = new List<int>();
        for (int i = 0; i < pathList.Count; i++)
        {
            if (pathList[i].Contains(index))
            {
                // if (pathList[i][0] == index)//如果是路径的起始，直接选择该路径
                // {
                //     containedPathList.Add(pathList[i]);
                //     pathIDList.Add(i);
                //     return containedPathList;
                // }
                // else
                {
                    containedPathList.Add(pathList[i]);
                    pathIDList.Add(i);
                }
            }
        }

        return containedPathList;
    }

    private int CalculatePathValue(List<int> path, int pathID, int variableIndex, int actionID)
    {
        int res = 0;
        if (gameVariableDict.ContainsKey(variableIndex))
        {
            //Dictionary<int, int> savedData = gameVariableDict[variableIndex];
            Dictionary<int, Dictionary<int, int>> savedData = gameVariableDict[variableIndex];
            Dictionary<int, int> savedPathData = savedData[pathID];
            for (int i = 0; i < path.Count; i++)
            {
                int actionIndex = path[i];
                if (savedPathData.ContainsKey(actionIndex))
                    res += savedPathData[actionIndex];
                if (actionIndex == actionID)
                    return res;
            }
        }

        return res;
    }

    public int GetLoveVariableValue(int id)
    {
        int res = 0;
        if (gameVariableDict.ContainsKey(id))
        {
            // Dictionary<int,Dictionary<int, int>> savedData = gameVariableDict[id];
            GameVariable gameVariable = GameDataCache.Instance.gameVariableSet.GetGameVariableByID(id);
            Dictionary<int, Dictionary<int, int>> savedData = gameVariableDict[id];
            Dictionary<int, int> savedPathData = savedData[0];
            foreach (var VARIABLE in savedPathData)
            {
                res += VARIABLE.Value;
            }
        }

        return res;
    }


    public void ModifyVideoStatus(int videoID, int value)
    {
        if (videoStatusDict.ContainsKey(videoID))
            videoStatusDict[videoID] = value;
        else
            videoStatusDict.Add(videoID, value);
        SaveVideoStatus();
    }

    public void ModifyOptionStatus(int optionID, int value)
    {
        if (optionStatusDict.ContainsKey(optionID))
            optionStatusDict[optionID] = value;
        else
            optionStatusDict.Add(optionID, value);
        SaveOptionStatus();
    }

    public void ModifyChapterStatus(int chapterID, int value)
    {
        if (chapterStatusDict.ContainsKey(chapterID))
            chapterStatusDict[chapterID] = value;
        else
            chapterStatusDict.Add(chapterID, value);
        SaveChapterStatus();
    }

    public void ModifyQTEStatus(int QTEActionID, int value)
    {
        if (QTEStatusDict.ContainsKey(QTEActionID))
            QTEStatusDict[QTEActionID] = value;
        else
            QTEStatusDict.Add(QTEActionID, value);
        SaveQTEStatus();
    }
    public void ModifyActionHistory(int actionID)
    {
        historyActionList.Add(actionID);
        SaveActionHistory();
    }
    // public void LoadVariableList(int videoID)
    // {
    //     if (historyVariablesDict.ContainsKey(videoID))
    //     {
    //         variableList = new List<int>();
    //         for (int j = 0; j < historyVariablesDict[videoID].Count; j++)
    //         {
    //             variableList.Add(historyVariablesDict[videoID][j]);
    //         }
    //
    //         //variableList = historyVariablesDict[videoID];
    //     }
    //     else
    //         Init();
    // }


    // public void SaveDatas()
    // {
    //     LogManager.Log("Save Datas");
    //     ES3.Save<Dictionary<int, int>>("videoStatusDict", videoStatusDict);
    //     ES3.Save<Dictionary<int, int>>("optionStatusDict", optionStatusDict);
    //     ES3.Save<Dictionary<int, int>>("QTEStatusDict", QTEStatusDict);
    //     ES3.Save<Dictionary<int, int>>("chapterStatusDict", chapterStatusDict);
    // //   ES3.Save<Dictionary<int, List<int>>>("historiyVariablesDict", historyVariablesDict);
    //     ES3.Save<int>("lastVideoID", lastVideoID);
    //     //ES3.Save<List<int>>("variableList", variableList);
    //     //  ES3.Save<List<int>>("conditionVariableList", conditionVariableList);
    // }

    public void ModifyLastVideoID(int ID)
    {
        lastVideoID = ID;
        SaveLastVideoID();
    }

    private void SaveLastVideoID()
    {
        if(ConfigSettings.Instance.useLocalData)
            ES3.Save<int>("lastVideoID", lastVideoID);
    }
    private void SaveVideoStatus()
    {
        if(ConfigSettings.Instance.useLocalData)
            ES3.Save<Dictionary<int, int>>("videoStatusDict", videoStatusDict);
        string jsonString = JsonConvert.SerializeObject(videoStatusDict);
        MessageDispatcher.SendMessage(this, GameEvent.SET_VIDEO_REQ, jsonString, 0);
    }
    private void SaveChapterStatus()
    {
        if(ConfigSettings.Instance.useLocalData)
            ES3.Save<Dictionary<int, int>>("chapterStatusDict", chapterStatusDict);
        string jsonString = JsonConvert.SerializeObject(chapterStatusDict);
        MessageDispatcher.SendMessage(this, GameEvent.SET_CHAPTER_REQ, jsonString, 0);
    }
    private void SaveOptionStatus()
    {
        if(ConfigSettings.Instance.useLocalData)
            ES3.Save<Dictionary<int, int>>("optionStatusDict", optionStatusDict);
        string jsonString = JsonConvert.SerializeObject(optionStatusDict);
        MessageDispatcher.SendMessage(this, GameEvent.SET_OPTION_REQ, jsonString, 0);
    }
    private void SaveQTEStatus()
    {
        if(ConfigSettings.Instance.useLocalData)
            ES3.Save<Dictionary<int, int>>("QTEStatusDict", QTEStatusDict);
        string jsonString = JsonConvert.SerializeObject(QTEStatusDict);
        MessageDispatcher.SendMessage(this, GameEvent.SET_QTE_REQ, jsonString, 0);
    }
    private void SaveVariableData()
    {
        if(ConfigSettings.Instance.useLocalData)
            ES3.Save<Dictionary<int, Dictionary<int, Dictionary<int, int>>>>("gameVariableDict", gameVariableDict);
        
        
        string jsonString = JsonConvert.SerializeObject(gameVariableDict);
        MessageDispatcher.SendMessage(this, GameEvent.SET_Variables_REQ, jsonString, 0);
    }

    private void SaveActionHistory()
    {
       // historyActionList.Add(actionID);
        if(ConfigSettings.Instance.useLocalData)
            ES3.Save<List<int>>("historyActionList", historyActionList);
        
        string jsonString = JsonConvert.SerializeObject(historyActionList);
        MessageDispatcher.SendMessage(this, GameEvent.SET_ActionHistory_REQ, jsonString, 0);
    }
    

    public void ClearDatas()
    {
        ES3.DeleteFile(new ES3Settings());
    }


    public int GetChapterSeenVideoNums(int chapterID)
    {
        int res = 0;
        
        foreach (var VARIABLE in videoStatusDict)
       {
           VideoData videoData = GameDataCache.Instance.videoDataSet.GetVideoDataByID(VARIABLE.Key);
            if (videoData.chapterID == chapterID && VARIABLE.Value != 0)
            {
                res++;
            }
       }
        return res;
    }
    public int GetChapterTotalVideoNums(int chapterID)
    {
        int res = 0;
        
        
        
        
        foreach (var VARIABLE in GameDataCache.Instance.videoDataSet.VideoDataDict)
        {
            //VideoData videoData = GameDataCache.Instance.videoDataSet.GetVideoDataByID(VARIABLE.Key);
            if (VARIABLE.Value.chapterID == chapterID)
            {
                res++;
            }
        }
        return res;
    }
    public int GetChapterSeenEndingNums(int chapterID)
    {
        int res = 0;
        
        foreach (var VARIABLE in videoStatusDict)
        {
            VideoData videoData = GameDataCache.Instance.videoDataSet.GetVideoDataByID(VARIABLE.Key);
            if (videoData.chapterID == chapterID && VARIABLE.Value != 0 && videoData.isEnding)
            {
                res++;
            }
        }
        return res;
    }
    public int GetChapterTotalEndingNums(int chapterID)
    {
        int res = 0;
        
        foreach (var VARIABLE in GameDataCache.Instance.videoDataSet.VideoDataDict)
        {
           // VideoData videoData = GameDataCache.Instance.videoDataSet.GetVideoDataByID(VARIABLE.Key);
            if (VARIABLE.Value.chapterID == chapterID && VARIABLE.Value.isEnding)
            {
                res++;
            }
        }
        return res;
    }


}