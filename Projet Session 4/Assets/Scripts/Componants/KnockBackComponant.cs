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
            Vector3 dir = other.transform.position - transform.position;
            other.GetComponent<Rigidbody>().AddForce(dir.normalized * knockbackDistance, ForceMode.Impulse);
            StartCoroutine(KnockBackStun(other.transform));
        }


   }

    public IEnumerator KnockBackStun(Transform target)
    {
        target.GetComponent<Actor>().CanMove = false;
        yield return new WaitForSeconds(1f);
        target.GetComponent<Actor>().CanMove = true;

    }
}
