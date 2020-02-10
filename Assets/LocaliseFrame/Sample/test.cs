using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameFramework.Localisation;
public class test : MonoBehaviour
{
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        //GlobalLocalisation.Load();

        //GlobalLocalisation.SetLanguageToDefault();

        //GlobalLocalisation.TrySetAllowedLanguage("Chinese(Simplified)");

        //获取文字API
        text.text = GlobalLocalisation.GetText("test2");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeToChinese()
    {
        //切换语言API
        GlobalLocalisation.TrySetAllowedLanguage("Chinese(Simplified)");
    }

    public void ChangeToEnglish()
    {
        //切换语言API
        GlobalLocalisation.TrySetAllowedLanguage("English");
    }
}
