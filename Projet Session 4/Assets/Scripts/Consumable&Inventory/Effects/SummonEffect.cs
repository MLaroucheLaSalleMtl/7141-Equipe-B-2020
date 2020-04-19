using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonEffect : MonoBehaviour
{
    [Header("Summon & Level")]
    public Transform[] ObjectPrefab = null;
    private Transform positionToDrop = null;
    private int level = 0;

    void Start()
    {
        positionToDrop = GameObject.Find("DropPosition").transform;
    }

    public void Summon()
    {
        Actor caster = null;

        if (GetComponent<ConsumableComponant>() != null)
            caster = GetComponent<ConsumableComponant>().Caster;
        else if (GetComponent<SkillComponant>() != null)
        {
            caster = GetComponent<SkillComponant>().Caster;
            level = GetComponent<SkillComponant>().Skill.CurrentUpgrade;
        }

        Transform clone = Instantiate(ObjectPrefab[level], positionToDrop.position, caster.transform.rotation);
        if (clone.GetComponent<DamageComponant>() != null)
            clone.GetComponent<DamageComponant>().caster = caster;

        if (clone.GetComponent<DamageStayComponant>() != null)
            clone.GetComponent<DamageStayComponant>().caster = caster;

    }
}
