using System;
using System.Collections.Generic;

using DG.Tweening;

using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

/// <summary>
///     UI 管理
/// </summary>
public class UIManager : MonoSingleton<UIManager>
{
    private const string UIEnforcewindowDir = "UIPrefabs/UIEnforceWindows/";

    private const string UIWidgetDir = "UIPrefabs/UIWidgets/";

    public const string UIWindowDir = "UIPrefabs/UIWindows/";

    public GameObject commonCanvasRoot;

    public GameObject loadingIcon;

    public GameObject reqMask;

    public Text staticTips;

    public Camera UICamera;

    public GameObject UIRoot;

    [Header("Whether Sprites load from assetbundle")]
    [HideInInspector]
    public bool useSpriteBundle = false;

    private readonly List<UIEnforceWindow> _enforceWindowStack = new List<UIEnforceWindow>();

    /// <summary>
    ///     window缓存资源池容量
    /// </summary>
    private readonly int _poolCapacity = 10;

    /// <summary>
    ///     window栈，按照用户打开的ui顺序入栈
    /// </summary>
    private readonly List<UIWindow> _windowsStack = new List<UIWindow>();

    /// <summary>
    ///     当前window的位置序号
    /// </summary>
    private UIWindows _currentUiWindow;

    private List<UIEnforceWindow> _enforceWindowPool = new List<UIEnforceWindow>();

    // private int _uiCacheLayer = 8;
    //
    // private int _uiLayer = 5;

    /// <summary>
    ///     window缓存资源池，如果已经缓存过直接从池子获取
    /// </summary>
    private List<UIWindow> _windowPool = new List<UIWindow>();
    
    // private void Awake()
    // {
    //     // if (_instance != null)
    //     // {
    //     //     Destroy(gameObject);
    //     //     return;
    //     // }
    //     //
    //     // _instance = this;
    //     DontDestroyOnLoad(this);
    // }
    // 清空释放资源
    public void Clear()
    {
        {
            if (_windowsStack != null)
            {
                for (var i = 0; i < _windowsStack.Count; i++)
                    if (_windowsStack[i] != null)
                        _windowsStack[i].Clear();

                _windowsStack.Clear();
            }
        }
        {
            if (_enforceWindowStack != null) _enforceWindowStack.Clear();
        }
        {
            if (_windowPool != null)
            {
                for (var i = 0; i < _windowPool.Count; i++)
                    if (_windowPool[i] != null && _windowPool[i].gameObject != null)
                    {
                        Destroy(_windowPool[i].gameObject);
                        _windowPool[i] = null;
                    }

                _windowPool.Clear();
            }
        }
        {
            if (_enforceWindowPool != null)
            {
                for (var i = 0; i < _enforceWindowPool.Count; i++)
                    if (_enforceWindowPool[i] != null && _enforceWindowPool[i].gameObject != null)
                    {
                        Destroy(_enforceWindowPool[i].gameObject);
                        _enforceWindowPool[i] = null;
                    }

                _enforceWindowPool.Clear();
            }
        }
    }

    // 关闭强制window
    public void CloseEnforceWindow()
    {
        var backNum = 1;

        // 隐藏当前window
        while (_enforceWindowStack.Count >= 1 && backNum > 0)
        {
            backNum--;
            var forDestroyWindow = _enforceWindowStack[_enforceWindowStack.Count - 1];
            forDestroyWindow.Close();
            SetVisible(forDestroyWindow.gameObject, false);
            _enforceWindowStack.Remove(forDestroyWindow);
        }
    }

    public GameObject CreateWidget(UIWidgets type)
    {
        var prefabName = Enum.GetName(typeof(UIWidgets), type);
        var resourcePath = UIWidgetDir;
        var resourceName = resourcePath + prefabName;
        return Instantiate(Resources.Load(resourceName, typeof(GameObject))) as GameObject;
    }
    public GameObject CreateUIPrefab(string path)
    {
        var resourcePath = UIWidgetDir;
        var resourceName = resourcePath + path;
        return Instantiate(Resources.Load(resourceName, typeof(GameObject))) as GameObject;
    }

    public UIWindow GetCurrentUiWindow()
    {
        return _windowsStack[_windowsStack.Count - 1];
    }

    public void HideStaticTips()
    {
        staticTips.gameObject.SetActive(false);
    }

    // 回退上一个Window
    public void OpenBackWindow(OpenBackWindowArgs data = null, bool needUpdate = false)
    {
        var backNum = 1;
        if (data != null)
            backNum = data.backNums;
        if (_windowsStack.Count == 1)
        {
            var forDestroyWindow = _windowsStack[_windowsStack.Count - 1];
            forDestroyWindow.Close();
            SetVisible(forDestroyWindow.gameObject, false);
            _windowsStack.Remove(forDestroyWindow);
        }
        else
        {
            // 隐藏当前window
            while (_windowsStack.Count > 1 && backNum > 0)
            {
                backNum--;
                var forDestroyWindow = _windowsStack[_windowsStack.Count - 1];
                forDestroyWindow.Close();
                SetVisible(forDestroyWindow.gameObject, false);
                _windowsStack.Remove(forDestroyWindow);
            }

            // 显示之前window
            var lastWindow = _windowsStack[_windowsStack.Count - 1];
            SetVisible(lastWindow.gameObject, true);
            if (needUpdate && data != null)
            {
                var args = data.args;
                lastWindow.Set(args);
            }

            lastWindow.lastOpenTime = DateTime.Now;

            // UI切换效果
            var canvasGroup = lastWindow.GetComponent<CanvasGroup>();
            if (canvasGroup != null)
            {
                canvasGroup.alpha = 0;
                DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 1f, 0.5f);
            }
        }
    }

    // 打开强制window
    public void OpenEnforceWindow(OpenEnforceWindowArgs data)
    {
        var windowKey = data.windowKey;
        var args = data.args;

        // if (enforceWindowStack.Count != 0)
        // {
        // SetVisible(enforceWindowStack[enforceWindowStack.Count - 1].gameObject, false);
        // }
        // 注意释放该window变量
        var window = GetEnforceWindow(windowKey, args);

        // TODO UI转换效果
        var canvasGroup = window.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0;
            DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 1f, 0.0f);
        }

        var requestWindow = window.GetComponent<UIEnforceWindow>();
        if (_enforceWindowStack.Contains(requestWindow)) _enforceWindowStack.Remove(requestWindow);
        _enforceWindowStack.Add(requestWindow);
    }
    public void OpenEnforceWindow(UIEnforceWindows windowKey)
    {
        //var windowKey = data.windowKey;
        //var args = data.args;

        // if (enforceWindowStack.Count != 0)
        // {
        // SetVisible(enforceWindowStack[enforceWindowStack.Count - 1].gameObject, false);
        // }
        // 注意释放该window变量
        var window = GetEnforceWindow(windowKey, null);

        // TODO UI转换效果
        var canvasGroup = window.GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0;
            DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 1f, 0.0f);
        }

        var requestWindow = window.GetComponent<UIEnforceWindow>();
        if (_enforceWindowStack.Contains(requestWindow)) _enforceWindowStack.Remove(requestWindow);
        _enforceWindowStack.Add(requestWindow);
    }
    public void OpenUIWindow(UIWindows windowKey)
    {
        OpenUIWindow(windowKey, null);
    }

    public void OpenUIWindow(OpenUIWindowArgs data)
    {
        var windowKey = data.windowKey;
        var args = data.args;
        OpenUIWindow(windowKey, args);
    }

    public void OpenUIWindow(UIWindows windowKey, object args)
    {
        if (_windowsStack.Count != 0)
        {
            _windowsStack[_windowsStack.Count - 1].BecomeInvisible();
            SetVisible(_windowsStack[_windowsStack.Count - 1].gameObject, false);
        }

        // 注意释放该window变量
        GetWindow(windowKey, args);

        // if (windowID == (int)UIWindows.StatusWindow)//statuswindow特殊，不在windowStack中
        // {
        // defaultWindowList.Add(windowID, window);
        // return;
        // }
        // // TODO UI转换效果
        // var canvasGroup = window.GetComponent<CanvasGroup>();
        // if (canvasGroup != null)
        // {
        //     canvasGroup.alpha = 0;
        //     DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 1f, 0.5f);
        // }
        //
        // var requestWindow = window.GetComponent<UIWindow>();
        // if (_windowsStack.Contains(requestWindow)) _windowsStack.Remove(requestWindow);
        // _windowsStack.Add(requestWindow);
    }

    public void SetLoadingStatus(bool status)
    {
        loadingIcon.SetActive(status);
    }

    public void ShowDynamicTips(string content)
    {
        var tips = CreateWidget(UIWidgets.Tips).GetComponent<Tips>();

        tips.transform.SetParent(commonCanvasRoot.transform);
        tips.gameObject.transform.localScale = Vector3.one;
        tips.Init(content);
    }

    public void ShowReqMask(bool state)
    {
        if (state) reqMask.SetActive(true);
        else reqMask.SetActive(false);
    }

    public void ShowStaticTips(string content)
    {
        staticTips.DOFade(1, 0.0f);
        staticTips.gameObject.SetActive(true);
        staticTips.text = content;
    }

    public void ShowStaticTipsWithAnim(string content)
    {
        staticTips.DOFade(1, 0.0f);
        staticTips.gameObject.SetActive(true);
        staticTips.text = content;
        Tweener tweener = staticTips.DOFade(0, 2.0f);
        tweener.onComplete = delegate { staticTips.gameObject.SetActive(false); };
    }
    

    private GameObject GetEnforceWindow(UIEnforceWindows windowKey, object args)
    {
        if (_enforceWindowPool == null) _enforceWindowPool = new List<UIEnforceWindow>();

        UIEnforceWindow requesetWindow = null;

        // 如果缓存池中有，则直接加载
        for (var i = 0; i < _enforceWindowPool.Count; i++)
        {
            if (_enforceWindowPool[i].windowKey == windowKey)
            {
                requesetWindow = _enforceWindowPool[i];
                requesetWindow.lastOpenTime = DateTime.Now;
                break;
            }   
            
        }
        if (requesetWindow != null)
        {
            SetVisible(requesetWindow.gameObject, true);
            requesetWindow.Set(args);
        }
        else
        {
            // 如果缓存池中没有，则从硬盘加载
            var windowName = Enum.GetName(typeof(UIEnforceWindows), windowKey);
            var resourcePath = UIEnforcewindowDir;
            var resourceName = resourcePath + windowName;

            // requesetWindow = GameObject.Instantiate(ResourceLoader.GetObjectDirectly<GameObject>(resourceName)).GetComponent<UIEnforceWindow>();
            requesetWindow = (Instantiate(Resources.Load(resourceName, typeof(GameObject))) as GameObject)
                .GetComponent<UIEnforceWindow>();

            // windowObj.window = GameObject.Instantiate(ResourceLoader.GetObjectDirectly<UnityEngine.Object>(resourceName)) as GameObject;
            requesetWindow.transform.SetParent(UIRoot.transform);

            // 设置camera
            var canvases = requesetWindow.transform.GetComponentsInChildren<Canvas>();
            for (var i = 0; i < canvases.Length; i++) canvases[i].worldCamera = UICamera;
            var rect = requesetWindow.GetComponent<RectTransform>();
            rect.pivot = new Vector2(0.5f, 0.5f);
            rect.localPosition = Vector3.zero;
            rect.localScale = Vector3.one;
            rect.sizeDelta = Vector2.zero;
            requesetWindow.Init(args);
            requesetWindow.lastOpenTime = DateTime.Now;
            _enforceWindowPool.Add(requesetWindow);

            // 如果超出缓存容量，删除最早打开的window
            if (_enforceWindowPool.Count > _poolCapacity)
            {
                var forDestroyWindow = _enforceWindowPool[0];
                for (var i = 1; i < _enforceWindowPool.Count; i++)
                {
                    if (_enforceWindowPool[i].lastOpenTime < forDestroyWindow.lastOpenTime)
                        forDestroyWindow = _enforceWindowPool[i];   
                }
                LogManager.Log("Remove Window:" + forDestroyWindow.gameObject.name);
                _enforceWindowPool.Remove(forDestroyWindow);
                TryUnloadAtlas(forDestroyWindow);
                Destroy(forDestroyWindow.gameObject);
            }
        }

        return requesetWindow.gameObject;
    }

    private void GetWindow(UIWindows windowKey, object args)
    {
        if (_windowPool == null) _windowPool = new List<UIWindow>();
        UIWindow requestWindow = null;

        // 如果缓存池中有，则直接加载
        for (var i = 0; i < _windowPool.Count; i++)
        {
            if (_windowPool[i].windowKey == windowKey)
            {
                requestWindow = _windowPool[i];
                requestWindow.lastOpenTime = DateTime.Now;
                break;
            }   
        }
        if (requestWindow != null)
        {
            if (_windowsStack.Contains(requestWindow)) _windowsStack.Remove(requestWindow);
            _windowsStack.Add(requestWindow);
            
            SetVisible(requestWindow.gameObject, true);
            requestWindow.Set(args);
        }
        else
        {
            // 如果缓存池中没有，则从硬盘加载
            var windowName = Enum.GetName(typeof(UIWindows), windowKey);
            var resourcePath = UIWindowDir;
            var resourceName = resourcePath + windowName;

            
            // requesetWindow = Instantiate(ResourceLoader.GetObjectDirectly<GameObject>(resourceName)).GetComponent<UIWindow>();
            if(Resources.Load(resourceName, typeof(GameObject)) != null)
            {
                requestWindow = (Instantiate(Resources.Load(resourceName, typeof(GameObject))) as GameObject)
                    .GetComponent<UIWindow>();
                requestWindow.transform.SetParent(UIRoot.transform);

                // 设置camera
                var canvases = requestWindow.transform.GetComponentsInChildren<Canvas>(true);
                for (var i = 0; i < canvases.Length; i++) canvases[i].worldCamera = UICamera;
                var rect = requestWindow.GetComponent<RectTransform>();
                rect.pivot = new Vector2(0.5f, 0.5f);
                rect.localPosition = Vector3.zero;
                rect.localScale = Vector3.one;
                rect.sizeDelta = Vector2.zero;
                requestWindow.Init(args);
                requestWindow.lastOpenTime = DateTime.Now;
                _windowPool.Add(requestWindow);

                // 如果超出缓存容量，删除最早打开的window
                if (_windowPool.Count > _poolCapacity)
                {
                    var forDestroyWindow = _windowPool[0];
                    for (var i = 1; i < _windowPool.Count; i++)
                    {
                        if (_windowPool[i].lastOpenTime < forDestroyWindow.lastOpenTime)
                            forDestroyWindow = _windowPool[i];   
                    }
                    LogManager.Log("Remove Window:" + forDestroyWindow.gameObject.name);
                    _windowPool.Remove(forDestroyWindow);
                    forDestroyWindow.Clear();
                    TryUnloadAtlas(forDestroyWindow);
                    Destroy(forDestroyWindow.gameObject);
                    forDestroyWindow = null;
                }   
                var canvasGroup = requestWindow.GetComponent<CanvasGroup>();
                if (canvasGroup != null)
                {
                    canvasGroup.alpha = 0;
                    DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 1f, 0.5f);
                }

                if (_windowsStack.Contains(requestWindow)) _windowsStack.Remove(requestWindow);
                _windowsStack.Add(requestWindow);
            }
            else
            {
                 ResourceManager.Instance.CreatePrefabObjectAsync(windowName, delegate(GameObject obj)
            {
                requestWindow = obj.GetComponent<UIWindow>();
                requestWindow.transform.SetParent(UIRoot.transform);

                // 设置camera
                var canvases = requestWindow.transform.GetComponentsInChildren<Canvas>(true);
                for (var i = 0; i < canvases.Length; i++) canvases[i].worldCamera = UICamera;
                var rect = requestWindow.GetComponent<RectTransform>();
                rect.pivot = new Vector2(0.5f, 0.5f);
                rect.localPosition = Vector3.zero;
                rect.localScale = Vector3.one;
                rect.sizeDelta = Vector2.zero;
                requestWindow.Init(args);
                requestWindow.lastOpenTime = DateTime.Now;
                _windowPool.Add(requestWindow);

                // 如果超出缓存容量，删除最早打开的window
                if (_windowPool.Count > _poolCapacity)
                {
                    var forDestroyWindow = _windowPool[0];
                    for (var i = 1; i < _windowPool.Count; i++)
                    {
                        if (_windowPool[i].lastOpenTime < forDestroyWindow.lastOpenTime)
                            forDestroyWindow = _windowPool[i];   
                    }
                    LogManager.Log("Remove Window:" + forDestroyWindow.gameObject.name);
                    _windowPool.Remove(forDestroyWindow);
                    forDestroyWindow.Clear();
                    TryUnloadAtlas(forDestroyWindow);
                    Destroy(forDestroyWindow.gameObject);
                    forDestroyWindow = null;
                }
                
                var canvasGroup = requestWindow.GetComponent<CanvasGroup>();
                if (canvasGroup != null)
                {
                    canvasGroup.alpha = 0;
                    DOTween.To(() => canvasGroup.alpha, x => canvasGroup.alpha = x, 1f, 0.5f);
                }

                if (_windowsStack.Contains(requestWindow)) _windowsStack.Remove(requestWindow);
                _windowsStack.Add(requestWindow);
                
            }); 
            }
        }

        /*
        //整理层，把默认window放到最上面，刚刚打开的window放到默认window下
        windowObj.window.transform.SetAsLastSibling();

        var itr = defaultWindowList.GetEnumerator();
        while (itr.MoveNext())
        {
            itr.Current.Value.transform.SetAsLastSibling();
        }
        //如果打开的是enforcewindow，那么必须在billboardwindow下面，所以移到最上面
        if (enforceWindow)
            windowObj.window.transform.SetAsFirstSibling();
            */
        //return requesetWindow.gameObject;
    }

    private void SetVisible(GameObject obj, bool visible)
    {
        // 两种方式，一种通过acive一种通过layer，暂时使用layer的方式
        obj.SetActive(visible);

        /*
                Transform[] trans = obj.GetComponentsInChildren<Transform>();
                for(int i = 0; i < trans.Length; i++)
                {
                    if (visible)
                        trans[i].gameObject.layer = UILayer;
                    else
                        trans[i].gameObject.layer = UICacheLayer ;
                }
                */
    }

    /// <summary>
    ///     通过传入一个UIWindow来尝试卸载该界面引用的图集内存
    ///     如果没有其他界面引用则可以成功卸载
    /// </summary>
    /// <param name="window">UIWindow类型</param>
    private void TryUnloadAtlas(UIWindow window)
    {
        /*
        List<string> atlasNeedUnload = new List<string>();
        int usedCount;

        for (int i = 0; i < window.usedAtlas.Count; i++)
        {
            usedCount = 0;
            for (int j = 0; j < windowPool.Count; j++)
            {
                for (int k = 0; k < windowPool[j].usedAtlas.Count; k++)
                {
                    if (window.usedAtlas[i].Equals(windowPool[j].usedAtlas[k]))
                        usedCount++;
                }

            }

            for (int j = 0; j < enforceWindowPool.Count; j++)
            {
                for (int k = 0; k < windowPool[j].usedAtlas.Count; k++)
                {
                    if (window.usedAtlas[i].Equals(windowPool[j].usedAtlas[k]))
                        usedCount++;
                }

            }
            if(usedCount == 0)
                AtlasManager.Instance.UnloadAtlas(window.usedAtlas[i]);
        }
        */
    }

    /// <summary>
    ///     通过传入一个UIWindow来尝试卸载该界面引用的图集内存
    ///     如果没有其他界面引用则可以成功卸载
    /// </summary>
    /// <param name="window">UIEnforceWindow类型</param>
    private void TryUnloadAtlas(UIEnforceWindow window)
    {
        /*
        List<string> atlasNeedUnload = new List<string>();
        int usedCount;

        for (int i = 0; i < window.usedAtlas.Count; i++)
        {
            usedCount = 0;
            for (int j = 0; j < windowPool.Count; j++)
            {
                for (int k = 0; k < windowPool[j].usedAtlas.Count; k++)
                {
                    if (window.usedAtlas[i].Equals(windowPool[j].usedAtlas[k]))
                        usedCount++;
                }

            }

            for (int j = 0; j < enforceWindowPool.Count; j++)
            {
                for (int k = 0; k < windowPool[j].usedAtlas.Count; k++)
                {
                    if (window.usedAtlas[i].Equals(windowPool[j].usedAtlas[k]))
                        usedCount++;
                }

            }
            if (usedCount == 0)
                AtlasManager.Instance.UnloadAtlas(window.usedAtlas[i]);
        }
        */
    }
    
}

/// <summary>
///     打开UIWindow消息参数
/// </summary>
public class OpenUIWindowArgs
{
    public object args;

    public UIWindows windowKey;
}

/// <summary>
///     打开EnforceWindow消息参数
/// </summary>
public class OpenEnforceWindowArgs
{
    public object args;

    public UIEnforceWindows windowKey;
}

/// <summary>
///     打开上级UIWindow消息参数
/// </summary>
public class OpenBackWindowArgs
{
    public object args;

    // 回退窗口数量
    public int backNums = 1;
}

/// <summary>
///     普通提示页面消息参数
/// </summary>
public class WarningWindowArgs
{
    public Action confirmFunc;

    public string stringKey;
}
//所有UIWindow类型
public enum UIWindows
{
    Null = 0,
    LoginWindow,
    LoadingWindow,
    StartWindow,
    ChapterListWindow,
    ChapterWindow,
    PlayVideoWindow,
    Length,
}

//所有Enforce UIWindow类型
public enum UIEnforceWindows
{
    Null = 0,
    WarningEnforceWindow,
    NetworkStatusEnforceWindow,
    Length,
}
//所有UIWidgets类型
public enum UIWidgets
{
    Tips,
    SingleOptionAction,
    MultipleOptionAction,
    LoopOptionAction,
    SlideOptionAction,
    ImageOption,
    MultiOption,
    ClickQTEAction,
    SlideQTEAction,
    PressQTEAction,
    VideoInfo,
    Rhythm,
    ExtraVideoInfo,
    Length,
    TextOption,
}