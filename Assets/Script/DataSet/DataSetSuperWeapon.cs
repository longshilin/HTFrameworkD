using HT.Framework;
using System;
using UnityEngine;
/// <summary>
/// 超级武器数据集
/// </summary>
[Serializable]
[CreateAssetMenu(menuName = "HTFramework DataSet/超级武器数据集")]
public class DataSetSuperWeapon : DataSetBase
{
    /// <summary>
    /// 显示图像
    /// </summary>
    public Sprite Picture;
    /// <summary>
    /// 爆炸特效图像
    /// </summary>
    public Sprite[] ExplodePicture;
    /// <summary>
    /// 攻击力
    /// </summary>
    public int Attack;
    /// <summary>
    /// 飞行速度
    /// </summary>
    public int Speed;
    /// <summary>
    /// 冷却时间
    /// </summary>
    public float CoolingTime;
    /// <summary>
    /// 超级武器介绍
    /// </summary>
    public string Info;
    /// <summary>
    /// 附加Buff的类型
    /// </summary>
    public string BuffType = "<None>";
    /// <summary>
    /// 射击音效
    /// </summary>
    public AudioClip ShootSound;
    /// <summary>
    /// 爆炸音效
    /// </summary>
    public AudioClip ExplodeSound;

    /// <summary>
    /// 通过Json数据填充数据集
    /// </summary>
    public override void Fill(JsonData data)
    {
        
    }

	/// <summary>
    /// 将数据集打包为Json数据
    /// </summary>
    public override JsonData Pack()
    {
        JsonData data = new JsonData();
        return data;
    }
}
