using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour_Beamer : Boss
{
    [Header("Basic Beam Attack")]
    [SerializeField] private GameObject basicBeam = null;
    [SerializeField] private Cooldown cooldownBasicBeam = null;

    [Header("Orbital Strike")]
    [SerializeField] private int numberOfStrike = 6;
    [SerializeField] private int distanceOfStrike = 10;
    [SerializeField] private GameObject orbitalBeam = null;
    [SerializeField] private Cooldown cooldownOrbitalBeam = null;


    void Update()
    {
        cooldownBasicBeam.StartCooldown();
        cooldownOrbitalBeam.StartCooldown();

        if (cooldownBasicBeam.IsFinish())
        {
            GameObject clone = Instantiate(basicBeam, transform.position + (transform.forward * 11), transform.rotation, gameObject.GetComponentInParent<Transform>().transform);
            clone.GetComponentInChildren<DamageComponant>().caster = GetComponent<Actor>();
            cooldownBasicBeam.ResetCountdown();
        }
        if (cooldownOrbitalBeam.IsFinish())
        {
            StartCoroutine(OrbitalStrike());
            cooldownOrbitalBeam.ResetCountdown();
        }
        if(actor.Health.GetCurrentValue() <= actor.Health.GetBaseValue() / 2 && !secondStageActive)
        {
            actor.MovementSpeed.AddModifier(5);
            actor.AttackSpeed.AddModifier(-1.5f);
            cooldownBasicBeam.AddModifier(-6);
            numberOfStrike += 20;
            secondStageActive = true;
        }
       
    }

    public IEnumerator OrbitalStrike()
    {
        for (int i = 0; i < numberOfStrike; i++)
        {
            GameObject clone = Instantiate(orbitalBeam, new Vector3(transform.position.x + Random.Range(-distanceOfStrike, distanceOfStrike), Random.Range(7,16),
                transform.position.x + Random.Range(-distanceOfStrike, distanceOfStrike)), transform.rotation);
            clone.GetComponent<OnImpactCreate>().caster = GetComponent<Actor>();
            clone.GetComponent<DamageComponant>().caster = GetComponent<Actor>();
            if(!secondStageActive)
                yield return new WaitForSeconds(0.25f);

            yield return new WaitForSeconds(0.25f);
        }
    }



}
