using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageStayComponant : DamageComponant
{
    [Header("Time Componant")]
    [SerializeField] private float timeBetweenTick = 0;
    private float countdown = 0;

    void Update()
    {
        countdown = Mathf.Clamp(countdown -= Time.deltaTime, 0, timeBetweenTick);
    }

    private void OnTriggerStay(Collider other) // A REVOIR
    {
        if (countdown != 0) return;
        if (other.gameObject.tag == typeOfTarget.ToString())
        {
            Collider[] colliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, transform.rotation, LayerMask.GetMask("Target"));

            if (countdown == 0)
            {
                DealDamage(colliders);
                countdown = timeBetweenTick;
            }
        }
    }


}
