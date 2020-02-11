using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Actor
{
    [SerializeField] protected Transform target = null;
    [SerializeField] protected GameObject ennemyHitBox = null;
    public float attackCooldown = 1f;
    protected bool canAttack = true;
    public float rotationSpeed = 3.0f;
    public float moveSpeed = 3.0f;
    public float attackDistance;

    private void Awake()
    {
        transform.parent = null;
        target = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if (Vector3.Distance(target.transform.position, transform.position) <= attackDistance)
        {
            LookAt();
            if (canAttack)
                StartCoroutine(EnnemyAttack());
        }
        else
        {
            LookAt();
            MoveTo();
        }
        Death();    
    }
    private void LookAt()
    {
        // Look at Player
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position), rotationSpeed * moveSpeed);
    }
    private void MoveTo()
    {
        // Move at Player
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    private IEnumerator EnnemyAttack()
    {
        canAttack = false;
        Instantiate(ennemyHitBox, transform.position + (transform.forward * 2), transform.rotation);
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    protected override void Death() { if (HealthCurrent <= 0)  Destroy(gameObject); }

}
