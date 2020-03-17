using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class passive
{
    private int currentUpgrade = 0;
    private int maxUpgrade = 1;
    [SerializeField] private bool learned = false;
    [SerializeField] private string skillName = "";
    [SerializeField] private bool locked = false;
    [SerializeField] private int levelRequierement = 0;

    [SerializeField] private List<string> descriptions = null;

    public string SkillName { get => skillName; }
    public bool Learned { get => learned; }
    public int LevelRequierement { get => levelRequierement; }
    public int CurrentUpgrade { get => currentUpgrade; set => currentUpgrade = value; }

    public bool IsMaxLevel()
    {
        return currentUpgrade == maxUpgrade;
    }

    public int GetMaxUpgrade()
    {
        return maxUpgrade;
    }
    public string GetDescription()
    {
        int i = currentUpgrade;
        // if (i == maxUpgrade) return descriptions[i-1];
        return descriptions[i];
    }

    public void LearnSkill()
    {
        learned = true;
    }
    public bool IsLearned()
    {
        return learned;
    }

    public bool IsLocked()
    {
        return locked;
    }
    public void Unlock()
    {
        locked = false;
    }


}
