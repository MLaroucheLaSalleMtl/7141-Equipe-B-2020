using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour_Spirit : MonoBehaviour
{

    private bool stage2 = false;
    private Actor actor = null;
    private BossManager bossManager = null;

    void Start()
    {
        actor = GetComponent<Actor>();
        // currentState = State.OrbitalBeam;
        bossManager = GameObject.Find("BossManager").GetComponent<BossManager>();
        bossManager.InitializeBox(actor);

    }

    void Update()
    {
       

        if (actor.Health.GetCurrentValue() <= actor.Health.GetBaseValue() / 2 && !stage2)
        {
            stage2 = true;
        }
    }

}
