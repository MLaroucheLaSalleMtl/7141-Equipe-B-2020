using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBackComponant : MonoBehaviour
{
    public float knockbackDistance = 1;
    public TypeOfTarget typeOfTarget = 0;
   public void OnTriggerEnter(Collider other)
   {
        if (other.gameObject.tag == typeOfTarget.ToString())
        {
                StartCoroutine(KnockBackStun(other.transform));
        }


   }

    public IEnumerator KnockBackStun(Transform target)
    {
        Vector3 dir = target.transform.position - transform.position;

        target.GetComponent<Actor>().IncreaseStunMeter(1);
        target.GetComponent<Rigidbody>().AddForce(dir.normalized * knockbackDistance, ForceMode.VelocityChange);
        yield return new WaitForSeconds(1f);
    }
}
