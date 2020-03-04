using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionComponant : MonoBehaviour
{
    [Header("Explosion Properties")]
    [SerializeField] private float explosioRadius = 3f;
    [SerializeField] private TypeOfTarget typeOfTarget = 0;

   // [Header("Use HealingComponant")]
   // [SerializeField] private bool isHealing = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == typeOfTarget.ToString())
        {
            Explode();
        }
    }


    private void Explode()
    {
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosioRadius, LayerMask.GetMask("Target") );

        foreach (Collider collider in colliders)
        {
            if (collider.tag == typeOfTarget.ToString())
            {
                GetComponent<DamageComponant>().DealDamage(colliders);
            }
        }
    }
}
