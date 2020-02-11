using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Spell //En test je sais vraiment pas ce que je fais .
{
    #region Base
    public string name;
    private string description;
    private Texture icon;
    private int id;
    #endregion

    #region Management
    private float costMana;
    private float costHealth;
    private float cooldown;
    private int level;
    private int levelMax;
    #endregion

}
