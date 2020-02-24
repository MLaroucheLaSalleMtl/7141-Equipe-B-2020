using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    Enemy parentScript;
    private void Start()
    {
        parentScript = transform.parent.GetComponent<Enemy>();
    }
    private void OnTriggerEnter(Collider detect)
    {
        if (detect.gameObject.tag == "Player")
        {
            parentScript.EnemyDetected();
        }
    }
}
