using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using com.ootii.Messages;
/// <summary>
/// 普通UI Window
/// </summary>
public class UIWindow : MonoBehaviour
{
    public UIWindows windowKey;
    public DateTime lastOpenTime;  //窗口最后打开时间
    [HideInInspector]
	public List<string> usedAtlas;  //使用的图集列表

    public virtual void Init(object args)
    {
        //		this.transform.localScale = Vector3.zero;  // 暂时隐藏界面等待加载完成
        //
        //        //动态从bundle加载图集资源
        //        if(GameMain.Instance.useSpriteBundle)
        //        {
        //            LoadSprite[] imageSprite = this.transform.GetComponentsInChildren<LoadSprite>(true);
        //			int deCount;
        //			for (int i = 0; i < imageSprite.Length; i++)
        //            {
        //				imageSprite[i].Init();  //初始化所有Image
        //
        //				//记录该Window使用图集
        //				deCount = 0;
        //				for (int j = 0; j < usedAtlas.Count; j++) 
        //				{
        //					if (imageSprite [i].atlasName.Equals (usedAtlas [j])) 
        //					{
        //						break;  //如果有已经记录过的图集则跳出(去重)
        //					}
        //					deCount++;
        //				}
        //
        //				if (deCount.Equals (usedAtlas.Count)) 
        //				{
        //					usedAtlas.Add (imageSprite [i].atlasName);  //添加未记录的图集
        //				}
        //            }
        //        }
        //
        //LoadText[] texts = this.transform.GetComponentsInChildren<LoadText>();
        //for (int i = 0; i < texts.Length; i++)
        //{
        //    texts[i].Init();
        //}
        //
        //		this.transform.localScale = Vector3.one;
    }

    public virtual void Set(object args)
    {

    }

    public virtual void Close()
    {

    }

    public virtual void BecomeInvisible()
    {

    }

    public virtual void Clear()
    {

    }

}
