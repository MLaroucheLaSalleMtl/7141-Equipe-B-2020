using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryEffect : ConsumableComponant
{
    [Header("Recovery Modifier")]
    public float healingAmount = 0;
    public float manaAmount = 0;
    public float barrierAmount = 0;

    public void Restoration()
    {
        caster.AddHealth(healingAmount);
        caster.AddMana(manaAmount);
        caster.AddBarrier(manaAmount);
    }
}
