using HT.Framework;
using UnityEngine;
/// <summary>
/// 坦克常规状态
/// </summary>
[FiniteStateName("坦克/常规状态")]
public class TankStateNormal : FiniteStateBase
{
    private TankData _data;
    private Vector2 _move;
    private float _rotate = 0;
    
    /// <summary>
    /// 初始化
    /// </summary>
    public override void OnInit()
    {
        _data = StateMachine.CurrentData.Cast<TankData>();
    }

    /// <summary>
    /// 进入状态
    /// </summary>
    /// <param name="lastState">上一个离开的状态</param>
    public override void OnEnter(FiniteStateBase lastState)
    {
        _data.GenerateController();
        _data.Collider2D.enabled = true;
    }

    /// <summary>
    /// 离开状态
    /// </summary>
    /// <param name="nextState">下一个进入的状态</param>
    public override void OnLeave(FiniteStateBase nextState)
    {
        
    }

	/// <summary>
	/// 切换状态的动机
	/// </summary>
    public override void OnReason()
    {
       
    }

	/// <summary>
	/// 状态帧刷新
	/// </summary>
    public override void OnUpdate()
    {
        _data.Controller.UpdateControll();

        if (Mathf.Abs(_data.Controller.HMove) > Mathf.Abs(_data.Controller.VMove))
        {
            if (!_data.Controller.HMove.Approximately(0))
            {
                MoveHorizontal(_data.Controller.HMove * Time.deltaTime);
            }
        }
        else
        {
            if (!_data.Controller.VMove.Approximately(0))
            {
                MoveVertical(_data.Controller.VMove * Time.deltaTime);
            }
        }

        if (_data.Controller.IsShoot) Shoot();
        if (_data.Controller.IsShootSuper) ShootSuper();
    }

	/// <summary>
	/// 状态终结
	/// </summary>
    public override void OnTermination()
    {
       
    }

    /// <summary>
    /// 水平移动
    /// </summary>
    /// <param name="value">移动值</param>
    private void MoveHorizontal(float value)
    {
        _move.x = StateMachine.transform.position.x + value * _data.Speed;
        _move.y = StateMachine.transform.position.y;
        _data.Rigidbody.MovePosition(_move);

        if (value > 0)
        {
            _rotate = -90;
        }
        else
        {
            _rotate = 90;
        }

        if (StateMachine.transform.rotation.eulerAngles.z != _rotate)
        {
            StateMachine.transform.rotation = Quaternion.Euler(0, 0, _rotate);
        }
    }

    /// <summary>
    /// 垂直移动
    /// </summary>
    /// <param name="value">移动值</param>
    private void MoveVertical(float value)
    {
        _move.x = StateMachine.transform.position.x;
        _move.y = StateMachine.transform.position.y + value * _data.Speed;
        _data.Rigidbody.MovePosition(_move);

        if (value > 0)
        {
            _rotate = 0;
        }
        else
        {
            _rotate = 180;
        }

        if (StateMachine.transform.rotation.eulerAngles.z != _rotate)
        {
            StateMachine.transform.rotation = Quaternion.Euler(0, 0, _rotate);
        }
    }

    /// <summary>
    /// 普通射击
    /// </summary>
    private void Shoot()
    {
        // if (Main.m_ObjectPool.IsExistSpawnPool(Global.WeaponPoolName))
        // {
        //     GameObject obj = Main.m_ObjectPool.Spawn(Global.WeaponPoolName);
        //     Weapon weapon = obj.GetComponent<Weapon>();
        //     weapon.Fire(StateMachine, _data.Weapon, _rotate, _data.Side == TankSide.Player);
        // }
    }

    /// <summary>
    /// 超级武器射击
    /// </summary>
    private void ShootSuper()
    {
        // if (Main.m_ObjectPool.IsExistSpawnPool(Global.SuperWeaponPoolName))
        // {
        //     GameObject obj = Main.m_ObjectPool.Spawn(Global.SuperWeaponPoolName);
        //     SuperWeapon weapon = obj.GetComponent<SuperWeapon>();
        //     weapon.Fire(StateMachine, _data.SuperWeapon, _rotate, true);
        // }
    }
}