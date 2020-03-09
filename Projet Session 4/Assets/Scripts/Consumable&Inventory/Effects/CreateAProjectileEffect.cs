using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAProjectileEffect : ConsumableComponant
{
    [Header("Projectile Properties")]

    [Header("Level 1")]
    [SerializeField] private Transform projectile = null;
    [SerializeField] private int numberOfProjectile = 0;
    [SerializeField] private float delayBetweenShot = 0;

    [Header("Level 2")]
    [SerializeField] private Transform projectile2 = null;
    [SerializeField] private int numberOfProjectile2 = 0;
    [SerializeField] private float delayBetweenShot2 = 0;

    [Header("Level 3")]
    [SerializeField] private Transform projectile3 = null;
    [SerializeField] private int numberOfProjectile3 = 0;
    [SerializeField] private float delayBetweenShot3 = 0;

    public void Use()
    {
        StartCoroutine(CreateTheProjectile());
    }
    public void UseFireBall()
    {
        if(caster.FireBallLevel == 1)
        {
            StartCoroutine(CreateTheProjectile());
        }
        if (caster.FireBallLevel == 2)
        {
            StartCoroutine(CreateTheProjectile2());
        }
        if (caster.FireBallLevel == 3)
        {
            StartCoroutine(CreateTheProjectile3());
        }
    }
    public IEnumerator CreateTheProjectile()
    {
        for (int i = 0; i < numberOfProjectile; i++)
        {
            Vector3 playerPosition = new Vector3(caster.transform.position.x, caster.transform.position.y, caster.transform.position.z);
            Transform clone = Instantiate(projectile, playerPosition, caster.transform.rotation);
            clone.GetComponent<DamageComponant>().caster = caster;
            yield return new WaitForSeconds(delayBetweenShot);
        }
    }
    public IEnumerator CreateTheProjectile2()
    {
        for (int i = 0; i < numberOfProjectile2; i++)
        {
            Vector3 playerPosition = new Vector3(caster.transform.position.x, caster.transform.position.y, caster.transform.position.z);
            Transform clone = Instantiate(projectile2, playerPosition, caster.transform.rotation);
            clone.GetComponent<DamageComponant>().caster = caster;
            yield return new WaitForSeconds(delayBetweenShot2);
        }
    }
    public IEnumerator CreateTheProjectile3()
    {
        for (int i = 0; i < numberOfProjectile3; i++)
        {
            Vector3 playerPosition = new Vector3(caster.transform.position.x, caster.transform.position.y, caster.transform.position.z);
            Transform clone = Instantiate(projectile3, playerPosition, caster.transform.rotation);
            clone.GetComponent<DamageComponant>().caster = caster;
            yield return new WaitForSeconds(delayBetweenShot3);
        }
    }
}
