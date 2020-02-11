using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    Rigidbody rigidbody;
    public float moveSpeed = 25f;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        rigidbody.velocity = transform.TransformDirection(0, 0, moveSpeed);
    }
}
