using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SkillComponant : MonoBehaviour
{
    #region Variables & Attributs

    private Player caster = null;
    [Header("Skill Properties")]
    [SerializeField] private Cooldown cooldown = null;
    [SerializeField] private Image imgCooldown = null;
    [SerializeField] private Text manaCost_txt = null;
    [SerializeField] private int[] manaCost = null;

    public string nameOfTheSkill = null;
    private skill skill = null;

    [Header("Effect")]
    public UnityEvent OnUseEffect;

    public Cooldown Cooldown { get => cooldown; set => cooldown = value; }
    public Player Caster { get => caster; }
    public skill Skill { get => skill; set => skill = value; }
    #endregion

    #region Unity's Methods
    void Start()
    {
        caster = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        InventorySkill skillInventory = caster.GetComponent<InventorySkill>();
        skill = skillInventory.GetASkill(nameOfTheSkill);
        InvokeRepeating("UpdateMana", 0, 0.1f);
    }

    void Update()
    {
        cooldown.StartCooldown();
        imgCooldown.fillAmount = cooldown.GetCountdownValue() / cooldown.GetBaseValue();
    }

    void UpdateMana()
    {
        manaCost_txt.text = manaCost[skill.CurrentUpgrade].ToString();
    }
    #endregion

    #region Methods 

    #region Skill Actions
    public void UseSkill()
    {
        if (!cooldown.IsFinish() || caster.Mana.GetCurrentValue() < manaCost[skill.CurrentUpgrade])
            return;
        OnUseEffect.Invoke();
        cooldown.ResetCountdown();
        caster.Mana.DecreaseCurrentValue(manaCost[skill.CurrentUpgrade]);
    }

    #endregion

    #endregion
}
