/**
*   Author：onelei
*   Copyright © 2019 - 2020 ONELEI. All Rights Reserved
*/
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


    [CustomEditor(typeof(UIButton), true)]
    public class UIButtonEditor : UnityEditor.UI.ButtonEditor
    {
        [MenuItem("GameObject/UI/UIButton", false, UtilEditor.Priority_UIButton)]
        public static UIButton AddComponent()
        {
            UIButton component = UtilEditor.ExtensionComponentWhenCreate<UIButton>(typeof(UIButton).Name.ToString());
            UtilEditor.GetOrAddCompoment<UIImage>(component.gameObject);
            //设置默认值
            SetDefaultValue(component);
            return component;
        }

        UIButton component;
        public override void OnInspectorGUI()
        {
            component = (UIButton)target;
            base.OnInspectorGUI();
            if (!component.bInit)
            {
                component.bInit = true;
                SetDefaultValue(component);
            }
        }

        private static void SetDefaultValue(UIButton component)
        {
            if (component == null)
                return;
            if (component.targetGraphic != null)
                component.targetGraphic.raycastTarget = true;
        } 
    }

