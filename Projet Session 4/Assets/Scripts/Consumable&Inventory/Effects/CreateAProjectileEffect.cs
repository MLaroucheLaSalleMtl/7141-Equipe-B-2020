using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAProjectileEffect : MonoBehaviour
{
    [Header("Projectile Properties & Level")]
    public Transform[] ObjectPrefab = null;
    public int[] numberOfProjectile = null;
    public float[] delayBetweenShot = null;
    [HideInInspector] public int level = 0;

    public void Use()
    {
        StartCoroutine(CreateTheProjectile());
    }

    public IEnumerator CreateTheProjectile()
    {
            Actor caster = null;

        for (int i = 0; i < numberOfProjectile[level]; i++)
        {
            if (GetComponent<ConsumableComponant>() != null)
                caster = GetComponent<ConsumableComponant>().Caster;

            else if (GetComponent<SkillComponant>() != null)
            {
                caster = GetComponent<SkillComponant>().Caster;
                level = GetComponent<SkillComponant>().Skill.CurrentUpgrade;
            }

            Vector3 playerPosition = new Vector3(caster.transform.position.x, caster.transform.position.y, caster.transform.position.z);
            Transform clone = Instantiate(ObjectPrefab[level], playerPosition, caster.transform.rotation);
            if (clone.GetComponent<DamageComponant>() != null)
            {
                clone.GetComponent<DamageComponant>().caster = caster;
            }

            if (clone.GetComponent<OnImpactCreate>() != null)
                clone.GetComponent<OnImpactCreate>().caster = caster;


            yield return new WaitForSeconds(delayBetweenShot[level]);
        }
    }
}
