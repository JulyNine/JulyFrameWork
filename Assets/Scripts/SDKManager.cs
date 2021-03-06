﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using com.ootii.Messages;

//using GCloud.MSDK;
public class SDKManager : MonoSingleton<SDKManager>
{
    public void Init()
    {
	    /*
        MSDK.isDebug = true; // 设置 MSDK 为 Debug 模式，可以打印出更多级别的日志
        MSDK.Init ();
            // C# 示例
        MSDKLogin.LoginRetEvent += OnLoginRetEvent;
        MSDKLogin.LoginBaseRetEvent += OnLoginBaseRetEvent;
        */
    }

    public void Login()
    {
        // Unity 示例
        //string extraJson = "{\"isUE4Engine\":false, \"countDownTime\":60}";
        //MSDKLogin.LoginUI(extraJson);
        
        
        /*
        MSDKLogin.AutoLogin();
        */
    }
/*
   private void OnLoginRetEvent(MSDKLoginRet loginRet)
{
	Debug.Log ("OnLoginRetNotify in Ligin");
	string methodTag = "";
	if (loginRet.MethodNameId == (int)MSDKMethodNameID.MSDK_LOGIN_LOGIN) {
		methodTag = "Login";
	} else if (loginRet.MethodNameId == (int)MSDKMethodNameID.MSDK_LOGIN_BIND) {
		methodTag = "Bind";
	} else if (loginRet.MethodNameId == (int)MSDKMethodNameID.MSDK_LOGIN_AUTOLOGIN) {
		methodTag = "AutoLogin";
	} else if (loginRet.MethodNameId == (int)MSDKMethodNameID.MSDK_LOGIN_QUERYUSERINFO) {
		methodTag = "QueryUserInfo";
	}
    // GetLoginRet 为同步接口，不需要在回调中处理
//      else if (loginRet.MethodNameId == (int)MSDKMethodNameID.MSDK_LOGIN_GETLOGINRESULT) {
	//	methodTag = "GetLoginResult";
	//}
    else if (loginRet.MethodNameId == (int)MSDKMethodNameID.MSDK_LOGIN_LOGINWITHCONFIRMCODE) {
		methodTag = "LoginWithConfirmCode";
	}
	LogManager.Log("methodTag" + methodTag);
}			

private void OnLoginBaseRetEvent(MSDKBaseRet baseRet)
{
	Debug.Log ("OnBaseRetNotify in Login");

	if (baseRet.MethodNameId == (int)MSDKMethodNameID.MSDK_LOGIN_WAKEUP) {
		handleDiifAccount(baseRet);
	}
    else if (baseRet.MethodNameId == (int)MSDKMethodNameID.MSDK_LOGIN_LOGOUT) {
		string methodTag = "Logout";
		LogManager.Log("methodTag" + methodTag);
	}
}

// 处理异账号的逻辑
private void handleDiifAccount (MSDKBaseRet baseRet)
{
    string methodTag = "异账号";
    switch (baseRet.RetCode) {
    case MSDKError.SUCCESS: { // 本地原有票据有效，使用原有票据登录
	    LogManager.Log(methodTag + "使用原有票据登录，游戏无需处理");
            break;
        }
    case MSDKError.LOGIN_ACCOUNT_REFRESH: { // 新旧 openid 相同，票据不同。刷新登录票据
	    LogManager.Log (methodTag + "新旧 openid 相同，票据不同。刷新登录票据，游戏无需处理");
            break;
        }
    case MSDKError.LOGIN_URL_USER_LOGIN: {// 本地无openid，拉起有票据，使用新票据登录
	    LogManager.Log (methodTag + "本地无openid，拉起有票据，使用新票据登录，将自动触发切换游戏账号逻辑（SwitchUser），游戏需监控登录的回调结果");
            break;
        }
    case MSDKError.LOGIN_NEED_SELECT_ACCOUNT: {
           // SampleInstance.ShowSwithUserDialog ();
           LogManager.Log(methodTag);
            break;
        }
    case MSDKError.LOGIN_NEED_LOGIN: {
	    LogManager.Log (methodTag + "票据均无效，进入登录页面");
        }
        break;
    default:
        break;
    }
}

//销毁的时候需要移除监听
private void OnDestroy()
{
	MSDKLogin.LoginRetEvent -= OnLoginRetEvent;
	MSDKLogin.LoginBaseRetEvent -= OnLoginBaseRetEvent;
}



*/

string wifiData;
//public Text log;

public void GetWifiData()
{
    #if UNITY_ANDROID
	//AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
	//AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
	//wifiData = jo.Call<string>("ObtainWifiInfo");
	//OnWifiDataBack(wifiData);
	//jo.Call<string>("IsWifi");

	wifiData = AndroidHelper.Call<string>("ObtainWifiInfo");
	Debug.Log("wifiData:  " + wifiData);


	//bool res = AndroidHelper.Call<bool>("IsWifi");
	Boolean res =  AndroidHelper.Call<Boolean>("IsWifi");
	Debug.Log("IsWifi:  " + res);
	Boolean res2 =  AndroidHelper.Call<Boolean>("IsMobile");
	Debug.Log("IsMobile:  " + res2);
	// AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
	// AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
	// wifiData = jo.Call<string>("ObtainWifiInfo");
	// OnWifiDataBack(wifiData);
	
	//jo.Call<Boolean>("IsWifi");
    #endif

	Toast();

}

void OnWifiDataBack(string wifiData)//strength+"|"+intToIp(ip)+"|"+mac+"|"+ssid;
{
	//分析wifi信号强度
	//获取RSSI，RSSI就是接受信号强度指示。
	//得到的值是一个0到-100的区间值，是一个int型数据，其中0到-50表示信号最好，
	//-50到-70表示信号偏差，小于-70表示最差，
	//有可能连接不上或者掉线，一般Wifi已断则值为-200。
	
	//log.text += wifiData;
	string[] args = wifiData.Split('|');
	if (int.Parse(args[0]) > -50 && int.Parse(args[0]) < 0)
	{
		Debug.Log("Wifi信号强度很棒");
		//log.text += "Wifi信号强度很棒";
	}
	else if (int.Parse(args[0]) > -70 && int.Parse(args[0]) < -50)
	{
		Debug.Log("Wifi信号强度一般");
		//log.text += "Wifi信号强度一般";
	}
	else if (int.Parse(args[0]) > -150 && int.Parse(args[0]) < -70)
	{
		Debug.Log("Wifi信号强度很弱");
		//log.text += "Wifi信号强度很弱";
	}
	else if (int.Parse(args[0]) < -200)
	{
		Debug.Log("Wifi信号JJ了");
		//log.text += "Wifi信号JJ了";
	}
	// string ip = "IP：" + args[1];
	// string mac = "MAC:" + args[2];
	// string ssid = "Wifi Name:" + args[3];
	// log.text += ip;
	// log.text += mac;
	// log.text += ssid;
}


public void Toast()
{
	AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
	AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
	jo.Call("CreateToast","初始化中...");
	
}






    // Start is called before the first frame update
    void Start()
    {
	  //  checkWifiStatus = true;
	  //  GetCurrentNetwork();
    }


    public enum NetworkStatus
    {
	    None,
	    Mobile,
	    Wifi,
    }
    

    public NetworkStatus GetCurrentNetwork()
    {

	    currentNetworkStatus = (NetworkStatus)Application.internetReachability;
	    return currentNetworkStatus;
		#if UNITY_EDITOR
	    currentNetworkStatus = NetworkStatus.Wifi;
	    currentNetworkStatus = NetworkStatus.Mobile;
	    return currentNetworkStatus;
        #endif

		#if UNITY_ANDROID 
	    Boolean isWifi =  AndroidHelper.Call<Boolean>("IsWifi");
	    if (isWifi)
		    currentNetworkStatus = NetworkStatus.Wifi;
	    else
	    {
		    Boolean isMobile =  AndroidHelper.Call<Boolean>("IsMobile");
		    if (isMobile)
			    currentNetworkStatus = NetworkStatus.Mobile;
		    else
			    currentNetworkStatus = NetworkStatus.None;
	    }
        #endif

		#if UNITY_IOS 
   		 currentNetworkStatus = NetworkStatus.Wifi;
        #endif
	    Debug.Log("NetworkStatus: " + currentNetworkStatus);
	    return currentNetworkStatus;
    }

    private bool checkWifiStatus = false;
	[HideInInspector]
    public NetworkStatus currentNetworkStatus;
	private int times = 0;
	private int netstatusTransitionRange = 50;
	void Update()
    {
	    if (checkWifiStatus)
	    {
		    // if (currentNetworkStatus == NetworkStatus.Wifi || currentNetworkStatus == NetworkStatus.None)
		    // {
			//     if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
			//     {
			// 	    LostWifi();
			//     }
		    // }
			if(currentNetworkStatus != (NetworkStatus)Application.internetReachability)
			{
				Debug.LogError("NetworkStatus: " + currentNetworkStatus);
				times++;
				if(times >= netstatusTransitionRange)
				{
					if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
			    	{
				    	LostWifi();
			    	}
					times = 0;
					currentNetworkStatus = (NetworkStatus)Application.internetReachability;
				}
			}
				
	    }
    }
   
 // public void CheckNetworkStatus(bool check)
    // {
	   //  checkWifiStatus = check;
    // }
    // // Update is called once per frame
    // void Update()
    // {
	   //  if (checkWifiStatus)
	   //  {
		  //   Debug.Log("Check NetworkStatus: " + currentNetworkStatus);
		  //   if (currentNetworkStatus == NetworkStatus.Wifi || currentNetworkStatus == NetworkStatus.None)
		  //   {
			 //    if (GetCurrentNetwork() == NetworkStatus.Mobile)
			 //    {
				//     Debug.Log("Wifi lost, you use mobile data!");
				//     AndroidHelper.Call("CreateToast","Wifi lost");
				//     MessageDispatcher.SendMessage(GameEvent.LOST_WIFI);
			 //    }
		  //   }
		  //   else
		  //   {
			 //    GetCurrentNetwork();
		  //   }
    //
	   //  }
    // }

    public void StartCheckWifiStatus()
    {
	    checkWifiStatus = true;
	    //AndroidHelper.Call("StartReceiver");
    }
    public void FinishCheckWifiStatus()
    {
	    checkWifiStatus = false;
	   // AndroidHelper.Call("FinishReceiver");
    }

    public void LostWifi()
    {
	    Debug.Log("Wifi lost, you use mobile data!");
	    
	    
	    //UIManager.Instance.OpenEnforceWindow(UIEnforceWindows.NetworkStatusEnforceWindow);
	    
	    MessageDispatcher.SendMessage(GameEvent.LOST_WIFI);
    }
    
    
}
