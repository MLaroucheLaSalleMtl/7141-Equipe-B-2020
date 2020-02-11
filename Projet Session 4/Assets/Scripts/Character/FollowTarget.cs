using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] Transform target = null;
    Vector3 destination;
    NavMeshAgent agent;
    [SerializeField] private GameObject ennemyHitBox = null;
    public float attackCooldown = 1f;
    bool canAttack = true;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        destination = agent.destination;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(target.transform.position, transform.position) <= 3.0f)
            {
            agent.isStopped = true;
            if (canAttack)
            StartCoroutine(EnnemyAttack());
            }
        if (Vector3.Distance(destination, target.position) > 3.0f)
        {
            agent.isStopped = false;
            destination = target.position;
            agent.destination = destination;
        }
    }

    private IEnumerator EnnemyAttack()
    {
        Instantiate(ennemyHitBox, agent.transform.position + (agent.transform.forward * 2), agent.transform.rotation);
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;

    }
}
