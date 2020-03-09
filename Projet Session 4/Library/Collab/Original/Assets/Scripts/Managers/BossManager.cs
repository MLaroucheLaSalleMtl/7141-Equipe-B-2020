using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    private Boss Boss;
    public Image healthBar;
    public Text nameText;
    public GameObject BossBox = null;

    public void InitializeBox(Boss _boss)
    {
        Boss = _boss;
        nameText.text = _boss.BossName;
        healthBar.fillAmount = _boss.HealthCurrent / _boss.HealthMaximum.GetValue();
        BossBox.SetActive(true);
        InvokeRepeating("UpdateBoss", 0, 0.1f);
    }

    public void UpdateBoss()
    {
        healthBar.fillAmount = Boss.HealthCurrent / Boss.HealthMaximum.GetValue();
        if (Boss.HealthCurrent <= 0)
            BossBox.SetActive(false);
    }
}
