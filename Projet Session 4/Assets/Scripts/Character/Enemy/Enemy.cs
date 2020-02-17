using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Actor
{
    #region Variables
    [SerializeField] protected Transform target;
    [SerializeField] protected GameObject ennemyHitBox = null;

    public float attackCooldown = 0f;
    bool canAttack = true;
    bool stun = false;

    public float rotationSpeed = 3.0f;
    public float moveSpeed = 3.0f;
    public float attackDistance;
    bool ennemyDetect = false;

    public float raycastDistance;
    int layerMask = 1 << 10;
    RaycastHit hit;
    bool inFrontOfObstacles = false;
    #endregion

    private void Awake()
    {
        transform.parent = null;
    }

    void Update()
    {
        if (ennemyDetect)
        {
            AvoidObstacles();
            ChasePlayer();
        }
        Death();    
    }
    #region Methods For Move
    private void LookAt()
    {
        // Look at Player
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position), rotationSpeed);
    }
    private void MoveTo()
    {
        // Move at Player
        transform.position += transform.forward * moveSpeed * DashSpeed * Time.deltaTime;
    }

    private void ChasePlayer()
    {
        if (!inFrontOfObstacles)
        {
            if (Vector3.Distance(target.transform.position, transform.position) <= attackDistance)
            {
                LookAt();
                if (canAttack && !stun)
                    StartCoroutine(EnnemyAttack());
            }
            else
            {
                {
                    LookAt();
                    MoveTo();
                }
            }
        }
    }

    private void AvoidObstacles()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, raycastDistance, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation((target.transform.position - transform.position) + (Vector3.right)), rotationSpeed * moveSpeed);
            transform.position += transform.right * moveSpeed * Time.deltaTime;
            inFrontOfObstacles = true;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 100, Color.white);
            inFrontOfObstacles = false;
        }
    }
    public void EnemyDetected()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
        ennemyDetect = true;
    }
    #endregion

    #region Methods for Attack
    public void IsStun(float stunTimer)
    {
        StartCoroutine(StunDuration(stunTimer));
    }

    public IEnumerator StunDuration(float stunTimer)
    {
        stun = true;
        yield return new WaitForSeconds(stunTimer);
        stun = false;
    }
    private IEnumerator EnnemyAttack()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        if(!stun)
        {
            Instantiate(ennemyHitBox, transform.position + (transform.forward * 2), transform.rotation);
        }
        canAttack = true;
    }
    #endregion
    protected override void Death() { if (HealthCurrent <= 0)  Destroy(gameObject); }
}
