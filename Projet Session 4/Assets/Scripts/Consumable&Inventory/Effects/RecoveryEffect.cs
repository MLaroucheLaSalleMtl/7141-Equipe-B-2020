using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RecoveryEffect : MonoBehaviour
{
    [Header("Recovery & Level")]
    public float[] healingAmount = null;
    public float[] manaAmount = null;
    public float[] barrierAmount = null;
    [Range(0,2)]
    public float magicalRatio = 0;
   [HideInInspector] private int level = 0;


    public void Restoration()
    {
        Actor caster = null;
        if (GetComponent<ConsumableComponant>() != null)
            caster = GetComponent<ConsumableComponant>().Caster;
        else if (GetComponent<SkillComponant>() != null)
        {
            caster = GetComponent<SkillComponant>().Caster;
            level = GetComponent<SkillComponant>().Skill.CurrentUpgrade;
        }

        caster.Health.IncreaseCurrentValue(healingAmount[level] + (caster.PowerMagical.GetBaseValue() * magicalRatio));
            caster.Mana.IncreaseCurrentValue(manaAmount[level] + (caster.PowerMagical.GetBaseValue() * magicalRatio));
            caster.Barrier.IncreaseCurrentValue(barrierAmount[level] + (caster.PowerMagical.GetBaseValue() * magicalRatio));

    }
}
