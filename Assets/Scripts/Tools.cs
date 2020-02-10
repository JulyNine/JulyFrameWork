using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;
using UnityEngine;
public static class Tools
{
    // Start is called before the first frame update

    public static Coroutine DelayToDo(this MonoBehaviour mono, float delayTime, Action action, bool ignoreTimeScale = false)
    {
        Coroutine coroutine = null;
        if (ignoreTimeScale)
        {
            coroutine = mono.StartCoroutine(DelayIgnoreTimeToDo(delayTime, action));
        }
        else
        {
            coroutine = mono.StartCoroutine(DelayToInvokeDo(delayTime, action));

        }
        return coroutine;
    }

    public static IEnumerator DelayToInvokeDo(float delaySeconds, Action action)
    {
        yield return new WaitForSeconds(delaySeconds);
        action();
    }

    public static IEnumerator DelayIgnoreTimeToDo(float delaySeconds, Action action)
    {
        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < start + delaySeconds)
        {
            yield return null;
        }
        action();
    }

    
    
}
