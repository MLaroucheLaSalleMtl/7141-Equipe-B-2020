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
    #endregion

    #region Resistances & Evasion
    [Header("Resistances & Evasion")]
    [SerializeField] private float armorStack = 0;
    [SerializeField] private float armorReductionRatio = 5;
    [SerializeField] private Stat resistanceDamage = null;
    [SerializeField] private Stat resistancePhysical = null;
    [SerializeField] private Stat resistanceMagical = null;
    [SerializeField] private Stat evasion = null;
    #endregion

    #region Damage
    [Header("Damage & Armor Penetration")]
    [SerializeField] private Stat damagePhysical = null;
    [SerializeField] private Stat damageMagical = null;
    [SerializeField] private Stat damageThorn = null;
    [SerializeField] private Stat damagePenetration = null;
    public Stat DamagePhysical { get => damagePhysical; set => damagePhysical = value; }
    #endregion

    #region Critical
    //[Header("Critical Chance & Damage")]
    //[SerializeField] private Stat criticalChance;
    //[SerializeField] private Stat criticalDamage;
    #endregion

    #region AttackSpeed, Cooldown & MovementSpeed
    [Header("Speed related Parameters")]
    [SerializeField] private Stat attackSpeed = null;
    [SerializeField] private Stat movementSpeed = null;
    private bool canMove = true;
    //[SerializeField] private Stat cooldownReduction = null;

    public Stat AttackSpeed { get => attackSpeed; }
    public Stat MovementSpeed { get => movementSpeed; protected set => movementSpeed = value; }
    public bool CanMove { get => canMove; protected set => canMove = value; }
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
    private bool isdashing = false;
    public Stat DashCooldown { get => dashCooldown; set => dashCooldown = value; }
    public float DashCountdown { get => dashCountdown; set => dashCountdown = value; }
    public float DashSpeed { get => dashSpeed; set => dashSpeed = value; }
    public float DashCurrent { get => dashCurrent; set => dashCurrent = value; }
    public Stat DashMaximum { get => dashMaximum; set => dashMaximum = value; }
    public Stat DashRegenRatio { get => dashRegenRatio; set => dashRegenRatio = value; }
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
    public void TakeDamage(float Amount, int DamageTypeIndex)
    {
        if (evasion.GetValue() > Random.Range(0, 100)) return;

       Amount = ApplyResistance(Amount, DamageTypeIndex);

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
    private float ApplyResistance( float Value, int ResistanceIndex)
    {
        float pureReduction = (Value / 100) * resistanceDamage.GetValue();

        switch (ResistanceIndex)
        {
            case 1:
                {
                    float reduction = (Value / 100) * resistancePhysical.GetValue();
                    return Value -= (reduction + pureReduction);
                }
            case 2:
                {
                    float reduction = (Value / 100) * resistanceMagical.GetValue();
                    return Value -= (reduction + pureReduction);  
                }
            case 3: { return Value; }
        }
        return Value;
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

    protected IEnumerator UseDash()
    {
        if (dashCurrent <= 0) { yield break; }
        dashCountdown = 0f;
        dashCurrent--;
        dashSpeed += dashSpeedRatio.GetValue();
        yield return new WaitForSeconds(dashDuration);
        dashSpeed -= dashSpeedRatio.GetValue();
        yield return new WaitForSeconds(dashCooldown.GetValue());
        print("Ready to use Dash");

    }

    protected void StartDashCooldown()
    {
            if (dashCountdown == dashCooldown.GetValue()) { return; }
            dashCountdown = Mathf.Clamp(dashCountdown += Time.deltaTime, 0, dashCooldown.GetValue());
    }




    #endregion

    protected virtual void Death() { if (HealthCurrent <= 0) Destroy(gameObject); }
    protected virtual void Movement() { }
    protected virtual void Rotation() { }
    
    #endregion

}
