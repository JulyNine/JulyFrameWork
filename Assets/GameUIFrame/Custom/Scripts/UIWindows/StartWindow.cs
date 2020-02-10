using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using com.ootii.Messages;
using GameDataFrame;
using LitJson;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.U2D;



public class StartWindow : UIWindow
{
    public UIButton startButton;
    public UIButton continueButton;
    public UIButton allChapterButton;
    public UIButton settingsButton;

    public GameObject mainPanel;
    public GameObject loginPanel;

    public UIButton registerButton;
    public UIButton loginButton;
    public InputField nameInput;

    public override void Init(object args)
    {
        base.Init(args);
        startButton.Init(delegate() { this.StartButtonClick(this.gameObject); });
        continueButton.Init(delegate() { this.ContinueButtonClick(this.gameObject); });
        //allChapterButton.Init(delegate ()
        //{
        //    this.AllChapterButtonClick(this.gameObject);
        //});
        settingsButton.Init(delegate() { this.SettingsButtonClick(this.gameObject); });
        registerButton.Init(delegate() { this.RegisterButtonClick(this.gameObject); });
        loginButton.Init(delegate() { this.LoginButtonClick(this.gameObject); });
        Set(args);
    }

    public override void Set(object args)
    {
        //int videoID = (int)args;
        //PlayManager.Instance.PlayVideo(videoID);
        //GameMain.Instance.userId = 0;
        //iTween.MoveTo(backgroundImage.gameObject, iTween.Hash("x", 3000, "time", 300.0f, "islocal", true));

        //iTween.MoveTo(backgroundImage.gameObject, iTween.Hash("x", 3000, "easeType", "easeInOutExpo", "loopType", "loop", "delay", .1, "speed", 0.001));
    }

    void OnEnable()
    {
        // MessageDispatcher.AddListener(GameEvent.GetZombieInfoById_ACK, GetZombieInfoByIdAck);
    }

    void OnDisable()
    {
        //MessageDispatcher.RemoveListener(GameEvent.GetZombieInfoById_ACK, GetZombieInfoByIdAck);
    }

    void Update()
    {
    }

    public void StartButtonClick(GameObject go)
    {

        if(ConfigSettings.Instance.useLocalData)
            GameMain.Instance.LoadLocalData(true);
        
        // string jsonString = JsonUtility.ToJson(NetDataManager.Instance.chapterStatusDict);

        //        MessageDispatcher.SendMessage(GameEvent.SET_CHAPTER_REQ, jsonString);


        //UIManager.Instance.OpenUIWindow(UIWindows.ChapterWindow, 1);
        // return;

      //  int videoID = 1;
        // NetDataManager.Instance.chapterStatusDict[1] = 1;

        //SDKManager.Instance.Login();
      //  UIManager.Instance.OpenUIWindow(UIWindows.PlayVideoWindow, videoID);
        int videoID = NetDataManager.Instance.userData.lastVideoId;
        
        
        //UIManager.Instance.OpenUIWindow(UIWindows.PlayVideoWindow, videoID);
        
        // OpenPlayVideoWindowArgs args = new OpenPlayVideoWindowArgs();
        // args.videoID = videoID;
        // args.playType = PlayType.Common;
        // UIManager.Instance.OpenUIWindow(UIWindows.PlayVideoWindow, args);
    }

    public void ContinueButtonClick(GameObject go)
    {
        // if (index%2 == 1)
        // {
        //     string name = "Test";
        //     AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync(name);
        //     //AsyncOperationHandle<GameObject[]> handle = Addressables.LoadAssetsAsync<GameObject[]>(name);
        //     // AsyncOperationHandle<Sprite[]> handle = Addressables.LoadAssetAsync<Sprite[]>(atlasName);
        //     handle.Completed += (op) =>
        //     {
        //         if (op.Status == AsyncOperationStatus.Succeeded)
        //         {   
        //             res2 = handle.Result;
        //             //Addressables.InstantiateAsync(res);
        //             res2.transform.SetParent(UIManager.Instance.commonCanvasRoot.transform);
        //             res2.transform.position = Vector3.zero;
        //             Debug.Log("res:" + res2.name);
        //         }
        //     };   
        // }
        // else
        // {
        //    // Destroy(res2);
        //     Addressables.ReleaseInstance(res2);
        // }
        //
        // index++;
        //
        // return;
        
        UIManager.Instance.OpenUIWindow(UIWindows.ChapterListWindow);
        /*
        
        if(ConfigSettings.Instance.useLocalData)
            GameMain.Instance.LoadLocalData(false);
        MessageDispatcher.SendMessage(GameEvent.GET_VIDEO_REQ);

        
        int lastVideoID = NetDataManager.Instance.lastVideoID;
        lastVideoID = 1;
        if (lastVideoID == 0)
            return;
        // UIManager.Instance.OpenUIWindow(UIWindows.PlayVideoWindow, lastVideoID);

        VideoData videoData = GameDataCache.Instance.videoDataSet.GetVideoDataByID(lastVideoID);
        if (videoData != null)
        {
            int chapterID = videoData.chapterID;
            UIManager.Instance.OpenUIWindow(UIWindows.ChapterWindow, chapterID);
        }
*/

        //UIManager.Instance.OpenUIWindow(UIWindows.PlayVideoWindow, videoID);

        //VideoData videoData = GameDataCache.Instance.videoDataSet.GetVideoDataByID(lastVideoID);
        //VideoDataSet videoDataSet = new VideoDataSet();
        //videoDataSet = videoDataSet.Load();
        //videoData = videoDataSet.GetVideoDataByID(videoID);

        //int chapterID = 1;
        // UIManager.Instance.OpenUIWindow(UIWindows.ChapterWindow, chapterID);
    }

    public void AllChapterButtonClick(GameObject go)
    {
    }

    private GameObject res;
    private int index2 = 1;
    public void SettingsButtonClick(GameObject go)
    {
     //   string name = "QTEPrefabs";
     // if (index2 % 2 == 1)
     // {
     //     string name = "Action0-1";
     //     AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync(name);
     //     //AsyncOperationHandle<GameObject[]> handle = Addressables.LoadAssetsAsync<GameObject[]>(name);
     //     // AsyncOperationHandle<Sprite[]> handle = Addressables.LoadAssetAsync<Sprite[]>(atlasName);
     //     handle.Completed += (op) =>
     //     {
     //         if (op.Status == AsyncOperationStatus.Succeeded)
     //         {   
     //             res = handle.Result;
     //             //Addressables.InstantiateAsync(res);
     //             res.transform.SetParent(UIManager.Instance.commonCanvasRoot.transform);
     //             res.transform.position = Vector3.zero;
     //             Debug.Log("res:" + res.name);
     //         }
     //     };   
     // }
     // else
     // {
     //    // Addressables.Release(res);
     //     Addressables.ReleaseInstance(res);
     // }
     //
     // index2++;



     //SDKManager.Instance.GetWifiData();
    }
    private GameObject res2;
    private int index = 1;
    private SpriteAtlas atlas;
    public void RegisterButtonClick(GameObject go)
    {
        if (index % 2 == 1)
        {
            AsyncOperationHandle<SpriteAtlas> handle = Addressables.LoadAssetAsync<SpriteAtlas>("QTE");
            // AsyncOperationHandle<Sprite[]> handle = Addressables.LoadAssetAsync<Sprite[]>(atlasName);
            handle.Completed += (op) =>
            {
                if (op.Status == AsyncOperationStatus.Succeeded)
                {
                    atlas = handle.Result;

                    startButton.image.sprite = atlas.GetSprite("ui_button_001");
                    //  if (!_cachedAtlasDict.ContainsKey(atlasName))
                    //     _cachedAtlasDict.Add(atlasName, handle.Result);
                    // image.sprite = _cachedAtlasDict[atlasName].GetSprite(imageName);
                }
            };
        }
        else
        {
            Addressables.Release(atlas);
        }

        index++;
        return;
        
        
        
        
        
        
        
        // Debug.Log("Try Register! " + nameInput.text);
        // RegisterArgs args = new RegisterArgs();
        // args.account = nameInput.text;
        // NetworkManager.RegisterReq(args, RegisterAck);
    }


    public void RegisterAck(string resJsonString, IMessage sendMessage)
    {
        Debug.Log("resJsonString" + resJsonString);
        if (resJsonString.Trim() == "Success")
        {
            GameMain.Instance.accountName = nameInput.text;
            mainPanel.SetActive(true);
            loginPanel.SetActive(false);
        }
        else
        {
            UIManager.Instance.ShowStaticTipsWithAnim("昵称已存在");
        }
    }


    public void LoginButtonClick(GameObject go)
    {
        
        
        Debug.Log("Try Login! " + nameInput.text);

        if (string.IsNullOrEmpty(nameInput.text))
            return;
        GameMain.Instance.accountName = nameInput.text;
        mainPanel.SetActive(true);
        loginPanel.SetActive(false);
        
        //SDKManager.Instance.Login();
        LoginArgs args = new LoginArgs();
        args.sdkId = nameInput.text;
        var lMessage = new Message();
        lMessage.Type = GameEvent.LOGIN_REQ;
        lMessage.Sender = this;
        lMessage.Data = args;
        lMessage.Delay = EnumMessageDelay.IMMEDIATE;
        MessageDispatcher.SendMessage(lMessage);
        NetDataManager.Instance.GetServerDatas();
        // args.account = nameInput.text;
        // NetworkManager.LoginReq(args, LoginAck);
    }

    public void LoginAck(string resJsonString, IMessage sendMessage)
    {
        Debug.Log("resJsonString" + resJsonString);
        if (resJsonString.Trim() == "Success")
        {
            GameMain.Instance.accountName = nameInput.text;
            mainPanel.SetActive(true);
            loginPanel.SetActive(false);
        }
        else
        {
            UIManager.Instance.ShowStaticTipsWithAnim("请先注册");
        }
    }
}