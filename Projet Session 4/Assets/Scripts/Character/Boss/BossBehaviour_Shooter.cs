using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour_Shooter : Boss
{
    [Header("Bullet Hell")]
    [SerializeField] private Cooldown cooldownBulletHell = null;
    [SerializeField] private GameObject bullets = null;
    [SerializeField] private int numberOfProjectile = 60;
    [SerializeField] private float delayBetweenShot = 0.05f;
    [SerializeField] private float angleChange = 15f;

    [Header("Teleport")]
    [SerializeField] private Cooldown cooldownTeleport = null;
    private Vector3 origin;
    [SerializeField] private float range = 5;

    void Start()
    {
        origin = transform.position;
    }

    void Update()
    {
        cooldownBulletHell.StartCooldown();
        cooldownTeleport.StartCooldown();

        if (cooldownBulletHell.IsFinish())
        {
            cooldownBulletHell.ResetCountdown();
            StartCoroutine(BulletHell());
        }
        if (cooldownTeleport.IsFinish())
        {
            transform.position = new Vector3(Random.Range(-range, range), origin.y, Random.Range(-range, range));
            cooldownTeleport.ResetCountdown();
        }

        if (actor.Health.GetCurrentValue() <= actor.Health.GetBaseValue() / 2 && !secondStageActive)
        {
            secondStageActive = true;
            cooldownTeleport.AddModifier(-4);
            actor.Barrier.AddModifier(750);
            actor.Barrier.IncreaseCurrentValue(750);
            transform.position = new Vector3(Random.Range(-range, range), origin.y, Random.Range(-range, range));
            cooldownTeleport.ResetCountdown();
        }


    }

    IEnumerator BulletHell()
    {
        float angle = 0;
        int randomNumber = Random.Range(0, numberOfProjectile/2);
        for (int i = 0; i < numberOfProjectile; i++)
        {
                GameObject clone = Instantiate(bullets, transform.position, transform.rotation.normalized * Quaternion.Euler(0, angle, 0));
                clone.GetComponent<DamageComponant>().caster = actor;

            if (i == randomNumber)
            {
                angle += 50;
                randomNumber += randomNumber/2;
            }

            angle += angleChange;
            if(numberOfProjectile/2 == i)
                yield return new WaitForSeconds(1);

            yield return new WaitForSeconds(delayBetweenShot);
        }
    }


}
