using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryEffect : ConsumableComponant
{
    [Header("Recovery Modifier")]

    [Header("Level 1")]
    public float healingAmount = 0;
    public float manaAmount = 0;
    public float barrierAmount = 0;

    [Header("Level 2")]
    public float healingAmount2 = 0;
    public float manaAmount2 = 0;
    public float barrierAmount2 = 0;

    [Header("Level 3")]
    public float healingAmount3 = 0;
    public float manaAmount3 = 0;
    public float barrierAmount3 = 0;

    public void Restoration()
    {
        if(caster.HealLevel == 1)
        {
            caster.Health.IncreaseCurrentValue(healingAmount);
            caster.Mana.IncreaseCurrentValue(manaAmount);
            caster.Barrier.IncreaseCurrentValue(barrierAmount);
        }
        if (caster.HealLevel == 2)
        {
            caster.Health.IncreaseCurrentValue(healingAmount2);
            caster.Mana.IncreaseCurrentValue(manaAmount2);
            caster.Barrier.IncreaseCurrentValue(barrierAmount2);
        }
        if (caster.HealLevel == 3)
        {
            caster.Health.IncreaseCurrentValue(healingAmount3);
            caster.Mana.IncreaseCurrentValue(manaAmount3);
            caster.Barrier.IncreaseCurrentValue(barrierAmount3);
        }
    }
}
