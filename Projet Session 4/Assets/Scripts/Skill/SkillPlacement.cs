using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPlacement : MonoBehaviour
{
    private InventorySkill inventory;
    public GameObject skill;
    private Player _Player;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySkill>();
        _Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void ClickandPlace()
    {
        if (_Player.SkillPoints > 0)
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.isFull[i] == false)
                {
                    inventory.isFull[i] = true;

                    Instantiate(skill, inventory.slots[i].transform, false);
                    _Player.SkillPoints--;
                    break;
                }
            }
        }
    }
}
