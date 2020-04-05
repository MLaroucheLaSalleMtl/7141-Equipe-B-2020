using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : Actor
{
    #region Variables
    [Header("HealthBar")]
    [SerializeField] private Image img_Health = null;
    [SerializeField] private Image img_StunMeter = null;
    [SerializeField] private Image img_Barrier = null;

    #region Combat Properties
    [Header("Combat properties")]
    [SerializeField] private float detectionRadius = 15f;
    public float attackRangeDetection;
    public float walkBackRangeDetection;
    private Transform target;
    #endregion

    #region OnDeath
    [Header("OnDeath")]
    public int ExperienceReward;
    public GameObject ChestReward;
    #endregion

    #region Others
    [Header("Others")]
    public float raycastDistance;
    int layerMask = 1 << 10;
    RaycastHit hit;
    bool inFrontOfObstacles = false;
    private Player _Player;
    #endregion



    #endregion
    #region Unity's Methods

    private void Start()
    {
        transform.parent = null;
        if (gameObject.tag != "Ally")
        {
            GameManager.NumberOfEnemy++;
        }
    }
    protected override void Update()
    {
        base.Update();

        if (!isABoss)
        {
            img_Health.fillAmount = Health.GetCurrentValue() / Health.GetBaseValue();
            img_StunMeter.fillAmount = StunMeterCurrentValue / StunMeterMaximumValue;
            img_Barrier.fillAmount = Barrier.GetCurrentValue() / Health.GetBaseValue();
        }

        UpdateDetection();

        if (target != null)
        {
            if(CanMove)
            {
                AvoidObstacles();
                ChasePlayer();
            }
            
        }
        
        Death();
    }

    #endregion
    #region Methods For Move
    private void LookAt()
    {
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position), RotationSpeed.GetBaseValue());
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * RotationSpeed.GetBaseValue()).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    private void MoveTo()
    {
        if (Vector3.Distance(target.transform.position, transform.position) <= walkBackRangeDetection)
            transform.position += transform.forward * -MovementSpeed.GetBaseValue() * DashSpeed * Time.deltaTime;
        else
            transform.position += transform.forward * MovementSpeed.GetBaseValue() * DashSpeed * Time.deltaTime;
    }
    private void ChasePlayer()
    {
        if (!inFrontOfObstacles)
        {
            if (Vector3.Distance(target.transform.position, transform.position) <= attackRangeDetection && Vector3.Distance(target.transform.position, transform.position) > walkBackRangeDetection)
            {
                LookAt();
                if (CanAttack)
                    UseBasicAttack(target);

            }
            else 
            {
                LookAt();
                MoveTo();
                if (DashCooldown.IsFinish())
                {
                    StartCoroutine(ActivateDash());
                }
            }
        }
    }
    private void AvoidObstacles()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, raycastDistance, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            transform.position += transform.right * MovementSpeed.GetBaseValue() * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position), RotationSpeed.GetBaseValue());
            inFrontOfObstacles = true;
        }      
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 100, Color.green);
            inFrontOfObstacles = false;
        }
    }
    public void UpdateDetection()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, LayerMask.GetMask("Target"));

        foreach (Collider collider in colliders)
        {
            if(gameObject.tag != "Ally")
            {
                if (collider.tag == "Player")
                {
                    target = collider.transform;
                }
                if (collider.tag == "Ally")
                {
                    target = collider.transform;
                }
            }
            else if (collider.tag == "Enemy")
            {
                target = collider.transform;
            }
        }
    }
    #endregion

    protected void Death() { 
        if (Health.CurrentIsEmpty())
        {
            if (target != null)
            {
                target.GetComponent<Player>().IncreaseExperience(ExperienceReward);
                target.GetComponent<Player>().IncreaseGold(Random.Range(Gold,Gold*2));
            }
            GameManager.NumberOfEnemy--;
            if (isABoss)
                Instantiate(ChestReward, transform.position + new Vector3(0, -0.5f, 0), transform.rotation);
            Destroy(gameObject);
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.grey;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRangeDetection);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, walkBackRangeDetection);

    }
}
