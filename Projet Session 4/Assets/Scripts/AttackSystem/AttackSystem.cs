using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
    [SerializeField] private GameObject hitBox;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MeleeAttack();
    }

    public void MeleeAttack()
    {
        if (Input.GetButtonDown("Attack"))
        {
            Instantiate(hitBox, transform.position + (transform.forward * 2), transform.rotation);
        }
    }
}
