//using System;

using UnityEngine;
using GameFramework.Localisation.Components;
//using UnityEngine.UI;
//using com.ootii.Messages;
/// <summary>
/// 普通UIWidget
/// </summary>
public class UIWidget : MonoBehaviour
{
    RectTransform _rectTransform;
    RectTransform rectTransform
    {
        get
        {
            if (!_rectTransform)
            {
                _rectTransform = GetComponent<RectTransform>();
            }
            return _rectTransform;
        }
    }

    public virtual void Init(System.Object args)
    {
        ////动态从bundle加载图集资源
        //if(GameMain.Instance.useSpriteBundle)
        //{
        //    LoadSprite[] images = this.transform.GetComponentsInChildren<LoadSprite>();
        //    for (int i = 0; i < images.Length; i++)
        //    {
        //        images[i].Init();
        //    }
        //}

        LocaliseText [] texts = this.transform.GetComponentsInChildren<LocaliseText>();
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].Init();
        }

    }

    public virtual void Set(System.Object args)
    {

    }

    //public int dataIndex
    //{
    //    set
    //    {
    //        base.dataIndex = value;
    //    }
    //    get
    //    {
    //        return base.dataIndex;
    //    }
    //}

   
        
}
