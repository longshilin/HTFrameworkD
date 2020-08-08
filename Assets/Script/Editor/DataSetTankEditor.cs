using HT.Framework;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DataSetTank))]
public class DataSetTankEditor : HTFEditor<DataSetTank>
{
    protected override bool IsEnableRuntimeData => false;

    protected override void OnInspectorDefaultGUI()
    {
        base.OnInspectorDefaultGUI();

        PropertyField("Picture", "坦克图像");
        PropertyField("ExplodePicture", "爆炸特效图像");
        PropertyField("HP", "生命力");
        PropertyField("Speed", "行动力");
        PropertyField("Info", "介绍");
        PropertyField("Weapon", "常规主炮");
        PropertyField("SuperWeapon", "超级武器");
        PropertyField("Shield", "能量护盾");
        PropertyField("ExplodeSound", "爆炸音效");

        GUILayout.BeginHorizontal();
        GUILayout.Label("大脑（AI控制时）", GUILayout.Width(100));
        if (GUILayout.Button(Target.AIController, EditorGlobalTools.Styles.MiniPopup))
        {
            GenericMenu gm = new GenericMenu();
            List<Type> types = ReflectionToolkit.GetTypesInRunTimeAssemblies();
            for (int i = 0; i < types.Count; i++)
            {
                if (types[i].IsSubclassOf(typeof(TankController)) && types[i] != typeof(PlayerController))
                {
                    Type type = types[i];
                    gm.AddItem(new GUIContent(type.FullName), type.FullName == Target.AIController, () =>
                    {
                        Target.AIController = type.FullName;
                        HasChanged();
                    });
                }
            }
            gm.ShowAsContext();
        }
        GUILayout.EndHorizontal();
    }
}
