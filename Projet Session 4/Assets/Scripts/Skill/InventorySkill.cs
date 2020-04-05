using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySkill : MonoBehaviour
{
    [Header(" - - Quick Bar - - ")]
    public bool[] isFull = new bool[4];
    public GameObject[] slots;
    [Header(" - - Book Of skills - - ")]
    public skill[] skillsBook = null;

    private Player _Player;
    [SerializeField] private GameObject spiritWolf = null;


    #region skills

    void Start()
    {
        _Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

    }
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
        _Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        Debug.Log(_Player);
        _Player.Dash.AddModifier(3);
        _Player.Dash.IncreaseCurrentValue(3);
        _Player.canDash = true;
    }
    public void EnableSpiritWolf()
    {
        _Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        Instantiate(spiritWolf, _Player.transform.position, _Player.transform.rotation);
    }
    public void AddSpiritKnowledge()
    {
        _Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _Player.Barrier.AddModifier(50);
        _Player.Barrier.IncreaseCurrentValue(50);
    }
    #endregion
}
