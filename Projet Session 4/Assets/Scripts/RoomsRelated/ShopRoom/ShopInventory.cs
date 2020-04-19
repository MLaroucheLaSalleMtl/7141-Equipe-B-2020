using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInventory : MonoBehaviour
{
    public GameObject[] consumables;
    public GameObject[] positions;

    void Start()
    {
        InitializeShop();
    }

    public void InitializeShop()
    {
        for (int i = 0; i < consumables.Length; i++) // Permet de Randomize un array { Trouver sur internet }
        {
            GameObject rNB = consumables[i];
            int ranNumber = Random.Range(i, consumables.Length);
            consumables[i] = consumables[ranNumber];
            consumables[ranNumber] = rNB;
        }


        for (int i = 0; i < positions.Length; i++)
        {
            GameObject clone = Instantiate(consumables[i], positions[i].transform.position + new Vector3(0, 1, 0), Quaternion.Euler(-90, 0, 0), positions[i].transform);
        }
    }

    public void RerollShop()
    {
        foreach (GameObject child in positions)
        {
            child.GetComponent<ShopSlot>().DestroyItem();
        }
        InitializeShop();
    }

}
