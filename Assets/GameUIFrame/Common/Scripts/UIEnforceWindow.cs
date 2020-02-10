using System;
using System.Collections;
using System.Collections.Generic;
using GameFramework.Localisation.Components;
using UnityEngine;
using UnityEngine.UI;
//using com.ootii.Messages;
/// <summary>
/// 强制window
/// </summary>
public class UIEnforceWindow : MonoBehaviour
{
    public UIEnforceWindows windowKey;

	public DateTime lastOpenTime;  //窗口最后打开时间
	public List<string> usedAtlas;  //使用的图集列表

    public virtual void Init(object args)
    {
        //动态从bundle加载图集资源
        if (UIManager.Instance.useSpriteBundle)
        {
			LoadSprite[] imageSprite = this.transform.GetComponentsInChildren<LoadSprite>();
			int deCount;
			for (int i = 0; i < imageSprite.Length; i++)
			{
				imageSprite[i].Init();  //初始化所有Image

				//记录该Window使用图集
				deCount = 0;
				for (int j = 0; j < usedAtlas.Count; j++) 
				{
					if (imageSprite [i].atlasName.Equals (usedAtlas [j])) 
					{
						break;  //如果有已经记录过的图集则跳出(去重)
					}
					deCount++;
				}

				if (deCount.Equals (usedAtlas.Count)) 
				{
					usedAtlas.Add (imageSprite [i].atlasName);  //添加未记录的图集
				}
			}
        }

        LocaliseText [] texts = this.transform.GetComponentsInChildren<LocaliseText>();
        for (int i = 0; i < texts.Length; i++)
        {
            texts[i].Init();
        }
    }

    public virtual void Set(object args)
    {

    }

    public virtual void Close()
    {

    }

}
