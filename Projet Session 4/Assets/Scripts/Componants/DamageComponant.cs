using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum TypeOfDamage { Physical, Magical, True }
public class DamageComponant : MonoBehaviour
{
    
    #region Variables & Attributs

    [Header("Damage Componant")]
    [SerializeField] protected float baseDamage = 0;
    [Range(0,2)]
    [SerializeField] protected float damageRatio = 0;
    [SerializeField] protected TypeOfDamage damageType = 0;
    [SerializeField] protected TypeOfTarget typeOfTarget = 0;

    [Header("Better use for Projectile")]
    [SerializeField] protected int numberOfTickBeforeDirty = 0;
    
    // [ Actor's Stats Part ]
    public Actor caster;
    protected float bonusDamage;
    protected float armorPenetration;
    protected float thornDamage;
    protected float lifeSteal;
    protected float criticalChance;
    protected float criticalRatio;
    protected float calculatedDamage;
    private int numberOfTick = 0;
    private bool isDirty = false;

    #region Properties
    public float BaseDamage { get => baseDamage; set => baseDamage = value; }
    public float DamageRatio { get => damageRatio; set => damageRatio = value; }

    #endregion

    #endregion

    void Start()
    {
        if (damageType == TypeOfDamage.Physical)
            bonusDamage = caster.PowerPhysical.GetBaseValue();
        if (damageType == TypeOfDamage.Magical)
            bonusDamage = caster.PowerMagical.GetBaseValue();

        armorPenetration = caster.DamagePenetration.GetBaseValue();
        criticalChance = caster.CriticalChance.GetBaseValue();
        criticalRatio = caster.CriticalChance.GetBaseValue();
        lifeSteal = caster.LifeSteal.GetBaseValue() * 0.01f;

        calculatedDamage = baseDamage + (bonusDamage * damageRatio);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != typeOfTarget.ToString() || isDirty) return; 

        Collider[] colliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale, transform.rotation, LayerMask.GetMask("Target"));
        DealDamage(colliders);

        LifeStealing(calculatedDamage);

        if(numberOfTick == numberOfTickBeforeDirty)
        {
        isDirty = true;
        }
        else
        {
            numberOfTick++;
        }

    }

    public void DealDamage(Collider[] _colliders)
    {
        foreach (Collider collider in _colliders)
        {
            if (collider.gameObject.tag == typeOfTarget.ToString())
            { 
                collider.gameObject.GetComponent<Actor>().TakeDamage(calculatedDamage, armorPenetration, collider.gameObject.GetComponent<Actor>().CriticalChance, criticalRatio, damageType.ToString());

                float thornDamage = calculatedDamage * (collider.GetComponent<Actor>().PowerThorn.GetBaseValue() * 0.01f);
                caster.TakeThornDamage(thornDamage);
            }

        }
    }

    public void LifeStealing(float AmountOfDamage)
    {
        if(lifeSteal > 0)
        {
            AmountOfDamage *= lifeSteal;
            caster.Health.IncreaseCurrentValue(AmountOfDamage);
        }
    }

    void OnDrawGizmos()
    {
        // Draw a semitransparent blue cube at the transforms position
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(transform.position, transform.localScale);
    }
}
