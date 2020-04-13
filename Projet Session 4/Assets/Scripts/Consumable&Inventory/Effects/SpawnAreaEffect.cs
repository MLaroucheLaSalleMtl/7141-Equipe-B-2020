using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAreaEffect : MonoBehaviour
{
    [Header(" Area Of Effect & Level")]
    public float[] width = null;
    public float[] length = null;
    public Transform[] ObjectPrefab = null;
    private int level = 0;
    


    public void SpawnArea()
    {
        Actor caster = null;

        if (GetComponent<ConsumableComponant>() != null)
            caster = GetComponent<ConsumableComponant>().Caster;
        else if (GetComponent<SkillComponant>() != null)
        {
            caster = GetComponent<SkillComponant>().Caster;
            level = GetComponent<SkillComponant>().Skill.CurrentUpgrade;
        }

        Vector3 playerPosition = new Vector3(caster.transform.position.x, caster.transform.position.y, caster.transform.position.z);

        Transform clone = Instantiate(ObjectPrefab[level], playerPosition, Quaternion.identity);
            DamageComponant(clone);
            clone.GetComponent<Transform>().localScale += new Vector3(width[level], 0, length[level]);


    }

    private void DamageComponant(Transform clone)
    {
        Actor caster = null;

        if (GetComponent<ConsumableComponant>() != null)
            caster = GetComponent<ConsumableComponant>().Caster;
        else if (GetComponent<SkillComponant>() != null)
            caster = GetComponent<SkillComponant>().Caster;


        if (clone.GetComponent<DamageStayComponant>() != null)
            clone.GetComponent<DamageStayComponant>().caster = caster;

        if (clone.GetComponent<DamageComponant>() != null)
            clone.GetComponent<DamageComponant>().caster = caster;

        if (clone.GetComponent<RecoverOnHit>() != null)
            clone.GetComponent<RecoverOnHit>().caster = caster;

    }
}
