using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class skill
{
    public int currentUpgrade = 0;
    private int maxUpgrade = 3;
    [SerializeField] private List<string> description = null;

    public int GetMaxUpgrade()
    {
        return maxUpgrade;
    }

    public string GetDescription()
    {
        int i = currentUpgrade;
        return description[i];
    }


}
