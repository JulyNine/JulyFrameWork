using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using com.ootii.Messages;
/// <summary>
/// 游戏中各事件消息定义
/// </summary>
public partial class GameEvent
{
    //切换游戏语言
    public static string CHANGE_LANGUAGE = "ChangeLanguage";
    //加载version xml
    public static string LOADXML_FINISH = "LoadXMLFinish";
    //下载bundle
    public static string DOWNLOAD_ASSETBUNDLES = "DownloadAssetBundles";
    //下载某个bundle成功
    public static string DOWNLOAD_ASSETBUNDLE_SUCCESS = "DownloadAssetBundleSuccess";
    //下载所有bundle完成
    public static string DOWNLOAD_ASSETBUNDLES_FINISH = "DownloadAssetBundlesFinish";
    //加载数据
    public static string LOADDATA_FINISH = "LoadDataFinish";
    //打开UI Window
    public static string OPEN_UIWINDOW = "OpenUIWindow";
    //打开上一级UI Window
    public static string OPEN_BACKWINDOW = "OpenBackWindow";
    //打开强制Window
    public static string OPEN_ENFORCEWINDOW = "OpenEnforceWindow";
    //关闭强制Window
    public static string CLOSE_ENFORCEWINDOW = "CloseEnforceWindow";
    //显示提示文字
    public static string SHOW_TIPS = "ShowTips";
    //显示用户信息
    public static string SHOW_USERINFO = "ShowUserInfo";
    //显示用户信息
    public static string SHOW_ReqMask = "ShowReqMask";

    //注册
    public static string REGISTER_REQ = "RegisterReq";

    //丢失WIFI网络
    public static string LOST_WIFI = "LostWifi";



    //
    public static string PlayVideoStart = "PlayVideoStart";
    public static string PlayVideoReady = "PlayVideoReady";
    public static string PlayVideoFinish = "PlayVideoFinish";
    public static string QTEEventResult = "QTEEventResult";
    public static string SelectEventSelected = "SelectEventSelected";
    public static string ActionFinished = "ActionFinished";
    //创建玩家
    public static string CREATE_PLAYER_REQ = "CreatePlayerReq";
    public static string CREATE_PLAYER_ACK = "CreatePlayerAck";
}


/// <summary>
/// 切换游戏语言
/// </summary>
public class ChangeGameLanguageArgs
{
    //切换的目标语言
    public GameLanguage language;
}


/// <summary>
/// userInfo消息参数
/// </summary>
public class ShowUserInfoArgs
{
    public bool show;
    public bool showUserIcon;
}


/// <summary>
/// 注册参数
/// </summary>
public class RegisterArgs
{
    public string account;
    public string password;
}






/// <summary>
/// 显示主城3D
/// </summary>
public class Show3DSceneArgs
{
    public bool showAvatarScene = false;
    public bool showMainCity = false;
    public bool showBigMap = false;
}

/// <summary>
/// 大地图Card消息参数
/// </summary>
public class MapCardArgs
{
    public int mapID;
}
/// <summary>
/// ItemCard消息参数
/// </summary>
//public class ItemCardArgs
//{
//    public int itemID;
//    public Vector3 cardSize = new Vector3(1, 1, 1);
//    public Action<ItemCard> clickFunc;
//    //public Action<ItemCard> cancelClickFunc;
//    public bool showNum = false;
//    public bool multiSelect = false;//多选
//    //public bool repaetClick = false;//取消点击
//    public bool ableToClick = true;
//    public bool selected = false;
//    public int num;
//    //public int index = 0; //用于循环列表
//}
/// <summary>
/// chapter信息页面消息参数
/// </summary>
public class OpenChapterInfoEnforceWindowArgs
{
    public int chapterID;
}
/// <summary>
/// 消息参数
/// </summary>
public class CardRowArgs
{
    public int maxNum;
    public UIWidgets widgetType;
    public Vector3 rowSize;
    public Vector3 cardSize;
    public List<System.Object> initArgList;
}
/// <summary>
/// 消息参数
/// </summary>
//public class ItemRowArgs
//{
//    public int maxNum;
//    public UIWidgets widgetType;
//    public Vector3 rowSize = Vector3.zero;
//    public Vector3 cardSize;
//    public Action<int, ItemRow> reloadFunc;
//    public List<System.Object> initArgList;
//}


/// <summary>
///购买土地参数
/// </summary>
public class BuyLandArgs
{
    public int landId;
}


/// <summary>
///创建僵尸参数
/// </summary>
public class GetZombieInfoArgs
{
    public int id;
}

/// <summary>
/// 3D物体点击消息参数
/// </summary
public class Click3DObjectArg : EventArgs
{
    public GameObject selectedObject;
}