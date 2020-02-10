using System;
using UnityEditor;
using UnityEngine;

namespace GameDataFrame.Editor
{
    /// <summary>
    /// 数据打包Bundle类
    /// </summary>
    public class AssetBundleBuilder
    {
        public static void Build(string assetName, DataPlatform platform)
        {
            var target = GetBuildTarget(platform);
            var outputPath = Config.BUNDLE_FOLDER_PATH + Enum.GetName(typeof(DataPlatform), platform);

            var buildMap = new AssetBundleBuild[1];
            var resourcesAssets = new string[1]; //此资源包下面有多少文件
            resourcesAssets[0] = Config.ASSET_FOLDER_PATH + assetName + ".asset";
            buildMap[0].assetBundleName = assetName;
            buildMap[0].assetNames = resourcesAssets;
            BuildPipeline.BuildAssetBundles(outputPath, buildMap, BuildAssetBundleOptions.ForceRebuildAssetBundle, target);
            Debug.Log("wocao");
        }

        private static BuildTarget GetBuildTarget(DataPlatform platform)
        {
            BuildTarget target;
            switch (platform)
            {
                case DataPlatform.Android:
                    target = BuildTarget.Android;
                    break;
                case DataPlatform.IOS:
                    target = BuildTarget.iOS;
                    break;
                case DataPlatform.Editor:
                    target = BuildTarget.StandaloneWindows64;
                    break;
                case DataPlatform.WebGL:
                    target = BuildTarget.WebGL;
                    break;
                default:
                    target = BuildTarget.StandaloneWindows64;
                    break;
            }

            return target;
        }
    }
}