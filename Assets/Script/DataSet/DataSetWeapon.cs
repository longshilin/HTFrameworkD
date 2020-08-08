using HT.Framework;
using System;
using UnityEngine;
/// <summary>
/// 常规主炮数据集
/// </summary>
[Serializable]
[CreateAssetMenu(menuName = "HTFramework DataSet/常规主炮数据集")]
public class DataSetWeapon : DataSetBase
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
