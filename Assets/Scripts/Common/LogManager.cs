using UnityEngine;

//Log管理
public class LogManager
{
    //是否显示log
    public static bool showLog = true;

    public static void Log(string msg)
    {
        if (showLog) Debug.Log(msg);
    }

    public static void LogWarning(string msg)
    {
        if (showLog) Debug.LogWarning(msg);
    }

    public static void LogError(string msg)
    {
        if (showLog) Debug.LogError(msg);
    }
}