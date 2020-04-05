using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RecoveryEffect : MonoBehaviour
{
    [Header("Recovery & Level")]
    public float[] healingAmount = null;
    public float[] manaAmount = null;
    public float[] barrierAmount = null;
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

        caster.Health.IncreaseCurrentValue(healingAmount[level]);
            caster.Mana.IncreaseCurrentValue(manaAmount[level]);
            caster.Barrier.IncreaseCurrentValue(barrierAmount[level]);

    }
}
