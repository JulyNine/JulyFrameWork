/**
*   Author：onelei
*   Copyright © 2019 - 2020 ONELEI. All Rights Reserved
*/
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


    [CustomEditor(typeof(UIButton), true)]
    public class UIButtonEditor : Editor
    {
        [MenuItem("GameObject/UI/UIButton", false, UtilEditor.Priority_UIButton)]
        public static UIButton AddComponent()
        {
            UIButton component = UtilEditor.ExtensionComponentWhenCreate<UIButton>(typeof(UIButton).Name.ToString());
            UIImage image = UtilEditor.GetOrAddCompoment<UIImage>(component.gameObject);
            component.targetGraphic = image;
            //设置默认值
            SetDefaultValue(component);
            return component;
        }

        UIButton component;
//        public override void OnInspectorGUI()
//        {
//            component = (UIButton)target;
//            base.OnInspectorGUI();
//            
//            component.mouseClickSound = EditorGUILayout.TextField("ClickSound", component.mouseClickSound);
//            if (!component.bInit)
//            {
//                component.bInit = true;
//                SetDefaultValue(component);
//            }
//        }

        private static void SetDefaultValue(UIButton component)
        {
            if (component == null)
                return;
            if (component.targetGraphic != null)
                component.targetGraphic.raycastTarget = true;
        } 
    }

