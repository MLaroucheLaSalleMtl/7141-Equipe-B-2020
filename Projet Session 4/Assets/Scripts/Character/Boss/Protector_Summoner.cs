using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Protector_Summoner : MonoBehaviour
{
    [SerializeField] private int health = 5;
    private int currentHealth;
    [SerializeField] private Image img_HealthBar = null;
    void Start()
    {
        currentHealth = health;
        GetComponentInParent<BossBehaviour_Summoner>().numberOfActive++;
    }

    void Update()
    {
        Death();
        img_HealthBar.fillAmount =  (float)currentHealth / health;
    }

    public void TakeDamage()
    {
        currentHealth--;
    }

    void Death()
    {
        if(currentHealth <= 0)
        {
        GetComponentInParent<BossBehaviour_Summoner>().numberOfActive--;
        Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<DamageComponant>() != null)
        {
            TakeDamage();
        }
    }

}
