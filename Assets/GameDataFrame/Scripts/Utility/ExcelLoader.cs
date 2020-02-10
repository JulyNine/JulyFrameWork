using System.Collections.Generic;
using System.Data;
using System.IO;
using Excel;
using UnityEngine;

namespace GameDataFrame
{
    /// <summary>
    /// Excel加载解析类
    /// </summary>
    public class ExcelLoader
    {
        private DataTable _configData; // 从Excel中读取的数据
        private DataLanguage _language;
        private List<string> _paramNameList;


        private List<string> _paramTypeList;


        public ExcelLoader(string excelFileName)
        {
            _language = DataLanguage.Chinese;

            // 初始化
            //curLoadID = null;

            // 加载数据
            _configData = LoadExcel(excelFileName);
        }

        public ExcelLoader(string excelFileName, DataLanguage language)
        {
            _language = language;

            // 初始化
            //curLoadID = null;

            // 加载数据
            _configData = LoadExcel(excelFileName);
        }


        public Dictionary<int, string> paramTypeDict { get; private set; }
        public Dictionary<int, string> paramNameDict { get; private set; }
        public Dictionary<int, string> idDict { get; private set; }

        // 解析List数据
        private List<int> ParseListData(string strData)
        {
            var retList = new List<int>();
            if (strData == "null")
                return retList;
            var numList = strData.Split('|');
            foreach (var numStr in numList)
            {
                if (string.IsNullOrEmpty(numStr.Trim()))
                    continue;
                // Debug.Log(numStr);
                var num = int.Parse(numStr);
                retList.Add(num);
            }

            return retList;
        }

        // 解析List数据
        private List<string> ParseStringListData(string strData)
        {
            var retList = new List<string>();
            if (strData == "null")
                return retList;
            var strList = strData.Split('|');
            foreach (var str in strList)
            {
                if (string.IsNullOrEmpty(str.Trim()))
                    continue;
                retList.Add(str);
            }

            return retList;
        }

        // 加载Excel数据
        private DataTable LoadExcel(string excelFileName)
        {
            //Debug.Log("加载Excel数据 " + excelFileName + "，页名为 " + sheetName);
            // 读取Excel数据到DataTable中
            var stream = File.Open(excelFileName, FileMode.Open, FileAccess.Read);
            var excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

            var result = excelReader.AsDataSet();
            _configData = result.Tables[0];


            // 若Excel行数不符合规范，不解析
            if (_configData == null || _configData.Rows.Count <= 3)
            {
                Debug.LogError("Excel行数不符合规范或无数据，不解析。错误Excel文件是" + excelFileName);
                return null;
            }

            // 解析Excel的key和ID信息
            //ParseKeyAndIdInfo();

            return _configData;
        }

        public Dictionary<int, string> ParseOneRow(int rowIndex)
        {
            // description
            var dataDict = new Dictionary<int, string>();
            for (var colIndex = 0; colIndex < _configData.Columns.Count; ++colIndex)
            {
                var cell = ExcelUtility.GetCell(_configData, rowIndex, colIndex).Trim();
                if (string.IsNullOrEmpty(cell))
                    Debug.LogError("Warning! empty cell! at row:" + rowIndex + " col:" + colIndex);
                else
                    dataDict[colIndex] = cell;
            }

            return dataDict;
        }


        public Dictionary<int, string> ParseOneColumn(int colIndex)
        {
            // description
            string cell;
            var dataDict = new Dictionary<int, string>();
            for (var rowIndex = Config.DATA_ROW_START_INDEX; rowIndex < _configData.Rows.Count; ++rowIndex)
            {
                cell = ExcelUtility.GetCell(_configData, rowIndex, colIndex).Trim();
                if (string.IsNullOrEmpty(cell))
                    Debug.LogError("Warning! empty cell! at row:" + rowIndex + " col:" + colIndex);
                else
                    dataDict[rowIndex] = cell;
            }

            return dataDict;
        }

        public void ParseParamNameRow()
        {
            paramNameDict = new Dictionary<int, string>();
            paramNameDict = ParseOneRow(Config.NAME_ROW_INDEX);
            //paramNameList = ParseOneRow(Config.NAME_ROW_INDEX);
        }

        public void ParseParamTypeRow()
        {
            paramTypeDict = new Dictionary<int, string>();
            paramTypeDict = ParseOneRow(Config.TYPE_ROW_INDEX);
            //paramTypeList = ParseOneRow(Config.TYPE_ROW_INDEX);
        }


        public void ParseIdColumn()
        {
            idDict = new Dictionary<int, string>();
            idDict = ParseOneColumn(Config.ID_COL_INDEX);
            //paramTypeList = ParseOneRow(Config.TYPE_ROW_INDEX);
        }


        public object GetCellData(string dataType, int rowIndex, int colIndex)
        {
            // 获取数据类型信息
            //   int DATA_TYPE_ROW_INDEX = 3;
            // string dataType = ExcelUtility.GetCell(configData, DATA_TYPE_ROW_INDEX, colIndex);
            //Debug.Log("数据类型是：" + dataType);
            dataType = dataType.ToUpper();
            // 返回对象
            object ret = null;

            // 若是默认值
            var content = ExcelUtility.GetCell(_configData, rowIndex, colIndex);
            //if (string.IsNullOrEmpty(content.Trim()))
            //{
            //    switch (dataType)
            //    {
            //        case "INT":
            //            try
            //            {
            //                ret = int.Parse(dictDefaultVal2ColIndex[colIndex]);
            //            }
            //            catch
            //            {
            //                Debug.LogError("DataError: rowIndex: " + rowIndex + "colIndex:" + colIndex + content);
            //            }
            //            break;
            //        case "FLOAT":
            //            ret = float.Parse(dictDefaultVal2ColIndex[colIndex]);
            //            break;
            //        case "BOOL":
            //            ret = bool.Parse(dictDefaultVal2ColIndex[colIndex]);
            //            break;
            //        case "STRING":
            //            ret = dictDefaultVal2ColIndex[colIndex];
            //            break;
            //        case "STRINGKEY":
            //            string stringKey = dictDefaultVal2ColIndex[colIndex];
            //            ret = GetMultiLanguageText(stringKey);
            //            break;

            //        case "LIST":
            //            ret = ParseListData(dictDefaultVal2ColIndex[colIndex]);
            //            break;
            //        case "TEXT":
            //            //object languageID = dictDefaultVal2ColIndex[colIndex];
            //            //ret = GetMultiLanguageText(languageID);
            //            break;
            //        default:
            //            Debug.LogError("错误：未知的数据类型 " + dataType);
            //            break;
            //    }

            //    return ret;
            //}

            // 若是非默认值
            switch (dataType)
            {
                case "INT":
                    ret = ExcelUtility.GetIntCell(_configData, rowIndex, colIndex);
                    break;
                case "FLOAT":
                    ret = ExcelUtility.GetFloatCell(_configData, rowIndex, colIndex);
                    break;
                case "BOOL":
                    ret = ExcelUtility.GetBoolCell(_configData, rowIndex, colIndex);
                    break;
                case "STRING":
                    ret = ExcelUtility.GetCell(_configData, rowIndex, colIndex);
                    break;
                //case "STRINGKEY":
                //    string stringKey = ExcelUtility.GetCell(configData, rowIndex, colIndex);
                //    ret = GetMultiLanguageText(stringKey);
                //    break;
                case "LIST":
                    var data = ExcelUtility.GetCell(_configData, rowIndex, colIndex);
                    ret = ParseListData(data);
                    break;
                case "LIST<STRING>":
                    ret = ExcelUtility.GetCell(_configData, rowIndex, colIndex);
                    ret = ParseStringListData((string) ret);
                    break;
                case "LIST<INT>":
                    ret = ExcelUtility.GetCell(_configData, rowIndex, colIndex);
                    ret = ParseListData((string) ret);
                    break;
                case "TEXT":
                    //object languageID = ExcelUtility.GetCell(configData, rowIndex, colIndex);
                    //ret = GetMultiLanguageText(languageID);
                    break;
                default:
                    Debug.LogError("错误：未知的数据类型 " + dataType);
                    break;
            }

            return ret;
        }
    }
}