using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using com.ootii.Messages;
public class LoadingWindow : UIWindow {


    public Image progress;
    //当前加载进度  
    private int nowProcess;
    //异步资源
    private AsyncOperation async;
    //异步加载的场景名称
    static public string sceneName = "";

    public override void Init(object args)
    {
        base.Init(args);
   
        Set(args);
    }

    public override void Set(object args)
    {
        sceneName = "City";
        ShowProgressBar();
    }

    //外部调用的加载的方法  
    public void ShowProgressBar()
    {
        progress.fillAmount = 0f;
        //场景名不为空时 , 开启协程异步加载资源
        if (sceneName != "") StartCoroutine(LoadScene());
    }
    //异步加载scene  
    IEnumerator LoadScene()
    {
       // async = SceneManager.LoadSceneAsync(sceneName);
        async = SceneManager.LoadSceneAsync("City");
        //async = Application.LoadLevelAdditiveAsync(sceneName);
        async.allowSceneActivation = false;
        yield return async;
    }

    void OnEnable()
    {
      
    }
    void OnDisable()
    {
       
    }

    void Update()
    {
        if (async == null) return;

        int toProcess;
        // async.progress 你正在读取的场景的进度值  0---0.9      
        // 如果当前的进度小于0.9，说明它还没有加载完成，就说明进度条还需要移动      
        // 如果，场景的数据加载完毕，async.progress 的值就会等于0.9    
        if (async.progress < 0.9f)
        {
            toProcess = (int)async.progress * 100;
        }
        else
        {
            toProcess = 100;
        }
        // 如果滑动条的当前进度，小于，当前加载场景的方法返回的进度     
        if (nowProcess < toProcess)
        {
            nowProcess++;
        }

        progress.fillAmount = nowProcess / 100f;
        // 设置为true的时候，如果场景数据加载完毕，就可以自动跳转场景     
        if (nowProcess == 100)
        {
            async.allowSceneActivation = true;
           // BlockChain.Instance.Init();
            //UIManager.Instance.Clear();
        }
    }

   
}


