using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    [Header("Result")]
    [SerializeField] private GameObject[] rewards = null;
    [SerializeField] private GameObject[] consequences = null;
    [SerializeField] private GameObject[] positions = null;
    [SerializeField] private GameObject[] answers = null;
    // private bool success = false;
    // private bool failure = false;
    
    void Start()
    {
        InitializePuzzle();
    }

    void InitializePuzzle()
    {
        for (int i = 0; i < answers.Length; i++) // Permet de Randomize un array { Trouver sur internet }
        {
            GameObject rNB = answers[i];
            int ranNumber = Random.Range(i, answers.Length);
            answers[i] = answers[ranNumber];
            answers[ranNumber] = rNB;
        }


        for (int i = 0; i < positions.Length; i++)
        {
            GameObject clone = Instantiate(answers[Random.Range(0,answers.Length)], positions[i].transform.position, Quaternion.identity, positions[i].transform);
        }
    }



    public void Success()
    {
        foreach (GameObject reward in rewards)
        {
            reward.SetActive(true);
            Destroy(gameObject);
        }
    }
    public void Failure()
    {
        foreach (GameObject consequence in consequences)
        {
            consequence.SetActive(true);
            Destroy(gameObject);
        }
    }
}
