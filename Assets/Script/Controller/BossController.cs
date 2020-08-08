using HT.Framework;
using UnityEngine;
/// <summary>
/// 敌人控制器（Boss）
/// </summary>
public class BossController : TankController
{
    private float _weaponCoolingTime = 0;
    private float _superWeaponCoolingTime = 0;
    private bool _isChangeOrder = true;
    private float[] _moveOrders = new float[3] { -1f, 0f, 1f };

    public override void InitControll(TankData data)
    {
        base.InitControll(data);

        IsEnable = true;
    }

    public override void UpdateControll()
    {
        if (!IsEnable)
        {
            HMove = 0;
            VMove = 0;
            IsShoot = false;
            IsShootSuper = false;
            return;
        }

        if (_isChangeOrder)
        {
            _isChangeOrder = false;
            HMove = MathfToolkit.RandomValue(_moveOrders);
            VMove = MathfToolkit.RandomValue(_moveOrders);
            _data.StateMachine.DelayExecute(() => { _isChangeOrder = true; }, Random.Range(1f, 3f));
        }

        IsShoot = _data.Weapon && _weaponCoolingTime <= 0;
        IsShootSuper = _data.SuperWeapon && _superWeaponCoolingTime <= 0;

        if (IsShoot) _weaponCoolingTime = _data.Weapon.CoolingTime;
        if (IsShootSuper) _superWeaponCoolingTime = _data.SuperWeapon.CoolingTime + Random.Range(2f, 5f);

        if (_weaponCoolingTime > 0)
        {
            _weaponCoolingTime -= Time.deltaTime;
        }
        if (_superWeaponCoolingTime > 0)
        {
            _superWeaponCoolingTime -= Time.deltaTime;
        }
    }
}