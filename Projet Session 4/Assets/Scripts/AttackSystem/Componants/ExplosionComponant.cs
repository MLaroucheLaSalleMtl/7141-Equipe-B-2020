using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionComponant : MonoBehaviour
{
    [SerializeField] private float explosioRadius = 3f;
    [SerializeField] private TypeOfTarget typeOfTarget = 0;
    
    private void Explode()
    {

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosioRadius);

        foreach (Collider collider in colliders)
        {

            if (collider.tag == typeOfTarget.ToString())
            {
              
            }
        }
    }
}
