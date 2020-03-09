using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModifyStatsEffect : ConsumableComponant
{
    [Header("Level 1")]
    [SerializeField] private float duration = 0;
    [SerializeField] private float value = 0;

    [Header("Level 2")]
    [SerializeField] private float duration2 = 0;
    [SerializeField] private float value2 = 0;

    [Header("Level 3")]
    [SerializeField] private float duration3 = 0;
    [SerializeField] private float value3 = 0;


    //[SerializeField] private GameObject boostImage = null;

    public void ModifyStats()
    {
       StartCoroutine( caster.TemporaryBuff(caster.ResistanceDamage, duration, value));
    }
    public void DamageBoost()
    {
        if(caster.damageBuff.currentUpgrade == 1)
        {
            StartCoroutine(caster.TemporaryBuff(caster.PowerPhysical, duration, value));
            StartCoroutine(caster.TemporaryBuff(caster.PowerMagical, duration, value));
        }
        if (caster.damageBuff.currentUpgrade == 2)
        {
            StartCoroutine(caster.TemporaryBuff(caster.PowerPhysical, duration2, value2));
            StartCoroutine(caster.TemporaryBuff(caster.PowerMagical, duration2, value2));
        }
        if (caster.damageBuff.currentUpgrade == 3)
        {
            StartCoroutine(caster.TemporaryBuff(caster.PowerPhysical, duration3, value3));
            StartCoroutine(caster.TemporaryBuff(caster.PowerMagical, duration3, value3));
        }        
    }
}
