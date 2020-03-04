using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AncientSpirit : Npc
{
    [Header("Ancient Spirit")]
    [SerializeField] private GameObject[] wishPositions = null;
    [SerializeField] private GameObject[] randomEffects = null;

    void Start()
    {
        InitializeWishChoice();
    }

    public void InitializeWishChoice()
    {
        for (int i = 0; i < randomEffects.Length; i++) // Permet de Randomize un array { Trouver sur internet }
        {
            GameObject rNB = randomEffects[i];
            int ranNumber = Random.Range(i, randomEffects.Length);
            randomEffects[i] = randomEffects[ranNumber];
            randomEffects[ranNumber] = rNB;
        }


        for (int i = 0; i < wishPositions.Length; i++)
        {
            GameObject clone = Instantiate(randomEffects[i], wishPositions[i].transform.position, transform.rotation, wishPositions[i].transform);
        }
    }





}
