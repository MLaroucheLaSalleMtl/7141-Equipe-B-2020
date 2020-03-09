using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashUpgrade : MonoBehaviour
{
    private Player _Player = null;

    // Start is called before the first frame update
    void Start()
    {
        _Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void DashChangement()
    {
        //A faire en classe
    }
    public void UpgradeCountDash()
    {
        if (_Player.SkillPoints > 0)
        {
            if (_Player.DashSkillLevel == _Player.DashSkillLevelMax)
            {
                Debug.Log("This skill is at max Level");
            }
            else
            {
                _Player.HaveDashSkill = true;
                _Player.DashSkillLevel++;
                _Player.SkillPoints--;
            }
        }
    }
}
