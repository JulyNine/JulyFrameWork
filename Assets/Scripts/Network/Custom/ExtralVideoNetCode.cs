using com.ootii.Messages;
using System.Collections.Generic;
using UnityEngine;


#region Get

public partial class GameEvent
{
    //获取用户番外视频信息
    public static string GET_EXTRAVIDEO_REQ = "GetVideoReq";
}

public class GetExtraVideoArgs
{
    public string userName;
}

public partial class GameProtocol
{
    public static string GetExtraVideo_URL = HttpRootURL + "ExtralVideo";

    public static Dictionary<string, object> GetExtraVideo_ParamDict = new Dictionary<string, object>()
    {
        {"username", ""},
    };
}


public partial class NetworkManager
{
    //获取用户番外章节信息
    public void GetExtraVideoReq(IMessage rMessage)
    {
        rMessage.IsHandled = true;
        GameProtocol.GetExtraVideo_ParamDict["username"] = GameMain.Instance.accountName;
        HttpManager.Instance.Get(GameProtocol.GetExtraVideo_URL, GameProtocol.GetExtraVideo_ParamDict, rMessage, GetOptionAck);
    }

    public void ExtralVideo(string resJsonString, IMessage sendMessage)
    {
        Debug.Log("GetCVideoAck resJsonString" + resJsonString);
    }

    public void GetExtralVideoAck(string resJsonString, IMessage sendMessage)
    {
        Debug.Log("GetExtralVideoAck resJsonString == " + resJsonString);
        
        
        
        
    }


}
#endregion


#region Set

public partial class GameEvent
{
    //设置番外视频信息
    public static string SET_EXTRALVIDEO_REQ = "SetExtralVideoReq";
}

public class SetExtraVideoArgs
{
    public string userName;
}


public partial class GameProtocol
{
    //获取视频信息
    public static string SetExtralVideo_URL = HttpRootURL + "ExtralVideo";

    public static Dictionary<string, object> SetExtralVideo_ParamDict = new Dictionary<string, object>()
    {
        {"id", 0},
        {"username", ""},
        {"value", ""},
    };
}

public partial class NetworkManager
{
    public void SetExtraVideoReq(IMessage rMessage)
    {
        rMessage.IsHandled = true;
        string value = rMessage.Data as string;
        GameProtocol.SetVideo_ParamDict["username"] = GameMain.Instance.accountName;
        GameProtocol.SetVideo_ParamDict["id"] = 0;
        GameProtocol.SetVideo_ParamDict["value"] = value;
        HttpManager.Instance.Post(GameProtocol.SetVideo_URL, GameProtocol.SetVideo_ParamDict, rMessage, SetVideoAck);
    }

    public void SetExtraVideoAck(string resJsonString, IMessage sendMessage)
    {
        Debug.Log("SetExtraVideoAck resJsonString" + resJsonString);
    }

}


#endregion
