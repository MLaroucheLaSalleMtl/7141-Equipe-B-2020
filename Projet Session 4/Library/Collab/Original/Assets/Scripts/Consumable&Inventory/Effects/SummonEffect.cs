using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonEffect : ConsumableComponant
{
    [SerializeField] private Transform summonedObject = null;
    [SerializeField] private float range = 0;

    public void Summon()
    {
        Vector3 playerPosition = new Vector3(caster.transform.position.x, caster.transform.position.y, caster.transform.position.z + range);
        Transform clone = Instantiate(summonedObject, playerPosition, caster.transform.rotation);
    }
    public void Shoot()
    {
        Instantiate(summonedObject, caster.transform.position, caster.transform.rotation);
    }
}
