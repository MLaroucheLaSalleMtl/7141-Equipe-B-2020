using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAreaEffect : ConsumableComponant
{
    [Header("Level 1")]
    [SerializeField] private Transform areaOfEffect = null;
    [SerializeField] private float width = 0;
    [SerializeField] private float length = 0;

    [Header("Level 2")]
    [SerializeField] private Transform areaOfEffect2 = null;
    [SerializeField] private float width2 = 0;
    [SerializeField] private float length2 = 0;

    [Header("Level 3")]
    [SerializeField] private Transform areaOfEffect3 = null;
    [SerializeField] private float width3 = 0;
    [SerializeField] private float length3 = 0;

    public void SpawnArea()
    {
        Vector3 playerPosition = new Vector3(caster.transform.position.x, caster.transform.position.y, caster.transform.position.z);
        if (caster.RootCircleLevel == 1)
        {

            Transform clone = Instantiate(areaOfEffect, playerPosition, Quaternion.identity);
            DamageComponant(clone);
            clone.GetComponent<Transform>().localScale += new Vector3(width, 0, length);
        }
        if (caster.RootCircleLevel == 2)
        {
            Transform clone = Instantiate(areaOfEffect2, playerPosition, Quaternion.identity);
            DamageComponant(clone);
            clone.GetComponent<Transform>().localScale += new Vector3(width2, 0, length2);
        }
        if (caster.RootCircleLevel == 3)
        {
            Transform clone = Instantiate(areaOfEffect3, playerPosition, Quaternion.identity);
            DamageComponant(clone);
            clone.GetComponent<Transform>().localScale += new Vector3(width3, 0, length3);
        }

    }

    private void DamageComponant(Transform clone)
    {
        if (clone.GetComponent<DamageStayComponant>() != null)
        {
            clone.GetComponent<DamageStayComponant>().caster = caster;
        }
    }
}
