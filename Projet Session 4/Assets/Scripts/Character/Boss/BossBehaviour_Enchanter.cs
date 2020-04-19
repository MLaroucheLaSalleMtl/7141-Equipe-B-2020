using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour_Enchanter : Boss
{
    [Header("Spheres Mechanics")]
    [SerializeField] private GameObject partToRotate = null;
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private Cooldown cooldownSpeedBallUp = null;
    [SerializeField] private GameObject partToRotateStage2 = null;


    [Header("ForceField Mechanics")]
    [SerializeField] private Cooldown cooldownKnockbackSpell = null;
    [SerializeField] private GameObject knockSpell = null;

    void Start()
    {
        foreach (Transform child in partToRotate.gameObject.transform)
        {
            child.GetComponent<DamageComponant>().caster = actor;
            child.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        cooldownSpeedBallUp.StartCooldown();
        cooldownKnockbackSpell.StartCooldown();

        partToRotate.transform.Rotate(new Vector3(0, (rotationSpeed * 1) * Time.deltaTime, 0));
        if(secondStageActive)
        partToRotateStage2.transform.Rotate(new Vector3(0, (rotationSpeed * -1) * Time.deltaTime, 0));


        if (cooldownSpeedBallUp.IsFinish())
        {
            StartCoroutine(SpeedUpBalls());
            cooldownSpeedBallUp.ResetCountdown();
        }
        if (cooldownKnockbackSpell.IsFinish())
        {
            Instantiate(knockSpell, transform.position,Quaternion.identity);
            cooldownKnockbackSpell.ResetCountdown();
        }

        if (actor.Health.GetCurrentValue() <= actor.Health.GetBaseValue() / 2 && !secondStageActive)
        {
            secondStageActive = true;
            foreach (Transform child in partToRotateStage2.gameObject.transform)
            {
                child.GetComponent<DamageComponant>().caster = actor;
                child.gameObject.SetActive(true);
            }
            cooldownKnockbackSpell.AddModifier(-3);
        }
    }

    public IEnumerator SpeedUpBalls()
    {
        rotationSpeed += 50;
        yield return new WaitForSeconds(1f);
        rotationSpeed += 50;
        yield return new WaitForSeconds(1f);
        rotationSpeed += 100;
        yield return new WaitForSeconds(4f);
        rotationSpeed += -200;

    }

}
