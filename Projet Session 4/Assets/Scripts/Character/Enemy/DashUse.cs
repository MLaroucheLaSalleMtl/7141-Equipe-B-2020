using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashUse : MonoBehaviour
{
    Enemy enemyScript;
    public float dashDelay;
    bool delayIsDone = false;
    void Start()
    {
        enemyScript = transform.GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyScript.DashCountdown >= enemyScript.DashCooldown.GetValue() && enemyScript.DashCurrent >= 1f)
        {
            if(delayIsDone)
            {
                StartCoroutine(enemyScript.UseDash());
            }
            StartCoroutine(DashDelay());
        }
        enemyScript.StartDashCooldown();
    }
    private IEnumerator DashDelay()
    {
        delayIsDone = false;
        yield return new WaitForSeconds(dashDelay);
        delayIsDone = true;
    }
}
