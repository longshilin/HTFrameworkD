using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HT.Framework;
using DG.Tweening;


/// <summary>
/// 新建UI逻辑类
/// </summary>
[UIResource("ui", "Assets/Prefab/UI/UILogin.prefab", "null")]
public class UILogin : UILogicResident
{
    /// <summary>
    /// 初始化
    /// </summary>
    public override void OnInit()
    {
        base.OnInit();

        UIEntity.FindChildren("Button_Play").rectTransform().AddEventListener(OnPlay);
        UIEntity.FindChildren("Button_Quit").rectTransform().AddEventListener(OnQuit);
    }

    /// <summary>
    /// 打开UI
    /// </summary>
    public override void OnOpen(params object[] args)
    {
        base.OnOpen(args);
    }

    /// <summary>
    /// 置顶UI
    /// </summary>
    public override void OnPlaceTop()
    {
        base.OnPlaceTop();
    }

    /// <summary>
    /// 关闭UI
    /// </summary>
    public override void OnClose()
    {
        base.OnClose();
    }

    /// <summary>
    /// 销毁UI
    /// </summary>
    public override void OnDestroy()
    {
        base.OnDestroy();
    }

    /// <summary>
    /// UI逻辑刷新
    /// </summary>
    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    private void OnPlay()
    {
        Main.m_Procedure.SwitchProcedure<ProcedureReady>();
    }

    private void OnQuit()
    {
        Application.Quit();
    }
}