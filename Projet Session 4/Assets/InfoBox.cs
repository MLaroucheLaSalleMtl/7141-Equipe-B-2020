using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoBox : MonoBehaviour
{
    [SerializeField] private Text skillName = null;
    [SerializeField] private bool isAPassive = false;
    [SerializeField] private Text skillLevel = null;
    [SerializeField] private Text skillDescription = null;

    private skill skill;

    void Start()
    {
        if (GetComponentInParent<SkillPlacement>() != null)
            skill = GetComponentInParent<SkillPlacement>().Skill;
        else
            skill = GetComponentInParent<PassivePlacement>().Skill;

        UpdateInfoBox();
    }

    public void UpdateInfoBox()
    {
        skillName.text = skill.SkillName;

        if (!skill.IsLearned())
            skillLevel.text = "Unlearned";
        else if (isAPassive)
            skillLevel.text = "Passive Activated ";
        else
            skillLevel.text = "Level : " + (skill.CurrentUpgrade + 1);
        skillDescription.text = skill.GetDescription();
    }

}
