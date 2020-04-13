using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAllSkills : MonoBehaviour
{
    private InventorySkill skill = null;
    private Player player = null;
    public bool resetON = false;
    [SerializeField] private GameObject[] skillUI = null;
    [SerializeField] private SkillPlacement[] SkillTreeIcon = null;

    // Start is called before the first frame update
    void Start()
    {
        skill = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySkill>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetSkills()
    {
        foreach (skill skills in skill.skillsBook)
        {
            if (skills.maxUpgrade == 0)
            {
            }
            else
            {
                if (skills.CurrentUpgrade == 2)
                {
                    player.SkillPoints += 3;
                }
                if (skills.CurrentUpgrade == 1)
                {
                    player.SkillPoints += 2;
                }
                if (skills.CurrentUpgrade == 0 && skills.Learned == true)
                {
                    player.SkillPoints++;
                }
                skills.Learned = false;
                skills.CurrentUpgrade = 0;
            }
        }
        foreach (GameObject obj in skillUI)
        {
            obj.GetComponent<RemoveSkill>().SkillRemove();
        }
        StartCoroutine(Reset());
    }

    IEnumerator Reset()
    {
        resetON = true;
        yield return new WaitForSeconds(0.2f);
        resetON = false;
    }

}
