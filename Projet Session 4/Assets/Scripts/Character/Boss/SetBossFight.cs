using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBossFight : MonoBehaviour
{
    [SerializeField] private Boss FinalBoss = null;
    [SerializeField] private GameObject portal = null;
    private bool bossFightActivated = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bossFightActivated)
        {
            if (GameManager.victory) return;
            if (FinalBoss == null)
                GameManager.victory = true;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (!bossFightActivated && collider.tag == "Player")
        {
            FinalBoss.gameObject.SetActive(true);
            portal.SetActive(false);
            bossFightActivated = true;
        }
    }
}
