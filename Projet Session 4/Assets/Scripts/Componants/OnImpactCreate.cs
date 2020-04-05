using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnImpactCreate : MonoBehaviour
{
    [SerializeField] private GameObject objectToCreate = null;
    [HideInInspector] public Actor caster = null;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ground")
        {
           GameObject clone = Instantiate(objectToCreate, transform.position,Quaternion.identity);
            clone.GetComponentInChildren<DamageComponant>().caster = this.caster;
        }
        Destroy(gameObject);
    }
}
