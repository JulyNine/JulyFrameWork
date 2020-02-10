using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.ootii.Messages;
using Newtonsoft.Json;
using System;
using UnityEngine;

#region Get
public partial class GameEvent
{
    public static string GET_ActionHistory_REQ = "GetActionHistoryReq";
}

public class GetActionHistoryArgs
{
    public string userName;
}


public partial class GameProtocol
{
    public static string GetActionHistory_URL = HttpRootURL + "historyaction";

    public static Dictionary<string, object> GetActionHistory_ParamDict = new Dictionary<string, object>()
    {
        {"username", ""},
    };
}

public partial class NetworkManager
{
    public void GetActionHistoryReq(IMessage rMessage)
    {
        rMessage.IsHandled = true;
        // GetChapterArgs args = rMessage.Data as GetChapterArgs;
        GameProtocol.GetActionHistory_ParamDict["username"] = GameMain.Instance.accountName;
        // GameProtocol.Login_ParamDict["password"] = args.password;
        HttpManager.Instance.Get(GameProtocol.GetActionHistory_URL, GameProtocol.GetActionHistory_ParamDict, rMessage, GetActionHistoryAck);
    }

    public void GetActionHistoryAck(string resJsonString, IMessage sendMessage)
    {
        Debug.Log("GetActionHistoryAck resJsonString" + resJsonString);
        try
        {
            Dictionary<string, object> resDict =  JsonConvert.DeserializeObject<Dictionary<string, object>>(resJsonString);
            string res = resDict["message"].ToString();
            if (res.Equals("SUCCESS"))
            {
                if (resDict["data"] != null)
                {
                    Dictionary<string, object> dataDict =  JsonConvert.DeserializeObject<Dictionary<string, object>>(resDict["data"].ToString());
                      
                      
                    List<int> data = JsonConvert.DeserializeObject<List<int>>(dataDict["value"].ToString());
                    NetDataManager.Instance.historyActionList = data;   
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
    public static string SET_ActionHistory_REQ = "SetActionHistoryReq";
}

public class SetActionHistoryArgs
{
    public string userName;
}


public partial class GameProtocol
{
    public static string SetActionHistory_URL = HttpRootURL + "historyaction";

    public static Dictionary<string, object> SetActionHistory_ParamDict = new Dictionary<string, object>()
    {
        {"id", 0},
        {"username", ""},
        {"value", ""},
    };
}

public partial class NetworkManager
{
    public void SetActionHistoryReq(IMessage rMessage)
    {
        rMessage.IsHandled = true;
        // GetChapterArgs args = rMessage.Data as GetChapterArgs;
        string value = rMessage.Data as string;
        GameProtocol.SetActionHistory_ParamDict["username"] = GameMain.Instance.accountName;
        GameProtocol.SetActionHistory_ParamDict["id"] = 0;
        GameProtocol.SetActionHistory_ParamDict["value"] = value;
        // GameProtocol.Login_ParamDict["password"] = args.password;
        HttpManager.Instance.Post(GameProtocol.SetActionHistory_URL, GameProtocol.SetActionHistory_ParamDict, rMessage, SetActionHistoryAck);
    }

    public void SetActionHistoryAck(string resJsonString, IMessage sendMessage)
    {
        Debug.Log("SetActionHistoryAck resJsonString" + resJsonString);
    }
}
#endregion