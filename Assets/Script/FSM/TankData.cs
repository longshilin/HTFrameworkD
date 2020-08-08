using HT.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 坦克状态机数据
/// </summary>
public class TankData : FSMDataBase
{
    /// <summary>
    /// 刚体
    /// </summary>
    public Rigidbody2D Rigidbody { get; private set; }
    /// <summary>
    /// 渲染器
    /// </summary>
    public SpriteRenderer Renderer { get; private set; }
    /// <summary>
    /// 碰撞器
    /// </summary>
    public BoxCollider2D Collider2D { get; private set; }
    /// <summary>
    /// 坦克名称
    /// </summary>
    public string Name
    {
        get
        {
            return _dataSet.name;
        }
    }
    /// <summary>
    /// 显示图像
    /// </summary>
    public Sprite Picture
    {
        get
        {
            return _dataSet.Picture;
        }
    }
    /// <summary>
    /// 爆炸特效图像
    /// </summary>
    public Sprite[] ExplodePicture
    {
        get
        {
            return _dataSet.ExplodePicture;
        }
    }
    /// <summary>
    /// 最大生命力
    /// </summary>
    public int MaxHP { get; private set; }
    /// <summary>
    /// 生命力
    /// </summary>
    public int HP { get; private set; }
    /// <summary>
    /// 最大护盾值
    /// </summary>
    // public int MaxShield
    // {
    //     get
    //     {
    //         return _shield != null ? _shield.MaxValue : 0;
    //     }
    // }
    /// <summary>
    /// 护盾值
    /// </summary>
    // public int Shield
    // {
    //     get
    //     {
    //         return _shield != null ? _shield.Value : 0;
    //     }
    // }
    /// <summary>
    /// 行动力
    /// </summary>
    public float Speed { get; set; }
    /// <summary>
    /// 常规主炮数据
    /// </summary>
    public DataSetWeapon Weapon
    {
        get
        {
            return _dataSet.Weapon;
        }
    }
    /// <summary>
    /// 超级武器数据
    /// </summary>
    public DataSetSuperWeapon SuperWeapon
    {
        get
        {
            return _dataSet.SuperWeapon;
        }
    }
    /// <summary>
    /// 坦克行为控制器
    /// </summary>
    public TankController Controller { get; private set; }
    /// <summary>
    /// 坦克阵营
    /// </summary>
    public TankSide Side { get; private set; }
    /// <summary>
    /// 爆炸音效
    /// </summary>
    public AudioClip ExplodeSound
    {
        get
        {
            return _dataSet.ExplodeSound;
        }
    }

    //坦克数据集
    private DataSetTank _dataSet;
    // private Shield _shield;
    // private HashSet<Buff> _buffs = new HashSet<Buff>();

    /// <summary>
    /// 设置坦克数据
    /// </summary>
    // public void SetData(DataSetTank dataSet, string fsmName, TankSide side = TankSide.Enemy)
    // {
    //     Rigidbody = StateMachine.GetComponent<Rigidbody2D>();
    //     Renderer = StateMachine.GetComponent<SpriteRenderer>();
    //     Collider2D = StateMachine.GetComponent<BoxCollider2D>();
    //
    //     _dataSet = dataSet;
    //     if (_dataSet.Shield && _dataSet.Shield.ShieldValue > 0)
    //     {
    //         _shield = StateMachine.GetComponentByChild<Shield>("护盾");
    //         _shield.Generate(_dataSet.Shield);
    //     }
    //     else
    //     {
    //         _shield = null;
    //     }
    //     Side = side;
    //     MaxHP = HP = _dataSet.HP;
    //     Speed = _dataSet.Speed;
    //     StateMachine.Name = fsmName;
    //
    //     Renderer.sprite = _dataSet.Picture;
    //     Collider2D.size = _dataSet.Picture.rect.size * 0.01f;
    // }

    /// <summary>
    /// 生成行为控制器
    /// </summary>
    public void GenerateController()
    {
        if (Controller == null)
        {
            if (Side == TankSide.Enemy)
            {
                Type type = ReflectionToolkit.GetTypeInRunTimeAssemblies(_dataSet.AIController);
                Controller = Activator.CreateInstance(type) as TankController;
            }
            else
            {
                Controller = new PlayerController();
            }
            Controller.InitControll(this);
        }
    }

    /// <summary>
    /// 被敌对击中
    /// </summary>
    // public void Hit(int attack, Buff buff = null)
    // {
    //     if (Shield > 0)
    //     {
    //         _shield.Value -= attack;
    //         if (_shield.Value <= 0)
    //         {
    //             _shield.Destroy();
    //             _shield = null;
    //         }
    //     }
    //     else
    //     {
    //         HP -= attack;
    //         if (HP < 0) HP = 0;
    //     }
    //
    //     if (Side == TankSide.Enemy) Main.m_Event.Throw(Main.m_ReferencePool.Spawn<EventHitEnemy>().Fill(StateMachine, attack, buff != null ? buff.Name : ""));
    //     else Main.m_Event.Throw(Main.m_ReferencePool.Spawn<EventHitPlayer>().Fill(attack, buff != null ? buff.Name : ""));
    //
    //     if (HP == 0)
    //     {
    //         StateMachine.Final();
    //     }
    //     else
    //     {
    //         if (buff != null && !_buffs.Contains(buff))
    //         {
    //             _buffs.Add(buff);
    //             buff.Target = this;
    //             buff.Valid();
    //             StateMachine.DelayExecute(() => 
    //             {
    //                 if (buff.State == Buff.BuffState.Valid)
    //                 {
    //                     buff.Invalid();
    //                     Main.m_ReferencePool.Despawn(buff);
    //                     _buffs.Remove(buff);
    //                 }
    //             }, buff.Duration);
    //         }
    //     }
    // }

    public override void OnInit()
    {
        
    }

    public override void OnFinal()
    {
        
    }
    
    public override void OnRenewal()
    {
        
    }

    public override void OnTermination()
    {
        // StateMachine.StopAllCoroutines();
        // foreach (var buff in _buffs)
        // {
        //     if (buff.State == Buff.BuffState.Valid)
        //     {
        //         buff.Invalid();
        //         Main.m_ReferencePool.Despawn(buff);
        //     }
        // }
        // _buffs.Clear();
        // Controller = null;
        // _dataSet = null;
        // _shield = null;
    }
}

/// <summary>
/// 坦克阵营
/// </summary>
public enum TankSide
{
    /// <summary>
    /// 玩家阵营
    /// </summary>
    Player,
    /// <summary>
    /// 敌人阵营
    /// </summary>
    Enemy
}