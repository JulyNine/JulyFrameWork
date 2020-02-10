using System.Collections.Generic;
using com.ootii.Messages;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///     互斥Button
/// </summary>
public class UITagButton : UIButton
{
    public Image bgImg;
    public List<Image> colorImgList = new List<Image>();
    public int groupID;
    public Color highlightColor = Color.yellow;
    public Color normalColor = Color.white;

    public bool HighLight
    {
        set
        {
            for (var i = 0; i < colorImgList.Count; i++) colorImgList[i].color = value ? highlightColor : normalColor;
            if (buttonImage)
                buttonImage.color = value ? highlightColor : normalColor;
            if (buttonText)
                buttonText.color = value ? highlightColor : normalColor;
        }
    }

    public new void Init()
    {
        base.Init();
        onClick.AddListener(delegate { OnTagButtonClick(gameObject); });
    }

    /// <summary>
    ///     tagbutton 点击
    /// </summary>
    /// <param name="args"></param>
    private void OnTagButtonClick(GameObject go)
    {
        var arg = new TagButtonArg();
        arg.name = buttonName;
        arg.groupID = groupID;
        arg.ID = ID;
        var lMessage = new Message();
        lMessage.Type = GameEvent.TAGBUTTON_CLICK;
        lMessage.Sender = this;
        lMessage.Data = arg;
        lMessage.Delay = EnumMessageDelay.IMMEDIATE;
        MessageDispatcher.SendMessage(lMessage);
    }

    public void Select(bool select)
    {
        if (selectedImage != null)
            selectedImage.gameObject.SetActive(select);
    }
}