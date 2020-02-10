using com.ootii.Messages;
using System;
using UnityEngine;
using UnityEngine.UI;
public class WarningEnforceWindow : UIEnforceWindow {

    //public UIButton shutButton;
    public UIButton closeButton;
    public UIButton confirmButton;
    public Text message;
    protected Action confirmFunc;
    public UIButton cancelButton;
    //public UIButton registerButton;
    // public UIButton settingButton;


    public override void Init(object args)
    {
        base.Init(args);
        closeButton.Init();
        closeButton.onClick.AddListener(delegate ()
        {
            this.CloseButtonClick(this.gameObject);
        });
        confirmButton.Init();
        confirmButton.onClick.AddListener(delegate ()
        {
            this.ConfirmButtonClick(this.gameObject);
        });

        cancelButton.Init ( );
        cancelButton.onClick.AddListener (delegate ( )
        {
            this.CloseButtonClick (this.gameObject);
        });

        Set (args);
    }

    public override void Set(object args)
    {
        if (args == null)
            return;
        WarningWindowArgs argRecv = args as WarningWindowArgs;
        confirmFunc = argRecv.confirmFunc;

        InitUI (argRecv);
    }

    public void CloseButtonClick(GameObject go)
    {
        MessageDispatcher.SendMessage(GameEvent.CLOSE_ENFORCEWINDOW);
    }

    public void ConfirmButtonClick(GameObject go)
    {
        if (confirmFunc != null)
            confirmFunc();
    }

    private void InitUI(WarningWindowArgs argRecv)
    {
        message.text = LanguageManager.Instance.GetCommonLanguageString(argRecv.stringKey);
    }

}
