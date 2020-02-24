using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private List<GameObject> ennemies = new List<GameObject>();
    [SerializeField] private List<GameObject> spawnPoints = new List<GameObject>();

    void Start()
    {
        
        for(int i=0;i<ennemies.Count;i++)
        {  
            Instantiate(ennemies[i], spawnPoints[i].transform.position + transform.up, Quaternion.identity);
        }           
    }
}
