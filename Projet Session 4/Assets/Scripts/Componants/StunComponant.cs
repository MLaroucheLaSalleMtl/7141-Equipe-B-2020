using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunComponant : MonoBehaviour
{
    [SerializeField] private float stunDuration = 0;
    [SerializeField] private TypeOfTarget typeOfTarget = 0;
    private bool isDirty = false;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == typeOfTarget.ToString() && !isDirty)
        {
            if (collision.gameObject.GetComponent<Actor>() != null)
                collision.gameObject.GetComponent<Actor>().IncreaseStunMeter(stunDuration);
            
            isDirty = true;
        }
    }
}
