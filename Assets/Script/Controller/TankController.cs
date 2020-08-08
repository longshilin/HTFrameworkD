/// <summary>
/// 坦克行为控制器
/// </summary>
public abstract class TankController
{
    /// <summary>
    /// 激活控制器
    /// </summary>
    public bool IsEnable { get; set; }
    /// <summary>
    /// 移动方向逆反
    /// </summary>
    public bool IsInverse { get; set; }
    /// <summary>
    /// 水平移动值
    /// </summary>
    public float HMove { get; protected set; }
    /// <summary>
    /// 垂直移动值
    /// </summary>
    public float VMove { get; protected set; }
    /// <summary>
    /// 常规武器射击
    /// </summary>
    public bool IsShoot { get; protected set; }
    /// <summary>
    /// 超级武器射击
    /// </summary>
    public bool IsShootSuper { get; protected set; }

    protected TankData _data;
    
    /// <summary>
    /// 初始化控制器
    /// </summary>
    public virtual void InitControll(TankData data)
    {
        _data = data;
    }

    /// <summary>
    /// 更新行为控制
    /// </summary>
    public abstract void UpdateControll();
}