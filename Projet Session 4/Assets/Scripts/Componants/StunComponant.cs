using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunComponant : MonoBehaviour
{
    [SerializeField] private float stunTimer = 0;
    [SerializeField] private TypeOfTarget typeOfTarget = 0;
    private bool isDirty = false;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == typeOfTarget.ToString() && !isDirty)
        {
            if(collision.gameObject.name != "Summoner Enemy")
            {
                if(collision.gameObject.GetComponent<Enemy>() != null)
                collision.gameObject.GetComponent<Enemy>().IsStun(stunTimer);
            }
            isDirty = true;
        }
    }
}
