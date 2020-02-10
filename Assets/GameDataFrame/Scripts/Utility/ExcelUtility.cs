using System;
using System.Data;
using System.Data.Odbc;
using System.IO;
using UnityEngine;

namespace GameDataFrame
{
    /// <summary>
    /// Excel 加载辅助工具类
    /// </summary>
    public class ExcelUtility
    {
        public DataTable LoadExcel(string filePath, string sheetName)
        {
            if (!File.Exists(filePath))
            {
                Debug.LogError(filePath + " not exist!");
                return null;
            }

            var con = "Driver={Microsoft Excel Driver (*.xls)}; DriverId=790; Dbq=" + filePath + ";";
            var query = "SELECT * FROM [" + sheetName + "$]";

            var oCon = new OdbcConnection(con);
            var oCmd = new OdbcCommand(query, oCon);

            var sheetData = new DataTable("SheetData");

            oCon.Open();
            try
            {
                var rData = oCmd.ExecuteReader();
                sheetData.Load(rData);
                rData.Close();
            }
            catch (Exception ex)
            {
                Debug.LogError("query error");
            }

            oCon.Close();

            return sheetData;
        }

        public static object GetEnumCell(DataTable excelData, int row, int column, Type enumType)
        {
            try
            {
                return Enum.Parse(enumType, GetCell(excelData, row, column));
            }
            catch (Exception e)
            {
                Debug.LogError("convert enum cell error at [" + row + "," + column + "], value : " +
                               GetCell(excelData, row, column));
                throw null;
            }
        }

        public static bool GetBoolCell(DataTable excelData, int row, int column)
        {
            try
            {
                return Convert.ToBoolean(GetCell(excelData, row, column));
            }
            catch (Exception e)
            {
                Debug.LogError("convert bool cell error at [" + row + "," + column + "], value : " +
                               GetCell(excelData, row, column));
                throw null;
            }
        }

        public static float GetFloatCell(DataTable excelData, int row, int column)
        {
            try
            {
                return Convert.ToSingle(GetCell(excelData, row, column));
            }
            catch (Exception e)
            {
                Debug.LogError("convert float cell error at [" + row + "," + column + "], value : " +
                               GetCell(excelData, row, column));
                throw null;
            }
        }

        public static int GetIntCell(DataTable excelData, int row, int column, bool handleEmpty = false,
            int emptyValue = 0)
        {
            if (handleEmpty)
            {
                var str = GetCell(excelData, row, column);
                if (str.Equals(string.Empty))
                    return emptyValue;
                try
                {
                    return Convert.ToInt32(str);
                }
                catch (Exception e)
                {
                    Debug.LogError("convert int cell error at [" + row + "," + column + "], value : " +
                                   GetCell(excelData, row, column));
                    throw null;
                }
            }

            try
            {
                return Convert.ToInt32(GetCell(excelData, row, column));
            }
            catch (Exception e)
            {
                Debug.LogError("convert int cell error at [" + row + "," + column + "], value : " +
                               GetCell(excelData, row, column));
                throw null;
            }
        }

        public static string GetCell(DataTable excelData, int row, int column)
        {
            var cellValue = excelData.Rows[row][excelData.Columns[column].ColumnName].ToString().Trim();
            return cellValue;
        }
    }
}