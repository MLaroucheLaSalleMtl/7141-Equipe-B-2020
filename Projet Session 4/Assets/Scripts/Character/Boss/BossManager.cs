using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{
    private Actor Boss;
    public Image healthBar;
    public Image BarrierBar;
    public Text nameText;
    public Color barColor;
    public GameObject BossBox = null;

    public void InitializeBox(Actor _boss)
    {
        barColor = healthBar.color;
        Boss = _boss;
        nameText.text = _boss.name;
        healthBar.fillAmount = _boss.Health.GetCurrentValue() / _boss.Health.GetBaseValue();
        BarrierBar.fillAmount = _boss.Barrier.GetCurrentValue() / _boss.Health.GetBaseValue();
        BossBox.SetActive(true);
        InvokeRepeating("UpdateBoss", 0, 0.1f);
    }


    public void UpdateBoss()
    {
        if (Boss.isInvulnerable)
            healthBar.color = Color.yellow;
        else
            healthBar.color = barColor;

        healthBar.fillAmount = Boss.Health.GetCurrentValue() / Boss.Health.GetBaseValue();
        BarrierBar.fillAmount = Boss.Barrier.GetCurrentValue() / Boss.Health.GetBaseValue();
        if (Boss.Health.CurrentIsEmpty())
            BossBox.SetActive(false);
    }
}
