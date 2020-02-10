using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using com.ootii.Messages;
/// <summary>
/// 临时全局数据资源管理
/// </summary>
public class TempDataManager
{
    // 获取单例
    private static TempDataManager m_instance = null;   // 单例
    public static TempDataManager Instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = new TempDataManager();
            }
            return m_instance;
        }
    }

}