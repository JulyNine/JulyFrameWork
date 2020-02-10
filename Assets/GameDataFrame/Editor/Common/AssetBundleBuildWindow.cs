using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.AddressableAssets.Build;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
namespace GameDataFrame.Editor
{
    /// <summary>
    /// 数据打包Bundle 界面
    /// </summary>
    public class AssetBundleBuildWindow : EditorWindow
    {
        private List<bool> checklist;
        private List<string> dataNameList;
        private string[] dataPathArray;
        private List<bool> platformChecklist;

        private DataPlatform[] platforms;
        private Vector2 scrollPos;
        private string versionStr;

        [MenuItem("DataTools/BuildAssetBundle")]
        private static void Init()
        {
            var window = (AssetBundleBuildWindow) GetWindow(typeof(AssetBundleBuildWindow));
            window.minSize = new Vector2(300, 600);
            window.maxSize = new Vector2(301, 600);
            window.Show(true);
        }

        private void OnEnable()
        {
            InitParams();
        }

        // Use this for initialization
        private void OnGUI()
        {
            DrawBody();
        }

        private void DrawBody()
        {
            EditorGUILayout.BeginVertical();
            GUILayout.Box("AssetBundle Build Tool", GUILayout.ExpandWidth(true));
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(25);
            EditorGUILayout.BeginVertical(GUILayout.Width(250));
            GUILayout.Space(10);
            ShowDataAssetView();
            GUILayout.Space(10);
            ShowPlatformView();
            GUILayout.Space(10);
            if (GUILayout.Button("Build"))
            {
                Build();
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
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(320));
            if (checklist != null)
                for (var i = 0; i < checklist.Count; i++)
                    checklist[i] = EditorGUILayout.Toggle(dataNameList[i], checklist[i]);

            EditorGUILayout.EndScrollView();
        }

        private void ShowPlatformView()
        {
            GUILayout.Box("Select Target Platform", GUILayout.ExpandWidth(true));
            if (platformChecklist != null)
                for (var i = 0; i < platformChecklist.Count; i++)
                    platformChecklist[i] =
                        EditorGUILayout.Toggle(Enum.GetName(typeof(DataPlatform), i), platformChecklist[i]);
        }

        private void InitParams()
        {
            dataNameList = new List<string>();
            GetDataNames();
            checklist = new List<bool>();
            platformChecklist = new List<bool>();
            platforms = (DataPlatform[]) Enum.GetValues(typeof(DataPlatform));
            for (var i = 0; i < platforms.Length; i++) platformChecklist.Add(false);
            for (var i = 0; i < dataNameList.Count; i++) checklist.Add(false);
        }

        private void GetDataNames()
        {
            dataPathArray = Directory.GetFiles(Config.ASSET_FOLDER_PATH, "*.asset");
            foreach (var dataName in dataPathArray) dataNameList.Add(GetDataName(dataName));
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

        private void SetAll(bool boolvalue)
        {
            for (var i = 0; i < checklist.Count; i++) checklist[i] = boolvalue;
        }

        private void Build()
        {
            BuildScript.buildCompleted = BuildComplete;
            AddressableAssetSettings.BuildPlayerContent();
            //AddressableAssetSettings.BuildPlayerContent();
            return;
            
            Debug.Log("Build Start");
            for (var i = 0; i < checklist.Count; i++)
                if (checklist[i])
                    for (var j = 0; j < platformChecklist.Count; j++)
                        if (platformChecklist[j])
                            AssetBundleBuilder.Build(dataNameList[i], platforms[j]);
        }


        private void BuildComplete(AddressableAssetBuildResult result)
        {
            Debug.Log("AssetBundle Build Complete: " + result.Error);
        }
        
    }
}