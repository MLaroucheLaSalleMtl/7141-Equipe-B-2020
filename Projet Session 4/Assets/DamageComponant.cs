using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageComponant : MonoBehaviour
{
    public float physicalDamage;
    public bool isDirty = false;

    private void OnTriggerEnter(Collider collision)
    {
        
        if (collision.gameObject.tag == "Enemy" && isDirty == false)
        {
            print("Tick");
            collision.gameObject.GetComponent<Enemy>().TakeDamage(physicalDamage, 1);
            isDirty = true;
        }
    }
}
