using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.ootii.Messages;
using System;
using GameDataFrame;
using Newtonsoft.Json;
using UnityEngine;
public partial class GameEvent
{
    //获取用户章节信息
    public static string GET_CHAPTER_REQ = "GetChapterReq";
}
/// <summary>
/// 获取章节消息参数
/// </summary>
public class GetChapterArgs
{
    public string userName;
}


public partial class GameProtocol
{
    //获取章节信息
    public static string GetChapter_URL = HttpRootURL + "chapter";

    public static Dictionary<string, object> GetChapter_ParamDict = new Dictionary<string, object>()
    {
        {"username", ""},
    };
}

public partial class NetworkManager
{
    //获取用户章节信息
    public void GetChapterReq(IMessage rMessage)
    {
        rMessage.IsHandled = true;
       // GetChapterArgs args = rMessage.Data as GetChapterArgs;
        GameProtocol.GetChapter_ParamDict["username"] = GameMain.Instance.accountName;
        // GameProtocol.Login_ParamDict["password"] = args.password;
        HttpManager.Instance.Get(GameProtocol.GetChapter_URL, GameProtocol.GetChapter_ParamDict, rMessage, GetChapterAck);
    }
    
    public void GetChapterAck(string resJsonString, IMessage sendMessage)
    {
        Debug.Log("GetChapterAck resJsonString" + resJsonString);
        
        try
        {
            Dictionary<string, object> resDict =  JsonConvert.DeserializeObject<Dictionary<string, object>>(resJsonString);
            string res = resDict["message"].ToString();
              if (res.Equals("SUCCESS"))
              {
                  if (resDict["data"] != null)
                  {
                      Dictionary<string, object> dataDict =  JsonConvert.DeserializeObject<Dictionary<string, object>>(resDict["data"].ToString());
                      
                      
                      Dictionary<int, int> data = JsonConvert.DeserializeObject<Dictionary<int, int>>(dataDict["value"].ToString());
                      NetDataManager.Instance.chapterStatusDict = data;   
                  }
                  else
                  {
                      //暂时没有服务器数据,自己动手造数据
                      Dictionary<int, int> data = new Dictionary<int, int>();
                      for (int i = 1; i <= GameDataCache.Instance.chapterDataSet.ChapterDataDict.Count;i++)
                      {
                          data.Add(i, 0);
                      }

                      data[1] = 1;
                      NetDataManager.Instance.chapterStatusDict = data;   
                  }
                  // MessageDispatcher.SendMessage(GameEvent.GET_HEROINFOS_ACK);
              }
              else
              {
                  LogManager.LogError(res);
              }
        }
        catch (ArgumentNullException e)
        {
            LogManager.LogError(e.Message);
        }
    }
}





public partial class GameEvent
{
    //设置用户章节信息
    public static string SET_CHAPTER_REQ = "SetChapterReq";
}
/// <summary>
/// 设置章节消息参数
/// </summary>
public class SetChapterArgs
{
    public string userName;
}


public partial class GameProtocol
{
    //获取章节信息
    public static string SetChapter_URL = HttpRootURL + "chapter";

    public static Dictionary<string, object> SetChapter_ParamDict = new Dictionary<string, object>()
    {
        {"id", ""},
        {"username", ""},
        {"value", ""},
    };
}

public partial class NetworkManager
{
    //设置用户章节信息
    public void SetChapterReq(IMessage rMessage)
    {
        rMessage.IsHandled = true;
        // GetChapterArgs args = rMessage.Data as GetChapterArgs;
        string value = rMessage.Data as string;
        GameProtocol.SetChapter_ParamDict["username"] = GameMain.Instance.accountName;
        GameProtocol.SetChapter_ParamDict["id"] = GameMain.Instance.accountId;
        GameProtocol.SetChapter_ParamDict["value"] = value;
        // GameProtocol.Login_ParamDict["password"] = args.password;
        HttpManager.Instance.Post(GameProtocol.SetChapter_URL, GameProtocol.SetChapter_ParamDict, rMessage, SetChapterAck);
    }
    
    public void SetChapterAck(string resJsonString, IMessage sendMessage)
    {
        Debug.Log("GetChapterAck resJsonString" + resJsonString);

    }
}













