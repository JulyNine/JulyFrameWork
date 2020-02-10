using System;
using System.Collections.Generic;
using com.ootii.Messages;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// 互斥Button组
/// </summary>
public class UITagButtonGroup : MonoBehaviour
{
    private Action<UITagButton> _clickFunc;
    public int groupID;
    public List<UITagButton> tagButtonList = new List<UITagButton>();


    private void OnEnable()
    {
        MessageDispatcher.AddListener(GameEvent.TAGBUTTON_CLICK, OnTagButtonClickAck, true);
    }

    private void OnDisable()
    {
        MessageDispatcher.RemoveListener(GameEvent.TAGBUTTON_CLICK, OnTagButtonClickAck);
    }

    public void Init(Action<UITagButton> clickFunc)
    {
        _clickFunc = clickFunc;
        for (var i = 0; i < tagButtonList.Count; i++) tagButtonList[i].Init();

        //tagButtonList[0].onClick.Invoke();
    }

    public void DefalutClick()
    {
        tagButtonList[0].onClick.Invoke();
    }

    public void SetTagButtonsOn(List<int> ID)
    {
        Debug.Log("IDList.count = " + ID.Count);
        for (var i = 0; i < tagButtonList.Count; i++) tagButtonList[i].Select(false);
        if (ID.Count != 0)
            for (var i = 0; i < ID.Count; i++)
                tagButtonList[ID[i]].Select(true);
    }

    /// <summary>
    ///     tagbutton 点击 回调
    /// </summary>
    /// <param name="args"></param>
    private void OnTagButtonClickAck(IMessage rMessage)
    {
        rMessage.IsHandled = true;
        var argRecv = rMessage.Data as TagButtonArg;
        //Debug.Log(argRecv.ID.ToString());
        if (argRecv.groupID == groupID)
            Set(argRecv.ID, _clickFunc);
    }

    public void Set(int ID, Action<UITagButton> excute)
    {
        for (var i = 0; i < tagButtonList.Count; i++)
            if (ID == tagButtonList[i].ID)
            {
                tagButtonList[i].Select(true);
                tagButtonList[i].HighLight = true;
                //tagButtonList[i].buttonImage.color = tagButtonList[i].highlightColor;
                if (tagButtonList[i].buttonText != null)
                    tagButtonList[i].buttonText.color = tagButtonList[i].highlightColor;
                if (tagButtonList[i].bgImg)
                {
                    //  iTween.ScaleTo(tagButtonList[i].bgImg.gameObject, Vector3.one, 0.35f);
                    // iTween.ColorTo(tagButtonList[i].bgImg.gameObject, Color.white, 0.35f);
                    tagButtonList[i].bgImg.transform.DOScale(Vector3.one, 0.35f);
                    tagButtonList[i].bgImg.DOColor(Color.white, 0.35f);
                }

                if (excute != null)
                    excute(tagButtonList[i]);
            }
            else
            {
                tagButtonList[i].Select(false);
                tagButtonList[i].HighLight = false;
                //tagButtonList[i].buttonImage.color = tagButtonList[i].normalColor;
                if (tagButtonList[i].buttonText != null)
                    tagButtonList[i].buttonText.color = tagButtonList[i].normalColor;
                if (tagButtonList[i].bgImg)
                {
                    tagButtonList[i].bgImg.transform.DOScale(new Vector3(0, 1, 1), 0.35f);
                    tagButtonList[i].bgImg.DOColor(new Color(1, 1, 1, 0), 0.35f);
                    //iTween.ScaleTo(tagButtonList[i].bgImg.gameObject, new Vector3(0, 1, 1), 0.35f);
                    //iTween.ColorTo(tagButtonList[i].bgImg.gameObject, new Color(1, 1, 1, 0), 0.35f);
                }
            }
    }
}

/// <summary>
///     TagButton消息参数
/// </summary
public class TagButtonArg : EventArgs
{
    public int groupID;
    public int ID;
    public string name;
}

public partial class GameEvent
{
    //点击TagButton
    public static string TAGBUTTON_CLICK = "TagButtonClick";
}