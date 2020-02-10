using UnityEngine;

/// <summary>
/// 单例模板
/// </summary>
public abstract class Singleton<T> where T : class, new()
{
    protected static T _instance;

    protected Singleton()
    {
        if (null != _instance) Debug.LogError("This " + typeof(T) + "Singleton Instance is not null!!!");
    }

    public static T Instance
    {
        get
        {
            if (null == _instance) _instance = new T();
            return _instance;
        }
    }


    public virtual void Init()
    {
    }
}