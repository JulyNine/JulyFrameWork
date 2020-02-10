using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace GameDataFrame
{
    /// <summary>
    /// 用于自动生成游戏中数据类，数据库类和数据转换器
    /// </summary>
    public class CodeGenerator
    {
        private static readonly string DataClassHeader = @"using UnityEngine;
using System;
using System.Collections.Generic;

using GameDataFrame;

namespace GameDataFrame
{
    [System.Serializable]
    public class ";


        private static readonly string DataSetClassFormat = @"using UnityEngine;
using System;
using System.Collections.Generic;
using GameDataFrame;
using LitJson;
namespace GameDataFrame
{{
    public class {0} : CallbackScriptableObject
    {{
        
        public List<{1}> {1}List;
        public Dictionary<int, {1}> {1}Dict;

        public {0} Load()
        {{
            {0} dataSet = null;
            if (GameDataManager.Instance.dataType == DataType.Json)
            {{
                string content = GameDataManager.Instance.GetDataSet<string>(""{0}"");
                {1}List = JsonMapper.ToObject<List<{1}>>(content);
                dataSet = this;
             }}
            else if(GameDataManager.Instance.dataType == DataType.UnityAsset)
            {{
                dataSet = GameDataManager.Instance.GetDataSet<{0}>(""{0}"");
             }}
            dataSet.OnLoadFinished();
            return dataSet;
         }}




        public override void OnLoadFinished()
        {{
            {1}Dict = new Dictionary<int, {1}>();
            foreach ({1} data in {1}List)
            {{
                {1}Dict[data.id] = data;
            }}
        }}
        public override void Release()
        {{
            {1}List.Clear();
            ClearDictionary({1}Dict);
        }}

        public {1} Get{1}ByID(int ID)
        {{
            if ({1}Dict.ContainsKey(ID))
                return {1}Dict[ID];
            else
            {{
                return null;
            }}
        }}
    }}
}}";


        private static readonly string DataConvertorClassFormat = @"using UnityEngine;
using System;
using System.Collections.Generic;
using LitJson;
using GameDataFrame;
namespace GameDataFrame
{{
    public class {0}Convertor : DataConvertor
    {{
        public void ConvertToUnityAsset()
        {{
            string assetFileName =  ""{0}Set.asset"";
            string excelPath = Config.EXCEL_FOLDER_PATH + ""/{0}.xlsx"";
            {0}Set  assetData = ScriptableObject.CreateInstance<{0}Set>();
            ExcelLoader excelLoader = new ExcelLoader(excelPath);
            assetData.{0}List = CreateDataList<{0}>(excelLoader);
            CreateAsset(assetData, assetFileName);
        }}
        public void ConvertToJson()
        {{
            string excelPath = Config.EXCEL_FOLDER_PATH + ""/{0}.xlsx"";
            {0}Set  assetData = ScriptableObject.CreateInstance<{0}Set>();
            ExcelLoader excelLoader = new ExcelLoader(excelPath);
            assetData.{0}List = CreateDataList<{0}>(excelLoader);
            string jsonString = JsonMapper.ToJson(assetData.{0}List);
            WriteFile(jsonString, ""{0}Set.json"");
        }}
    }}
}}";

        public static void GenDataClass(string excelPath, string calssName)
        {
            var excelLoader = new ExcelLoader(excelPath);
            var paramTypeDict = excelLoader.ParseOneRow(Config.TYPE_ROW_INDEX);
            var paramNameDict = excelLoader.ParseOneRow(Config.NAME_ROW_INDEX);
            GenDataClass(calssName, paramTypeDict, paramNameDict);
        }


        private static void GenDataClass(string dataClassName, Dictionary<int, string> paramTypeDict,
            Dictionary<int, string> paramNameDict)
        {
            if (paramTypeDict.Count != paramNameDict.Count)
            {
                Debug.LogError("Error, data num for ParamName row and ParamType row is different! ");
                return;
            }

            var sb = new StringBuilder();
            sb.Append(DataClassHeader);
            sb.Append(dataClassName);
            sb.Append("\n");
            sb.Append("\t{");
            sb.Append("\n");
            var itor = paramTypeDict.GetEnumerator();
            while (itor.MoveNext())
            {
                if (!paramNameDict.ContainsKey(itor.Current.Key))
                {
                    Debug.LogError("Error,  ParamName row does not have key：" + itor.Current.Key);
                    return;
                }

                sb.Append("\t\t");
                sb.Append("public ");
                sb.Append(paramTypeDict[itor.Current.Key]);
                sb.Append(" ");
                sb.Append(paramNameDict[itor.Current.Key]);
                sb.Append(";\n");
            }

            sb.Append("\t}");
            sb.Append("\n");
            sb.Append("}");
            sb.Append("\n");
            var fullPath = Path.Combine(Config.DATACLASS_FOLDER_PATH, dataClassName + ".cs");
            WriteFile(sb, fullPath);
        }


        public static void GenDataSetClass(string dataSetClassName, string dataClassName)
        {
            var content = string.Format(DataSetClassFormat, dataSetClassName, dataClassName);
            var fullPath = Path.Combine(Config.DATACLASS_FOLDER_PATH, dataSetClassName + ".cs");
            WriteFile(content, fullPath);
        }

        public static void GenDataConvertorClass(string dataClassName)
        {
            var content = string.Format(DataConvertorClassFormat, dataClassName);
            var fullPath = Path.Combine(Config.CONVERTOR_FOLDER_PATH, dataClassName + "Convertor.cs");
            WriteFile(content, fullPath);
        }


        private static void WriteFile(StringBuilder sb, string path)
        {
            WriteFile(sb.ToString(), path);
        }

        private static void WriteFile(string content, string path)
        {
            File.WriteAllText(path, content);
            Debug.Log("Generate  class: " + path);
        }
    }
}