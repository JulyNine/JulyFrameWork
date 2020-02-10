using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace GameDataFrame
{
    /// <summary>
    /// 数据转换器基类
    /// </summary>
    public class DataConvertor
    {
        protected static List<T> CreateDataList<T>(ExcelLoader loader) where T : new()
        {
            var list = new List<T>();

            T data;
            FieldInfo field;
            var type = typeof(T);

            loader.ParseParamNameRow();
            loader.ParseParamTypeRow();
            loader.ParseIdColumn();

            var rowItor = loader.idDict.GetEnumerator();
            while (rowItor.MoveNext())
            {
                data = new T();
                var colItor = loader.paramTypeDict.GetEnumerator();
                while (colItor.MoveNext())
                {
                    if (!loader.paramNameDict.ContainsKey(colItor.Current.Key))
                    {
                        Debug.LogError("Error,  ParamName row does not have key：" + colItor.Current.Key);
                        return null;
                    }

                    field = type.GetField(loader.paramNameDict[colItor.Current.Key],
                        BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
                    if (field != null)
                        field.SetValue(data,
                            loader.GetCellData(colItor.Current.Value, rowItor.Current.Key, colItor.Current.Key));
                }

                list.Add(data);
            }

            return list;
        }
        /// <summary>
        /// 创建Asset格式资源
        /// </summary>
        /// <param name="assetData"></param>
        /// <param name="assetFileName"></param>
        public static void CreateAsset(Object assetData, string assetFileName)
        {
            AssetDatabase.CreateAsset(assetData, Config.ASSET_FOLDER_PATH + assetFileName);
            Debug.Log("export asset success! :" + assetFileName);
        }
        /// <summary>
        /// 创建Json格式资源
        /// </summary>
        /// <param name="content"></param>
        /// <param name="fileName"></param>
        public static void WriteFile(string content, string fileName)
        {
            var fullPath = Path.Combine(Config.JSON_FOLDER_PATH, fileName);
            File.WriteAllText(fullPath, content);
            Debug.Log("Create Data File: " + fileName);
        }
    }
}