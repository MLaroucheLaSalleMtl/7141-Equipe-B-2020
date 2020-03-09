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
        healthBar.fillAmount = _boss.Health.GetCurrentValue() / _boss.Health.GetBaseValue();
        BossBox.SetActive(true);
        InvokeRepeating("UpdateBoss", 0, 0.1f);
    }

    public void UpdateBoss()
    {
        healthBar.fillAmount = Boss.Health.GetCurrentValue() / Boss.Health.GetBaseValue();
        if (Boss.Health.CurrentIsEmpty())
            BossBox.SetActive(false);
    }
}
