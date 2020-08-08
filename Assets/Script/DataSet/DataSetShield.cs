using HT.Framework;
using System;
using UnityEngine;
/// <summary>
/// 护盾数据集
/// </summary>
[Serializable]
[CreateAssetMenu(menuName = "HTFramework DataSet/护盾数据集")]
public class DataSetShield : DataSetBase
{
    /// <summary>
    /// 显示图像
    /// </summary>
    public Sprite[] Picture;
    /// <summary>
    /// 护盾值
    /// </summary>
    public int ShieldValue;

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
