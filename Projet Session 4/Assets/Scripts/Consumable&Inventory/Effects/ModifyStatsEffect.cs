using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModifyStatsEffect : ConsumableComponant
{
    [SerializeField] private float duration = 0;
    [SerializeField] private float value = 0;
    [SerializeField] private GameObject boostImage = null;

    public void ModifyStats()
    {
       StartCoroutine( caster.TemporaryBoost(caster.ResistanceDamage, duration, value, Cooldown, boostImage));
    }
    public void DamageBoost()
    {
        StartCoroutine(caster.TemporaryBoost(caster.PowerPhysical, duration, value, Cooldown, boostImage));
        StartCoroutine(caster.TemporaryBoost(caster.PowerMagical, duration, value, Cooldown, boostImage));
    }
}
