using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    protected bool secondStageActive = false;
    protected Actor actor = null;
    protected BossManager bossManager = null;
    protected Rigidbody rig;


    void Awake()
    {
        rig = GetComponent<Rigidbody>();
        actor = GetComponent<Actor>();
        bossManager = GameObject.Find("BossManager").GetComponent<BossManager>();
        bossManager.InitializeBox(actor);
    }
}
