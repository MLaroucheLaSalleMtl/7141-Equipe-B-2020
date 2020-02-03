using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class ProceduralGenerationManager : MonoBehaviour
{
    #region Variables & Initialization 
    [SerializeField] private List<GameObject> waypoints = new List<GameObject>();

    #region List of Rooms
    private int[] villageRoomsType = new int[24]; // The number represent the maximum number of generated rooms in the village section.
    private int[] forestRoomsType = new int[56]; // The number represent the maximum number of generated rooms in the village section.
    private int[] plainRoomsType = new int[88]; // The number represent the maximum number of generated rooms in the village section.

    [Header("Lists of Rooms")]

    [SerializeField] private GameObject[] emptyRooms = null;
    [SerializeField] private GameObject[] battleRooms = null;
    [SerializeField] private GameObject[] treasureRooms = null;
    [SerializeField] private GameObject[] shopRooms = null;
    [SerializeField] private GameObject[] sanctuaryRooms = null;
    [SerializeField] private GameObject[] puzzleRooms = null;
    [SerializeField] private GameObject[] bossRooms = null;
    #endregion

    [Header("Player")]
    [SerializeField] private GameObject player = null;

    #endregion

    #region Unity's Methods
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            TestDummyScript.IsAlive = true;
            RandomGeneratedArray(); 
            GenerateRooms();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            TestDummyScript.IsAlive = false;
            player.transform.position = new Vector3(0, 1.17f, 0);

        }

    }
    #endregion

    #region Methods
    private void RandomGeneratedArray()
    {
        RandomizeRooms(villageRoomsType);
        RandomizeRooms(forestRoomsType);
        RandomizeRooms(plainRoomsType);

    }

    private void GenerateRooms()
    {
        int x = 0;

        for (int i = 0; i < waypoints.Count; i++)
        {

            if (i < 24)
            {
                //  int randomNumber = Random.Range(0, 24);
                InstantiateRooms(i,x, 0, villageRoomsType);
            }
            else if (i >= 24 && i < 80)
            {
                if(x >= 23) { x = 0; }
               // int randomNumber = Random.Range(24, 80);
                InstantiateRooms(i,x,1, forestRoomsType);
            }
            else if (i > 79)
            {
                if (x >= 55) { x = 0; }
                // int randomNumber = Random.Range(80, 168);
                InstantiateRooms(i, x, 2, plainRoomsType);
            }
            x++;
        }
    }


    private void InstantiateRooms(int i ,int x, int RoomID, int[] array )
    {
        switch (array[x])
        {
            case 0: { Instantiate(emptyRooms[RoomID], waypoints[i].transform.position, Quaternion.identity); } break;
            case 1: { Instantiate(battleRooms[RoomID], waypoints[i].transform.position, Quaternion.identity); } break;
            case 2: { Instantiate(treasureRooms[RoomID], waypoints[i].transform.position, Quaternion.identity); } break;
            case 3: { Instantiate(shopRooms[RoomID], waypoints[i].transform.position, Quaternion.identity); } break;
            case 4: { Instantiate(sanctuaryRooms[RoomID], waypoints[i].transform.position, Quaternion.identity); } break;
            case 5: { Instantiate(puzzleRooms[RoomID], waypoints[i].transform.position, Quaternion.identity); } break;
            case 6: { Instantiate(bossRooms[RoomID], waypoints[i].transform.position, Quaternion.identity); } break;
        }
    }

    private void RandomizeRooms(int[] array)
    {
        array[0] = 2; // The first element is a treasure room
        array[1] = 3; // The second element is a shop room

        for (int i = 2; i < array.Length; i++)
        {
            if (i % 10 == 0)
            {
                array[i] = Random.Range(2, 6); // [ 2 = Treasure, 3 = Shop, 4 = Sanctuary, 5 = Puzzle ]
            }
            else if (i % 21 == 0)
            {
                array[i] = 6; // [ 6 = Boss ]
            }
            else
            {
                array[i] = Random.Range(0, 2); // [ 0 = Nothing, 1 = Battle ]
            }
        }

        for (int i = 0; i < array.Length; i++) // Permet de Randomize un array { Trouver sur internet }
        {
            int rNB = array[i];
            int ranNumber = Random.Range(i, array.Length);
            array[i] = array[ranNumber];
            array[ranNumber] = rNB;
        }
    }
    #endregion





    #region TOOLS
    [ContextMenu("Assign all waypoints")]
    void AssignWaypoints()
    {
        List<GameObject> waypoints = new List<GameObject>(GameObject.FindGameObjectsWithTag("Waypoint"));

        foreach (GameObject w in waypoints)
        {
            this.waypoints.Add(w);
        }
    }
    #endregion
}
