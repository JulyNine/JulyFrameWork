using System.Collections.Generic;
using System.IO;
using LitJson;
using UnityEngine;

namespace GameDataFrame
{
    /// <summary>
    /// 数据版本号控制类
    /// </summary>
    public class GameDataVersion
    {
        public readonly Dictionary<string, int> dataVersionDict = new Dictionary<string, int>();
        public int gameDataVersion;
        /// <summary>
        /// 加载数据版本信息
        /// </summary>
        /// <returns></returns>
        public static GameDataVersion Load()
        {
            var versionAsset = Resources.Load<TextAsset>("DataVersion");

            if (versionAsset == null)
            {
                var version = new GameDataVersion();
                var jsonStr = JsonMapper.ToJson(version);
                var fullPath = Path.Combine(Config.RESOURCE_FOLDER_PATH, "DataVersion.json");
                File.WriteAllText(fullPath, jsonStr);
                return version;
            }
            else
            {
                var content = versionAsset.text;
                var version = JsonMapper.ToObject<GameDataVersion>(content);
                Debug.Log("version:" + version.gameDataVersion);
                return version;
            }
        }

        /// <summary>
        /// 更新某数据的版本号
        /// </summary>
        /// <param name="dataName"></param>
        public void AddDataVersion(string dataName)
        {
            if (dataVersionDict.ContainsKey(dataName))
                dataVersionDict[dataName]++;
            else
                dataVersionDict.Add(dataName, 1);
        }
        /// <summary>
        /// 更新数据整体版本号
        /// </summary>
        public void UpdateGameDataVersion()
        {
            //this.gameDataVersion = 0;
            //foreach (var dataVersion in dataVersionDict)
            //{
            //    this.gameDataVersion += dataVersion.Value;
            //}
            gameDataVersion++;
            var jsonStr = JsonMapper.ToJson(this);
            var fullPath = Path.Combine(Config.RESOURCE_FOLDER_PATH, "DataVersion.json");
            File.WriteAllText(fullPath, jsonStr);
        }
    }
}