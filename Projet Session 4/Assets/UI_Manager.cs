using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    private Player _Player;

   // [SerializeField] private Text[] texts = null;

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

    [Header("Panel Toggle")]
    [SerializeField] private GameObject[] panels = null;
    [SerializeField] private Selectable[] defaultButtons = null;
    void Start()
    {
        _Player = GameObject.Find("Player").GetComponent<Player>();
    }
    void Update()
    {
        txt_Power.text = _Player.Power.ToString();
        txt_PhysicalPower.text = _Player.PowerPhysical.GetValue().ToString();
        txt_CriticalDamage.text = _Player.CriticalDamage.GetValue().ToString();
        txt_HealthRegeneration.text = _Player.HealthRegenRatio.GetValue().ToString();
        txt_MovementSpeed.text = _Player.MovementSpeed.GetValue().ToString();

        txt_Vigilance.text = _Player.Vigilance.ToString();
        txt_AttackSpeed.text = _Player.AttackSpeed.GetValue().ToString();
        txt_CriticalChance.text = _Player.CriticalChance.GetValue().ToString();
        txt_Evasion.text = _Player.Evasion.GetValue().ToString();
        txt_DashRegeneration.text = _Player.DashRegenRatio.GetValue().ToString();

        txt_Mind.text = _Player.Mind.ToString();
        txt_MagicalPower.text = _Player.PowerMagical.GetValue().ToString();
        txt_DamagePenetration.text = _Player.DamagePenetration.GetValue().ToString();
        txt_Barrier.text = _Player.BarrierMaximum.GetValue().ToString();
        txt_Mana.text = _Player.ManaMaximum.GetValue().ToString();

        txt_Resilience.text = _Player.Resilience.ToString();
        txt_ThornDamage.text = _Player.DamageThorn.GetValue().ToString();
        txt_PhysicalResistance.text = _Player.ResistancePhysical.GetValue().ToString();
        txt_health.text = _Player.HealthMaximum.GetValue().ToString();
        txt_DamageResistance.text = _Player.ResistanceDamage.GetValue().ToString();

        txt_WillPower.text = _Player.WillPower.ToString();
        txt_CooldownReduction.text = _Player.CooldownReduction.GetValue().ToString();
        txt_MagicalResistance.text = _Player.ResistanceMagical.GetValue().ToString();
        txt_ManaRegeneration.text = _Player.ManaRegenRatio.GetValue().ToString();
        txt_BuffEnhancement.text = _Player.BonusBuffRatio.GetValue().ToString();

        txt_CharacteristicPoints.text = _Player.CharacteristicsPoints.ToString();
    }


    #region Methods
    public void PanelToggle(int position)
    {
        Input.ResetInputAxes();
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(position == i);
            if (position == i)
            {
                defaultButtons[i].Select();
            }

        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadScene(string level)
    {
        SceneManager.LoadScene(level);
    }
    #endregion
}
