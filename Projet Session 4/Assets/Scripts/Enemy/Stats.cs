using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] private int hp = 20;
    [SerializeField] private int dam;
    [SerializeField] private int def;
    [SerializeField] private int acc; // accuracy

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        IsDead();
    }

    private void OnTriggerEnter(Collider collision)
    {
            if (collision.gameObject.tag == "meleeAttack")
            {
                hp -= 5;
                
            }
    }
    void IsDead()
    {
        if(hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
