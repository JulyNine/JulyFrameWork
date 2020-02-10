using com.ootii.Messages;
using LitJson;
using System.Collections.Generic;
using System.Text;
using System;
using UnityEngine;
public partial class NetworkManager
{
    protected static NetworkManager instance;
    public static NetworkManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new NetworkManager();
            }
            return instance;
        }
    }

    //监听
    public void Init()
    {
        MessageDispatcher.AddListener(GameEvent.LOGIN_REQ, LoginReq,true);
        
        MessageDispatcher.AddListener(GameEvent.GET_CHAPTER_REQ, GetChapterReq,true);
        MessageDispatcher.AddListener(GameEvent.SET_CHAPTER_REQ, SetChapterReq,true);
        
        MessageDispatcher.AddListener(GameEvent.GET_VIDEO_REQ, GetVideoReq,true);
        MessageDispatcher.AddListener(GameEvent.SET_VIDEO_REQ, SetVideoReq,true);
        MessageDispatcher.AddListener(GameEvent.GET_OPTION_REQ, GetOptionReq,true);
        MessageDispatcher.AddListener(GameEvent.SET_OPTION_REQ, SetOptionReq,true);
        MessageDispatcher.AddListener(GameEvent.GET_QTE_REQ, GetQTEReq,true);
        MessageDispatcher.AddListener(GameEvent.SET_QTE_REQ, SetQTEReq,true);
        MessageDispatcher.AddListener(GameEvent.GET_ActionHistory_REQ, GetActionHistoryReq,true);
        MessageDispatcher.AddListener(GameEvent.SET_ActionHistory_REQ, SetActionHistoryReq,true);
        MessageDispatcher.AddListener(GameEvent.GET_Variables_REQ, GetVariablesReq,true);
        MessageDispatcher.AddListener(GameEvent.SET_Variables_REQ, SetVariablesReq,true);
        //MessageDispatcher.AddListener(GameEvent.BuyLand_REQ, BuyLandReq);
        //MessageDispatcher.AddListener(GameEvent.GetZombiesCountByUserId_REQ, GetZombiesCountByUserIdReq);
        //MessageDispatcher.AddListener(GameEvent.GetZombiesByOwner_REQ, GetZombiesByOwnerReq);

        //MessageDispatcher.AddListener(GameEvent.GetLandInfoById_REQ, GetLandInfoByIdReq);

        // MessageDispatcher.AddListener(GameEvent.CREATE_PLAYER_REQ, CreatePlayerReq);

    }


    public void BuyLandReq(IMessage rMessage)
    {
        rMessage.IsHandled = true;
        //BuyLandArgs args = rMessage.Data as BuyLandArgs;
        //GameProtocol.BuyLand_ParamDict["landId"] = args.landId.ToString();
        //GameProtocol.BuyLand_ParamDict["id"] = GameMain.Instance.userId.ToString();
        //HttpManager.Instance.Get(GameProtocol.BuyLand_URL, GameProtocol.BuyLand_ParamDict, rMessage, BuyLandAck);
    }
    public void BuyLandAck(string resJsonString, IMessage sendMessage)
    {
        Debug.Log("resJsonString" + resJsonString);

        //string[] res = resJsonString.Split(',');
        //if (res.Length < 2)
        //    return;
        //LandBlockData landData = new LandBlockData();
        //landData.id = int.Parse(res[0]);
        //landData.name = res[1];
        //landData.dna = long.Parse(res[2]);
        //landData.level = int.Parse(res[3]);
        //landData.readyTime = res[4];
        //landData.price = long.Parse(res[7]);
        //landData.owner = res[8];
        ////zombie.dna = long.Parse(res[1]);
        ////zombie.name = res[0];
        //Message lMessage = new Message();
        //lMessage.Type = GameEvent.UpdateLandInfoWindow;
        //lMessage.Sender = this;
        //lMessage.Data = landData;
        //lMessage.Delay = EnumMessageDelay.IMMEDIATE;
        //MessageDispatcher.SendMessage(lMessage);

    }


    

}