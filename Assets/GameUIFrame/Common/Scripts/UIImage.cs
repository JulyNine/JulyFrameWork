/**
*   Author：onelei
*   Copyright © 2019 - 2020 ONELEI. All Rights Reserved
*/

using UnityEngine;
using UnityEngine.UI;


[AddComponentMenu("UI/UIImage")]
public class UIImage : Image
{
    [HideInInspector] public bool bInit = false;

    /// <summary>
    /// 多语言key
    /// </summary>
    public string key = string.Empty;

    private GameObject _CacheGameObject = null;

    public GameObject CacheGameObject
    {
        get
        {
            if (_CacheGameObject == null)
            {
                _CacheGameObject = gameObject;
            }

            return _CacheGameObject;
        }
    }

    private Transform _CacheTransform = null;

    public Transform CacheTransform
    {
        get
        {
            if (_CacheTransform == null)
            {
                _CacheTransform = transform;
            }

            return _CacheTransform;
        }
    }
}