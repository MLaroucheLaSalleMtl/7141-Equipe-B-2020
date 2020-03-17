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


    void Update()
    {
        
        if (GetComponent<SkillComponant>() != null)
            level = GetComponent<SkillComponant>().Skill.CurrentUpgrade;
    }
    public void ModifyStats()
    {
        Actor caster = null;

        if (GetComponent<ConsumableComponant>() != null)
            caster = GetComponent<ConsumableComponant>().Caster;
        else if (GetComponent<SkillComponant>() != null)
            caster = GetComponent<SkillComponant>().Caster;

        StartCoroutine(caster.TemporaryBuff(caster.ResistanceDamage, duration[level], value[level]));
    }
}
