using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacrificePit : MonoBehaviour
{
    public GameObject reward;
    private float delayBetweenTry = 1f;
    public int rewardChance;


    void Update()
    {
        if (delayBetweenTry > 0)
            delayBetweenTry -= Time.deltaTime;
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player" && delayBetweenTry <= 0)
        {
            int roll = Random.Range(0, 101);

            if(rewardChance > roll)
            {
                other.GetComponent<Player>().TakeDamage(20, 0, 0, 0, "True");
                reward.SetActive(true);
                delayBetweenTry = 1f;
            }
            else
            {
                other.GetComponent<Player>().TakeDamage(20,0,0,0,"True");
                delayBetweenTry = 1f;

            }
        }
    }

    

}
