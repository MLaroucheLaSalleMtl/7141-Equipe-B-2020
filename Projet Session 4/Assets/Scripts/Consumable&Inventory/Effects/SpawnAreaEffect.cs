using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAreaEffect : ConsumableComponant
{
    [SerializeField] private Transform areaOfEffect = null;
    [SerializeField] private float width = 0;
    [SerializeField] private float length = 0;

    public void SpawnArea()
    {
        Vector3 playerPosition = new Vector3(caster.transform.position.x, caster.transform.position.y, caster.transform.position.z);
        Transform clone = Instantiate(areaOfEffect, playerPosition, Quaternion.identity);
        if (clone.GetComponent<DamageStayComponant>() != null)
        {
            clone.GetComponent<DamageStayComponant>().caster = caster;
        }
        clone.GetComponent<Transform>().localScale += new Vector3(width, 0, length);

    }
}
