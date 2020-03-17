using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class UI_Manager : MonoBehaviour
{
    private Player _Player;
    private int currentPanel = -1;
    private int currentPanelSkill = -1;


    [SerializeField] private Text txt_Power = null;
    [SerializeField] private Text txt_PhysicalPower = null;
    [SerializeField] private Text txt_CriticalDamage = null;
    [SerializeField] private Text txt_HealthRegeneration = null;
    [SerializeField] private Text txt_MovementSpeed = null;

    [SerializeField] private Text txt_Vigilance = null;
    [SerializeField] private Text txt_AttackSpeed = null;
    [SerializeField] private Text txt_CriticalChance = null;
    [SerializeField] private Text txt_Evasion = null;
    [SerializeField] private Text txt_DashRegeneration = null;

    [SerializeField] private Text txt_Mind = null;
    [SerializeField] private Text txt_MagicalPower = null;
    [SerializeField] private Text txt_DamagePenetration = null;
    [SerializeField] private Text txt_Barrier = null;
    [SerializeField] private Text txt_Mana = null;

    [SerializeField] private Text txt_Resilience = null;
    [SerializeField] private Text txt_ThornDamage = null;
    [SerializeField] private Text txt_PhysicalResistance = null;
    [SerializeField] private Text txt_health = null;
    [SerializeField] private Text txt_DamageResistance = null;

    [SerializeField] private Text txt_WillPower = null;
    [SerializeField] private Text txt_CooldownReduction = null;
    [SerializeField] private Text txt_MagicalResistance = null;
    [SerializeField] private Text txt_ManaRegeneration = null;
    [SerializeField] private Text txt_BuffEnhancement = null;
    // Start is called before the first frame update
    [SerializeField] private Text txt_CharacteristicPoints = null;

    [Header("Character UI")]
    [SerializeField] private Image img_HealthBar = null;
    [SerializeField] private Text txt_HealthBar = null
        ;
    [SerializeField] private Image img_ManaBar = null;
    [SerializeField] private Text txt_ManaBar = null;

    [SerializeField] private Image img_BarrierBar = null;
    [SerializeField] private Text txt_BarrierBar = null;

    [SerializeField] private Image img_XpBar = null;
    [SerializeField] private Text txt_Level = null;

    [SerializeField] private Text txt_NumbOfArmorStack = null;

    [SerializeField] private Text txt_DashCount = null;

    [SerializeField] private Text txt_Gold = null;

    [Header("Panel Toggle")]
    [SerializeField] private GameObject[] panels = null;
    [SerializeField] private Selectable[] defaultButtons = null;

    [Header("Panel Toggle For Skills")]
    [SerializeField] private GameObject[] panelsSkill = null;

    [Header("Skill")]
    [SerializeField] private Text txt_SkillPointsNumber = null;
    [SerializeField] private Text txt_LevelSkill = null;
    [SerializeField] private Image img_XpBarSkill = null;
    [SerializeField] private GameObject dashUI = null;

    void Start()
    {
        Time.timeScale = 1;
        _Player = GameObject.Find("Player").GetComponent<Player>();
        dashUI.SetActive(false);
    }
    void Update()
    {
        txt_Gold.text = _Player.Gold.ToString();

        txt_Power.text = _Player.Power.GetBaseValue().ToString();
        txt_PhysicalPower.text = _Player.PowerPhysical.GetBaseValue().ToString();
        txt_CriticalDamage.text = _Player.CriticalDamage.GetBaseValue().ToString();
        txt_HealthRegeneration.text = _Player.Health.recoveryValue.GetBaseValue().ToString();
        txt_MovementSpeed.text = _Player.MovementSpeed.GetBaseValue().ToString();

        txt_Vigilance.text = _Player.Vigilance.GetBaseValue().ToString();
        txt_AttackSpeed.text = _Player.AttackSpeed.GetBaseValue().ToString();
        txt_CriticalChance.text = _Player.CriticalChance.GetBaseValue().ToString();
        txt_Evasion.text = _Player.Evasion.GetBaseValue().ToString();
        txt_DashRegeneration.text = _Player.Dash.recoveryValue.GetBaseValue().ToString();

        txt_Mind.text = _Player.Mind.GetBaseValue().ToString();
        txt_MagicalPower.text = _Player.PowerMagical.GetBaseValue().ToString();
        txt_DamagePenetration.text = _Player.DamagePenetration.GetBaseValue().ToString();
        txt_Barrier.text = _Player.Barrier.GetBaseValue().ToString();
        txt_Mana.text = _Player.Mana.GetBaseValue().ToString();

        txt_Resilience.text = _Player.Resilience.GetBaseValue().ToString();
        txt_ThornDamage.text = _Player.PowerThorn.GetBaseValue().ToString();
        txt_PhysicalResistance.text = _Player.ResistancePhysical.GetBaseValue().ToString();
        txt_health.text = _Player.Health.GetBaseValue().ToString();
        txt_DamageResistance.text = _Player.ResistanceDamage.GetBaseValue().ToString();

        txt_WillPower.text = _Player.WillPower.GetBaseValue().ToString();
        txt_CooldownReduction.text = _Player.CooldownReduction.GetBaseValue().ToString();
        txt_MagicalResistance.text = _Player.ResistanceMagical.GetBaseValue().ToString();
        txt_ManaRegeneration.text = _Player.Mana.recoveryValue.GetBaseValue().ToString();
        txt_BuffEnhancement.text = _Player.BuffEnhancement.GetBaseValue().ToString();

        txt_CharacteristicPoints.text = _Player.CharacteristicsPoints.ToString();

        img_HealthBar.fillAmount = _Player.Health.GetCurrentValue() / _Player.Health.GetBaseValue();
        txt_HealthBar.text = _Player.Health.GetCurrentValue().ToString("F0") + " / " + _Player.Health.GetBaseValue().ToString("F0");

        img_ManaBar.fillAmount = _Player.Mana.GetCurrentValue() / _Player.Mana.GetBaseValue();
        txt_ManaBar.text = _Player.Mana.GetCurrentValue().ToString("F0") + " / " + _Player.Mana.GetBaseValue().ToString("F0");

        img_BarrierBar.fillAmount = _Player.Barrier.GetCurrentValue() / _Player.Health.GetBaseValue();
        txt_BarrierBar.text = _Player.Barrier.GetCurrentValue().ToString("F0") + " / " + _Player.Barrier.GetBaseValue().ToString("F0");

        txt_NumbOfArmorStack.text = ": " + _Player.ArmorStack.ToString();
        txt_DashCount.text = ": " + (int)_Player.Dash.GetCurrentValue();

        img_XpBar.fillAmount = _Player.ExperienceCurrent / _Player.ExperienceMaximum;

        txt_Level.text = "Level : " + _Player.LevelCurrent.ToString();

        //Skills
        txt_LevelSkill.text = "Level : " + _Player.LevelCurrent.ToString();
        img_XpBarSkill.fillAmount = _Player.ExperienceCurrent / _Player.ExperienceMaximum;
        txt_SkillPointsNumber.text = "SkillPoints: " + _Player.SkillPoints.ToString();
        dashUI.SetActive(true);
    }

    #region Methods
    public void PanelToggle(int position)
    {
        Input.ResetInputAxes();
        for (int i = 0; i < panels.Length; i++)
        {
            
            panels[i].SetActive(position == i);
            currentPanel = position;
            if (position == i)
            {
                defaultButtons[i].Select();
            }
            

        }
        if(position >= 0 && position != 5 && position != 7)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        _Player.GetComponent<Actor>().CanAttack = true;
    }
    public void PanelToggleSkills(int positionSkill)
    {
        Input.ResetInputAxes();
        for (int i = 0; i < panelsSkill.Length; i++)
        {
            panelsSkill[i].SetActive(positionSkill == i);
            currentPanelSkill = positionSkill;
        }
        Time.timeScale = 0;
    }

    public void Cancel(InputAction.CallbackContext context)
    {
        if (GameManager.gameOver) return;
        if (currentPanel == 0 && context.started)
        {
            PanelToggle(-1);
            currentPanel = -1;
            return;
        }
        else
        if (context.started)
        {
            PanelToggle(0);
            return;
        }
    }
    public void OpenCharacteristics(InputAction.CallbackContext context)
    {
        if (GameManager.gameOver) return;
        if (currentPanel == 5 && context.started)
        {
            PanelToggle(-1);
            currentPanel = -1;
            return;
        }
        else
        if (context.started)
        {
            PanelToggle(5);
            return;
        }
    }
    public void OpenSkill(InputAction.CallbackContext context)
    {
        if (GameManager.gameOver) return;
        if (currentPanel == 7 && context.started)
        {
            PanelToggle(-1);
            currentPanel = -1;
            return;
        }
        else
        if (context.started)
        {
            PanelToggle(7);
            return;
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadScene(string level)
    {
        Time.timeScale = 1;
        GameManager.NumberOfEnemy = 0;
        SceneManager.LoadScene(level);
    }

    #endregion
}
