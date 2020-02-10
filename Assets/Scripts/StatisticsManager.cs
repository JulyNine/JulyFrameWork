using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.XR;
//using Newtonsoft.Json;

public static class StatisticsManager
{

    //注册
    public static string URL = "http://223.223.179.220/FMVGmaeStatistics/UserAction.php";
    public static Dictionary<string, object> ActionLog_ParamDict = new Dictionary<string, object>()
    {
        { "name", ""},
        { "id", ""},
        { "type", ""},
        { "res", ""},
      //  { "password", ""},
    };

    public static void UploadActionData(int id, int type, int res)
    {

        ActionLog_ParamDict["name"] = GameMain.Instance.accountName;
        ActionLog_ParamDict["id"] = id.ToString();
        ActionLog_ParamDict["type"] = type.ToString();
        ActionLog_ParamDict["res"] = res.ToString();
       // GameProtocol.Login_ParamDict["password"] = args.password;
       //HttpManager.Instance.Post(URL, ActionLog_ParamDict, null, null);

    }
    //public static void LoginAck(string resJsonString, IMessage sendMessage)
    //{
    //    Debug.Log("resJsonString" + resJsonString);
    //    if (resJsonString.Trim() == "Success")
    //    {
    //        GameMain.Instance.accountName = nameInput.text;
    //        mainPanel.SetActive(true);
    //        loginPanel.SetActive(false);
    //    }
    //    else
    //    {
    //        UIManager.Instance.ShowTipsWithAnim("请先注册");
    //    }
    //}

}
