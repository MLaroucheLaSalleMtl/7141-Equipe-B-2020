using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModifyStatsEffect : MonoBehaviour
{
    [Header("Use StatsModifcations Effect")]
    public int[] duration = null;
    private Actor caster;
    private int level = 0;
    public float[] value = null;
    public GameObject imageDamageBoost;
    public GameObject imageDefenseBoost;
    public GameObject imageSpeedBoost;




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

    public void ModifyStats()
    {
        StartCoroutine(caster.TemporaryBuff(caster.ResistanceDamage, duration[level], value[level], imageDefenseBoost));
    }
    public void BuffDamage()
    {
        StartCoroutine(caster.TemporaryBuff(caster.PowerPhysical, duration[level], value[level], imageDamageBoost));
    }

    public void BuffSpeed()
    {
        StartCoroutine(caster.TemporaryBuff(caster.MovementSpeed, duration[level], value[level], imageSpeedBoost));

    }
}
