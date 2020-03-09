using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Totem : Actor
{
    private Transform target;
    public float range = 15f;
    
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    protected override void Update()
    {
        if (target == null) return;
        AttackSpeed.StartCooldown();

        if (CanAttack)
            UseBasicAttack(target);

        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0f,rotation.y,0f);
    }

    void UpdateTarget() // [ Source : Tutorial De tower Defense Brackeys ]
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(TypeOfTarget.Enemy.ToString());
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            
        }
        else
        {
            target = null;
        }
    }
}
