using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunComponant : MonoBehaviour
{
    [SerializeField] private float stunDuration = 0;
    [SerializeField] private TypeOfTarget typeOfTarget = 0;
    private bool isDirty = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != typeOfTarget.ToString() || isDirty) return;

        Collider[] colliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale, transform.rotation, LayerMask.GetMask("Target"));
        StunTarget(colliders);

        isDirty = true;
    }

    public void StunTarget(Collider[] _colliders)
    {
        foreach (Collider collider in _colliders)
        {
            if (collider.gameObject.tag == typeOfTarget.ToString())
            {
                collider.gameObject.GetComponent<Actor>().IncreaseStunMeter(stunDuration);
            }

        }
    }
}
