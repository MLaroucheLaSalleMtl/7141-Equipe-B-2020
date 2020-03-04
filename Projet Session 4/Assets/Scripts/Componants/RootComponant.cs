using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootComponant : MonoBehaviour
{
    [SerializeField] private TypeOfTarget typeOfTarget = 0;
    public bool root = false;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == typeOfTarget.ToString())
        {
            collision.gameObject.GetComponent<Enemy>().IsRoot();
        }
    }
}
