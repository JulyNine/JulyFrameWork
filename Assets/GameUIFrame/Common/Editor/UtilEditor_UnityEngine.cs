using UnityEngine;
using UnityEditor;
using System.Text;


    public static partial class UtilEditor
    {
        public const int Priority_UIText = 2000;
        public const int Priority_UIRawImage = Priority_UIText - 6;
        public const int Priority_UIImage = Priority_UIText - 5;
        public const int Priority_UIImageBox = Priority_UIText - 4;
        public const int Priority_UIButton = Priority_UIText - 3;
        public const int Priority_UITagButton = Priority_UIText - 2;
        public const int Priority_UITagButtonGroup = Priority_UIText - 1;

        public static T ExtensionComponentWhenCreate<T>(string gameObejctName) where T : Component
        {
            GameObject go = new GameObject(gameObejctName);
            if (Selection.gameObjects.Length != 0)
            {
                GameObject parent = Selection.gameObjects[0];
                //设置父节点
                go.transform.SetParent(parent.transform, false);
                //设置和父节点一样的层级
                go.layer = parent.layer;
            }
            //设置选中
            Selection.activeObject = go;
            return go.AddComponent<T>();
        }
        public static T GetOrAddCompoment<T>(this GameObject gameObject) where T : Component
        {
            T component = gameObject.GetComponent<T>();
            if (component == null)
            {
                component = gameObject.AddComponent<T>();
            }
            return component;
        } 

//        public static string GetPath(Transform child, Transform parent)
//        {
//            string path = string.Empty;
//            if (child == null || parent == null || child.name == parent.name)
//                return path;
//            Transform tmp = child;
//            while (tmp != null)
//            {
//                if (tmp != parent)
//                {
//                    path = StringPool.Concat(tmp.name, "/", path);
//                    tmp = tmp.parent;
//                }
//                else
//                {
//                    break;
//                }
//            }
//            return path;
//        }
    }

