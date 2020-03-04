using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAProjectileEffect : ConsumableComponant
{
    [Header("Projectile Properties")]
    [SerializeField] private Transform projectile = null;
    [SerializeField] private int numberOfProjectile = 0;
    [SerializeField] private float delayBetweenShot = 0;

    public void Use()
    {
        StartCoroutine(CreateTheProjectile());
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
}
