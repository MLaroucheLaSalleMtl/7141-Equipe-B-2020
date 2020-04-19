using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationComponant : MonoBehaviour
{
    [SerializeField] private float rotationScaling = -1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Rotate(0, rotationScaling, 0,Space.World);
    }
}
