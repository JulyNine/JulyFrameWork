/**
*   Author：onelei
*   Copyright © 2019 - 2020 ONELEI. All Rights Reserved
*/

using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(UIImage), true)]
public class QImageEditor : UnityEditor.UI.ImageEditor
{
    [MenuItem("GameObject/UI/UIImage", false, UtilEditor.Priority_UIImage)]
    public static UIImage AddComponent()
    {
        UIImage component = UtilEditor.ExtensionComponentWhenCreate<UIImage>(typeof(UIImage).Name.ToString());
        //设置默认值
        SetDefaultValue(component);
        return component;
    }

    UIImage component;

    public override void OnInspectorGUI()
    {
        component = (UIImage) target;
        base.OnInspectorGUI();
        component.key = EditorGUILayout.TextField("KEY", component.key);
        if (!component.bInit)
        {
            component.bInit = true;
            SetDefaultValue(component);
        }
    }

    private static void SetDefaultValue(UIImage component)
    {
        if (component == null)
            return;
        component.raycastTarget = false;
    }
}