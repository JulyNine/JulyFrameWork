using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//枚举类


//游戏状态枚举
public enum GameState
{
    PreStart,
    Play,
    Reconnection,
    Pause,
}
//游戏语言枚举
public enum GameLanguage
{
    Chinese,
    ChineseTraditional,
    English,
}


public enum SkillCardUpType
{
    Default, QualityUp, LevelUp
}

public enum LoginServer
{

}

public enum Channel
{
    None,
}



public enum ActionType
{
    OptionAction,
    QTEAction,
}
public enum VariableType
{
    AlwaysTrue,
    VideoID,
    OptionID,
    QTEActionID,
    VariableID,
}
public enum JudgeType
{
    Equal,
    Less,
    Than,
}


public enum OptionActionType
{
    Single,
    Multiple,
    Loop,
    Slide,
}
public enum QTEActionType
{
    Click,
    Slide,
    Press,
}

public enum QualityType
{
    VeryLow,
    Low,
    Medium,
    High,
    VeryHigh,
    SuperHigh,
}
public enum SwipeDirection
{
    UP,
    DOWN,
    LEFT,
    RIGHT,
    NONE,
}
public enum GameVariableType
{
    ConditonVariable,
    LoveVariable,
}
public enum OptionUIType
{
    Common,
    Image,
}

/// <summary>
/// 番外视频解锁条件类型
/// </summary>
public enum ExtraVideoUnlockConditionType
{
    //完成主线剧情视频
    CompletePlotVideo = 0,
    //消耗金币
    Coins = 1,
    //达到一定好感度
    Favorability = 2,
}