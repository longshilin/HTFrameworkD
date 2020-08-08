using HT.Framework;
using UnityEngine;

/// <summary>
/// 玩家控制器
/// </summary>
public class PlayerController : TankController
{
    private float _weaponCoolingTime = 0;
    private float _superWeaponCoolingTime = 0;

    public override void InitControll(TankData data)
    {
        base.InitControll(data);

        IsEnable = true;
        IsInverse = false;
    }

    public override void UpdateControll()
    {
        HMove = Main.m_Input.GetAxis(InputAxisType.Horizontal);
        VMove = Main.m_Input.GetAxis(InputAxisType.Vertical);
        IsShoot = _data.Weapon && _weaponCoolingTime <= 0 && Main.m_Input.GetButtonDown("Shoot");
        IsShootSuper = _data.SuperWeapon && _superWeaponCoolingTime <= 0 && Main.m_Input.GetButtonDown("ShootSuperWeapon");

        if (IsInverse)
        {
            HMove *= -1;
            VMove *= -1;
        }

        if (IsShoot) _weaponCoolingTime = _data.Weapon.CoolingTime;
        if (IsShootSuper) _superWeaponCoolingTime = _data.SuperWeapon.CoolingTime;

        if (_weaponCoolingTime > 0)
        {
            _weaponCoolingTime -= Time.deltaTime;
            // Main.m_Event.Throw(Main.m_ReferencePool.Spawn<EventWeaponCooling>().Fill(_weaponCoolingTime / _data.Weapon.CoolingTime));
        }
        if (_superWeaponCoolingTime > 0)
        {
            _superWeaponCoolingTime -= Time.deltaTime;
            // Main.m_Event.Throw(Main.m_ReferencePool.Spawn<EventSuperWeaponCooling>().Fill(_superWeaponCoolingTime / _data.SuperWeapon.CoolingTime));
        }
    }
}
