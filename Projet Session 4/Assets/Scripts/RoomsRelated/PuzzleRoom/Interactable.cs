using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool goodAnswer = false;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(goodAnswer)
            {
                GetComponentInParent<Puzzle>().Success();
            }
            else
            {
                GetComponentInParent<Puzzle>().Failure();
            }
        }
    }
}
