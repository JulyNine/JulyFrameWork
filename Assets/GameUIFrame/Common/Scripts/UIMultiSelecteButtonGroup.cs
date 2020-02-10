using System;
using System.Collections.Generic;
using com.ootii.Messages;
using UnityEngine;
/// <summary>
/// 多选Button组
/// </summary>
public class UIMultiSelecteButtonGroup : MonoBehaviour
{
    private Action<UIMultiSelecteButton> _clickFunc;
    public int groupId;
    [HideInInspector] public List<UIMultiSelecteButton> multiSelecteButtonList = new List<UIMultiSelecteButton>();

    private void OnEnable()
    {
        MessageDispatcher.AddListener(GameEvent.MULTISELECTEBUTTON_CLICK, OnMultiSelecteButtonClickAck, true);
    }

    private void OnDisable()
    {
        MessageDispatcher.RemoveListener(GameEvent.MULTISELECTEBUTTON_CLICK, OnMultiSelecteButtonClickAck);
    }

    public void Init(Action<UIMultiSelecteButton> clickFunc)
    {
        _clickFunc = clickFunc;
        for (var i = 0; i < multiSelecteButtonList.Count; i++) multiSelecteButtonList[i].Init();

        //tagButtonList[0].onClick.Invoke();
    }

    public void DefalutClick()
    {
        for (var i = 0; i < multiSelecteButtonList.Count; i++) multiSelecteButtonList[i].Select(false);
    }

    public void SetMultiSelecteButtonsOn(List<int> ID)
    {
        Debug.Log("IDList.count = " + ID.Count);
        for (var i = 0; i < multiSelecteButtonList.Count; i++) multiSelecteButtonList[i].Select(false);
        for (var j = 0; j < ID.Count; j++) multiSelecteButtonList[ID[j]].Select(true);
    }

    /// <summary>
    ///     tagbutton 点击 回调
    /// </summary>
    /// <param name="args"></param>
    private void OnMultiSelecteButtonClickAck(IMessage rMessage)
    {
        rMessage.IsHandled = true;
        var argRecv = rMessage.Data as MultiSelecteButtonArgs;
        //LogManager.Log(argRecv.ID.ToString());
        if (argRecv.groupID == groupId)
            Set(argRecv.ID, _clickFunc);
    }

    public void Set(int ID, Action<UIMultiSelecteButton> excute)
    {
        for (var i = 0; i < multiSelecteButtonList.Count; i++)
            if (ID == multiSelecteButtonList[i].ID)
            {
                if (multiSelecteButtonList[i].buttonText != null)
                    multiSelecteButtonList[i].buttonText.color = multiSelecteButtonList[i].highlightColor;
                if (multiSelecteButtonList[i].selectedImage != null &&
                    !multiSelecteButtonList[i].selectedImage.gameObject.activeInHierarchy)
                    multiSelecteButtonList[i].Select(true);
                else
                    multiSelecteButtonList[i].Select(false);
                if (excute != null)
                    excute(multiSelecteButtonList[i]);
            }
    }
}

/// <summary>
///     MultiSelecteButton消息参数
/// </summary
public class MultiSelecteButtonArgs : EventArgs
{
    public int groupID;
    public int ID;
    public string name;
}