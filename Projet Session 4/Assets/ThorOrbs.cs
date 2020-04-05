using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThorOrbs : MonoBehaviour
{

    void OnTriggerEnter(Collider collider)
    {
        if( collider.tag == "Enemy")
        {
            gameObject.SetActive(false);
        }
    }

}
