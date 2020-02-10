using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using com.ootii.Messages;

public class NetworkStatusEnforceWindow : UIEnforceWindow
{
    //public UIButton shutButton;
    public UIButton closeButton;
    public UIButton confirmButton;

    protected Action confirmFunc;
    //  public Text message;
    //   protected Action confirmFunc;
    //  public UIButton cancelButton;
    //public UIButton registerButton;
    // public UIButton settingButton;


    public override void Init(object args)
    {
        base.Init(args);
        closeButton.Init();
        closeButton.onClick.AddListener(delegate() { this.CloseButtonClick(this.gameObject); });
        confirmButton.Init();
        confirmButton.onClick.AddListener(delegate() { this.ConfirmButtonClick(this.gameObject); });


        Set(args);
    }

    public override void Set(object args)
    {
        //if (args == null)
        //    return;
        if (args == null)
            return;
        WarningWindowArgs argRecv = args as WarningWindowArgs;
        confirmFunc = argRecv.confirmFunc;
    }

    public void CloseButtonClick(GameObject go)
    {
        if (confirmFunc != null)
            confirmFunc();
        UIManager.Instance.CloseEnforceWindow();
    }

    public void ConfirmButtonClick(GameObject go)
    {
        if (confirmFunc != null)
            confirmFunc();
        UIManager.Instance.CloseEnforceWindow();
    }
}