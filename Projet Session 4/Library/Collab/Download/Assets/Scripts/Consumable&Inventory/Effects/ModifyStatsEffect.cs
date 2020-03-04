using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyStatsEffect : ConsumableComponant
{
    [SerializeField] private float duration = 0;
    [SerializeField] private float value = 0;

    public void ModifyStats()
    {
       StartCoroutine( caster.TemporaryBoost(caster.ResistanceDamage, duration, value, Cooldown));
    }


}
