using System;
using System.Collections.Generic;
using System.IO;
using GameDataFrame;
using UnityEditor;
using UnityEngine;
 /// <summary>
 /// 数据转换页面
 /// </summary>
public class DataConvertorWindow : EditorWindow
{
    private List<bool> _checklist;
    private List<bool> _convertTypeCheckList;
    private readonly List<string> _convertTypeList = new List<string> {"ConvertToUnityAsset", "ConvertToJson"};
    private List<string> _dataNameList;
    private string[] _excelPathArray;
    private string _message;
    private Vector2 _scrollPos;
    private DataLanguage _selectedLanguage;
    private GameDataVersion _version;
    private string _versionStr;

    [MenuItem("DataTools/GameDataConvertor")]
    private static void Init()
    {
        var window = (DataConvertorWindow) GetWindow(typeof(DataConvertorWindow));
        window.minSize = new Vector2(300, 600);
        window.maxSize = new Vector2(301, 600);
        window.Show(true);
    }

    private void OnEnable()
    {
        InitParams();
    }

    private void InitParams()
    {
        _dataNameList = new List<string>();
        GetDataNames();
        _checklist = new List<bool>();
        _convertTypeCheckList = new List<bool>();
        for (var i = 0; i < _dataNameList.Count; i++) _checklist.Add(false);
        for (var i = 0; i < _convertTypeList.Count; i++) _convertTypeCheckList.Add(false);
        _version = GameDataVersion.Load();

        //TextAsset versionAsset = Resources.Load<TextAsset>("DataVersion");
        //string JsonStr;
        //string fullPath;

        //if (versionAsset == null)
        //{
        //    version = new GameDataVersion();
        //    version.gameDataVersion = 1;
        //    JsonStr = JsonMapper.ToJson(version);
        //    fullPath = Path.Combine(Config.RESOURCE_FOLDER_PATH, "DataVersion.json");
        //    File.WriteAllText(fullPath, JsonStr);
        //}
        //else
        //{
        //    string content = versionAsset.text;
        //    version = JsonMapper.ToObject<GameDataVersion>(content);
        //    Debug.Log("version:" + version.gameDataVersion);
        //}
    }

    private void GetDataNames()
    {
        _excelPathArray = Directory.GetFiles(Config.EXCEL_FOLDER_PATH, "*.xlsx");
        foreach (var dataName in _excelPathArray) _dataNameList.Add(GetDataName(dataName));
    }

    private string GetDataName(string excelPath)
    {
        var tmpArray = excelPath.Split('/');
        if (tmpArray.Length == 0)
        {
            Debug.LogError("Path Error!");
            return null;
        }

        return tmpArray[tmpArray.Length - 1].Split('.')[0];
    }

    // Use this for initialization
    private void OnGUI()
    {
        DrawBody();
    }

    private void DrawBody()
    {
        EditorGUILayout.BeginVertical();
        GUILayout.Box("Data Convert Tool", GUILayout.ExpandWidth(true));
        EditorGUILayout.BeginHorizontal(GUILayout.Height(900));
        GUILayout.Space(25);
        EditorGUILayout.BeginVertical(GUILayout.Width(200));
        GUILayout.Space(10);
        ShowLanguageView();
        GUILayout.Space(10);
        ShowVersionView();
        GUILayout.Space(10);
        ShowDataAssetView();
        ShowTargetTypeView();
        if (GUILayout.Button("Convert"))
        {
            Convert();
            //version.gameDataVersion++;
            //string JsonStr = JsonMapper.ToJson(version);
            //string fullPath = Path.Combine(Config.RESOURCE_FOLDER_PATH, "DataVersion.json");
            //File.WriteAllText(fullPath, JsonStr);
            _version.UpdateGameDataVersion();
            GUIUtility.ExitGUI();
        }

        EditorGUILayout.EndVertical();
        GUILayout.Space(25);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.EndVertical();
    }

    private void ShowLanguageView()
    {
        EditorGUILayout.BeginHorizontal();
        _selectedLanguage = (DataLanguage) EditorGUILayout.EnumPopup("GameLanguage : ", _selectedLanguage);
        EditorGUILayout.EndHorizontal();
    }

    private void ShowVersionView()
    {
        EditorGUILayout.BeginHorizontal();

        //string content = Resources.Load<TextAsset>("test").text;
        //Debug.Log("content" + content);

        //DataVersion.ParseXml(content);

        //versionStr = DataVersion.gameVersion;


        EditorGUILayout.LabelField("client ver:" + _version.gameDataVersion);
        //versionStr = EditorGUILayout.TextField("client ver:", version.gameDataVersion);


        //if (GUILayout.Button("Set"))
        //{


        //   // DataVersion.gameVersion = "1.0.0";
        //    //string content = Resources.Load<TextAsset>("test").text;
        //    //Debug.Log("content" + content);

        //    //DataVersion.ParseXml(content);


        //    // DataVersion.Load();
        //    //DataVersion.Save(Config.ASSET_FOLDER_PATH + "test.xml");

        //    return;

        //    //DataVersion version = DataVersion.LoadXMLEditor(DataUtility.AssetDataVersionPath);
        //    //if (version != null)
        //    //{
        //    //    version.gameVersion = versionStr;
        //    //    version.Save(DataUtility.AssetDataVersionPath);
        //    //}
        //}
        EditorGUILayout.EndHorizontal();
    }


    private void ShowDataAssetView()
    {
        GUILayout.Box("Select Data", GUILayout.ExpandWidth(true));
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("All")) SetAll(true);
        if (GUILayout.Button("None")) SetAll(false);
        EditorGUILayout.EndHorizontal();
        _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, GUILayout.Height(320));
        if (_checklist != null)
            for (var i = 0; i < _checklist.Count; i++)
                _checklist[i] = EditorGUILayout.Toggle(_dataNameList[i], _checklist[i]);

        EditorGUILayout.EndScrollView();
    }

    private void ShowTargetTypeView()
    {
        GUILayout.Box("Select Target Type", GUILayout.ExpandWidth(true));
        if (_convertTypeCheckList != null)
            for (var i = 0; i < _convertTypeCheckList.Count; i++)
                _convertTypeCheckList[i] = EditorGUILayout.Toggle(_convertTypeList[i], _convertTypeCheckList[i]);
    }

    private void SetAll(bool boolvalue)
    {
        for (var i = 0; i < _checklist.Count; i++) _checklist[i] = boolvalue;
    }

    private void Convert()
    {
        for (var i = 0; i < _checklist.Count; i++)
            if (_checklist[i])
                for (var j = 0; j < _convertTypeCheckList.Count; j++)
                    if (_convertTypeCheckList[j])
                    {
                        var type = Type.GetType("GameDataFrame." + _dataNameList[i] + "Convertor");
                        //  Type type = Type.GetType(dataNameList[i] + "Convertor");
                        var obj = Activator.CreateInstance(type); // 创建实例

                        var method = type.GetMethod(_convertTypeList[j], new Type[] { }); // 获取方法信息
                        object[] parameters = null;
                        method.Invoke(obj, parameters); // 调用方法，参数为空
                        _version.AddDataVersion(_dataNameList[i] + "Set");
                    }
    }
}