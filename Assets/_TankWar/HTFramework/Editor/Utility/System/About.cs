﻿using UnityEditor;
using UnityEngine;

namespace HT.Framework
{
    internal sealed class About : HTFEditorWindow
    {
        [InitializeOnLoadMethod]
        private static void OnEditorStart()
        {
            if (EditorApplication.timeSinceStartup < 30)
            {
                if (EditorPrefs.GetBool(EditorPrefsTable.AboutIsShowOnStart, true))
                {
                    EditorApplication.delayCall += () =>
                    {
                        About about = GetWindow<About>(true, "HTFramework About", true);
                        about.position = new Rect(200, 200, 600, 350);
                        about.minSize = new Vector2(600, 350);
                        about.maxSize = new Vector2(600, 350);
                        about.Show();
                    };
                }
            }
        }
        
        private Texture _frameworkLogo;
        private Texture _csdnLogo;
        private Texture _githubLogo;
        private GUIContent _csdnGUIContent;
        private GUIContent _githubGUIContent;
        private GUIContent _pcGUIContent;
        private GUIContent _androidGUIContent;
        private GUIContent _webglGUIContent;
        private VersionInfo _versionInfo;
        private string _versionNumber;
        private bool _isShowOnStart;
        private Vector2 _scroll;
        private Color[] _LOGOColors;
        private Color _lastColor;
        private Color _currentColor;
        private int _colorIndex;
        private float _colorPos;

        protected override bool IsEnableTitleGUI
        {
            get
            {
                return false;
            }
        }

        private void OnEnable()
        {
            _frameworkLogo = AssetDatabase.LoadAssetAtPath<Texture>("Assets/HTFramework/Editor/Main/Texture/HTFrameworkLOGOTitle2.png");
            _csdnLogo = AssetDatabase.LoadAssetAtPath<Texture>("Assets/HTFramework/Editor/Main/Texture/CSDN.png");
            _githubLogo = AssetDatabase.LoadAssetAtPath<Texture>("Assets/HTFramework/Editor/Main/Texture/Github.png");
            _csdnGUIContent = new GUIContent();
            _csdnGUIContent.image = _csdnLogo;
            _csdnGUIContent.text = "CSDN";
            _githubGUIContent = new GUIContent();
            _githubGUIContent.image = _githubLogo;
            _githubGUIContent.text = "Github";
            _pcGUIContent = new GUIContent();
            _pcGUIContent.image = EditorGUIUtility.IconContent("BuildSettings.Standalone.Small").image;
            _pcGUIContent.text = "PC,Mac & Linux Standalone";
            _androidGUIContent = new GUIContent();
            _androidGUIContent.image = EditorGUIUtility.IconContent("BuildSettings.Android.Small").image;
            _androidGUIContent.text = "Android";
            _webglGUIContent = new GUIContent();
            _webglGUIContent.image = EditorGUIUtility.IconContent("BuildSettings.WebGL.Small").image;
            _webglGUIContent.text = "WebGL";
            _LOGOColors = new Color[] { new Color(1, 1, 1, 0.2f), Color.white };
            _lastColor = Color.white;
            _currentColor = _LOGOColors[0];
            _colorIndex = 0;
            _colorPos = 0;

            ReadCurrentVersion();

            EditorApplication.update += RefreshLOGOColor;
        }

        private void OnDestroy()
        {
            EditorApplication.update -= RefreshLOGOColor;
        }

        private void RefreshLOGOColor()
        {
            if (_colorPos <= 1)
            {
                _colorPos += 0.01f;
            }
            else
            {
                _colorPos = 0;
                _colorIndex += 1;
                if (_colorIndex > _LOGOColors.Length - 1) _colorIndex = 0;
                _lastColor = _currentColor;
                _currentColor = _LOGOColors[_colorIndex];
            }
            Repaint();
        }

        private void ReadCurrentVersion()
        {
            _versionInfo = AssetDatabase.LoadAssetAtPath<VersionInfo>("Assets/HTFramework/Editor/Utility/Version/Version.asset");
            _versionNumber = _versionInfo.CurrentVersion.GetFullNumber();
            _isShowOnStart = EditorPrefs.GetBool(EditorPrefsTable.AboutIsShowOnStart, true);
        }

        protected override void OnBodyGUI()
        {
            base.OnBodyGUI();

            LOGOGUI();

            AboutGUI();
        }

        private void LOGOGUI()
        {
            GUI.color = Color.Lerp(_lastColor, _currentColor, _colorPos);
            GUI.DrawTexture(new Rect(10, 0, 400, 100), _frameworkLogo);
            GUI.color = Color.white;

            GUI.Label(new Rect(80, 100, 100, 20), "Version: " + _versionNumber);
            if (GUI.Button(new Rect(200, 100, 120, 16), "Previous Versions", EditorGlobalTools.Styles.MiniPopup))
            {
                VersionViewer.OpenWindow(_versionInfo);
            }
        }

        private void AboutGUI()
        {
            GUILayout.BeginVertical();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button(_githubGUIContent, EditorGlobalTools.Styles.Label))
            {
                Application.OpenURL("https://github.com/SaiTingHu/HTFramework");
            }
            EditorGUIUtility.AddCursorRect(GUILayoutUtility.GetLastRect(), MouseCursor.Link);
            if (GUILayout.Button(_csdnGUIContent, EditorGlobalTools.Styles.Label))
            {
                Application.OpenURL("https://blog.csdn.net/qq992817263/category_9283445.html");
            }
            EditorGUIUtility.AddCursorRect(GUILayoutUtility.GetLastRect(), MouseCursor.Link);
            GUILayout.EndHorizontal();

            GUILayout.FlexibleSpace();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Release Notes: [Date " + _versionInfo.CurrentVersion.ReleaseDate + "]");
            GUILayout.EndHorizontal();

            GUILayout.BeginVertical(GUILayout.Height(100));
            _scroll = GUILayout.BeginScrollView(_scroll);
            GUILayout.Label(_versionInfo.CurrentVersion.ReleaseNotes);
            GUILayout.EndScrollView();
            GUILayout.EndVertical();
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("Supported Runtime Platforms: ");
            GUILayout.Label(_pcGUIContent, EditorGlobalTools.Styles.Wordwrapminibutton);
            GUILayout.Label(_androidGUIContent, EditorGlobalTools.Styles.Wordwrapminibutton);
            GUILayout.Label(_webglGUIContent, EditorGlobalTools.Styles.Wordwrapminibutton);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Supported Unity Versions: " + _versionInfo.CurrentVersion.UnityVersions);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Scripting Runtime Versions: " + _versionInfo.CurrentVersion.ScriptingVersions);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Api Compatibility Level: " + _versionInfo.CurrentVersion.APIVersions);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUI.color = Color.yellow;
            if (GUILayout.Button("Copyright (c) 2019 HuTao", EditorGlobalTools.Styles.Label))
            {
                Application.OpenURL("https://github.com/SaiTingHu/HTFramework/blob/master/LICENSE");
            }
            EditorGUIUtility.AddCursorRect(GUILayoutUtility.GetLastRect(), MouseCursor.Link);
            GUI.color = Color.white;
            GUILayout.FlexibleSpace();
            bool isShowOnStart = GUILayout.Toggle(_isShowOnStart, "Show On Start");
            if (isShowOnStart != _isShowOnStart)
            {
                _isShowOnStart = isShowOnStart;
                EditorPrefs.SetBool(EditorPrefsTable.AboutIsShowOnStart, _isShowOnStart);
            }
            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }
    }
}