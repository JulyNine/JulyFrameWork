using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 从bundle动态加载图集资源工具
/// </summary>
public class LoadSprite : MonoBehaviour {
    public Image image;
    public string imageName;
    public string atlasName;
    public bool needTraslation = false;
    //该image是否从bundle下载
    public bool useSpriteBundle = true;

    private bool isLoaded = false;

    public void Init()
    {
        if (isLoaded)
            return;
      
        if (UIManager.Instance.useSpriteBundle && useSpriteBundle)
        {
            if (image == null)
                image = this.transform.GetComponent<Image>();
            AtlasManager.Instance.SetSprite(image, atlasName, imageName);
			isLoaded = true;
        }
    }

    void Awake()
    {
        Init();
    }
}
