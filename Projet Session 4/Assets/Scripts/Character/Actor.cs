using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    #region Variables & Attributs

    #region Health
    [Header("Health & Recovery")]
    [SerializeField] private float healthCurrent = 0;
    [SerializeField] private Stat healthMaximum = null;
    [SerializeField] private Stat healthRegenRatio = null;
    private bool isAlive = true;
    public float HealthCurrent { get => healthCurrent; protected set => healthCurrent = value; }
    public Stat HealthMaximum { get => healthMaximum; protected set => healthMaximum = value; }
    public Stat HealthRegenRatio { get => healthRegenRatio; protected set => healthRegenRatio = value; }
    public bool IsAlive { get => isAlive;  set => isAlive = value; }
    #endregion

    #region Mana
    [Header("Mana")]
    [SerializeField] private float manaCurrent = 0;
    [SerializeField] private Stat manaMaximum = null;
    [SerializeField] private Stat manaRegenRatio = null;
    public float ManaCurrent { get => manaCurrent; set => manaCurrent = value; }
    public Stat ManaMaximum { get => manaMaximum; set => manaMaximum = value; }
    public Stat ManaRegenRatio { get => manaRegenRatio; set => manaRegenRatio = value; }
    #endregion

    #region Barrier
    [Header("Barrier")]
    [SerializeField] private float barrierCurrent = 0;
    [SerializeField] private Stat barrierMaximum = null;
    public Stat BarrierMaximum { get => barrierMaximum; set => barrierMaximum = value; }
    #endregion

    #region Resistances & Evasion
    [Header("Resistances & Evasion")]
    [SerializeField] private float armorStack = 0;
    [SerializeField] private float armorReductionRatio = 5;
    [SerializeField] private Stat resistanceDamage = null;
    [SerializeField] private Stat resistancePhysical = null;
    [SerializeField] private Stat resistanceMagical = null;
    [SerializeField] private Stat evasion = null;
    public Stat ResistanceDamage { get => resistanceDamage; set => resistanceDamage = value; }
    public Stat ResistancePhysical { get => resistancePhysical; set => resistancePhysical = value; }
    public Stat ResistanceMagical { get => resistanceMagical; set => resistanceMagical = value; }
    public Stat Evasion { get => evasion; set => evasion = value; }
    #endregion

    #region Damage
    [Header("Damage & Armor Penetration")]
    [SerializeField] private Stat powerPhysical = null;
    [SerializeField] private Stat powerMagical = null;
    [SerializeField] private Stat damageThorn = null;
    [SerializeField] private Stat damagePenetration = null;
    public Stat PowerPhysical { get => powerPhysical; set => powerPhysical = value; }
    public Stat PowerMagical { get => powerMagical; set => powerMagical = value; }
    public Stat DamageThorn { get => damageThorn; set => damageThorn = value; }
    public Stat DamagePenetration { get => damagePenetration; set => damagePenetration = value; }
    #endregion

    #region Critical
    [Header("Critical Chance & Damage")]
    [SerializeField] private Stat criticalChance;
    [SerializeField] private Stat criticalDamage;
    public Stat CriticalChance { get => criticalChance; set => criticalChance = value; }
    public Stat CriticalDamage { get => criticalDamage; set => criticalDamage = value; }
    #endregion

    #region AttackSpeed, Cooldown & MovementSpeed
    [Header("Speed related Parameters")]
    [SerializeField] private Stat attackSpeed = null;
    [SerializeField] private Stat movementSpeed = null;
    private bool canMove = true;
    [SerializeField] private Stat cooldownReduction = null;
    [SerializeField] private Stat bonusBuffRatio = null;

    public Stat AttackSpeed { get => attackSpeed; }
    public Stat MovementSpeed { get => movementSpeed; protected set => movementSpeed = value; }
    public bool CanMove { get => canMove; protected set => canMove = value; }
    public Stat CooldownReduction { get => cooldownReduction; set => cooldownReduction = value; }
    public Stat BonusBuffRatio { get => bonusBuffRatio; set => bonusBuffRatio = value; }
    #endregion

    #region Dash
    [Header("Dash related Parameters")]
    [SerializeField] private Stat dashCooldown = null;
    [SerializeField] private float dashCountdown = 5f;
    private float dashDuration = 0.15f;
    [SerializeField] private float dashCurrent = 0;
    [SerializeField] private Stat dashMaximum = null;
    [SerializeField] private Stat dashRegenRatio = null;
    private float dashSpeed = 1f;
    [SerializeField] private Stat dashSpeedRatio = null;
   // private bool isdashing = false;
    public Stat DashCooldown { get => dashCooldown; set => dashCooldown = value; }
    public float DashCountdown { get => dashCountdown; set => dashCountdown = value; }
    public float DashSpeed { get => dashSpeed; set => dashSpeed = value; }
    public float DashCurrent { get => dashCurrent; set => dashCurrent = value; }
    public Stat DashMaximum { get => dashMaximum; set => dashMaximum = value; }
    public Stat DashRegenRatio { get => dashRegenRatio; set => dashRegenRatio = value; }
    #endregion

    #region Experience & Level
    [SerializeField] private int levelCurrent;
    public int LevelCurrent { get => levelCurrent; set => levelCurrent = value; }
    #endregion

    #endregion
    void Start()
    {
        dashCountdown = dashCooldown.GetValue();
    }


    #region Methods

    #region Recovery ( Health, Mana, Barrier )
    public void AddHealth(float Amount)
    {
        HealthCurrent = Mathf.Clamp(HealthCurrent += Amount, 0, HealthMaximum.GetValue());
    }
    public void AddMana(float Amount)
    {
        manaCurrent = Mathf.Clamp(manaCurrent += Amount, 0, manaMaximum.GetValue());
    }
    public void AddBarrier(float Amount)
    {
       barrierCurrent = Mathf.Clamp(barrierCurrent += Amount,0,barrierMaximum.GetValue());
    }
    public float Regeneration(float CurrentAmount, Stat MaximumAmount, Stat RatioPerSecond)
    {
        if (CurrentAmount >= MaximumAmount.GetValue()) return CurrentAmount;
        CurrentAmount += RatioPerSecond.GetValue() * Time.deltaTime;
        return Mathf.Clamp(CurrentAmount,0,MaximumAmount.GetValue());
    }


    #endregion

    #region Damage

    public void TakeThornDamage(float Amount)
    {

    }
    public void TakeDamage(float Amount, float _ArmorPenetration, float _CriticalChance, float _CriticalRatio, string _TypeOfDamage)
    {
        if (evasion.GetValue() > Random.Range(0, 101)) return;
        if (_CriticalChance > Random.Range(0, 101))
        {
            Amount *= _CriticalRatio;
            print("Critical Strike!");
        }
       Amount = ApplyResistance(Amount,_ArmorPenetration, _TypeOfDamage);

        if(barrierCurrent >= 0)
        {
            float theLeftOver;
            barrierCurrent -= Amount;
            if(barrierCurrent < 0)
            {
                theLeftOver = Mathf.Abs(barrierCurrent);
                HealthCurrent -= theLeftOver;
            }

        }
        else
        {
        HealthCurrent -= Amount;
        }
    }
    #endregion

    #region Armor and Resistance 
    private float ApplyResistance( float DamageTaken, float _ArmorPenetration, string _TypeOfDamage)
    {
        float pureReduction = (DamageTaken / 100) * resistanceDamage.GetValue();
        float purePenetration = (DamageTaken / 100) * _ArmorPenetration;

        switch (_TypeOfDamage)
        {
            case "Physical":
                {
                    float reduction = (DamageTaken / 100) * resistancePhysical.GetValue();

                    return DamageTaken -= (reduction + pureReduction - purePenetration);
                }
            case "Magical":
                {
                    float reduction = (DamageTaken / 100) * resistanceMagical.GetValue();
                    return DamageTaken -= (reduction + pureReduction - purePenetration);  
                }
            case "True": { return DamageTaken; }
        }
        return DamageTaken -= (pureReduction - purePenetration);
    }
    public void IncreaseArmorStack(int Amount)
    {
        if (armorStack >= 5)
        {
            armorStack = 5;
            return;
        }
        resistanceDamage.AddModifier( Amount * armorReductionRatio);
        armorStack += Amount;
    }
    public void DecreaseArmorStack(int Amount)
    {
        if (armorStack <= 0)
        {
            armorStack = 0;
            return;
        }
        resistanceDamage.RemoveModifier(Amount * armorReductionRatio);
        armorStack -= Amount;
    }

    #endregion

    #region Attack Animation & Feeling
    public IEnumerator AttackRootEffect(float Duration)
    {
        CanMove = false;
        yield return new WaitForSeconds(Duration);
        CanMove = true;
    }
    #endregion

    #region Dash

    public IEnumerator UseDash()
    {
        if (dashCurrent <= 0) { yield break; }
        dashCountdown = 0f;
        dashCurrent--;
        dashSpeed += dashSpeedRatio.GetValue();
        yield return new WaitForSeconds(dashDuration);
        dashSpeed -= dashSpeedRatio.GetValue();
        yield return new WaitForSeconds(dashCooldown.GetValue());


    }

    public void StartDashCooldown()
    {
            if (dashCountdown == dashCooldown.GetValue()) { return; }
            dashCountdown = Mathf.Clamp(dashCountdown += Time.deltaTime, 0, dashCooldown.GetValue());
    }




    #endregion

    #region Level
    protected void LevelUp()
    {
        levelCurrent++;
    }
    protected void SetLevel(int Value)
    {
        levelCurrent = Value;
    }

    #endregion
    protected virtual void Death() { if (HealthCurrent <= 0) Destroy(gameObject); }
    protected virtual void Movement() { }
    protected virtual void Rotation() { }
    
    #endregion

}
