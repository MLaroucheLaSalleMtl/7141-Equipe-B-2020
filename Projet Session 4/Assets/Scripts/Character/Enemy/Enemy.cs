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
    public TypeOfTarget typeOfTarget = 0;
    [SerializeField] private float detectionRadius = 15f;
    public float attackRangeDetection;
    public float walkBackRangeDetection;
    private Transform target = null;
    #endregion

    #region OnDeath
    [Header("OnDeath")]
    public int ExperienceReward;
    public GameObject ChestReward;
    #endregion

    #region Others
    [Header("Others")]
   // public float raycastDistance;
  //  int layerMask = 1 << 10;
  //  RaycastHit hit;
   // bool inFrontOfObstacles = false;
    private Player _Player;
    #endregion

    #region NavMesh
    private NavMeshAgent navMeshAgent;
    private Vector3 destination;
    #endregion

    #endregion
    #region Unity's Methods
    
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        destination = navMeshAgent.destination;
        navMeshAgent.stoppingDistance = attackRangeDetection;
        target = GameObject.Find("Player").transform;
        transform.parent = null;
        if(gameObject.tag != "Ally")
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
        if (target == null) return;
        if (Vector3.Distance(target.transform.position, transform.position) <= attackRangeDetection && Vector3.Distance(target.transform.position, transform.position) > walkBackRangeDetection && TargetDetection())
        {
            RaycastHit hitray;

            Rotation();
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitray, 1000) && CanAttack)
            {
                if(hitray.collider.tag == typeOfTarget.ToString())
                UseBasicAttack(target);
                Debug.DrawLine(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.yellow);
            }
            

        }
        else
        {
            if(TargetDetection())
            {
            Rotation();
            }

            if(CanMove)
            Movement();

            if (DashCooldown.IsFinish())
            {
                StartCoroutine(ActivateDash());
            }
        }

        Death();
    }

    #endregion
    #region Methods For Move

   
    private void Rotation()
    {
        Vector3 direction = target.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * RotationSpeed.GetBaseValue()).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }
    private bool TargetDetection()
    {
        Vector3 direction = target.transform.position - transform.position;
        RaycastHit hit;
        int layerMask = 1 << 13;
        layerMask = ~layerMask;

        if (Physics.Raycast(transform.position, direction, out hit, 1000,layerMask))
        {


            if (hit.collider.gameObject.tag == "Environment")
            {
                 Debug.DrawLine(transform.position, target.position, Color.red);
                return false;
            }

            if (hit.collider.gameObject.tag == "Player")
            {

                 Debug.DrawLine(transform.position, target.position, Color.green);
                return true;
            }


        }
        return false;
    }
    private void Movement()
    {
        Vector3 origin = transform.position;
        navMeshAgent.destination = target.transform.position;


        if (Vector3.Distance(target.transform.position, transform.position) <= walkBackRangeDetection)
            transform.position += transform.forward * -MovementSpeed.GetBaseValue() * DashSpeed * Time.deltaTime;
        else
            transform.position += transform.forward * MovementSpeed.GetBaseValue() * DashSpeed * Time.deltaTime;


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
