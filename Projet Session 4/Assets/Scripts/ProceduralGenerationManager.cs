using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class ProceduralGenerationManager : MonoBehaviour
{
    #region Variables & Initialization 
   // [ContextMenuItem("Assign all Waypoints", "AssignWaypoints")]
    [SerializeField] private List<GameObject> villageWaypoints = new List<GameObject>();
    [SerializeField] private GameObject[] villageCells = null;
    [SerializeField] private GameObject[] forestCells = null;
    [SerializeField] private GameObject player;

    #endregion

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            for(int i = 0; i < villageWaypoints.Count; i++)
            {
                if (i < 24)
                {
                    int randomNumber = Random.Range(0, villageCells.Length);
                    Instantiate(villageCells[randomNumber], villageWaypoints[i].transform.position, Quaternion.identity);
                }
                else if(i < 90)
                {
                    int randomNumber = Random.Range(0, forestCells.Length);
                    Instantiate(forestCells[randomNumber], villageWaypoints[i].transform.position, Quaternion.identity);
                }

            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            player.transform.position = new Vector3(0, 1.17f, 0);
            foreach (GameObject item in villageCells)
            {
                Destroy(item.gameObject);
            }
            
        }
    }

    #region TOOLS
    [ContextMenu("Assign all waypoints")]
    void AssignWaypoints()
    {
        List<GameObject> waypoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("Waypoint"));

        foreach (GameObject w in waypoints)
        {
            villageWaypoints.Add(w);
        }
    }
    #endregion
}
