using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Altar : MonoBehaviour
{
    [SerializeField] private bool isActive;
    public GameObject interactionSprite;
    [SerializeField] private ParticleSystem particle = null;

    void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (interactionSprite != null)
                interactionSprite.SetActive(true);

            if (collider.GetComponent<Player>().isInteracting == true && !isActive) // Switch Consumable
            {
                particle.Play();
                GameManager.NumberOfActiveAltar += 1;
                isActive = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (interactionSprite != null)
            interactionSprite.SetActive(false);
    }



}
