using HT.Framework;
using UnityEditor;

[CustomEditor(typeof(DataSetShield))]
public class DataSetShieldEditor : HTFEditor<DataSetShield>
{
    protected override bool IsEnableRuntimeData => false;

    protected override void OnInspectorDefaultGUI()
    {
        base.OnInspectorDefaultGUI();

        PropertyField("Picture", "能量特效图像");
        PropertyField("ShieldValue", "护盾值");
    }
}
