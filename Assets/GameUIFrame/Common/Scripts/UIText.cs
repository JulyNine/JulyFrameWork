using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using GameFramework.Localisation;

public class UIText : Text
{
    [AddComponentMenu("UI/UIText")]
 
    [HideInInspector]
    public bool bInit = false;
    /// <summary>
    /// 多语言key
    /// </summary>
    public string key = string.Empty;

    private GameObject _CacheGameObject = null;
    public GameObject CacheGameObject { get { if (_CacheGameObject == null) { _CacheGameObject = gameObject; } return _CacheGameObject; } }

    private Transform _CacheTransform = null;
    public Transform CacheTransform { get { if (_CacheTransform == null) { _CacheTransform = transform; } return _CacheTransform; } }

    public override string text
    {
        get
        {
            return m_Text;
        }
        set
        {
            if (String.IsNullOrEmpty(value))
            {
                if (String.IsNullOrEmpty(m_Text))
                    return;
                m_Text = "";
                SetVerticesDirty();
            }
            else if (m_Text != value)
            {
                m_Text = value;
                SetVerticesDirty();
                SetLayoutDirty();
            }
        }
    }

    public void Refresh()
    {
        if (string.IsNullOrEmpty(key))
            return;
        text = GlobalLocalisation.GetText(key);
       //text = LanguageManager.Instance.GetUILanguageString(key);
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        bInit = false;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        //Refresh();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

}
