using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    #region Variables
    [SerializeField] private GameObject[] buttons = null;
    [SerializeField] private List<GameObject> environment = new List<GameObject>();
    [SerializeField] private GameObject fog = null;
    [SerializeField] private bool activeFog = true;
    #endregion

    #region Unity's Methods
    private void OnTriggerEnter(Collider collider)
    {
        
        if (activeFog && collider.tag == "Player")
        {
            foreach (GameObject button in buttons)
            {
                button.SetActive(false);
            }
            foreach (GameObject e in environment)
            {
                e.SetActive(true);
            }
            fog.SetActive(false);
        }
    }
    #endregion

    // REVENIR ICI CAR CA SERAIT UTILE DE LE FAIRE FONCTIONNER...
    [ContextMenu("Assign all Environment Objects")]
    void AssignEnvironment()
    {
        List<GameObject> props = new List<GameObject>(GameObject.FindGameObjectsWithTag("Environment"));

        foreach (GameObject p in props)
        {
            Debug.Log(p.name);
           // environment.Add(p);
        }
    }
}
