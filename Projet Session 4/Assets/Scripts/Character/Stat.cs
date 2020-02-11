using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat 
{

    [SerializeField] private float baseValue = 0;

    private List<float> modifiers = new List<float>();

    public float GetValue ()
    {
        float finalvalue = baseValue;
        modifiers.ForEach(x => finalvalue += x); // Question ici 
        return finalvalue;
    }

    public void AddModifier ( float modifier)
    {
        if(modifier != 0)
        {
            modifiers.Add(modifier);
        }
    }

    public void RemoveModifier(float modifier)
    {
        if (modifier != 0)
        {
            modifiers.Remove(modifier);
        }
    }

}
