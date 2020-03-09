using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPlacement : MonoBehaviour
{
    #region Variables
    private InventorySkill inventory;
    public GameObject skill;
    private Player _Player;
    private bool damagebuffIsThere = false;
    private bool healIsThere = false;
    private bool rootIsThere = false;
    private bool fireIsThere = false;
    [SerializeField] private GameObject InfoPanel = null;
    #endregion

    private void Start()
    {
        InfoPanel.SetActive(false);
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySkill>();
        _Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    #region SkillPLacement Method
    public void ClickandPlace()
    {
        if (_Player.SkillPoints > 0)
        {
            switch(skill.name)
            {
                case "DamageBuff [ Effect ]":
                    {
                        if(!damagebuffIsThere)
                        {
                            InstantiateSkill();
                            if(_Player.SkillPoints == 0)
                                _Player.damageBuff.currentUpgrade++;
                        }
                        break;
                    }
                case "Heal [ Effect ]":
                    {
                        if (!healIsThere)
                        {
                            InstantiateSkill();
                            if (_Player.SkillPoints == 0)
                                _Player.HealLevel++;
                        }
                        break;
                    }
                case "Root aoe [ Effect ]":
                    {
                        if (!rootIsThere)
                        {
                            InstantiateSkill();
                            if (_Player.SkillPoints == 0)
                                _Player.RootCircleLevel++;
                        }
                        break;
                    }

                case "FireBall [ Effect ]":
                    {
                        if (!fireIsThere)
                        {
                            InstantiateSkill();
                            if (_Player.SkillPoints == 0)
                                _Player.FireBallLevel++;
                        }
                        break;
                    }
            }
        }
    }
    void InstantiateSkill()
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
    #endregion

    #region UpgradeCount Method
    public void UpgradeCountForDamageBuff()
    {
        if (_Player.SkillPoints > 0)
        {
            if (_Player.damageBuff.currentUpgrade == _Player.damageBuff.GetMaxUpgrade())
            {
                Debug.Log("This skill is at max Level");
            }
            else
            {
                _Player.damageBuff.currentUpgrade++;
                if (damagebuffIsThere)
                {
                    _Player.SkillPoints--;
                }
                damagebuffIsThere = true;
            }
        }
    }
    public void UpgradeCountHeal()
    {
        if (_Player.SkillPoints > 0)
        {
            if (_Player.HealLevel == _Player.HealLevelMax)
            {
                Debug.Log("This skill is at max Level");
            }
            else
            {
                _Player.HealLevel++;
                if (healIsThere)
                {
                    _Player.SkillPoints--;
                }
                healIsThere = true;
            }
        }
    }
    public void UpgradeCountRoot()
    {
        if (_Player.SkillPoints > 0)
        {
            if (_Player.RootCircleLevel == _Player.RootCircleLevelMax)
            {
                Debug.Log("This skill is at max Level");
            }
            else
            {
                _Player.RootCircleLevel++;
                if (rootIsThere)
                {
                    _Player.SkillPoints--;
                }
                rootIsThere = true;
            }
        }                  
    }
    public void UpgradeCountFireBall()
    {
        if (_Player.SkillPoints > 0)
        {
            if (_Player.FireBallLevel == _Player.FireBallLevelMax)
            {
                Debug.Log("This skill is at max Level");
            }
            else
            {
                _Player.FireBallLevel++;
                if (fireIsThere)
                {
                    _Player.SkillPoints--;
                }
                fireIsThere = true;
            }
        }
    }
    #endregion

    #region Panel Methods
    public void ShowPanel()
    {
        InfoPanel.SetActive(true);
    }
    public void HidePanel()
    {
        InfoPanel.SetActive(false);
    }
    #endregion
}
