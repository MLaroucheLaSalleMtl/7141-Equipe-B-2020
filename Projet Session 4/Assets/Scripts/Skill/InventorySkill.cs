using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySkill : MonoBehaviour
{
    public bool[] isFull = new bool[4];
    public GameObject[] slots;

    [Header(" - - Book Of skills - - ")]
    public skill[] skillsBook = null;



    #region skills

    public skill GetASkill(string Name)
    {
        foreach (skill _skill in skillsBook)
        {

            if (_skill.SkillName == Name)
            {
                return _skill;
            }
        }
        return skillsBook[0];
    }

    public void EnableDash()
    {
        GetComponent<Player>().Dash.AddModifier(3);
        GetComponent<Player>().Dash.IncreaseCurrentValue(3);
        GetComponent<Player>().canDash = true;
    }
    #endregion
}
