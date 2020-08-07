﻿using UnityEngine;
using UnityEngine.UI;

namespace HT.Framework
{
    /// <summary>
    /// 操作控制器的助手接口
    /// </summary>
    public interface IControllerHelper : IInternalModuleHelper
    {
        /// <summary>
        /// 控制模式
        /// </summary>
        ControlMode TheControlMode { get; set; }
        /// <summary>
        /// 主摄像机
        /// </summary>
        Camera MainCamera { get; set; }
        /// <summary>
        /// 自由控制：是否限制控制外围
        /// </summary>
        bool NeedLimit { get; set; }
        /// <summary>
        /// 自由控制：当前摄像机注视点
        /// </summary>
        Vector3 LookPoint { get; }
        /// <summary>
        /// 自由控制：当前摄像机注视视角
        /// </summary>
        Vector3 LookAngle { get; }
        /// <summary>
        /// 自由控制：是否启用摄像机移动控制
        /// </summary>
        bool EnablePositionControl { get; set; }
        /// <summary>
        /// 自由控制：是否启用摄像机旋转控制
        /// </summary>
        bool EnableRotationControl { get; set; }
        /// <summary>
        /// 自由控制：在UGUI目标上是否可以控制
        /// </summary>
        bool IsCanControlOnUGUI { get; set; }
        /// <summary>
        /// 自由控制：允许在输入滚轮超越距离限制时，启用摄像机移动
        /// </summary>
        bool AllowOverstepDistance { get; set; }
        /// <summary>
        /// 自由控制：摄像机是否始终保持注视目标
        /// </summary>
        bool IsLookAtTarget { get; set; }
        /// <summary>
        /// 当前射线击中的目标
        /// </summary>
        MouseRayTargetBase RayTarget { get; }
        /// <summary>
        /// 当前射线击中的目标
        /// </summary>
        GameObject RayTargetObj { get; }
        /// <summary>
        /// 当前射线击中的点
        /// </summary>
        Vector3 RayHitPoint { get; }
        /// <summary>
        /// 是否启用高光特效
        /// </summary>
        bool EnableHighlightingEffect { get; set; }
        /// <summary>
        /// 是否启用鼠标射线
        /// </summary>
        bool EnableMouseRay { get; set; }
        /// <summary>
        /// 是否启用鼠标射线击中提示框
        /// </summary>
        bool EnableMouseRayHitPrompt { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="rayAction">射线击中事件</param>
        void OnInitialization(HTFAction<MouseRayTargetBase, Vector3, Vector2> rayAction);
        /// <summary>
        /// 刷新
        /// </summary>
        void OnRefresh();
        /// <summary>
        /// 终结
        /// </summary>
        void OnTermination();

        /// <summary>
        /// 自由控制：设置控制外围限定最小值
        /// </summary>
        /// <param name="value">视野平移、旋转时，视角在x,y,z三个轴的最小值</param>
        void SetMinLimit(Vector3 value);
        /// <summary>
        /// 自由控制：设置控制外围限定最大值
        /// </summary>
        /// <param name="value">视野平移、旋转时，视角在x,y,z三个轴的最大值</param>
        void SetMaxLimit(Vector3 value);
        /// <summary>
        /// 自由控制：设置摄像机注视点
        /// </summary>
        /// <param name="point">目标位置</param>
        /// <param name="damping">阻尼缓动模式</param>
        void SetLookPoint(Vector3 point, bool damping = true);
        /// <summary>
        /// 自由控制：设置摄像机注视角度
        /// </summary>
        /// <param name="x">视角x值</param>
        /// <param name="y">视角y值</param>
        /// <param name="distance">视角距离</param>
        /// <param name="damping">阻尼缓动模式</param>
        void SetLookAngle(float x, float y, float distance, bool damping = true);
        /// <summary>
        /// 自由控制：进入保持追踪模式
        /// </summary>
        /// <param name="target">追踪目标</param>
        void EnterKeepTrack(Transform target);
        /// <summary>
        /// 自由控制：退出保持追踪模式
        /// </summary>
        void LeaveKeepTrack();
        /// <summary>
        /// 设置射线发射器的焦点提示框
        /// </summary>
        /// <param name="background">提示框背景</param>
        /// <param name="content">提示文字框</param>
        /// <param name="uIType">提示框UI类型</param>
        void SetMouseRayFocusImage(Image background, Text content, UIType uIType = UIType.Overlay);
    }
}