using UnityEditor;
using UnityEngine;

    [CustomEditor(typeof(UIText), true)]
    [CanEditMultipleObjects]
    public class UITextEditor : UnityEditor.UI.TextEditor
    {

        [MenuItem("GameObject/UI/UIText", false, UtilEditor.Priority_UIText)]
        public static UIText CreateComponent()
        {
            UIText component = UtilEditor.ExtensionComponentWhenCreate<UIText>(typeof(UIText).Name.ToString());

            //设置默认值
            SetDefaultValue(component);
            return component;
        }
        
        
        private static void SetDefaultValue(UIText component)
        {
            if (component == null)
                return;
            component.font = DefaultFont;
            component.supportRichText = false;
            component.raycastTarget = false;
            component.alignment = TextAnchor.MiddleCenter;
            component.horizontalOverflow = HorizontalWrapMode.Overflow;
            component.color = Color.black;
            component.fontSize = 18;
            component.text = "UIText";
        }

        UIText component;
        public override void OnInspectorGUI()
        {
            component = (UIText)target;
            base.OnInspectorGUI();
            component.key = EditorGUILayout.TextField("KEY", component.key);
            if (!component.bInit)
            {
                component.bInit = true;
                SetDefaultValue(component);
            }
        }
        

        private static Font font;
        public static Font DefaultFont
        {
            get
            {
                if (font == null)
                {
                    font = Resources.Load<Font>("Default");
                }
                return font;
            }
        }
    }
