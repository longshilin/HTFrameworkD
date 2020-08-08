﻿using HT.Framework;
using UnityEngine;
/// <summary>
/// 全局
/// </summary>
public static class Global
{
    public const string ABDataSet = "dataset";
    public const string ABLevel = "level";
    public const string ABTem = "tem";
    public const string ABUI = "ui";

    public const string WeaponPoolName = "常规主炮";
    public const string SuperWeaponPoolName = "超级武器";
    public const string HurtBloodPoolName = "伤害飘血";

    public const float AnimationSpeed = 0.1f;

    private static AudioClip KillSound;

    /// <summary>
    /// 切换背景音乐
    /// </summary>
    /// <param name="musicName">背景音乐名称</param>
    public static void ChangeBGMusic(string musicName)
    {
        Main.m_Audio.StopBackgroundMusic();
        Main.m_Audio.PlayBackgroundMusic(Main.Current.GetAudioClipParameter(musicName));
    }

    /// <summary>
    /// 播放击杀音效
    /// </summary>
    public static void PlayKillSound()
    {
        if (KillSound == null) KillSound = Main.Current.GetAudioClipParameter("敌人击杀音效");
        Main.m_Audio.PlaySingleSound(KillSound);
    }
}