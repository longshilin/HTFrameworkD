using HT.Framework;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 准备界面
/// </summary>
[UIResource(Global.ABUI, "Assets/Prefab/UI/UIReady.prefab", "null")]
public class UIReady : UILogicResident
{
    //中间部分（坦克选择面板的ScrollRect组件）
    private ScrollRect _scrollView_Tanks;

    //左侧部分（坦克介绍信息）
    private GameObject _panel_TankData;

    //右侧部分（坦克武器装备）
    private GameObject _panel_TankWeapon;

    //下方的进入游戏按钮
    private Button _button_Start;

    //中间部分的所有坦克选择按钮（这里其实是Toggle组件）
    private Toggle[] _tankItems;

    //用户当前是否改变了选择的坦克
    private bool _isChangeChoose;

    //当前选择的坦克
    private UITankItem _currentChoose;

    /// <summary>
    /// 当前选择的坦克
    /// </summary>
    public UITankItem CurrentChoose
    {
        get { return _currentChoose; }
        set
        {
            if (_currentChoose != value)
            {
                _currentChoose = value;
                if (_currentChoose != null)
                {
                    Image picture = _panel_TankData.GetComponentByChild<Image>("Picture/Value");
                    picture.sprite = _currentChoose.DataSet.Picture;
                    picture.gameObject.SetActive(true);
                    _panel_TankData.GetComponentByChild<Text>("Name/Value").text = _currentChoose.DataSet.name;
                    _panel_TankData.GetComponentByChild<Text>("Info/Value").text = _currentChoose.DataSet.Info;

                    _panel_TankWeapon.GetComponentByChild<Text>("Shield/Title").text = _currentChoose.DataSet.Shield ? "能量护盾：" : "能量护盾：（未装备）";
                    _panel_TankWeapon.GetComponentByChild<Text>("Shield/Name").text = _currentChoose.DataSet.Shield ? _currentChoose.DataSet.Shield.name : "";
                    _panel_TankWeapon.GetComponentByChild<Text>("Weapon/Title").text = _currentChoose.DataSet.Weapon ? "常规主炮：" : "常规主炮：（未装备）";
                    _panel_TankWeapon.GetComponentByChild<Text>("Weapon/Name").text = _currentChoose.DataSet.Weapon ? _currentChoose.DataSet.Weapon.name : "";
                    _panel_TankWeapon.GetComponentByChild<Text>("SuperWeapon/Title").text = _currentChoose.DataSet.SuperWeapon ? "超级武器：" : "超级武器：（未装备）";
                    _panel_TankWeapon.GetComponentByChild<Text>("SuperWeapon/Name").text = _currentChoose.DataSet.SuperWeapon ? _currentChoose.DataSet.SuperWeapon.name : "";
                    _panel_TankWeapon.GetComponentByChild<Text>("SuperWeapon/Info").text = _currentChoose.DataSet.SuperWeapon ? _currentChoose.DataSet.SuperWeapon.Info : "";

                    _button_Start.interactable = true;
                }
                else
                {
                    Image picture = _panel_TankData.GetComponentByChild<Image>("Picture/Value");
                    picture.gameObject.SetActive(false);
                    _panel_TankData.GetComponentByChild<Text>("Name/Value").text = "";
                    _panel_TankData.GetComponentByChild<Text>("Info/Value").text = "";

                    _panel_TankWeapon.GetComponentByChild<Text>("Shield/Title").text = "能量护盾：";
                    _panel_TankWeapon.GetComponentByChild<Text>("Shield/Name").text = "";
                    _panel_TankWeapon.GetComponentByChild<Text>("Weapon/Title").text = "常规主炮：";
                    _panel_TankWeapon.GetComponentByChild<Text>("Weapon/Name").text = "";
                    _panel_TankWeapon.GetComponentByChild<Text>("SuperWeapon/Title").text = "超级武器：";
                    _panel_TankWeapon.GetComponentByChild<Text>("SuperWeapon/Name").text = "";
                    _panel_TankWeapon.GetComponentByChild<Text>("SuperWeapon/Info").text = "";

                    _button_Start.interactable = false;
                }
            }
        }
    }

    /// <summary>
    /// 初始化
    /// </summary>
    public override void OnInit()
    {
        base.OnInit();

        _scrollView_Tanks = UIEntity.GetComponentByChild<ScrollRect>("ScrollView_Tanks");
        _panel_TankData = UIEntity.FindChildren("Panel_TankData");
        _panel_TankWeapon = UIEntity.FindChildren("Panel_TankWeapon");
        _button_Start = UIEntity.GetComponentByChild<Button>("Panel_Start/Button_Start");
        _tankItems = _scrollView_Tanks.content.GetComponentsInChildren<Toggle>();
        _isChangeChoose = false;

        for (int i = 0; i < _tankItems.Length; i++)
        {
            UITankItem tankItem = _tankItems[i].GetComponent<UITankItem>();
            _tankItems[i].interactable = tankItem != null && tankItem.DataSet != null && tankItem.IsActive;
            _tankItems[i].onValueChanged.AddListener((value) => { _isChangeChoose = true; });
        }

        UIEntity.FindChildren("Button_Back").rectTransform().AddEventListener(() => { Main.m_Procedure.SwitchProcedure<ProcedureLogin>(); });

        _button_Start.onClick.AddListener(() =>
        {
            Main.m_Procedure.GetProcedure<ProcedureGame>().ChooseDataSet = CurrentChoose.DataSet;
            Main.m_Procedure.SwitchProcedure<ProcedureChooseLevel>();
        });
    }

    /// <summary>
    /// UI逻辑刷新
    /// </summary>
    public override void OnUpdate()
    {
        base.OnUpdate();

        if (_isChangeChoose)
        {
            _isChangeChoose = false;
            RefreshChoose();
        }
    }

    /// <summary>
    /// 刷新选择
    /// </summary>
    private void RefreshChoose()
    {
        for (int i = 0; i < _tankItems.Length; i++)
        {
            if (_tankItems[i].isOn)
            {
                CurrentChoose = _tankItems[i].GetComponent<UITankItem>();
                return;
            }
        }

        CurrentChoose = null;
    }
}