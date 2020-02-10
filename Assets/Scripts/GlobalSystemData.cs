using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Security.Cryptography;
/// <summary>
/// 全局数据
/// </summary>
public static class GlobalSystemData
{
    // clock timer
    public const int SECOND_TO_TICKS = 10000000;
    public const int MINUTES_TO_SECOND = 60;
    public const int HOUR_TO_SECOND = 60 * 60;
    public const int DAY_TO_SECOND = 60 * 60 * 24;

    //Channel
    public static string CHANNEL_NONE = "empty";

    public static class Net
    {
        public const int netPort = 12010;
    }
    //各资源目录
    public const string UI_WIDGET_DIR = "UIPrefabs/UIWidgets/";
    public const string PREFAB_3D_DIR = "3DPrefabs/";
    public const string MAP_DIR = "Maps/";
    public const string UI_WINDOW_DIR = "UIPrefabs/UIWindows/";
    public const string SCENE_PREFAB_DIR = "ScenePrefab/";
    public const string UI_ENFORCEWINDOW_DIR = "UIPrefabs/UIEnforceWindows/";
    public const string UI_DIR = "UI";
	public const string SKILL_DIR = "Skill";
    public const string AVATAR_PREFAB_DIR = "Avatars/";
	public const string CARD_EFFECT_MAT_DIR = "Materials/";
    public const string UI_EFFECT_DIR = "Effect/UI/";

    public static string TEMP_PATH = Application.persistentDataPath + "/Temp";
    public static string LANGUAGEDATA_FOLDER_PATH = Application.dataPath + "/Datas/ExcelData/LanguageDatas/";
    public static string GAMEDATA_FOLDER_PATH = Application.dataPath + "/Datas/ExcelData/GameDatas/";

    public static string TESTCASEDATA_FOLDER_PATH = Application.dataPath + "/Datas/TestCaseDatas/";
   
    public const string ASSET_FOLDER_PATH = "Assets/Datas/AssetData/";
    public const string XML_FOLDER_PATH = "Assets/Datas/XMLData/";
    public const string JSON_FOLDER_PATH = "Assets/Datas/JsonData/";

    // Save

    // Tag
    public static class Tags
    {
        public const string SKILL = "Skill";
        public const string PLAYER = "Player";
    }
    //时长常量
    public const float COMMON_NETWORK_TIME_OUT = 7f;
	public const float LONG_NETWORK_TIME_OUT = 30f;
	public const float NETWORK_LOGIN_TIME_OUT = 25f;

    //颜色
    public static Color GRAY_COLOR = new Color(0.5f, 0.5f, 0.5f);
    public static Color RED_COLOR = new Color(247f / 255f, 74f / 255f, 74f / 255f);
    public static Color WHITE_COLOR = new Color(1f, 1f, 1f);
    public static Color PURPLE_COLOR = new Color(0.67f, 0.41f, 0.8f);
    public static Color YELLOW_COLOR = new Color(156f / 255f, 156f / 255f, 126f / 255f);
    public static Color RED_HUN = new Color(148f / 255f, 0f / 255f, 0f / 255f);
    public static Color RED_HUN_HIGHTLIGHT = new Color(200f / 255f, 0f / 255f, 0f / 255f);
    public static Color YELLOW_SHANG = new Color(248f / 255f, 168f / 255f, 48f / 255f);
    public static Color YELLOW_SHANG_HIGHTLIGHT = new Color(248f / 255f, 205f / 255f, 141f / 255f);
    public static Color GREEN_LIGHT = new Color(108f / 255f, 280f / 255f, 24f / 255f);
    public static Color BLACK_COLOR = new Color(0, 0, 0);

    public static int UICameraDepth = 10;

}
/*
public class DebugSetting
{
    public static bool turnoffeffect = false;
    public static bool turnoffdamagenumber = false;
}

public enum SceneEventID
{
    NONE            = 0,
};//enum SceneEventID
*/
