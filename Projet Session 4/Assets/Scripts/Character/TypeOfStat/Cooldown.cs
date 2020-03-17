using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Cooldown : Stat
{
    private float countdownValue;
    public void StartCooldown()
    {
        if (countdownValue == 0) return;
        else
            countdownValue = Mathf.Clamp(countdownValue -= Time.deltaTime, 0, GetBaseValue());
    }
    public float GetCountdownValue()
    {
        return countdownValue;
    }
    public void ResetCountdown()
    {
        countdownValue = GetBaseValue();
    }
    public bool IsFinish()
    {
        return countdownValue == 0;
    }

}
