using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace GameDataFrame.Editor
{
    /// <summary>
    /// 自动生成数据类代码页面
    /// </summary>
    public class CodeGeneratorWindow : EditorWindow
    {
        private List<bool> _checklist;
        private List<string> _dataNameList;
        private string[] _excelPathArray;
        private Vector2 _scrollPos;

        [MenuItem("DataTools/CodeGenerator")]
        private static void Init()
        {
            var window = (CodeGeneratorWindow) GetWindow(typeof(CodeGeneratorWindow));
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
            for (var i = 0; i < _dataNameList.Count; i++) _checklist.Add(false);
            //DataVersion version = DataVersion.LoadXMLEditor(DataUtility.AssetDataVersionPath);
            //      version.AddGameVersion();
            //if (version == null)
            //{
            //	Debug.LogError("fail to load dataversion.xml");
            //}
            //else
            //{
            //	versionstring = version.gameVersion;
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
            EditorGUILayout.BeginVertical(GUILayout.Width(250));
            GUILayout.Space(10);
            ShowDataAssetView();
            GUILayout.Space(5);
            if (GUILayout.Button("GenerateCode"))
            {
                GenerateCode();
                GUIUtility.ExitGUI();
            }

            EditorGUILayout.EndVertical();
            GUILayout.Space(25);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
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

        private void SetAll(bool boolvalue)
        {
            for (var i = 0; i < _checklist.Count; i++) _checklist[i] = boolvalue;
        }

        private void GenerateCode()
        {
            for (var i = 0; i < _checklist.Count; i++)
                if (_checklist[i])
                {
                    CodeGenerator.GenDataClass(_excelPathArray[i], _dataNameList[i]);
                    CodeGenerator.GenDataSetClass(_dataNameList[i] + "Set", _dataNameList[i]);
                    CodeGenerator.GenDataConvertorClass(_dataNameList[i]);
                }
        }
    }
}