using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnImpactCreate : MonoBehaviour
{
    [SerializeField] private GameObject objectToCreate = null;
    public Actor caster = null;
    public TypeOfTarget type;

    private void OnTriggerEnter(Collider other)
    {

        if(other.tag == "Ground" || other.tag == type.ToString())
        {

            GameObject clone = Instantiate(objectToCreate, transform.position,Quaternion.identity);
            clone.GetComponentInChildren<DamageComponant>().caster = this.caster;
            Destroy(gameObject);

        }
        if ( other.tag == "Environment")
            Destroy(gameObject);
        
    }
}
