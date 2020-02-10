using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  AvatarData_Net
{
    public int avatarId;
    public int userId;
    public int avatarPrototypeId;
    public int exp;
    public int quality;
    public int growRatio;
    //public int hpAdditional;
    //public int attackAdditional;
    //public int defenceAdditional;
    //public int magicAttackAdditional;
    //public int magicDefenceAdditional;
    //public int speedAdditional;
    //public int critRateAdditional;
    //public int mpAdditional;
    //public int mpRecoverAdditional;
    //public int critAdditional;
    //public int magicDodgeRateAdditional;

    public List<SkillData_Net> learnedSkillList;
    public List<SkillData_Net> usingSkillList;//卡牌技能
    public List<SkillData_Net> talentSkillList;//英雄技能
}

public class UserBasicData_Net
{
    public int userId;
    public string userName;
    public string password;
    public string nickName;
    //public string icon;
    public string avatarBattleList;
    public string sex;
    //public string createTime;

    //public int userLv;
}

public class UserData_Net
{
    public MoneyData_Net money;
    public List<SkillData_Net> userSkillList;  //玩家拥有卡牌列表
	public int isFirstGacha;
}

public class MoneyData_Net
{
    public int id;
    public int coin;
    public int userId;
    public int energy;
    public int diamond;
    public int bp;
    public int rank;
    public int consume;
    public int exp;
    public int energyTimestamp;
}



public class SkillData_Net//技能卡
{
    public int skillId;
    public int avatarId;//装备该技能的avatarID，（未被装备时为0）
    public int userId;
    public int skillPrototypeId;
    public int exp;
    public int quality;
    public bool locked;
}

public class ItemData_Net
{
    public int itemId;
    public int num;
}

public class ChapterData_Net
{
    public int chapterId;
    public int isPassed;
    public int star;
    public int remainTimes;
}

public class MapData_Net
{
    public int bigMapId;
    public string openRewardList;
}

public class ARChapterData_Net
{
    public int id;
    public int userId;
    public string today;
    public int archapterId;
}