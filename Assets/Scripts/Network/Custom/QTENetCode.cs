using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.ootii.Messages;
using System;
using Newtonsoft.Json;
using UnityEngine;

#region Get
public partial class GameEvent
{
    public static string GET_QTE_REQ = "GetQTEReq";
}

public class GetQTEArgs
{
    public string userName;
}


public partial class GameProtocol
{
    //获取章节信息
    public static string GetQTE_URL = HttpRootURL + "qte";

    public static Dictionary<string, object> GetQTE_ParamDict = new Dictionary<string, object>()
    {
        {"username", ""},
    };
}

public partial class NetworkManager
{
    public void GetQTEReq(IMessage rMessage)
    {
        rMessage.IsHandled = true;
        // GetChapterArgs args = rMessage.Data as GetChapterArgs;
        GameProtocol.GetQTE_ParamDict["username"] = GameMain.Instance.accountName;
        // GameProtocol.Login_ParamDict["password"] = args.password;
        HttpManager.Instance.Get(GameProtocol.GetQTE_URL, GameProtocol.GetQTE_ParamDict, rMessage, GetQTEAck);
    }

    public void GetQTEAck(string resJsonString, IMessage sendMessage)
    {
        Debug.Log("GetQTEAck resJsonString" + resJsonString);
        
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
                    NetDataManager.Instance.QTEStatusDict = data;   
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
    public static string SET_QTE_REQ = "SetQTEReq";
}

public class SetQTEArgs
{
    public string userName;
}


public partial class GameProtocol
{
    public static string SetQTE_URL = HttpRootURL + "qte";

    public static Dictionary<string, object> SetQTE_ParamDict = new Dictionary<string, object>()
    {
        {"id", 0},
        {"username", ""},
        {"value", ""},
    };
}

public partial class NetworkManager
{
    public void SetQTEReq(IMessage rMessage)
    {
        rMessage.IsHandled = true;
        // GetChapterArgs args = rMessage.Data as GetChapterArgs;
        string value = rMessage.Data as string;
        GameProtocol.SetQTE_ParamDict["username"] = GameMain.Instance.accountName;
        GameProtocol.SetQTE_ParamDict["id"] = 0;
        GameProtocol.SetQTE_ParamDict["value"] = value;
        // GameProtocol.Login_ParamDict["password"] = args.password;
        HttpManager.Instance.Post(GameProtocol.SetQTE_URL, GameProtocol.SetQTE_ParamDict, rMessage, SetQTEAck);
    }

    public void SetQTEAck(string resJsonString, IMessage sendMessage)
         {
        Debug.Log("SetQTEAck resJsonString" + resJsonString);
    }
}
#endregion