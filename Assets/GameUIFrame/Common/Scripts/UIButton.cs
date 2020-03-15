using GameFramework.Localisation;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
/// <summary>
///     按钮类
/// </summary>
[AddComponentMenu("UI/UIButton")]
public class UIButton : Button
{
    [HideInInspector] public bool bInit = false;
    private static Material _disableMat;
    public Image buttonImage;
    public string buttonImageAtalas; //按钮背景
    public string buttonImageName;
    public string buttonName;
    public Text buttonText;
    public int ID;
    
    private bool _isLocked; // 是否锁定

    public Button lockImg; // 锁的图标
    public string mouseClickSound = "Click_Button";
    public Image selectedImage;

    public string textKey; //文字查表Key
    
    public UnityEvent onClick = new UnityEvent();
    public float durationThreshold = 0.2f;
    public UnityEvent onLongPress = new UnityEvent();
    public bool isPointerDown = false;
    public bool longPressTriggered = false;
    public float timePressStarted;
    public UnityEvent onButtonUp = new UnityEvent();
    //public UnityEvent onPPress = new UnityEvent();
    
    private static Material DisableMat
    {
        get
        {
            if (_disableMat == null) _disableMat = Resources.Load<Material>("Materials/ImageGreyMat");
            return _disableMat;
        }
    }

    // void IPointerClickHandler.OnPointerClick(PointerEventData data)
    // {
    //     //MasterAudio.StopAllOfSound(mouseClickSound);
    //     //MasterAudio.PlaySoundAndForget(mouseClickSound);
    //     if(_isLocked)
    //         return;
    //     if (!string.IsNullOrEmpty(mouseClickSound))
    //         AudioManager.Instance.PlayAudio(mouseClickSound);
    //     onClick?.Invoke();
    // }
    //
    //初始化
    public void Init()
    {
        if(onClick == null)
            onClick = new UnityEvent();
        _isLocked = false;
        //设置文字
        if (buttonText != null)
            if (!string.IsNullOrEmpty(textKey))
                buttonText.text = GlobalLocalisation.GetText(textKey);
        //设置图片背景
        if (UIManager.Instance.useSpriteBundle)
            if (!string.IsNullOrEmpty(buttonImageAtalas) && !string.IsNullOrEmpty(buttonImageName))
                AtlasManager.Instance.SetSprite(buttonImage, buttonImageAtalas, buttonImageName);
    }

    public void Init(UnityAction call)
    {
        onClick = new UnityEvent();
        onClick.AddListener(call);
        Init();
    }
    
    
    public void Lock()
    {
        //Debug.LogError("Lock!!!!!!!!!!!!!!");
        _isLocked = true;
        if (buttonText != null)
            buttonText.color = colors.disabledColor;
        if (GetComponent<Image>() != null)
            GetComponent<Image>().material = DisableMat;
        if (lockImg != null)
            lockImg.gameObject.SetActive(true);
        //else
        //    LogManager.LogError("No Lock Img!");
    }

    public void UnLock()
    {
        //Debug.LogError("UnLock!!!!!!!!!!!!!!");
        _isLocked = false;
        if (buttonText != null)
            buttonText.color = colors.normalColor;
        if (GetComponent<Image>() != null)
            GetComponent<Image>().material = null;
        if (lockImg != null)
            lockImg.gameObject.SetActive(false);
        //else
        //    LogManager.LogError("No Lock Img!");
    }
    
   
	/// <summary>
	/// 设置信息
	/// </summary>
	/// <param name="textKey"></param>
	public void SetLocalisationText(string key)
	{
        buttonText.text = GlobalLocalisation.GetText(key);
	}

	/// <summary>
	/// 设置标签信息
	/// </summary>
	/// <param name="text"></param>
	public void SetText(string text)
	{
		buttonText.text = text;
	}
    
    private void Update() 
    {
        if (IsPressed())
        {
            onLongPress.Invoke();
        }
        // if (isPointerDown && !longPressTriggered) 
        // {
        //     if (Time.time - timePressStarted > durationThreshold) 
        //     {
        //         longPressTriggered = true;
        //         onLongPress.Invoke();
        //         DoStateTransition(SelectionState.Pressed, false);
        //     }
        // }
    }

    // public void OnPointerDown(PointerEventData eventData)
    // {
    //     timePressStarted = Time.time;
    //     isPointerDown = true;
    //     longPressTriggered = false;
    //     DoStateTransition(SelectionState.Pressed, false);
    // }
    //
    // public void OnPointerUp(PointerEventData eventData) 
    // {
    //     isPointerDown = false;
    //     onButtonUp.Invoke();
    //     DoStateTransition(SelectionState.Normal, false);
    // }
    //
    // public void OnPointerExit(PointerEventData eventData)
    // {
    //     isPointerDown = false;
    //     DoStateTransition(SelectionState.Normal, false);
    //     OnDeselect(eventData);
    //     IsHighlighted();
    // }
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        onButtonUp?.Invoke();
        // if (eventData.button != PointerEventData.InputButton.Left)
        //     return;
        //
        // isPointerDown = false;
        // EvaluateAndTransitionToSelectionState();
    }
    private void Press()
    {
        if (!IsActive() || !IsInteractable())
            return;
        UISystemProfilerApi.AddMarker("Button.onClick", this);
        if(_isLocked)
            return;
        if (!string.IsNullOrEmpty(mouseClickSound))
            AudioManager.Instance.PlayAudio(mouseClickSound);
        onClick?.Invoke();
        //onClick.Invoke();
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        Press();
    }
    
    public virtual void OnSubmit(BaseEventData eventData)
    {
        Press();

        // if we get set disabled during the press
        // don't run the coroutine.
        if (!IsActive() || !IsInteractable())
            return;

        DoStateTransition(SelectionState.Pressed, false);
        StartCoroutine(OnFinishSubmit());
    }

    private IEnumerator OnFinishSubmit()
    {
        var fadeTime = colors.fadeDuration;
        var elapsedTime = 0f;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        DoStateTransition(currentSelectionState, false);
    }
    
}