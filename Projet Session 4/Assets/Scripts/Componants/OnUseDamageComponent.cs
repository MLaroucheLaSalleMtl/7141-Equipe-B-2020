using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnUseDamageComponent : MonoBehaviour
{
    [Header("Use StatsModifcations Effect")]
    private Actor caster;
    private int level = 0;
    public float[] value = null;

    void Start()
    {
        if (GetComponent<ConsumableComponant>() != null)
            caster = GetComponent<ConsumableComponant>().Caster;
        else if (GetComponent<SkillComponant>() != null)
        {
            caster = GetComponent<SkillComponant>().Caster;
            level = GetComponent<SkillComponant>().Skill.CurrentUpgrade;
        }
    }

    public void TakeFlatDamage()
    {
        caster.TakeThornDamage(value[level]);
    }
}
