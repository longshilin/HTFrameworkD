﻿using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HT.Framework
{
    /// <summary>
    /// 操作控制器
    /// </summary>
    [DisallowMultipleComponent]
    [InternalModule(HTFrameworkModule.Controller)]
    public sealed class ControllerManager : InternalModuleBase
    {
        /// <summary>
        /// 默认的控制模式【请勿在代码中修改】
        /// </summary>
        [SerializeField] internal ControlMode DefaultControlMode = ControlMode.FreeControl;
        /// <summary>
        /// Dotween动画的默认缓动类型【请勿在代码中修改】
        /// </summary>
        [SerializeField] internal Ease DefaultEase = Ease.Linear;
        /// <summary>
        /// Dotween动画的默认自动启动方式【请勿在代码中修改】
        /// </summary>
        [SerializeField] internal AutoPlay DefaultAutoPlay = AutoPlay.All;
        /// <summary>
        /// Dotween动画是否自动销毁【请勿在代码中修改】
        /// </summary>
        [SerializeField] internal bool IsAutoKill = true;

        /// <summary>
        /// 切换至自由控制事件
        /// </summary>
        public event HTFAction SwitchToFreeControlEvent;
        /// <summary>
        /// 切换至第一人称事件
        /// </summary>
        public event HTFAction SwitchToFirstPersonEvent;
        /// <summary>
        /// 切换至第三人称事件
        /// </summary>
        public event HTFAction SwitchToThirdPersonEvent;
        /// <summary>
        /// 射线投射事件(MouseRayTargetBase：当前射中的目标，Vector3：当前射中的点，Vector2：当前鼠标位置转换后的UGUI坐标)
        /// </summary>
        public event HTFAction<MouseRayTargetBase, Vector3, Vector2> RayEvent;
        
        private Dictionary<MouseRayTargetBase, HTFAction> _mouseClickTargets = new Dictionary<MouseRayTargetBase, HTFAction>();
        private IControllerHelper _helper;

        internal override void OnInitialization()
        {
            base.OnInitialization();
            
            DOTween.defaultEaseType = DefaultEase;
            DOTween.defaultAutoPlay = DefaultAutoPlay;
            DOTween.defaultAutoKill = IsAutoKill;
            
            _helper = Helper as IControllerHelper;
            _helper.OnInitialization(OnRay);
        }
        internal override void OnPreparatory()
        {
            base.OnPreparatory();

            TheControlMode = DefaultControlMode;
        }
        internal override void OnRefresh()
        {
            base.OnRefresh();

            _helper.OnRefresh();
            
            if (Main.m_Input.GetButtonDown(InputButtonType.MouseLeft))
            {
                if (RayTarget != null)
                {
                    if (_mouseClickTargets.ContainsKey(RayTarget))
                    {
                        _mouseClickTargets[RayTarget]?.Invoke();
                    }
                }
            }
        }
        internal override void OnTermination()
        {
            base.OnTermination();

            _helper.OnTermination();

            ClearClickListener();
        }

        /// <summary>
        /// 主摄像机
        /// </summary>
        public Camera MainCamera
        {
            get
            {
                return _helper.MainCamera;
            }
        }
        /// <summary>
        /// 控制模式
        /// </summary>
        public ControlMode TheControlMode
        {
            set
            {
                if (_helper.TheControlMode != value)
                {
                    _helper.TheControlMode = value;
                    switch (_helper.TheControlMode)
                    {
                        case ControlMode.FreeControl:
                            SwitchToFreeControlEvent?.Invoke();
                            break;
                        case ControlMode.FirstPerson:
                            SwitchToFirstPersonEvent?.Invoke();
                            break;
                        case ControlMode.ThirdPerson:
                            SwitchToThirdPersonEvent?.Invoke();
                            break;
                    }
                }
            }
            get
            {
                return _helper.TheControlMode;
            }
        }
        /// <summary>
        /// 自由控制：是否限制控制外围
        /// </summary>
        public bool NeedLimit
        {
            set
            {
                _helper.NeedLimit = value;
            }
            get
            {
                return _helper.NeedLimit;
            }
        }
        /// <summary>
        /// 自由控制：当前摄像机注视点
        /// </summary>
        public Vector3 LookPoint
        {
            get
            {
                return _helper.LookPoint;
            }
        }
        /// <summary>
        /// 自由控制：当前摄像机注视视角
        /// </summary>
        public Vector3 LookAngle
        {
            get
            {
                return _helper.LookAngle;
            }
        }
        /// <summary>
        /// 自由控制：是否启用摄像机移动控制
        /// </summary>
        public bool EnablePositionControl
        {
            get
            {
                return _helper.EnablePositionControl;
            }
            set
            {
                _helper.EnablePositionControl = value;
            }
        }
        /// <summary>
        /// 自由控制：是否启用摄像机旋转控制
        /// </summary>
        public bool EnableRotationControl
        {
            get
            {
                return _helper.EnableRotationControl;
            }
            set
            {
                _helper.EnableRotationControl = value;
            }
        }
        /// <summary>
        /// 自由控制：在UGUI目标上是否可以控制
        /// </summary>
        public bool IsCanControlOnUGUI
        {
            get
            {
                return _helper.IsCanControlOnUGUI;
            }
            set
            {
                _helper.IsCanControlOnUGUI = value;
            }
        }
        /// <summary>
        /// 自由控制：允许在输入滚轮超越距离限制时，启用摄像机移动
        /// </summary>
        public bool AllowOverstepDistance
        {
            get
            {
                return _helper.AllowOverstepDistance;
            }
            set
            {
                _helper.AllowOverstepDistance = value;
            }
        }
        /// <summary>
        /// 自由控制：摄像机是否始终保持注视目标
        /// </summary>
        public bool IsLookAtTarget
        {
            get
            {
                return _helper.IsLookAtTarget;
            }
            set
            {
                _helper.IsLookAtTarget = value;
            }
        }
        /// <summary>
        /// 当前射线击中的目标
        /// </summary>
        public MouseRayTargetBase RayTarget
        {
            get
            {
                return _helper.RayTarget;
            }
        }
        /// <summary>
        /// 当前射线击中的目标
        /// </summary>
        public GameObject RayTargetObj
        {
            get
            {
                return _helper.RayTargetObj;
            }
        }
        /// <summary>
        /// 当前射线击中的点
        /// </summary>
        public Vector3 RayHitPoint
        {
            get
            {
                return _helper.RayHitPoint;
            }
        }
        /// <summary>
        /// 是否启用高光特效
        /// </summary>
        public bool EnableHighlightingEffect
        {
            get
            {
                return _helper.EnableHighlightingEffect;
            }
            set
            {
                _helper.EnableHighlightingEffect = value;
            }
        }
        /// <summary>
        /// 是否启用鼠标射线
        /// </summary>
        public bool EnableMouseRay
        {
            get
            {
                return _helper.EnableMouseRay;
            }
            set
            {
                _helper.EnableMouseRay = value;
            }
        }
        /// <summary>
        /// 是否启用鼠标射线击中提示框
        /// </summary>
        public bool EnableMouseRayHitPrompt
        {
            get
            {
                return _helper.EnableMouseRayHitPrompt;
            }
            set
            {
                _helper.EnableMouseRayHitPrompt = value;
            }
        }

        /// <summary>
        /// 自由控制：设置控制外围限定最小值
        /// </summary>
        /// <param name="value">视野平移、旋转时，视角在x,y,z三个轴的最小值</param>
        public void SetMinLimit(Vector3 value)
        {
            _helper.SetMinLimit(value);
        }
        /// <summary>
        /// 自由控制：设置控制外围限定最大值
        /// </summary>
        /// <param name="value">视野平移、旋转时，视角在x,y,z三个轴的最大值</param>
        public void SetMaxLimit(Vector3 value)
        {
            _helper.SetMaxLimit(value);
        }
        /// <summary>
        /// 自由控制：设置摄像机注视点
        /// </summary>
        /// <param name="point">目标位置</param>
        /// <param name="damping">阻尼缓动模式</param>
        public void SetLookPoint(Vector3 point, bool damping = true)
        {
            _helper.SetLookPoint(point, damping);
        }
        /// <summary>
        /// 自由控制：设置摄像机注视角度
        /// </summary>
        /// <param name="angle">目标角度</param>
        /// <param name="damping">阻尼缓动模式</param>
        public void SetLookAngle(Vector3 angle, bool damping = true)
        {
            _helper.SetLookAngle(angle.x, angle.y, angle.z, damping);
        }
        /// <summary>
        /// 自由控制：设置摄像机注视角度
        /// </summary>
        /// <param name="angle">目标角度</param>
        /// <param name="distance">注视距离</param>
        /// <param name="damping">阻尼缓动模式</param>
        public void SetLookAngle(Vector2 angle, float distance, bool damping = true)
        {
            _helper.SetLookAngle(angle.x, angle.y, distance, damping);
        }
        /// <summary>
        /// 自由控制：进入保持追踪模式
        /// </summary>
        /// <param name="target">追踪目标</param>
        public void EnterKeepTrack(Transform target)
        {
            _helper.EnterKeepTrack(target);
        }
        /// <summary>
        /// 自由控制：退出保持追踪模式
        /// </summary>
        public void LeaveKeepTrack()
        {
            _helper.LeaveKeepTrack();
        }
        /// <summary>
        /// 设置射线发射器的焦点提示框
        /// </summary>
        /// <param name="background">提示框背景</param>
        /// <param name="content">提示文字框</param>
        /// <param name="uIType">提示框UI类型</param>
        public void SetMouseRayFocusImage(Image background, Text content, UIType uIType = UIType.Overlay)
        {
            _helper.SetMouseRayFocusImage(background, content, uIType);
        }

        /// <summary>
        /// 为挂载 MouseRayTargetBase 的目标添加鼠标左键点击事件
        /// </summary>
        /// <param name="target">目标</param>
        /// <param name="callback">点击事件回调</param>
        public void AddClickListener(GameObject target, HTFAction callback)
        {
            MouseRayTargetBase mouseRayTargetBase = target.GetComponent<MouseRayTargetBase>();
            if (mouseRayTargetBase)
            {
                if (!_mouseClickTargets.ContainsKey(mouseRayTargetBase))
                {
                    _mouseClickTargets.Add(mouseRayTargetBase, callback);
                }
            }
        }
        /// <summary>
        /// 为挂载 MouseRayTargetBase 的目标移除鼠标左键点击事件
        /// </summary>
        /// <param name="target">目标</param>
        public void RemoveClickListener(GameObject target)
        {
            MouseRayTargetBase mouseRayTargetBase = target.GetComponent<MouseRayTargetBase>();
            if (mouseRayTargetBase)
            {
                if (_mouseClickTargets.ContainsKey(mouseRayTargetBase))
                {
                    _mouseClickTargets.Remove(mouseRayTargetBase);
                }
            }
        }
        /// <summary>
        /// 清空所有点击事件
        /// </summary>
        public void ClearClickListener()
        {
            _mouseClickTargets.Clear();
        }
        
        private void OnRay(MouseRayTargetBase mouseRayTargetBase, Vector3 point, Vector2 pos)
        {
            RayEvent?.Invoke(mouseRayTargetBase, point, pos);
        }
    }

    /// <summary>
    /// 控制模式
    /// </summary>
    public enum ControlMode
    {
        /// <summary>
        /// 自由控制
        /// </summary>
        FreeControl,
        /// <summary>
        /// 第一人称控制
        /// </summary>
        FirstPerson,
        /// <summary>
        /// 第三人称控制
        /// </summary>
        ThirdPerson
    }
}