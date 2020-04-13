using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowingComponant : MonoBehaviour
{
    public float growingScale = 0;
    public float growingRate = 0.1f;
    public bool notY = false;

    void Start()
    {
        InvokeRepeating("TimeUpdate", 0, growingRate);
    }

    void TimeUpdate()
    {
        if(!notY)
            transform.localScale += new Vector3(growingScale, growingScale, growingScale);     
        else
            transform.localScale += new Vector3(growingScale, 0, growingScale);

    }
}
