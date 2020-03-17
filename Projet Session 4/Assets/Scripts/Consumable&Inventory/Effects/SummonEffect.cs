using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonEffect : MonoBehaviour
{
    [Header("Summon & Level")]
    public Transform[] ObjectPrefab = null;
    private int level = 0;

    void Update()
    {
        if (GetComponent<SkillComponant>() != null)
            level = GetComponent<SkillComponant>().Skill.CurrentUpgrade;
    }
    public void Summon()
    {
        Actor caster = null;

        if (GetComponent<ConsumableComponant>() != null)
            caster = GetComponent<ConsumableComponant>().Caster;
        else if (GetComponent<SkillComponant>() != null)
            caster = GetComponent<SkillComponant>().Caster;

        Vector3 playerPosition = new Vector3(caster.transform.position.x, caster.transform.position.y, caster.transform.position.z);
        Transform clone = Instantiate(ObjectPrefab[level], playerPosition, caster.transform.rotation);
    }
}
