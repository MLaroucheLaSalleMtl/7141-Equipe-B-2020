using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour_Summoner : Boss
{
    [Header("Summon Mechanic")]
    [SerializeField] private GameObject[] summons = null;
    [SerializeField] private Cooldown cooldownSummon = null;

    [Header("Bullet Swarm Mechanic")]
    [SerializeField] private GameObject baseProjectile = null;
    [SerializeField] private int amountOfProjectile = 6;
    [SerializeField] private Cooldown cooldownSwarmBullet = null;

    public int numberOfActive = 0;
    void Update()
    {
        cooldownSummon.StartCooldown();
        cooldownSwarmBullet.StartCooldown();

        if(numberOfActive >= 1)
        {
            actor.isInvulnerable = true;
        }
        else
        {
            actor.isInvulnerable = false;

        }


        if (cooldownSummon.IsFinish())
        {
            int randomNumber = Random.Range(0, summons.Length);

            GameObject clone = Instantiate(summons[randomNumber], new Vector3(transform.position.x + Random.Range(-4, 4), 0.5f,
    transform.position.z + Random.Range(-4, 4)), transform.rotation);
            cooldownSummon.ResetCountdown();
        }
        if (cooldownSwarmBullet.IsFinish())
        {
            StartCoroutine(BulletSwarm());
            cooldownSwarmBullet.ResetCountdown();
        }

        if (actor.Health.GetCurrentValue() <= actor.Health.GetBaseValue() / 2 && !secondStageActive)
        {
            secondStageActive = true;
            cooldownSummon.AddModifier(-4);
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }


    public IEnumerator BulletSwarm()
    {
        for (int i = 0; i < amountOfProjectile; i++)
        {
            GameObject projectile  = Instantiate(baseProjectile, new Vector3(transform.position.x + Random.Range(-8, 8), 1f,
    transform.position.x + Random.Range(-8, 8)), transform.rotation);
            projectile.GetComponent<DamageComponant>().caster = actor;
        yield return new WaitForSeconds(0.75f);
        }

    }
}
