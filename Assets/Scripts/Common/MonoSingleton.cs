using System;
using UnityEngine;

/// <summary>
/// 继承于Mono的单例模板
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    protected static T _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(this);
        }
    }

    public static T Instance
    {
        get
        {
            if (null == _instance)
            {
                var go = GameObject.Find("MonoSingletons");
                if (null == go)
                {
                    go = new GameObject("MonoSingletons");
                    DontDestroyOnLoad(go);
                }

                _instance = go.GetComponent<T>();
                if (null == _instance) _instance = go.AddComponent<T>();
            }

            return _instance;
        }
    }
}