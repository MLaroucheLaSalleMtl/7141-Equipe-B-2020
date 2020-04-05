using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModifyStatsEffect : MonoBehaviour
{
    [Header("Use StatsModifcations Effect")]
    private int level = 0;
    public int[] duration = null;
    public float[] value = null;

    public void ModifyStats()
    {
        Actor caster = null;

        if (GetComponent<ConsumableComponant>() != null)
            caster = GetComponent<ConsumableComponant>().Caster;
        else if (GetComponent<SkillComponant>() != null)
        {
            caster = GetComponent<SkillComponant>().Caster;
            level = GetComponent<SkillComponant>().Skill.CurrentUpgrade;
        }

        StartCoroutine(caster.TemporaryBuff(caster.ResistanceDamage, duration[level], value[level]));
    }
}
