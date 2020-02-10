using com.ootii.Messages;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///     多选Button
/// </summary>
public class UIMultiSelecteButton : UIButton
{
    public Image bgImg;
    public int groupID;
    public Color highlightColor = Color.yellow;
    public Color normalColor = Color.white;

    public new void Init()
    {
        base.Init();
        onClick.AddListener(delegate { OnMultiSelecteButtonClick(gameObject); });
    }

    /// <summary>
    ///     Multiselectebutton 点击
    /// </summary>
    /// <param name="args"></param>
    private void OnMultiSelecteButtonClick(GameObject go)
    {
        var arg = new MultiSelecteButtonArgs();
        arg.name = buttonName;
        arg.groupID = groupID;
        arg.ID = ID;
        var lMessage = new Message();
        lMessage.Type = GameEvent.MULTISELECTEBUTTON_CLICK;
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