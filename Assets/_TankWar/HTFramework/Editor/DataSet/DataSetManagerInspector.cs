﻿using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace HT.Framework
{
    [CustomEditor(typeof(DataSetManager))]
    [GithubURL("https://github.com/SaiTingHu/HTFramework")]
    [CSDNBlogURL("https://wanderer.blog.csdn.net/article/details/89395574")]
    internal sealed class DataSetManagerInspector : InternalModuleInspector<DataSetManager>
    {
        private IDataSetHelper _dataSetHelper;
        private Dictionary<Type, List<DataSetBase>> _dataSets;

        protected override string Intro
        {
            get
            {
                return "DataSet Manager, create, modify, delete all data sets!";
            }
        }

        protected override Type HelperInterface
        {
            get
            {
                return typeof(IDataSetHelper);
            }
        }

        protected override void OnRuntimeEnable()
        {
            base.OnRuntimeEnable();

            _dataSetHelper = Target.GetType().GetField("_helper", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(Target) as IDataSetHelper;
            _dataSets = _dataSetHelper.GetType().GetField("_dataSets", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(_dataSetHelper) as Dictionary<Type, List<DataSetBase>>;
        }

        protected override void OnInspectorRuntimeGUI()
        {
            base.OnInspectorRuntimeGUI();

            if (_dataSets.Count == 0)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("No Runtime Data!");
                GUILayout.EndHorizontal();
            }

            foreach (var item in _dataSets)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("DataSet Type: " + item.Key.Name);
                GUILayout.EndHorizontal();

                if (item.Value.Count == 0)
                {
                    GUILayout.BeginHorizontal();
                    GUILayout.Space(20);
                    GUILayout.Label("Count 0！");
                    GUILayout.EndHorizontal();
                }
                else
                {
                    for (int i = 0; i < item.Value.Count; i++)
                    {
                        GUILayout.BeginHorizontal();
                        GUILayout.Space(20);
                        EditorGUILayout.ObjectField(item.Value[i], typeof(DataSetBase), true);
                        GUILayout.EndHorizontal();
                    }
                }
            }
        }
    }
}