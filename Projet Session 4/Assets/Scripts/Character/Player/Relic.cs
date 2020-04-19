using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Relic 
{
    
    public string _name = null;
    [TextArea(2,10)]
    public string _description = null;
    public GameObject lockedVisual = null;
    public bool isLocked = true;
    public GameObject activeVisual = null;
    public bool isActive = false;
    public Sprite icon = null;
    [TextArea(2, 10)]
    public string discoveryDescription = null;

    public Relic(string _name)
    {
        this._name = _name;
    }

    public void UnlockTheRelic()
    {
        isLocked = false;
    }
}
