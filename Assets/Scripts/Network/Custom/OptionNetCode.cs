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
    public static string GET_OPTION_REQ = "GetOptionReq";
}

public class GetOptionArgs
{
    public string userName;
}


public partial class GameProtocol
{
    //获取章节信息
    public static string GetOption_URL = HttpRootURL + "options";

    public static Dictionary<string, object> GetOption_ParamDict = new Dictionary<string, object>()
    {
        {"username", ""},
    };
}

public partial class NetworkManager
{
    public void GetOptionReq(IMessage rMessage)
    {
        rMessage.IsHandled = true;
        // GetChapterArgs args = rMessage.Data as GetChapterArgs;
        GameProtocol.GetOption_ParamDict["username"] = GameMain.Instance.accountName;
        // GameProtocol.Login_ParamDict["password"] = args.password;
        HttpManager.Instance.Get(GameProtocol.GetOption_URL, GameProtocol.GetOption_ParamDict, rMessage, GetOptionAck);
    }

    public void GetOptionAck(string resJsonString, IMessage sendMessage)
    {
        Debug.Log("GetOptionAck resJsonString" + resJsonString);
        
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
                    NetDataManager.Instance.optionStatusDict = data;   
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
    public static string SET_OPTION_REQ = "SetOptionReq";
}

public class SetOptionArgs
{
    public string userName;
}


public partial class GameProtocol
{
    public static string SetOption_URL = HttpRootURL + "options";

    public static Dictionary<string, object> SetOption_ParamDict = new Dictionary<string, object>()
    {
        {"id", 0},
        {"username", ""},
        {"value", ""},
    };
}

public partial class NetworkManager
{
    public void SetOptionReq(IMessage rMessage)
    {
        rMessage.IsHandled = true;
        // GetChapterArgs args = rMessage.Data as GetChapterArgs;
        string value = rMessage.Data as string;
        GameProtocol.SetOption_ParamDict["username"] = GameMain.Instance.accountName;
        GameProtocol.SetOption_ParamDict["id"] = 0;
        GameProtocol.SetOption_ParamDict["value"] = value;
        // GameProtocol.Login_ParamDict["password"] = args.password;
        HttpManager.Instance.Post(GameProtocol.SetOption_URL, GameProtocol.SetOption_ParamDict, rMessage, SetOptionAck);
    }

    public void SetOptionAck(string resJsonString, IMessage sendMessage)
    {
        Debug.Log("SetOptionAck resJsonString" + resJsonString);
    }
}
#endregion