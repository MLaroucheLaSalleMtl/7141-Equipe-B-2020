using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour // Brackeys Tuto 
{
    private Queue<string> texts;
    public Text nameText;
    public Text dialogueText;
    public Image image;
    public GameObject dialogueBox = null;
    private float timeBetweenLetter = 0f;
   // private bool skip = false;

    void Start()
    {
        texts = new Queue<string>();
    }

    public void StartDialogue (Dialogue dialogue)
    {
        nameText.text = dialogue.name;
        image.sprite = dialogue.image;
        dialogueBox.SetActive(true);
        texts.Clear();

        foreach (string text in dialogue.texts)
        {
            texts.Enqueue(text);
        }

        DisplayNextText();

        
    }


    public void DisplayNextText()
    {
        if(texts.Count == 0)
        {
            EndDialogue();
            return;
        }

            string text = texts.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeText(text));
        

    }
    IEnumerator TypeText (string text)
    {
        dialogueText.text = "";
        foreach (char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(timeBetweenLetter);
        }
    }

    public void EndDialogue()
    {
        dialogueBox.SetActive(false);
    }

}
