using HT.Framework;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DataSetSuperWeapon))]
public class DataSetSuperWeaponEditor : HTFEditor<DataSetSuperWeapon>
{
    protected override bool IsEnableRuntimeData => false;

    protected override void OnInspectorDefaultGUI()
    {
        base.OnInspectorDefaultGUI();

        PropertyField("Picture", "超武图像");
        PropertyField("ExplodePicture", "爆炸特效图像");
        PropertyField("Attack", "攻击力");
        PropertyField("Speed", "飞行力");
        PropertyField("CoolingTime", "冷却时间");
        PropertyField("ShootSound", "射击音效");
        PropertyField("ExplodeSound", "爆炸音效");

        GUILayout.BeginHorizontal();
        GUILayout.Label("描述信息", GUILayout.Width(100));
        Target.Info = EditorGUILayout.TextArea(Target.Info);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("携带BUFF类型", GUILayout.Width(100));
        if (GUILayout.Button(Target.BuffType, EditorGlobalTools.Styles.MiniPopup))
        {
            GenericMenu gm = new GenericMenu();
            List<Type> types = ReflectionToolkit.GetTypesInRunTimeAssemblies();
            for (int i = 0; i < types.Count; i++)
            {
                // if (types[i].IsSubclassOf(typeof(Buff)))
                // {
                //     Type type = types[i];
                //     gm.AddItem(new GUIContent(type.FullName), type.FullName == Target.BuffType, () =>
                //     {
                //         Target.BuffType = type.FullName;
                //         HasChanged();
                //     });
                // }
            }
            gm.ShowAsContext();
        }
        GUILayout.EndHorizontal();

        if (GUI.changed)
        {
            HasChanged();
        }
    }
}
