using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveSkill : MonoBehaviour
{
    private InventorySkill inventory;
    private Player player;
    public int i;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySkill>();
    }
    private void Update()
    {
        if(transform.childCount <= 0)
        {
            inventory.isFull[i] = false;
        }
    }
    public void SkillRemove()
    {
        foreach(Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
            player.SkillPoints++;
        }
    }
}
