using UnityEngine;

public class ConfigSettings : MonoSingleton<ConfigSettings>
{
    public  bool useSpriteBundle = false;
    public bool loadLocalAsset = true;
    public Platform platform = Platform.Editor;
    public Server server = Server.None;
    public bool showLog = true;
    
    public bool isCleanCache = false;

    ////�Ƿ����Prefs��������
    public bool isCleanPrefs = false;
    public GameLanguage curLanguage;
    public string curLanguageString;

    public bool useLocalData = false;
    // Start is called before the first frame update

    // Update is called once per frame
    public void InitSettings()
    {
        UIManager.Instance.useSpriteBundle = useSpriteBundle;
        AssetBundleManager.Instance.loadLocalAsset = loadLocalAsset;
        AssetBundleManager.Instance.platform = platform;
        LogManager.showLog = showLog;
    }
}

//游戏平台枚举
public enum Platform
{
    Editor,
    Android,
    IOS,
    WebGL
}

public enum Server
{
    None,
    DevelopeServer,
    TestServer,
    StableTestServer,
    StableDevelopeServer
}