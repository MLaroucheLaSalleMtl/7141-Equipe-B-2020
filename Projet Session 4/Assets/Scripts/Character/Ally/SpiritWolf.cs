using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpiritWolf : Actor
{
    #region Combat Properties
    [Header("Combat properties")]
    public TypeOfTarget typeOfTarget = 0;
    [SerializeField] private float detectionRadius = 15f;
    public float attackRangeDetection;
    public float walkBackRangeDetection;
    private Transform target = null;
    #endregion


    #region NavMesh
    private NavMeshAgent navMeshAgent;
    private Vector3 destination;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        destination = navMeshAgent.destination;
        navMeshAgent.stoppingDistance = attackRangeDetection;
        target = GameObject.Find("Player").transform;
        transform.parent = null;
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player").transform;


        if (Vector3.Distance(target.transform.position, transform.position) <= attackRangeDetection && Vector3.Distance(target.transform.position, transform.position) > walkBackRangeDetection && TargetDetection())
        {
            RaycastHit hitray;

            Rotation();
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitray, 1000) && CanAttack)
            {
                if (hitray.collider.tag == typeOfTarget.ToString())
                    UseBasicAttack(target);
                Debug.DrawLine(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.yellow);
            }


        }
        else
        {
            if (TargetDetection())
            {
                Rotation();
            }

            if (CanMove)
                Movement();

            if (DashCooldown.IsFinish())
            {
                StartCoroutine(ActivateDash());
            }
        }

    }
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

        if (Physics.Raycast(transform.position, direction, out hit, 1000, layerMask))
        {


            if (hit.collider.gameObject.tag == "Environment")
            {
                Debug.DrawLine(transform.position, target.position, Color.red);
                return false;
            }

            if (hit.collider.gameObject.tag == "Enemy")
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

    void UpdateTarget() // [ Source : Tutorial De tower Defense Brackeys ]
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(TypeOfTarget.Enemy.ToString());
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= detectionRadius)
        {
            target = nearestEnemy.transform;

        }
        else
        {
            target = null;
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
