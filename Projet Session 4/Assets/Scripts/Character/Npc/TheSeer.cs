using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheSeer : Npc
{

    public void ShowDivinity()
    {

        GameObject.Find("UIManager").GetComponent<UI_Manager>().PanelToggle(2);
    }
}
