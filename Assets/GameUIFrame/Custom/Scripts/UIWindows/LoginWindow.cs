using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using com.ootii.Messages;
public class LoginWindow : UIWindow {

    public UIButton loginButton;
    public UIButton registerButton;
    public UIButton settingButton;

    public InputField accountFiled;
    public InputField passwordFiled;
    public Image backgroundImage1;
    public Image backgroundImage2;
    public float moveSpeed = 20;
    public Image zombieImage;
    public Text name;
    public Text dna;


    public override void Init(object args)
    {
        base.Init(args);
        loginButton.Init();
        registerButton.Init();
        settingButton.Init();
        loginButton.onClick.AddListener(delegate ()
        {
            this.LoginButtonClick(this.gameObject);
        });
        registerButton.onClick.AddListener(delegate ()
        {
            this.RegisterButtonClick(this.gameObject);
        });
        settingButton.onClick.AddListener(delegate ()
        {
            this.SettingButtonClick(this.gameObject);
        });
        //MessageDispatcher.AddListener("EVERYONE", OnEveryoneMessageReceived);
        Set(args);
    }

    public override void Set(object args)
    {
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

    public void LoginButtonClick(GameObject go)
    {
        // 调用网页上的MyFunction1并不使用参数。
        Application.ExternalCall("GetLandInfo", 2);

        Debug.Log(" LoginButtonClick");
        if (string.IsNullOrEmpty(accountFiled.text))
        {
            //Tools.ShowTips(LanguageManager.Instance.GetCommonLanguageString("EMPTY_ACCOUNT_INPUT"));
           // return;
        }
        if (string.IsNullOrEmpty(passwordFiled.text))
        {
           // Tools.ShowTips(LanguageManager.Instance.GetCommonLanguageString("EMPTY_PASSWORD_INPUT"));
            //return;
        }
        //Message lMessage = new Message();
        //lMessage.Type = GameEvent.CreateZombie_REQ;
        //lMessage.Sender = this;
        //CreateZombieArgs args = new CreateZombieArgs() {name = accountFiled.text, id = int.Parse(passwordFiled.text)};
        //lMessage.Data = args;
        //lMessage.Delay = EnumMessageDelay.IMMEDIATE;
        //MessageDispatcher.SendMessage(lMessage);


        //Message lMessage = new Message();
        //lMessage.Type = GameEvent.GET_USER_INFOS;
        //lMessage.Sender = this;
        //lMessage.Delay = EnumMessageDelay.IMMEDIATE;
        //MessageDispatcher.SendMessage(lMessage);
    }
    public void RegisterButtonClick(GameObject go)
    {
        if (string.IsNullOrEmpty(accountFiled.text))
        {
           // Tools.ShowTips(LanguageManager.Instance.GetCommonLanguageString("EMPTY_ACCOUNT_INPUT"));
          //  return;
        }
        if (string.IsNullOrEmpty(passwordFiled.text))
        {
           // Tools.ShowTips(LanguageManager.Instance.GetCommonLanguageString("EMPTY_PASSWORD_INPUT"));
           // return;
        }
        /*
        Message lMessage = new Message();
        lMessage.Type = GameEvent.REGISTER_REQ;
        lMessage.Sender = this;
        RegisterArgs args = new RegisterArgs() { account = accountFiled.text, password = passwordFiled.text };
        lMessage.Data = args;
        lMessage.Delay = EnumMessageDelay.IMMEDIATE;
        MessageDispatcher.SendMessage(lMessage);
        */
        // Message lMessage = new Message();
        // lMessage.Type = GameEvent.GetZombiesByOwner_REQ;
        // lMessage.Sender = this;
        // //CreateZombieArgs args = new CreateZombieArgs() { name = accountFiled.text, id = int.Parse(passwordFiled.text) };
        // //lMessage.Data = args;
        // lMessage.Delay = EnumMessageDelay.IMMEDIATE;
        // MessageDispatcher.SendMessage(lMessage);

    }
    public void SettingButtonClick(GameObject go)
    {
        //Message lMessage = new Message();
        //lMessage.Type = GameEvent.GetZombieInfoById_REQ;
        //lMessage.Sender = this;
        //GetZombieInfoArgs args = new GetZombieInfoArgs() { id = int.Parse(passwordFiled.text) };
        //lMessage.Data = args;
        //lMessage.Delay = EnumMessageDelay.IMMEDIATE;
        //MessageDispatcher.SendMessage(lMessage);

    }

    public void GetZombieInfoByIdAck(IMessage rMessage)
    {
        //rMessage.IsHandled = true;
        //Zombie zombie = rMessage.Data as Zombie;
        //name.text = zombie.name;
        //dna.text = "DNA:" + zombie.dna.ToString();
        //long index = zombie.dna/100 % 250 + 1;

        //zombieImage.sprite = Resources.Load<Sprite>("UI/hero/hero" + index);
        
    }

}


