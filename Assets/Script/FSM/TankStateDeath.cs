using HT.Framework;
using UnityEngine;
/// <summary>
/// 坦克死亡状态
/// </summary>
[FiniteStateName("坦克/死亡状态")]
public class TankStateDeath : FiniteStateBase
{
    private TankData _data;
    private int _explodeIndex = 0;
    private float _explodeTimer = 0;

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
        _data.Collider2D.enabled = false;
        _explodeIndex = 0;
        _explodeTimer = Global.AnimationSpeed;
        _data.Renderer.sprite = _data.ExplodePicture[_explodeIndex];
        
        Main.m_Audio.PlayMultipleSound(_data.ExplodeSound);
        if(_data.Side == TankSide.Enemy) Global.PlayKillSound();
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
        if (_explodeTimer > 0)
        {
            _explodeTimer -= Time.deltaTime;
        }
        else
        {
            _explodeIndex += 1;
            _explodeTimer = Global.AnimationSpeed;
            if (_explodeIndex < _data.ExplodePicture.Length)
            {
                _data.Renderer.sprite = _data.ExplodePicture[_explodeIndex];
            }
            else
            {
                _data.Renderer.enabled = false;
                // if (_data.Side == TankSide.Player)
                // {
                //     Main.m_Event.Throw<EventGameFailed>();
                // }
                // else
                // {
                //     if (_data.StateMachine.Group == "Enemy")
                //     {
                //         Main.m_Event.Throw<EventEnemyKill>();
                //     }
                //     else if (_data.StateMachine.Group == "BOSS")
                //     {
                //         Main.m_Event.Throw<EventBossKill>();
                //     }
                // }
                Main.Kill(StateMachine.gameObject);
            }
        }
    }

	/// <summary>
	/// 状态终结
	/// </summary>
    public override void OnTermination()
    {
       
    }
}
