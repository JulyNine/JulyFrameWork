using com.ootii.Messages;
using UnityEngine;
using GameDataFrame;
using System;
using System.IO;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using com.ootii.Messages;
using UnityEngine.SceneManagement;

public class GameMain : MonoBehaviour
{
    ////��ǰ��Ϸ����
    //public GameLanguage curLanguage;

    // public string curLanguageString;
    //
    // ////�Ƿ���ʾlog
    // public bool showLog = false;
    // //public bool showStory = true;
    //
    // ////�Ƿ�ӱ��ؼ���bundle��Դ
    // public bool loadLocalAsset = true;
    //
    // public Platform assetDefautPlatform;

    ////�Ƿ��bundle�ȸ�����ͼ����Դ
    //public bool useSpriteBundle = true;

    ////��ǰ��Ϸ������
   // public Server server;

    ////�Ƿ���������
    //public bool networkConnected = false;
    ////�Ƿ��������
   // public bool isCleanCache = false;

    ////�Ƿ����Prefs��������
   // public bool isCleanPrefs = false;

    ////�˻�����
    [HideInInspector]
    public string accountName = "";
    [HideInInspector]
    public string accountId = "";
    ////�˻�ID
    //public int accountID = 12345;
    ////�Ƿ��ǹ���ԱȨ��
    //public bool isAdmin = false;

    private static bool isDestroy;

    ////��Ϸ�汾��
    //public string gameVer;
    ////��Դ�汾��
    //public string resourceVer;
    ///*
    //public string serverVer;
    //public int saveID = 0;

    //public bool isLoaded = false;
    //public bool isDownLoaded = false;
    //public bool isLogined = false;
    //*/
    //public float globalTimeScale = 0.5f;
    //public float effectTimeScale = 1f;
    //public Splash splash;
    //public GameObject objPool;
    //public int userId;
    private static GameMain s_Instance;

    // Note: this is set on Awake()
    public static GameMain Instance
    {
        get
        {
            if (s_Instance == null && !isDestroy)
            {
                s_Instance = ((GameObject) Instantiate(Resources.Load("GameItem/GameMain"))).GetComponent<GameMain>();
            }

            return s_Instance;
        }
    }


    void Awake()
    {
        //if (s_Instance != null)
        //{
        //    Destroy(gameObject);
        //    return;
        //}
        s_Instance = this;
        DontDestroyOnLoad(this);
        //splash.Init();
        InitGlobalManagers();
        GameGlobalSetting();
        //LoadLocalData();
        //game start 
        //ResourceManager.Instance.DownLoadResource();
        //Application.targetFrameRate = 30;  //限制帧率为30
    }

    /// <summary>
    /// ���ر�������
    /// </summary>
    //public void LoadLocalData()
    //{
    //    //ES3.Save<List<int>>("test",  new List<int>() { 0,1} );
    //    //Debug.Log("ssss" + ES3.FileExists(new ES3Settings()));
    //    if(isCleanCache)
    //        ES3.DeleteFile(new ES3Settings());
    //    //Debug.Log("ssss" + ES3.FileExists(new ES3Settings()));
    //    if(ES3.FileExists(new ES3Settings()))
    //       NetDataManager.Instance.LoadData();

    //    // List<int> test = ES3.Load<List<int>>("test");

    //    //NetDataManager.Instance.LoadData();

    //    //Debug.Log("test" + test[0]);
    //    //Debug.Log("test" + test[1]);
    //    // LocalSaveManager.LoadData();
    //}
    public void LoadLocalData(bool refresh)
    {
        if (refresh)
            ES3.DeleteFile(new ES3Settings());
        if (ES3.FileExists(new ES3Settings()))
            NetDataManager.Instance.LoadLocalData();
    }

    /// <summary>
    /// ������Ϸ����
    /// </summary>
    void GameGlobalSetting()
    {
        ConfigSettings.Instance.InitSettings();
        if (ConfigSettings.Instance.isCleanCache)
        {
            Caching.ClearCache();
        }
        if (ConfigSettings.Instance.isCleanPrefs)
        {
            PlayerPrefs.DeleteAll();
        }
        Application.runInBackground = true;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        System.GC.AddMemoryPressure(2097152);

        
        // Application.targetFrameRate = 60;
        // Screen.SetResolution(1920, 1080, true, 60);
    }

    void OnApplicationPause(bool paused)
    {
    }

    void OnApplicationQuit()
    {
    }

    void OnEnable()
    {
        // MessageDispatcher.AddListener(GameEvent.CHANGE_LANGUAGE, ChangeLanguage);
    }

    void OnDisable()
    {
        // MessageDispatcher.RemoveListener(GameEvent.CHANGE_LANGUAGE, ChangeLanguage);
    }

    // Use this for initialization
    void Start()
    {
        //UIManager.Instance.OpenUIWindow(UIWindows.ChapterWindow);


        //  UIManager.Instance.OpenUIWindow(UIWindows.ChapterWindow);
        //SceneManager.LoadSceneAsync("FMVMain");

        //userId = 1;

        //Message IMessage = new Message();
        //IMessage.Type = GameEvent.OPEN_UIWINDOW;
        //IMessage.Sender = this;
        //OpenUIWindowArgs args = new OpenUIWindowArgs() { windowKey = UIWindows.CityMenuWindow };
        //IMessage.Data = args;
        ////IMessage.Delay = enum
        //MessageDispatcher.SendMessage(IMessage);
    }

    void GameStart()
    {
        Debug.Log("Game Start");
        GameDataCache.Instance.Load();
        //UIManager.Instance.OpenUIWindow(UIWindows.ChapterWindow);
        UIManager.Instance.OpenUIWindow(UIWindows.StartWindow);
    }

    // Update is called once per frame
    void Update()
    {
        // if (LanguageManager.Instance.isLoaded && DataManager.Instance.isLoaded && BlockChain.Instance.initFinish)
        {
            // CityManager.Instance.GameInit();
            // LanguageManager.Instance.isLoaded = false;
            // DataManager.Instance.isLoaded = false;
            // BlockChain.Instance.initFinish = false;
        }
        //NativeSensor.Instance.GetSteps ();
        // System.Random r = new System.Random(3);
        // int num = r.Next(0, 100);
        // Debug.Log("num" + num.ToString());

        //Time.timeScale = globalTimeScale * effectTimeScale;
    }

    /// <summary>
    /// ��ʼ����������
    /// </summary>
    private void InitGlobalManagers()
    {
        GameDataManager.Instance.EventLoadDataFinish += GameStart;
        GameDataManager.Instance.Init();
        AtlasManager.Instance.Init();
        NetworkManager.Instance.Init();
        SDKManager.Instance.Init();
        AssetBundleManager.Instance.Init();
        //GameDataManager.Instance.LoadAllDataSet();
        //gameObject.AddComponent<HttpManager>();
        //MessageDispatcher.Init();
        //NetworkManager.Instance.Init();
        //UIManager.Instance.Init();
        //LanguageManager.Instance.Init();
        // DataManager.Instance.Init();
        //AtlasManager.Instance.Init();
        //AssetBundleManager.Instance.Init();
        //ResourceManager.Instance.Init();
        //LogManager.Init();
    }

    /// <summary>
    /// �л���Ϸ����
    /// </summary>
    /// <param name="rMessage"></param>
    public void ChangeLanguage(IMessage rMessage)
    {
        //rMessage.IsHandled = true;
        //GameMain.Instance.curLanguage = ((ChangeGameLanguageArgs)rMessage.Data).language;
        //GameMain.Instance.curLanguageString = GameMain.Instance.curLanguage.ToString().ToLower();
        //Clear();
        //ResourceManager.Instance.DownLoadResource();
    }


    public void OnDestroy()
    {
        isDestroy = true;
    }

    public void CleanUp()
    {
        //accountID = 0;
        /*
        isDownLoaded = false;
        isLoaded = false;
        isLogined = false;
        */
        Clear();
    }


    public void Clear()
    {
        // GlobalSystemData.GLOBAL_VARIABLE.Clear();
        //AssetBundleManager.Instance.Clear();
        //LanguageManager.Instance.Clear();
        //DataManager.Instance.Clear();
        //AtlasManager.Instance.Clear();
        //UIManager.Instance.Clear();
        Resources.UnloadUnusedAssets();
    }
}


public static class GameSettings
{
    public static QualityType defaultQuality = QualityType.Low;
    public static float defaultVolume = 0.5f;
    public static float defaultPlayRate = 1.0f;
    public static float defaultBrightness = 1.0f;
}