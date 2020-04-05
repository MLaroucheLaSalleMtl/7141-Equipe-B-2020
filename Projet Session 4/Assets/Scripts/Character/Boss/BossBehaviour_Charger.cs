using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour_Charger : Boss
{
    [Header("ChargeDistance")]
    [SerializeField] private Cooldown cooldownCharge = null;
    [SerializeField] private float distanceCharge = 200;
    [SerializeField] private GameObject Warning = null;
    private bool collided = false;
    private bool isWalking = true;

    [Header("Bullet Swarm + Charge Mechanic")]
    [SerializeField] private GameObject baseProjectile = null;
    [SerializeField] private int amountOfProjectile = 30;

  

    void Start()
    {
        actor.isInvulnerable = true;

    }

    void Update()
    {
        cooldownCharge.StartCooldown();

        if (cooldownCharge.IsFinish())
        {

            StartCoroutine(Charge());

            cooldownCharge.ResetCountdown();
        }

        if (actor.Health.GetCurrentValue() <= actor.Health.GetBaseValue() / 2 && !secondStageActive)
        {
            secondStageActive = true;
            actor.MovementSpeed.AddModifier(6);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Environment" && !collided && !isWalking)
        {
            StartCoroutine(StunnedCharge());
            actor.TakeThornDamage(250);
            
        }
        else if (other.gameObject.tag == "Player" && !collided)
        {
            StartCoroutine(StunnedCharge());
            other.gameObject.GetComponent<Actor>().TakeThornDamage(25);
        }
    }


    public IEnumerator Charge()
    {
        isWalking = false;
        Warning.SetActive(true);
        actor.MovementSpeed.AddModifier(-4);
        yield return new WaitForSeconds(1.5f);
        actor.RotationSpeed.AddModifier(-8);
        rig.AddRelativeForce( Vector3.forward * distanceCharge, ForceMode.Impulse);
        if (secondStageActive)
            StartCoroutine(BulletSwarm());
        yield return new WaitForSeconds(1.25f);
        actor.RotationSpeed.AddModifier(8);
        actor.MovementSpeed.AddModifier(4);
        Warning.SetActive(false);
        isWalking = true;

    }

    public IEnumerator StunnedCharge()
    {
        actor.isInvulnerable = false;
        collided = true;
        actor.RotationSpeed.AddModifier(-8);
        actor.MovementSpeed.AddModifier(-8);
        rig.AddRelativeForce(Vector3.back * distanceCharge/2, ForceMode.Impulse);
        rig.constraints = RigidbodyConstraints.FreezeAll;
        if (!secondStageActive)
            yield return new WaitForSeconds(1);
        yield return new WaitForSeconds(0.5f);

        collided = false;
        rig.constraints = RigidbodyConstraints.None;
        actor.MovementSpeed.AddModifier(8);
        actor.RotationSpeed.AddModifier(8);
        actor.isInvulnerable = true;

    }

    public IEnumerator BulletSwarm()
    {
        for (int i = 0; i < amountOfProjectile; i++)
        {
            GameObject projectile = Instantiate(baseProjectile, new Vector3(transform.position.x + Random.Range(-3, 3), 1f,
    transform.position.z + Random.Range(-3, 3)), transform.rotation * Quaternion.Euler(0,Random.Range(-180,180),0));
            projectile.GetComponent<DamageComponant>().caster = actor;
            yield return new WaitForSeconds(0.05f);
        }

    }
}
