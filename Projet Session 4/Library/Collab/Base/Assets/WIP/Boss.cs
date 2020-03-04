using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Actor
{
    [Header("FirstAttack")]
    [SerializeField] private float timeBetweenAction = 0;
    [SerializeField] private GameObject basicProjectile = null;
    [SerializeField] private float delayBetweenShot = 0.1f;
    private Transform target;

    [Header("SecondAttack")]
    [SerializeField] private GameObject basicZone = null;
    [SerializeField] private GameObject[] attackZone = null;

    [Header("ThirdAttack")]
    [SerializeField] private GameObject basicRootZone = null;
    private int actionRoll;

   // [Header("OnDeath Componant")]
   // public int xpGive = 0;
   // public int goldReward = 0;
   void Start()
   {
        actionRoll = 1;
   }

    void Update()
    {

        if (CanMove)
        {
            //Movement();
            //Rotation();
        }
        if (timeBetweenAction != 0)
            timeBetweenAction = Mathf.Clamp(timeBetweenAction -= Time.deltaTime, 0, Mathf.Infinity);
        else
        {
            actionRoll = Random.Range(0, 5);
            State(actionRoll);
        }
    }

    public void State(int i)
    {
        switch (i)
        {
            case 0 : { timeBetweenAction = 1; } break;
            case 1 : { StartCoroutine(Move()); timeBetweenAction = 3; } break;
            case 2 : { StartCoroutine(FirstAttack()); timeBetweenAction = 11; } break;
            case 3 : { StartCoroutine(SecondAttack()); timeBetweenAction = 3; } break;
            case 4 : { StartCoroutine(ThirdAttack()); timeBetweenAction = 2; } break;
            case 5 : { } break;
        }
    }




    protected override void Movement()
    {
        transform.position += transform.forward * MovementSpeed.GetValue() * DashSpeed * Time.deltaTime;
    }

    protected override void Rotation()
    {
        target = GameObject.Find("Player").transform;
    }

    public IEnumerator Move()
    {
        CanMove = true;
        yield return new WaitForSeconds(3);
        CanMove = false;

    }

    public IEnumerator FirstAttack()
    {
        StartCoroutine(FireProjectile(100));

        yield return new WaitForSeconds(1);
    }

    IEnumerator SecondAttack()
    {
        for (int i = 0; i < attackZone.Length; i++)
        {
            GameObject zone = Instantiate(basicZone, attackZone[i].transform.position, transform.rotation);
            zone.GetComponent<DamageComponant>().caster = GetComponent<Actor>();
        }

        yield return new WaitForSeconds(1);
    }

    IEnumerator FireProjectile(int NumberOfProjectile)
    {
        for (int i = 0; i < NumberOfProjectile; i++)
        {
            transform.Rotate(new Vector3(0, 10, 0));
            GameObject projectile = Instantiate(basicProjectile, transform.position + transform.forward, transform.rotation);
            if (projectile.GetComponent<DamageComponant>() != null)
                projectile.GetComponent<DamageComponant>().caster = GetComponent<Actor>();
            yield return new WaitForSeconds(delayBetweenShot);
        }
    }
    IEnumerator ThirdAttack()
    {
        GameObject zone = Instantiate(basicRootZone, transform.position - new Vector3(0,1,0), Quaternion.identity);
        yield return new WaitForSeconds(1);
    }

}
