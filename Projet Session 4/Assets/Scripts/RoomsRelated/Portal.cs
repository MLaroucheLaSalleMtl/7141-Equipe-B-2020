using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

    [SerializeField] private GameObject exit = null;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (exit != null)
                other.gameObject.transform.position = exit.transform.localPosition + new Vector3(0, -1, 2);
        }
    }

}
