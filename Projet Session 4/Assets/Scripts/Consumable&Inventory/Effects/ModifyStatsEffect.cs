using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModifyStatsEffect : MonoBehaviour
{
    [Header("Use StatsModifcations Effect")]
    public int[] duration = null;
    private Actor caster;
    [HideInInspector] public int level = 0;
    public float[] value = null;
    public GameObject imageDamageBoost;
    public GameObject imageDefenseBoost;
    public GameObject imageSpeedBoost;




    void Start()
    {

    }

    public void ModifyStats()
    {
        SetLevelInside();
        StartCoroutine(caster.TemporaryBuff(caster.ResistanceDamage, duration[level], value[level], imageDefenseBoost));
    }
    public void BuffDamage()
    {
        SetLevelInside();
        StartCoroutine(caster.TemporaryBuff(caster.PowerPhysical, duration[level], value[level] * caster.PowerPhysical.GetBaseValue(), imageDamageBoost));
    }

    public void BuffSpeed()
    {
        SetLevelInside();
        StartCoroutine(caster.TemporaryBuff(caster.MovementSpeed, duration[level], value[level], imageSpeedBoost));

    }

    void SetLevelInside()
    {
        if (GetComponent<ConsumableComponant>() != null)
            caster = GetComponent<ConsumableComponant>().Caster;
        else if (GetComponent<SkillComponant>() != null)
        {
            caster = GetComponent<SkillComponant>().Caster;
            level = GetComponent<SkillComponant>().Skill.CurrentUpgrade;
            Debug.Log(level);
        }
    }
}
