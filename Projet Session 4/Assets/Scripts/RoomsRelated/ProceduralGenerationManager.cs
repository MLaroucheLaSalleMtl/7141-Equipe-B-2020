using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class ProceduralGenerationManager : MonoBehaviour
{
    #region Variables & Initialization 
    [SerializeField] private int numberOfVillagePrefab = 0;
    [SerializeField] private int numberOfForestPrefab = 0;
    [SerializeField] private int numberOfPlainPrefab = 0;
    [SerializeField] private List<GameObject> waypoints = new List<GameObject>();
    private Actor actorManager;

    #region List of Rooms
    private int[] villageRoomsType = new int[24]; // The number represent the maximum number of generated rooms in the village section.
    private int[] forestRoomsType = new int[56]; // The number represent the maximum number of generated rooms in the forest section.
    private int[] plainRoomsType = new int[88]; // The number represent the maximum number of generated rooms in the Plain section.

    [Header("Lists of Rooms")]

    [SerializeField] private GameObject[] emptyRooms = null;
    [SerializeField] private GameObject[] battleRooms = null;
    [SerializeField] private GameObject[] treasureRooms = null;
    [SerializeField] private GameObject[] shopRooms = null;
    [SerializeField] private GameObject[] sanctuaryRooms = null;
    [SerializeField] private GameObject[] puzzleRooms = null;
    [SerializeField] private GameObject[] bossRooms = null;
    [SerializeField] private GameObject[] altarRooms = null;


    #endregion


    #endregion

    #region Unity's Methods

    #endregion

    #region Methods

    public void GenerateTheDungeon()
    {
        RandomizeAllListOfRoomsType();
        GenerateRooms();
    }


    #region Randomize the Generation
    private void GenerateTheListOfRoomType(int[] array)
    {
        array[0] = 2; // The first element is a treasure room
        array[1] = 3; // The second element is a shop room
        array[2] = 7; // The third element is an altar room

        for (int i = 3; i < array.Length; i++)
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
    private void RandomizeAllListOfRoomsType()
    {
        GenerateTheListOfRoomType(villageRoomsType);
        GenerateTheListOfRoomType(forestRoomsType);
        GenerateTheListOfRoomType(plainRoomsType);

    }

    #endregion
    private void GenerateRooms()
    {
        int x = 0;

        for (int i = 0; i < waypoints.Count; i++)
        {

            if (i < 24)
            {
                AssignRoomTypes(i,x, Random.Range(0,numberOfVillagePrefab), villageRoomsType);
            }
            else if (i >= 24 && i < 80)
            {
                if(x == 24 && i == 24) { x = 0; }
                AssignRoomTypes(i,x,numberOfForestPrefab + numberOfVillagePrefab -1, forestRoomsType);

            }
            else if (i > 79)
            {
                if (x >= 55 && i == 80) { x = 0; }
                AssignRoomTypes(i, x, numberOfForestPrefab + numberOfVillagePrefab + numberOfPlainPrefab -1, plainRoomsType);
            }
            x++;
        }
    }
    private void AssignRoomTypes(int WaypointIndex ,int ArrayIndex, int RoomID, int[] array )
    {
        switch (array[ArrayIndex])
        {
            case 0: { Instantiate(emptyRooms[RoomID], waypoints[WaypointIndex].transform.position, Quaternion.identity); } break;
            case 1: { Instantiate(battleRooms[RoomID], waypoints[WaypointIndex].transform.position, Quaternion.identity); } break;
            case 2: { Instantiate(treasureRooms[RoomID], waypoints[WaypointIndex].transform.position, Quaternion.identity); } break;
            case 3: { Instantiate(shopRooms[RoomID], waypoints[WaypointIndex].transform.position, Quaternion.identity); } break;
            case 4: { Instantiate(sanctuaryRooms[RoomID], waypoints[WaypointIndex].transform.position, Quaternion.identity); } break;
            case 5: { Instantiate(puzzleRooms[RoomID], waypoints[WaypointIndex].transform.position, Quaternion.identity); } break;
            case 6: { Instantiate(bossRooms[RoomID], waypoints[WaypointIndex].transform.position, Quaternion.identity); } break;
            case 7: { Instantiate(altarRooms[RoomID], waypoints[WaypointIndex].transform.position, Quaternion.identity); } break;

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
