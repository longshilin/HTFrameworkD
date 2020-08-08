using HT.Framework;
using System;
using UnityEngine;
/// <summary>
/// 坦克数据集
/// </summary>
[Serializable]
[CreateAssetMenu(menuName = "HTFramework DataSet/坦克数据集")]
public class DataSetTank : DataSetBase
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
    /// 生命力
    /// </summary>
    public int HP;
    /// <summary>
    /// 行动力
    /// </summary>
    public float Speed;
    /// <summary>
    /// 坦克介绍
    /// </summary>
    public string Info;
    /// <summary>
    /// 常规主炮
    /// </summary>
    public DataSetWeapon Weapon;
    /// <summary>
    /// 超级武器
    /// </summary>
    public DataSetSuperWeapon SuperWeapon;
    /// <summary>
    /// 能量护盾
    /// </summary>
    public DataSetShield Shield;
    /// <summary>
    /// 作为AI时的控制器
    /// </summary>
    public string AIController;
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
