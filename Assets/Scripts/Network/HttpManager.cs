using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.ootii.Messages;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class HttpManager : MonoBehaviour
{
    private readonly List<HttpReqStatus> httpReqList = new List<HttpReqStatus>();
    private readonly float intervalTime = 1.0f;

    private string PicByte;
    private readonly float timeOutTime = 3.0f;

    // Note: this is set on Awake()
    public static HttpManager Instance { get; private set; }

    //   // Use this for initialization
    //   void Start () {

    //}
    // Update is called once per frame
    private void FixedUpdate()
    {
        for (var i = 0; i < httpReqList.Count; i++)
            if (httpReqList[i].finished && DateTime.Now > httpReqList[i].reqTime.AddSeconds(intervalTime))
            {
                httpReqList.RemoveAt(i);
                i--;
            }
            else if (DateTime.Now > httpReqList[i].reqTime.AddSeconds(timeOutTime))
            {
                LogManager.LogError("TimeOut! :" + httpReqList[i].url + httpReqList[i].reqTime + ":" + DateTime.Now);
                //  Tools.ShowTips(LanguageManager.Instance.GetCommonLanguageString("NET_BAD"));
                httpReqList.RemoveAt(i);
                i--;
            }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private bool IsReqProcessing(string url)
    {
        var res = false;
        for (var i = 0; i < httpReqList.Count; i++)
            if (httpReqList[i].url == url)
                res = true;

        return res;
    }

    private void FinishReq(string url)
    {
        // LogManager.Log("FinishReq:" + url);
        for (var i = 0; i < httpReqList.Count; i++)
            if (httpReqList[i].url == url)
                httpReqList[i].finished = true;

        // UIManager.Instance.CancelNetProcessing();
    }

    private bool StartReq(string url)
    {
        if (IsReqProcessing(url))
            return false;
        httpReqList.Add(new HttpReqStatus(url, DateTime.Now));
        return true;
    }


    public void Post(string url, Dictionary<string, object> param, IMessage sendMessage,
        Action<string, IMessage> callbackFunc)
    {
        if (!StartReq(url + ConvertParamToString(param)))
            return;
        // UIManager.Instance.SetNetProcessing();
        StartCoroutine(POST(url, param, sendMessage, callbackFunc));
    }

    public void Get(string url, Dictionary<string, object> param, IMessage sendMessage,
        Action<string, IMessage> callbackFunc)
    {
        if (!StartReq(url + ConvertParamToString(param)))
            return;
        //  UIManager.Instance.SetNetProcessing();
        StartCoroutine(GET(url, param, sendMessage, callbackFunc));
    }

    private string ConvertParamToString(Dictionary<string, object> param)
    {
        string paramStr = null;
        foreach (var post_arg in param)
        {
            paramStr += post_arg.Key;
            paramStr += post_arg.Value.ToString();
        }

        return paramStr;
    }


    //POST请求(Form表单传值、效率低、安全 ，)  
    // IEnumerator POST(string url, Dictionary<string, object> post, IMessage sendMessage, Action<string, IMessage> callbackFunc)
    // {
    //     //表单   
    //     WWWForm form = new WWWForm();
    //     //从集合中取出所有参数，设置表单参数（AddField()).  
    //     foreach (KeyValuePair<string, object> post_arg in post)
    //     {
    //         form.AddField(post_arg.Key, post_arg.Value);
    //     }
    //     //表单传值，就是post   
    //     WWW www = new WWW(url, form);
    //     yield return www;
    //     // mJindu = www.progress;
    //     FinishReq(url + ConvertParamToString(post));
    //     if (www.error != null)
    //     {
    //         //POST请求失败  
    //         Debug.Log("error :" + www.error);
    //         //mContent = "error :" + www.error;
    //         //callbackFunc(www.error);
    //     }
    //     else
    //     {
    //         //POST请求成功  
    //         //   mContent = www.text;
    //         if(callbackFunc != null)
    //             callbackFunc(www.text, sendMessage);
    //     //    Debug.Log("Http Post :" + www.text);
    //     }
    // }


    //POST请求(Form表单传值、效率低、安全 ，)  
    private IEnumerator POST(string url, Dictionary<string, object> post, IMessage sendMessage,
        Action<string, IMessage> callbackFunc)
    {
        var data = JsonConvert.SerializeObject(post);

       // Debug.Log("向服务器发送请求：" + url + "  data: " + data);

        var bodyRaw = Encoding.UTF8.GetBytes(data);

        var request = new UnityWebRequest(url, "POST");


        request.uploadHandler = new UploadHandlerRaw(bodyRaw);

        request.SetRequestHeader("Content-Type", "application/json;charset=utf-8");

        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();
        FinishReq(url + ConvertParamToString(post));
        if (request.isNetworkError)
        {
            Debug.Log("http 请求错误:" + request.error);
        }
        else
        {
            var result = request.downloadHandler.text;
            if (callbackFunc != null)
                callbackFunc(result, sendMessage);
        }
    }


    //GET请求（url?传值、效率高、不安全 ）  
    private IEnumerator GET(string url, Dictionary<string, object> get, IMessage sendMessage,
        Action<string, IMessage> callbackFunc)
    {
        string Parameters;
        var requestURL = CreateGetData(url, get);
        //UnityWebRequest request = null;
        var request = UnityWebRequest.Get(requestURL);

        yield return request.SendWebRequest();
        FinishReq(url + ConvertParamToString(get));
        if (request.isHttpError || request.isNetworkError)
        {
            Debug.LogErrorFormat("Request Error: {0}", request.error);
            callbackFunc(request.error, sendMessage);
        }

        if (request.isDone) callbackFunc?.Invoke(request.downloadHandler.text, sendMessage);


        //
        // // testC = "getURL :" + Parameters;
        //
        //  //直接URL传值就是get  
        //  WWW www = new WWW(url + Parameters);
        //  yield return www;
        //  //  mJindu = www.progress;
        //  FinishReq(url + ConvertParamToString(get));
        //  if (www.error != null)
        //  {
        //      //GET请求失败  
        //      //   mContent = "error :" + www.error;
        //      callbackFunc(www.error, sendMessage);
        //  }
        //  else
        //  {
        //      callbackFunc(www.text, sendMessage);
        // //     Debug.Log("Http Get :" + www.text);
        //  }
    }


    private static string CreateGetData(string url, Dictionary<string, object> form)
    {
        var data = "";
        if (form != null && form.Count > 0)
            foreach (var item in form)
            {
                data += item.Key + "=";
                data += item.Value + "&";
            }

        if (url.IndexOf("?") == -1)
            url += "?";
        else
            url += "&";

        url += data.TrimEnd('&');
        return url;
    }


    private IEnumerator GETTexture(string picURL)
    {
        var wwwTexture = new WWW(picURL);

        yield return wwwTexture;

        if (wwwTexture.error != null)
        {
            //GET请求失败  
            Debug.Log("error :" + wwwTexture.error);
        }
    }

    private IEnumerator GETTextureByte(string picURL)
    {
        var www = new WWW(picURL);

        yield return www;

        if (www.error != null)
            //GET请求失败  
            Debug.Log("error :" + www.error);
        else
            //GET请求成功  
            Debug.Log("PicBytes text = " + www.text);
        /*
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(new StringReader(www.text));

            //通过索引查找子节点   
            PicByte = xmlDoc.GetElementsByTagName("base64Binary").Item(0).InnerText;
            testC = PicByte;

            mPictureByte = BttetoPic(PicByte);
            */
    }


    //图片与byte[]互转  
    public void convertPNG(Texture2D pic)
    {
        //byte[] data = pic.EncodeToPNG();
        //Debug.Log("data = " + data.Length + "|" + data[0]);
        //mConvertPNG = new Texture2D(200, 200);
        //mConvertPNG.LoadImage(data);
    }

    //byte[]与base64互转   
    private Texture2D BttetoPic(string base64)
    {
        var pic = new Texture2D(200, 200);
        //将base64转码为byte[]   
        var data = Convert.FromBase64String(base64);
        //加载byte[]图片  
        pic.LoadImage(data);

        var base64str = Convert.ToBase64String(data);
        Debug.Log("base64str = " + base64str);

        return pic;
    }
}

public class HttpReqStatus
{
    public bool finished;
    public DateTime reqTime;
    public string url;

    public HttpReqStatus(string _url, DateTime _reqTime)
    {
        url = _url;
        reqTime = _reqTime;
        finished = false;
    }
}