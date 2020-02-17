using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    #region Variables
    [Header("Access points")]
    [SerializeField] private GameObject[] Doors = null;

    [Header("Room Related")]
    [SerializeField] private List<GameObject> envrionmentObjects = new List<GameObject>();
    [SerializeField] private List<GameObject> ennemies = new List<GameObject>();

    [SerializeField] private GameObject fog = null;
    private Actor actorManager;
    private bool activeFog = true;

    #endregion

    #region Unity's Methods
    void Start()
    {
        actorManager = GameObject.Find("Player").GetComponent<Actor>();
    }
    private void Update()
    {
        if (!actorManager.IsAlive)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter(Collider collider)
    {

        if (activeFog && collider.tag == "Player")
        {
            foreach (GameObject door in Doors)
            {
                door.SetActive(false);
            }
            foreach (GameObject obj in envrionmentObjects)
            {
                if (obj != null)
                obj.SetActive(true);
            }
            foreach(GameObject obj in ennemies)
            {
                obj.SetActive(true);
            }
            fog.SetActive(false);
        }
    }
    #endregion

    #region TOOLS
    // REVENIR ICI CAR CA SERAIT UTILE DE LE FAIRE FONCTIONNER...
    [ContextMenu("Assign all Environment Objects")]
    void AssignEnvironment()
    {
        List<GameObject> props = new List<GameObject>(GameObject.FindGameObjectsWithTag("Environment"));

        foreach (GameObject p in props)
        {
            envrionmentObjects.Add(p);
        }
    }
    #endregion
}
