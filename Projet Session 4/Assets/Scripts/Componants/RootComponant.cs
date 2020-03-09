using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootComponant : MonoBehaviour
{

    // A REVOIR 
    [SerializeField] private TypeOfTarget typeOfTarget = 0;
    public bool root = false;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == typeOfTarget.ToString())
        {
            collision.gameObject.GetComponent<Actor>().CanMove = false;
        }
    }
}
