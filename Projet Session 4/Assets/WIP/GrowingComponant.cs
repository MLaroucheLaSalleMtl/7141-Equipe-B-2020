using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingComponant : MonoBehaviour
{
    public float growingScale = 0;
    public float growingRate = 0.1f;

    void Start()
    {
        InvokeRepeating("TimeUpdate", 0, growingRate);
    }

    void TimeUpdate()
    {
            transform.localScale += new Vector3(growingScale, 0, growingScale);      
    }
}
