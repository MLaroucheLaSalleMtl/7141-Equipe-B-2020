using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DurationComponant : MonoBehaviour
{
    [SerializeField] private float expirationTime = 3f;
    void Update()
    {
        expirationTime -= 1 * Time.deltaTime;
        if (expirationTime <= 0)
            Destroy(gameObject);
    }
}
