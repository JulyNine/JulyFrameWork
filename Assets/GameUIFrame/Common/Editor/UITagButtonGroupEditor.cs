/**
*   Author：onelei
*   Copyright © 2019 - 2020 ONELEI. All Rights Reserved
*/
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(UITagButtonGroup))]
    public class UITagButtonGroupEditor : Editor
    {
        [MenuItem("GameObject/UI/UITagButtonGroup", false, UtilEditor.Priority_UITagButtonGroup)]
        public static UITagButtonGroup AddComponent()
        {
            UITagButtonGroup component = UtilEditor.ExtensionComponentWhenCreate<UITagButtonGroup>(typeof(UITagButtonGroup).Name.ToString());
//            component.list.Clear();
//
//            for (int i = 0; i < 2; i++)
//            {
//                Selection.activeObject = component;
//                QToggleButton button = QToggleButtonEditor.AddComponent();
//                component.list.Add(button);
//            }
//            Selection.activeObject = component;
            return component;
        }

        UITagButtonGroup component;
        int index = 0; 
        public override void OnInspectorGUI()
        { 
            component = (UITagButtonGroup)target;
//            GUILayout.BeginHorizontal();
//            index = EditorGUILayout.IntField("index", index);
//            if (GUILayout.Button("Choose"))
//            {
//                component.SetToggleGroupEditor(index);
//            }
//            GUILayout.EndHorizontal();

            base.OnInspectorGUI();
        }
    }

 