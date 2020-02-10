using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeCountdown : MonoBehaviour
{
    public enum CountDownEnum
    {
        None,
        Image,
        Text,
        All
    }
    public GameObject imageCountDownRoot;
    public GameObject textCountDownRoot;

    public List<Image> countDownCircleList = new List<Image>();
    public Text countDownText;


    public void Init(CountDownEnum countDownEnum = CountDownEnum.Image)
    {
        bool isImage = countDownEnum == CountDownEnum.Image || countDownEnum == CountDownEnum.All;
        bool isText = countDownEnum == CountDownEnum.Text || countDownEnum == CountDownEnum.All;
        
        //图片倒计时
        imageCountDownRoot.SetActive(isImage);
        for (int index = 0; index < countDownCircleList.Count; index++)
        {
            countDownCircleList[index].gameObject.SetActive(isImage);
            countDownCircleList[index].fillAmount = 1;
        }

        //数字倒计时
        textCountDownRoot.SetActive(isText);
        countDownText.gameObject.SetActive(isText);
        countDownText.text = "";
    }

    //public void AddCountDownImage(List<Image> images,bool retainLastImageList = true)
    //{
    //    if(retainLastImageList)
    //    {
    //        countDownCircleList.AddRange(images);
    //    }
    //    else
    //    {
    //        countDownCircleList.Clear();
    //        countDownCircleList.AddRange(images);
    //    }
    //}

    // Update is called once per frame
    public void UpdateTime(float leftTime, float totalTime)
    {
        for(int index = 0;index < countDownCircleList.Count;index++)
        {
            countDownCircleList[index].fillAmount = leftTime / totalTime;
        }
        int integer = (int)leftTime;
        float fractional = leftTime - integer;

        countDownText.text = (integer + (fractional > 0 ? 1 : 0)).ToString();
    }
    
}
