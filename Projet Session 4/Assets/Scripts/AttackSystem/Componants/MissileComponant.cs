using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileComponant : MonoBehaviour
{
    [SerializeField] private float range = 10f;
    [SerializeField] private float velocity = 15f;
    [SerializeField] private int piercingNumber = 0;
    [SerializeField] private bool isHoming = false;
    [SerializeField] private Transform target = null;
    [SerializeField] private TypeOfTarget typeOfTarget = 0;
    private bool isDirty = false;
    private Vector3 projectile_Direction;
    private Vector3 origin;
    Rigidbody rig;


    private void Start()
    {
        HomingProjectile();
        origin = transform.position;
        projectile_Direction = new Vector3(0, 0, 1);
        rig = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Vector3.Distance(origin, transform.position) >= range)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if (isHoming)
        {
            // transform.Translate(projectile_Direction * Time.deltaTime * velocity);
            rig.velocity = transform.forward * velocity;
            gameObject.transform.LookAt(target);

        }
        else
        {
            // transform.Translate(projectile_Direction * Time.deltaTime * velocity);
            rig.velocity = transform.forward * velocity;
        }
    }

    private void HomingProjectile()
    {
        if (isHoming && !isDirty)
        {
            Collider[] colliders = Physics.OverlapSphere(origin, range);
            foreach (Collider collider in colliders)
            {
                if(collider.tag == "Player")
                {
                    target = collider.gameObject.transform;
                    if (target != null)
                    {
                        isDirty = true;
                        return;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Environment")
        {
            Destroy(gameObject);
            return;
        }
        piercingNumber--;

        if(piercingNumber <= 0 && other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
