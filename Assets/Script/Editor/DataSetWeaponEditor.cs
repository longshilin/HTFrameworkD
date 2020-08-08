using HT.Framework;
using UnityEditor;

[CustomEditor(typeof(DataSetWeapon))]
public class DataSetWeaponEditor : HTFEditor<DataSetWeapon>
{
    protected override bool IsEnableRuntimeData => false;

    protected override void OnInspectorDefaultGUI()
    {
        base.OnInspectorDefaultGUI();

        PropertyField("Picture", "主炮图像");
        PropertyField("ExplodePicture", "爆炸特效图像");
        PropertyField("Attack", "攻击力");
        PropertyField("Speed", "飞行力");
        PropertyField("CoolingTime", "冷却时间");
        PropertyField("ShootSound", "射击音效");
        PropertyField("ExplodeSound", "爆炸音效");
    }
}
