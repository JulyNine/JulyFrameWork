using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class SpritesLoader : MonoBehaviour
{
    public bool needTraslation;
    public bool useSpriteBundle;
    public SpriteGroup[] spriteGroup;
    


	void Start ()
    {
        if (UIManager.Instance.useSpriteBundle && useSpriteBundle)
        {
            for (int i = 0; i < spriteGroup.Length; i++)
            {
                spriteGroup[i].LoadSprite();
            }
        }
	}

    [System.Serializable]
    public class SpriteGroup
    {
        public string spriteName;
        public string atlasName;
        public Image[] sprites;

        public void LoadSprite()
        {
            //Sprite sprite = AtlasManager.Instance.GetSprite(atlasName, spriteName);
            for (int i = 0; i < sprites.Length; i++)
            {
                if (sprites[i])
                {
                    AtlasManager.Instance.SetSprite(sprites[i] ,atlasName, spriteName);
                    //sprites[i].sprite = sprite;
                }                
            }
        }
    }

}
//Debug.Log("LoadSprite" + name);
//if (image == null)
//    image = this.transform.GetComponent<Image>();
//Sprite loaded = AtlasManager.Instance.GetSprite(atlasName, imageName, needTraslation);
//if (loaded)
//{
//    image.sprite = loaded;
//}
//image.sprite = AtlasManager.Instance.GetSprite(atlasName, imageName, needTraslation);