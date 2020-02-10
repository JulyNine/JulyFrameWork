using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.ootii.Messages;
using System;
using UnityEngine;
using Newtonsoft.Json;
#region Get
public partial class GameEvent
{
    //获取用户视频信息
    public static string GET_VIDEO_REQ = "GetVideoReq";
}

/// <summary>
/// 获取视频消息参数
/// </summary>
public class GetVideoArgs
{
    public string userName;
}


public partial class GameProtocol
{
    //获取章节信息
    public static string GetVideo_URL = HttpRootURL + "video";

    public static Dictionary<string, object> GetVideo_ParamDict = new Dictionary<string, object>()
    {
        {"username", ""},
    };
}

public partial class NetworkManager
{
    //获取用户章节信息
    public void GetVideoReq(IMessage rMessage)
    {
        rMessage.IsHandled = true;
        // GetChapterArgs args = rMessage.Data as GetChapterArgs;
        GameProtocol.GetVideo_ParamDict["username"] = GameMain.Instance.accountName;
        // GameProtocol.Login_ParamDict["password"] = args.password;
        HttpManager.Instance.Get(GameProtocol.GetVideo_URL, GameProtocol.GetVideo_ParamDict, rMessage, GetOptionAck);
    }

    public void Video(string resJsonString, IMessage sendMessage)
    {
        Debug.Log("GetCVideoAck resJsonString" + resJsonString);
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
                    NetDataManager.Instance.videoStatusDict = data;   
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
#endregion

#region Set

public partial class GameEvent
{
    //设置视频信息
    public static string SET_VIDEO_REQ = "SetVideoReq";
}

/// <summary>
/// 设置视频信息参数
/// </summary>
public class SetVideoArgs
{
    public string userName;
}


public partial class GameProtocol
{
    //获取视频信息
    public static string SetVideo_URL = HttpRootURL + "video";

    public static Dictionary<string, object> SetVideo_ParamDict = new Dictionary<string, object>()
    {
        {"id", 0},
        {"username", ""},
        {"value", ""},
    };
}

public partial class NetworkManager
{
    public void SetVideoReq(IMessage rMessage)
    {
        rMessage.IsHandled = true;
        // GetChapterArgs args = rMessage.Data as GetChapterArgs;
        string value = rMessage.Data as string;
        GameProtocol.SetVideo_ParamDict["username"] = GameMain.Instance.accountName;
        GameProtocol.SetVideo_ParamDict["id"] = 0;
        GameProtocol.SetVideo_ParamDict["value"] = value;
        // GameProtocol.Login_ParamDict["password"] = args.password;
        HttpManager.Instance.Post(GameProtocol.SetVideo_URL, GameProtocol.SetVideo_ParamDict, rMessage, SetVideoAck);
    }

    public void SetVideoAck(string resJsonString, IMessage sendMessage)
    {
        Debug.Log("SetVideoAck resJsonString" + resJsonString);
    }
}
#endregion