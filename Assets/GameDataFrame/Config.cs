using UnityEngine;

namespace GameDataFrame
{
    // 游戏语言枚举
    public enum DataLanguage
    {
        Chinese,
        ChineseTraditional,
        English
    }

    // 游戏平台枚举
    public enum DataPlatform
    {
        Editor,
        Android,
        IOS,
        WebGL
    }

    /// <summary>
    /// config the path and parameter
    /// </summary>
    public static class Config
    {
        public static readonly string EXCEL_FOLDER_PATH = Application.dataPath + "/GameDataFrame/ExcelData/";
        public static readonly string DATACLASS_FOLDER_PATH = Application.dataPath + "/GameDataFrame/Scripts/CustomDataClass/";
        public static readonly string CONVERTOR_FOLDER_PATH = Application.dataPath + "/GameDataFrame/Editor/Custom/";
        public static readonly string RESOURCE_FOLDER_PATH = "Assets/GameDataFrame/Resources/";
        public static readonly string ASSET_FOLDER_PATH = RESOURCE_FOLDER_PATH + "AssetData/";
        public static readonly string JSON_FOLDER_PATH = RESOURCE_FOLDER_PATH + "JsonData/";
        public static readonly string BUNDLE_FOLDER_PATH = RESOURCE_FOLDER_PATH + "BundleData/";
        public const int NAME_ROW_INDEX = 1;
        public const int TYPE_ROW_INDEX = 2;
        public const int DATA_ROW_START_INDEX = 3;
        public const int ID_COL_INDEX = 0;
    }
}