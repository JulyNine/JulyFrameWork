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
    public static string GET_Variables_REQ = "GetVariablesReq";
}

public class GetVariablesArgs
{
    public string userName;
}


public partial class GameProtocol
{
    public static string GetVariables_URL = HttpRootURL + "gamevariable";

    public static Dictionary<string, object> GetVariables_ParamDict = new Dictionary<string, object>()
    {
        {"username", ""},
    };
}

public partial class NetworkManager
{
    public void GetVariablesReq(IMessage rMessage)
    {
        rMessage.IsHandled = true;
        // GetChapterArgs args = rMessage.Data as GetChapterArgs;
        GameProtocol.GetVariables_ParamDict["username"] = GameMain.Instance.accountName;
        // GameProtocol.Login_ParamDict["password"] = args.password;
        HttpManager.Instance.Get(GameProtocol.GetVariables_URL, GameProtocol.GetVariables_ParamDict, rMessage, GetVariablesAck);
    }

    public void GetVariablesAck(string resJsonString, IMessage sendMessage)
    {
        Debug.Log("GetVariablesAck resJsonString" + resJsonString);
        
        try
        {
            Dictionary<string, object> resDict =  JsonConvert.DeserializeObject<Dictionary<string, object>>(resJsonString);
            string res = resDict["message"].ToString();
            if (res.Equals("SUCCESS"))
            {
                if (resDict["data"] != null)
                {
                    Dictionary<string, object> dataDict =  JsonConvert.DeserializeObject<Dictionary<string, object>>(resDict["data"].ToString());
                      
                      
                    Dictionary<int, Dictionary<int, Dictionary<int, int>>> data = JsonConvert.DeserializeObject<Dictionary<int, Dictionary<int, Dictionary<int, int>>>>(dataDict["value"].ToString());
                    NetDataManager.Instance.gameVariableDict = data;   
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
    public static string SET_Variables_REQ = "SetVariablesReq";
}

public class SetVariablesArgs
{
    public string userName;
}


public partial class GameProtocol
{
    public static string SetVariables_URL = HttpRootURL + "gamevariable";

    public static Dictionary<string, object> SetVariables_ParamDict = new Dictionary<string, object>()
    {
        {"id", 0},
        {"username", ""},
        {"value", ""},
    };
}

public partial class NetworkManager
{
    public void SetVariablesReq(IMessage rMessage)
    {
        rMessage.IsHandled = true;
        // GetChapterArgs args = rMessage.Data as GetChapterArgs;
        string value = rMessage.Data as string;
        GameProtocol.SetVariables_ParamDict["username"] = GameMain.Instance.accountName;
        GameProtocol.SetVariables_ParamDict["id"] = 0;
        GameProtocol.SetVariables_ParamDict["value"] = value;
        // GameProtocol.Login_ParamDict["password"] = args.password;
        HttpManager.Instance.Post(GameProtocol.SetVariables_URL, GameProtocol.SetVariables_ParamDict, rMessage, SetVariablesAck);
    }

    public void SetVariablesAck(string resJsonString, IMessage sendMessage)
    {
        Debug.Log("SetVariablesAck resJsonString" + resJsonString);
    }
}
#endregion