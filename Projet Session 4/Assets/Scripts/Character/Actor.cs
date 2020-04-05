using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    #region Variables & Attributs

    #region Basic Properties
    [Header(" - - Basic Properties - - ")]
    [SerializeField] private Recovery health = null;
    [SerializeField] private Recovery mana = null;
    [SerializeField] private Recovery barrier = null;
    [SerializeField] public bool isInvulnerable = false;
    public Recovery Health { get => health; set => health = value; }
    public Recovery Mana { get => mana; set => mana = value; }
    public Recovery Barrier { get => barrier; set => barrier = value; }

    #endregion

    #region Defensive Properties
    [Header(" - - Defensive Properties - - ")]
    [SerializeField] private Stat resistanceDamage = null;
    [SerializeField] private Stat resistancePhysical = null;
    [SerializeField] private Stat resistanceMagical = null;
    [SerializeField] private float armorStack = 0;
    [SerializeField] private const float armorReductionRatio = 5;
    [SerializeField] private Cooldown immunityFrame = null;

    [Header("Luck Based")]
    [SerializeField] private Stat evasion = null;

    public Stat ResistanceDamage { get => resistanceDamage; set => resistanceDamage = value; }
    public Stat ResistancePhysical { get => resistancePhysical; set => resistancePhysical = value; }
    public Stat ResistanceMagical { get => resistanceMagical; set => resistanceMagical = value; }
    public float ArmorStack { get => armorStack; set => armorStack = value; }
    public Cooldown ImmunityFrame { get => immunityFrame; set => immunityFrame = value; }
    public Stat Evasion { get => evasion; set => evasion = value; }
    #endregion

    #region Offensive Properties
    [Header(" - - Offensive Properties - - ")]
    [SerializeField] private Stat powerPhysical = null;
    [SerializeField] private Stat powerMagical = null;
    [SerializeField] private Stat powerThorn = null;
    [SerializeField] private Stat lifeSteal = null;
    [SerializeField] private Stat damagePenetration = null;
    [SerializeField] private Cooldown attackSpeed = null;

    [Header("Luck Based")]
    [SerializeField] private Stat criticalChance;
    [SerializeField] private Stat criticalDamage;
    public Stat PowerPhysical { get => powerPhysical; set => powerPhysical = value; }
    public Stat PowerMagical { get => powerMagical; set => powerMagical = value; }
    public Stat PowerThorn { get => powerThorn; set => powerThorn = value; }
    public Stat LifeSteal { get => lifeSteal; set => lifeSteal = value; }
    public Stat DamagePenetration { get => damagePenetration; set => damagePenetration = value; }
    public Cooldown AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
    public Stat CriticalChance { get => criticalChance; set => criticalChance = value; }
    public Stat CriticalDamage { get => criticalDamage; set => criticalDamage = value; }
    #endregion

    #region Movement Properties
    [Header(" - - Movement Properties - - ")]
    [SerializeField] private Stat movementSpeed = null;
    [SerializeField] private Stat rotationSpeed = null;
    private bool canMove = true;

    public Stat MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public bool CanMove { get => canMove; set => canMove = value; }
    public Stat RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }
    #endregion

    #region Managment Properties
    [Header(" - - Managment Properties - - ")]
    [SerializeField] private Stat cooldownReduction = null;
    [SerializeField] private Stat buffEnhancement = null;
    [SerializeField] private int gold = 0;
    public Stat CooldownReduction { get => cooldownReduction; set => cooldownReduction = value; }
    public Stat BuffEnhancement { get => buffEnhancement; set => buffEnhancement = value; }
    public int Gold { get => gold; set => gold = value; }
    #endregion

    #region Dash Properties
    [Header(" - - Dash Properties - - ")]
    [SerializeField] private bool useDash = true;
    [SerializeField] private Stat dashSpeedRatio = null;
    [SerializeField] private Recovery dash = null;
    [SerializeField] private Cooldown dashCooldown = null;
    private const float dashDuration = 0.15f;
    private float dashSpeed = 1f;
    public bool canDash = false;
    public float DashSpeed { get => dashSpeed; set => dashSpeed = value; }
    public Recovery Dash { get => dash; set => dash = value; }
    public Cooldown DashCooldown { get => dashCooldown; set => dashCooldown = value; }
    #endregion

    #region Combat Properties
    [Header(" - - Combat Properties - - ")]
    [SerializeField] private GameObject basicAttack = null;
    [SerializeField] protected float distanceOfAttack = 1;
    private const float stunMeterMaximumValue = 3;
    private float stunMeterCurrentValue = 0;
    private bool isStunned = false;
    private bool canAttack = true;
    public static float StunMeterMaximumValue => stunMeterMaximumValue;
    public float StunMeterCurrentValue { get => stunMeterCurrentValue; set => stunMeterCurrentValue = value; }
    public bool CanAttack { get => canAttack; set => canAttack = value; }
    #endregion

    [Header("Boss Properties")]
    [SerializeField] protected bool isABoss = false;

    protected Rigidbody rig;

    #endregion
    void Awake()
    {
        rig = GetComponent<Rigidbody>();

        // Recovery Initialize
        health.InitializeRecovery();
        mana.InitializeRecovery();
        barrier.InitializeRecovery();
        dash.InitializeRecovery();

        //Cooldown Initialize
        immunityFrame.ResetCountdown();
        dashCooldown.ResetCountdown();
        attackSpeed.ResetCountdown();
    }
    protected virtual void Update()
    {
        // Start Recovery
        Health.StartRecovery();
        Mana.StartRecovery();
        Barrier.StartRecovery();
        Dash.StartRecovery();

        // Start Cooldown
        ImmunityFrame.StartCooldown();
        DashCooldown.StartCooldown();
        AttackSpeed.StartCooldown();

        UpdateStun();
    }

    #region Methods

    #region offensive Methods
    public void TakeThornDamage(float Amount)
    {

        if (barrier.GetCurrentValue() >= 0)
        {
            float theLeftOver;
            barrier.DecreaseCurrentValue(Amount);
            if (barrier.GetCurrentValue() < 0)
            {
                theLeftOver = Mathf.Abs(barrier.GetCurrentValue());
                health.DecreaseCurrentValue(theLeftOver);
            }

        }
        else
        {
            health.DecreaseCurrentValue(Amount);
        }

    }
    public void TakeDamage(float Amount, float _ArmorPenetration, Stat _CriticalChance, float _CriticalRatio, string _TypeOfDamage)
    {
        if (isInvulnerable) return;
        if (!immunityFrame.IsFinish()) return;
        if (evasion.RollADice()) return; 
        if (_CriticalChance.RollADice())
        {
            Amount *= _CriticalRatio;
        }

       Amount = ApplyResistance(Amount,_ArmorPenetration, _TypeOfDamage);

        if(barrier.GetCurrentValue() >= 0)
        {
            float theLeftOver;
            barrier.DecreaseCurrentValue(Amount);
            if(barrier.GetCurrentValue() < 0)
            {
                theLeftOver = Mathf.Abs(barrier.GetCurrentValue());
                health.DecreaseCurrentValue(theLeftOver);
            }

        }
        else
        {
            health.DecreaseCurrentValue(Amount);
        }

        immunityFrame.ResetCountdown();

    }

    #endregion

    #region Defensive Methods
    private float ApplyResistance( float DamageTaken, float _ArmorPenetration, string _TypeOfDamage)
    {
        float pureReduction = (DamageTaken / 100) * resistanceDamage.GetBaseValue();
        float purePenetration = (DamageTaken / 100) * _ArmorPenetration;

        switch (_TypeOfDamage)
        {
            case "Physical":
                {
                    float reduction = (DamageTaken / 100) * resistancePhysical.GetBaseValue();

                    return DamageTaken -= (reduction + pureReduction - purePenetration);
                }
            case "Magical":
                {
                    float reduction = (DamageTaken / 100) * resistanceMagical.GetBaseValue();
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

    #region Dash Methods

    public IEnumerator ActivateDash()
    {
        if (dash.GetCurrentValue() <= 0 || !useDash) { yield break; }
        dashCooldown.ResetCountdown();
        dash.DecreaseCurrentValue(1);
        immunityFrame.ResetCountdown();
        dashSpeed += dashSpeedRatio.GetBaseValue();
        yield return new WaitForSeconds(dashDuration);
        dashSpeed -= dashSpeedRatio.GetBaseValue();
        yield return new WaitForSeconds(dashCooldown.GetBaseValue());


    }

    #endregion

    #region Managment Methods
    public IEnumerator TemporaryBuff(Stat stat, float Duration, float Value)
    {
        stat.AddModifier(Value);
        yield return new WaitForSeconds(Duration * BuffEnhancement.GetBaseValue());
        stat.RemoveModifier(Value);
    }
    public void PermanentBoost(Stat stat, float Value)
    {
        stat.AddModifier(Value);
    }

    public void IncreaseGold(int Amount)
    {
        Gold += Amount;
    }
    public void DecreaseGold(int Amount)
    {
        Gold -= Amount;
    }
    #endregion

    #region Combat Methods
    public void UseBasicAttack()
    {
        if (attackSpeed.IsFinish() && canAttack)
        {
            rig.freezeRotation = true;
            GameObject clone = Instantiate(basicAttack, transform.position + (transform.forward * distanceOfAttack), transform.rotation);
            clone.GetComponent<DamageComponant>().caster = GetComponent<Actor>();
            attackSpeed.ResetCountdown();
        }
    }
    public void UseBasicAttack(Transform target)
    {
        if (attackSpeed.IsFinish() && canAttack)
        {
            rig.freezeRotation = true;
            GameObject clone = Instantiate(basicAttack, transform.position + (transform.forward * distanceOfAttack), transform.rotation);
            clone.GetComponent<DamageComponant>().caster = GetComponent<Actor>();
            if (clone.GetComponent<MissileComponant>() != null)
                clone.GetComponent<MissileComponant>().target = target;
            attackSpeed.ResetCountdown();

        }
    }

    public IEnumerator AttackRootEffect(float Duration)
    {
        CanMove = false;
        yield return new WaitForSeconds(Duration);
        CanMove = true;
    }

    public void IncreaseStunMeter(float Amount)
    {
        stunMeterCurrentValue = Mathf.Clamp(stunMeterCurrentValue += Amount, 0, stunMeterMaximumValue);
    }
    public void UpdateStun()
    {
        if (isABoss) return;
        if(stunMeterCurrentValue != 0)
        stunMeterCurrentValue = Mathf.Clamp(stunMeterCurrentValue -= Time.deltaTime, 0, stunMeterMaximumValue);

        if (stunMeterCurrentValue > 0)
        {
            canAttack = false;
            canMove = false;
            isStunned = true;
        }
        else if (stunMeterCurrentValue == 0 && isStunned)
        {
            canAttack = true;
            canMove = true;
            isStunned = false;
        }
    }

    #endregion

    #endregion

}
