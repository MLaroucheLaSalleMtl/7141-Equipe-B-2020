using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Npc : MonoBehaviour
{
    public Player listener;
    public Dialogue dialogue;
    public GameObject npcOptions;
    public GameObject interactionSprite;

    void Awake()
    {
        dialogue.nameText.text = dialogue.name;

    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

     void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            listener = other.GetComponent<Player>();
            interactionSprite.SetActive(true);
        }
    }
    void OnTriggerExit(Collider other)
    {
        npcOptions.SetActive(false);
        interactionSprite.SetActive(false);
        FindObjectOfType<DialogueManager>().EndDialogue();

    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(other.GetComponent<Player>().isInteracting == true)
            {
                interactionSprite.SetActive(false);
                npcOptions.SetActive(true); 
            }
        }
    }

}
