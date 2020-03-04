using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : Actor
{
    #region Variables
    [SerializeField] protected Transform target;
    [SerializeField] protected GameObject ennemyHitBox = null;
    [SerializeField] private Image img_EnnemyHealthBar = null;

    public float attackCooldown = 0f;
    public int xpGive;
    bool canAttack = true;
    bool stun = false;
    bool root = false;

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
        GameManager.NumberOfEnemy++;
    }

    void Update()
    {
        img_EnnemyHealthBar.fillAmount = gameObject.GetComponent<Enemy>().HealthCurrent / gameObject.GetComponent<Enemy>().HealthMaximum.GetValue();
        if (ennemyDetect)
        {
            if(root == false)
            {
                AvoidObstacles();
                ChasePlayer();
            }
            DashCurrent = Regeneration(DashCurrent, DashMaximum, DashRegenRatio);
        }
        StartDamageImmunityCooldown();
        Death();
    }

    #region Methods For Move
    private void LookAt()
    {      
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position), rotationSpeed);
    }
    private void MoveTo()
    {
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
            transform.position += transform.right * moveSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position), rotationSpeed);
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
    public void IsRoot()
    {
        StartCoroutine(EnemyIsRoot());
    }
    private IEnumerator EnemyIsRoot()
    {
        root = true;
        yield return new WaitForSeconds(3f);
        root = false;
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
           GameObject clone = Instantiate(ennemyHitBox, transform.position + (transform.forward * 2), transform.rotation);
            if(clone.GetComponent<DamageComponant>() != null)
            clone.GetComponent<DamageComponant>().caster = GetComponent<Actor>();
        }
        canAttack = true;
    }
    #endregion
    protected override void Death() { 
        if (HealthCurrent <= 0)
        {
            if (target != null)
            target.GetComponent<Player>().ExperienceCurrent += xpGive;
            GameManager.NumberOfEnemy--;
            Destroy(gameObject);
        }
    }
}
