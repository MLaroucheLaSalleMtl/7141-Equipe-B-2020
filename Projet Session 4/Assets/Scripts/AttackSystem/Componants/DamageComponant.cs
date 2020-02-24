using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfDamage { Physical, Magical, True }
public class DamageComponant : MonoBehaviour
{
    
    #region Variables & Attributs
    public Actor caster;

    [Header("Damage Componant")]
    [SerializeField] private float baseDamage = 0;
    [Range(0,2)]
    [SerializeField] private float damageRatio = 0;
    [SerializeField] private TypeOfDamage damageType = 0;
    [SerializeField] private TypeOfTarget typeOfTarget = 0;
    [Header("Time Componant")]
    [SerializeField] private bool isOverTime = false;
    [SerializeField] private float timeBetweenTick = 0;
    private float countdown = 0;
    private float bonusDamage;
    private float armorPenatration;
    private float criticalChance;
    private float criticalRatio;
    private float calculatedDamage;

    private bool isDirty = false;
    #region Properties
    public float BaseDamage { get => baseDamage; set => baseDamage = value; }
    public float DamageRatio { get => damageRatio; set => damageRatio = value; }

    #endregion

    #endregion

    void Start()
    {
        if (damageType == TypeOfDamage.Physical)
            bonusDamage = caster.PowerPhysical.GetValue();
        if (damageType == TypeOfDamage.Magical)
            bonusDamage = caster.PowerMagical.GetValue();

        armorPenatration = caster.DamagePenetration.GetValue();
        criticalChance = caster.CriticalChance.GetValue();
        criticalRatio = caster.CriticalChance.GetValue();

        calculatedDamage = baseDamage + (bonusDamage * damageRatio);
    }

    void Update()
    {
        countdown = Mathf.Clamp(countdown -= Time.deltaTime, 0, timeBetweenTick);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == typeOfTarget.ToString() && !isDirty)
        {
            collision.gameObject.GetComponent<Actor>().TakeDamage(calculatedDamage,armorPenatration,criticalChance,criticalRatio,damageType.ToString());
            isDirty = true;
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == typeOfTarget.ToString() && isOverTime && countdown == 0)
        {
            collision.gameObject.GetComponent<Actor>().TakeDamage(calculatedDamage, armorPenatration, criticalChance, criticalRatio, damageType.ToString());
            countdown = timeBetweenTick;
        }
    }

}
