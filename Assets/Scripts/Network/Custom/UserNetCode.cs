using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.ootii.Messages;
using System;
using UnityEngine;
using Newtonsoft.Json;
#region Login
public partial class GameEvent
{
    //登录
    public static string LOGIN_REQ = "LoginReq";
}

/// <summary>
/// 登录参数
/// </summary>
public class LoginArgs
{
    public string sdkId;
    //  public string password;
}

public class UserData
{
   // public string userName;
   public int coin;
   public int lastVideoId;
   public string sdkid;
   public string userid;
}




public partial class GameProtocol
{
    //登录
    public static string Login_URL = HttpRootURL + "login";

    public static Dictionary<string, object> Login_ParamDict = new Dictionary<string, object>
    {
        {"sdkId", ""}
        //{ "password", ""},
    };
}

public partial class NetworkManager
{
    //登录
    public void LoginReq(IMessage rMessage)
    {
        rMessage.IsHandled = true;
        LoginArgs args = rMessage.Data as LoginArgs;
        GameProtocol.Login_ParamDict["sdkId"] = args.sdkId;
        // GameProtocol.Login_ParamDict["password"] = args.password;
        HttpManager.Instance.Post(GameProtocol.Login_URL, GameProtocol.Login_ParamDict, rMessage, LoginAck);
    }
    public void LoginAck(string resJsonString, IMessage sendMessage)
    {
        Debug.Log("LoginAck resJsonString" + resJsonString);
        try
        {
            Dictionary<string, object> resDict =  JsonConvert.DeserializeObject<Dictionary<string, object>>(resJsonString);
            string res = resDict["message"].ToString();
            if (res.Equals("SUCCESS"))
            {
                if (resDict["data"] != null)
                {
                    NetDataManager.Instance.userData = JsonConvert.DeserializeObject<UserData>(resDict["data"].ToString());
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

