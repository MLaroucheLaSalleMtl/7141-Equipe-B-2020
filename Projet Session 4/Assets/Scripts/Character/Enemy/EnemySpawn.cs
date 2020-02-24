using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemies = new List<GameObject>();
    [SerializeField] private List<GameObject> spawnPoints = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < spawnPoints.Count; i++) // Permet de Randomize un array { Trouver sur internet }
        {
            GameObject rNB = spawnPoints[i];
            int ranNumber = Random.Range(i, spawnPoints.Count);
            spawnPoints[i] = spawnPoints[ranNumber];
            spawnPoints[ranNumber] = rNB;
        }

        for (int i=0;i<enemies.Count;i++)
        {  
            Instantiate(enemies[i], spawnPoints[i].transform.position + transform.up, Quaternion.identity);
        }           
    }





}
