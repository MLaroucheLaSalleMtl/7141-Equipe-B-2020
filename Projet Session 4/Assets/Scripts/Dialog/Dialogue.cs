using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue 
{
    public string name;
    public Sprite image;
    [TextArea(3,100)]
    public string[] texts;
    public Text nameText;
}
