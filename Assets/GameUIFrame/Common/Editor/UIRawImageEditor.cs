/**
*   Author：onelei
*   Copyright © 2019 - 2020 ONELEI. All Rights Reserved
*/
using UnityEditor;
using UnityEngine;

namespace Lemon.UI
{
    [CustomEditor(typeof(UIRawImage), true)]
    public class QRawImageEditor : UnityEditor.UI.RawImageEditor
    {
        [MenuItem("GameObject/UI/UIRawImage", false, UtilEditor.Priority_UIRawImage)]
        public static UIRawImage AddComponent()
        { 
            UIRawImage component = UtilEditor.ExtensionComponentWhenCreate<UIRawImage>(typeof(UIRawImage).Name.ToString());
            //设置默认值
            SetDefaultValue(component);
            return component;
        }

        UIRawImage component;
        public override void OnInspectorGUI()
        {
            component = (UIRawImage)target;
            base.OnInspectorGUI();
            component.key = EditorGUILayout.TextField("KEY", component.key);
            if (!component.bInit)
            {
                component.bInit = true;
                SetDefaultValue(component);
            }
        }

        private static void SetDefaultValue(UIRawImage component)
        {
            if (component == null)
                return;
            component.raycastTarget = false;
        } 
    }
}
