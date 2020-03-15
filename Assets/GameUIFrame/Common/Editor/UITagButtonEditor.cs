/**
*   Author：onelei
*   Copyright © 2019 - 2020 ONELEI. All Rights Reserved
*/

using UnityEditor;
using UnityEngine.UI;
using UnityEngine;

[CustomEditor(typeof(UITagButton), true)]
public class UITagButtonEditor : UIButtonEditor
{
    [MenuItem("GameObject/UI/UITagButton", false, UtilEditor.Priority_UITagButton)]
    public static new UITagButton AddComponent()
    {
//            QImageBox image = QImageBoxEditor.AddComponent();
//            image.raycastTarget = true;
//
//            QToggleButton component = Util.GetOrAddCompoment<QToggleButton>(image.CacheGameObject);
//            component.name = typeof(QToggleButton).Name.ToString();
        //设置默认值
        UITagButton component =
            UtilEditor.ExtensionComponentWhenCreate<UITagButton>(typeof(UITagButton).Name.ToString());
        SetDefaultValue(component);
        return component;
    }

    UITagButton component;

    public override void OnInspectorGUI()
    {
        component = (UITagButton) target;
//            component.Normal = (GameObject)EditorGUILayout.ObjectField("Normal", component.Normal, typeof(GameObject), true);
//            component.Choose = (GameObject)EditorGUILayout.ObjectField("Choose", component.Choose, typeof(GameObject), true);
//
//            GUILayout.BeginHorizontal();
//            if (GUILayout.Button("Normal"))
//            {
//                component.SetToggleEditor();
//            }
//            if (GUILayout.Button("Choose"))
//            {
//                component.SetToggleEditor(true);
//            }
//            GUILayout.EndHorizontal();

        //base.OnInspectorGUI();
        if (!component.bInit)
        {
            component.bInit = true;
            SetDefaultValue(component);
        }
    }

    private static void SetDefaultValue(UITagButton component)
    {
        if (component == null)
            return;
        if (component.targetGraphic != null)
            component.targetGraphic.raycastTarget = true;
        //component.SetToggleEditor();
    }
}