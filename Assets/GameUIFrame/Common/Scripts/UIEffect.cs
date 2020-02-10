using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 帧动画
/// </summary>
public class UIEffect : MonoBehaviour
{
    public Color effectColor;

    public List<Sprite> effectSprites;

    //一秒多少帧
    private const float Fps = 15;


    public Image image;

    //动画帧总数
    private int _mFrameCount;

    //当前帧
    private int _nowFram;

    //限制帧的时间
    private float _time;

    public void Init()
    {
        for (var i = 0; i < effectSprites.Count; i++) image.color = effectColor;
        _mFrameCount = effectSprites.Count;
        image.sprite = effectSprites[0];
    }


    // Use this for initialization
    private void Start()
    {
        Init();
    }

    // Update is called once per frame
    private void Update()
    {
        //计算限制帧的时间
        _time += Time.deltaTime;
        //超过限制帧切换贴图
        if (_time >= 1.0 / Fps)
        {
            //帧序列切换
            _nowFram++;
            //限制帧清空
            _time = 0;
            //超过帧动画总数从第0帧开始
            if (_nowFram >= _mFrameCount) _nowFram = 0;
            image.sprite = effectSprites[_nowFram];
        }
    }
}