using System;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 计算当前FPS
/// </summary>
public class FPS : MonoBehaviour
{
    public Text text;
    public int lockFps = 30;
    public float textUpdateTime = 0.5f;
    
    private float _lastUpdateTime;
    private long _mFrameCount;
    private long _mLastFrameTime;

    private static long _mLastFps;
    
    // Use this for initialization
    private void Start()
    {
    }

    private void Update()
    {
        UpdateTick();
        UpdateFpsLabel();
    }

    private void UpdateFpsLabel()
    {
        if (Time.time - _lastUpdateTime > textUpdateTime && text != null)
        {
            _lastUpdateTime = Time.time;
            text.text = _mLastFps.ToString();
        }
    }

    private void UpdateTick()
    {
        if (true)
        {
            _mFrameCount++;
            var nCurTime = TickToMilliSec(DateTime.Now.Ticks);
            if (_mLastFrameTime == 0) _mLastFrameTime = TickToMilliSec(DateTime.Now.Ticks);

            if (nCurTime - _mLastFrameTime >= 1000)
            {
                var fps = (long) (_mFrameCount * 1.0f / ((nCurTime - _mLastFrameTime) / 1000.0f));

                _mLastFps = fps;

                _mFrameCount = 0;

                _mLastFrameTime = nCurTime;
                _mLastFrameTime = nCurTime;
            }
        }
    }

    private static long TickToMilliSec(long tick)
    {
        return tick / (10 * 1000);
    }
}