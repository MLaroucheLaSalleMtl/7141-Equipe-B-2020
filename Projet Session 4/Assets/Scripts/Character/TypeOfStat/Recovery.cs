using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Recovery : Stat
{
    private float currentValue;
    public Stat recoveryValue = null;
    public bool canRecover = true;

    public float GetCurrentValue()
    {
        return currentValue;
    }
    public void DecreaseCurrentValue(float Amount)
    {
        currentValue = Mathf.Clamp(currentValue -= Amount, -100, GetBaseValue());
    }
    public void IncreaseCurrentValue(float Amount)
    {
        currentValue = Mathf.Clamp(currentValue += Amount, -100, GetBaseValue());
    }
    public void InitializeRecovery()
    {
        currentValue = GetBaseValue();
    }
    public void StartRecovery()
    {
        if (canRecover)
            Recover();
    }
    private float Recover()
    {
        if (currentValue >= GetBaseValue()) return currentValue;
        currentValue += recoveryValue.GetBaseValue() * Time.deltaTime;
        return Mathf.Clamp(currentValue, 0, GetBaseValue());
    }
    public bool CurrentIsEmpty()
    {
        return currentValue <= 0;
    }
}
