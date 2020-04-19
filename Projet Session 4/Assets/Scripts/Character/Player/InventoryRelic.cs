using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryRelic : MonoBehaviour
{
    private Actor actor;
    private EffectsComponant effectComponant;
    [SerializeField] private Cooldown cooldownBetweenChange = null;
    [SerializeField] private Image cooldownVisual = null;

    [SerializeField] private Text divinityName = null;
    [SerializeField] private Text divinityDescription = null;

    public Relic[] relics = null;

    public string currentRelic = null;

    [Header("Odin")]
    public Cooldown odinBlessingCD = null;
    public GameObject iconOdin = null;

    [Header("Loki")]
    public Cooldown lokiBlessingCD = null;
    private bool lokiDamageReady = false;
    public GameObject icon = null;

    [Header("Thor")]
    public Cooldown thorBlessingCD = null;
    public GameObject partToRotate = null;

    [Header("Freya")]
    public bool freyaActive = false;




    void Awake()
    {
        effectComponant = GetComponent<EffectsComponant>();
        actor = GetComponent<Actor>();

    }

    void Start()
    {

    }

    void Update()
    {
        cooldownVisual.fillAmount = cooldownBetweenChange.GetCountdownValue() / cooldownBetweenChange.GetBaseValue();
        cooldownBetweenChange.StartCooldown();
        divinityName.text = currentRelic;

        switch (currentRelic)
        {
            

            case "Odin":
                {
                    if (!relics[0].isLocked)
                    {
                        OdinBlessing();
                        divinityDescription.text = relics[0]._description;
                        iconOdin.SetActive(true);

                    }

                } break;
            case "Loki":
                {
                    if (!relics[1].isLocked)
                    {
                        LokiBlessing();
                        divinityDescription.text = relics[1]._description;

                    }
                } break;
            case "Thor":
                {
                    if (!relics[2].isLocked)
                    {
                        ThorBlessing();
                        divinityDescription.text = relics[2]._description;

                    }
                }
                break;
            case "Freya":
                {
                    if (!relics[3].isLocked)
                    {
                        if(!freyaActive)
                        {
                            GetComponent<Player>().ExperienceRatio += 0.5f;
                            divinityDescription.text = relics[3]._description;

                            freyaActive = true;
                        }
                    }
                }
                break;
        }

    }

    public void ActivateBlessing(string name)
    {
        if (cooldownBetweenChange.IsFinish())
        {
            foreach (Relic relic in relics)
            {
                if (relic._name == name && !relic.isLocked)
                {
                    currentRelic = name;
                    relic.isActive = true;
                    relic.activeVisual.SetActive(true);
                    cooldownBetweenChange.ResetCountdown();

                }
                else 
                {
                    relic.isActive = false;
                    relic.activeVisual.SetActive(false);

                    if(relic._name == "Odin")
                    {
                        iconOdin.SetActive(false);
                    }

                    if (relic._name == "Loki")
                    {
                        icon.SetActive(false);
                    }

                        if (relic._name == "Freya")
                    {
                        GetComponent<Player>().ExperienceRatio = 1;
                        freyaActive = false;
                    }

                    if (relic._name == "Thor")
                    {
                        foreach (Transform child in partToRotate.gameObject.transform)
                        {
                            child.gameObject.SetActive(false);
                        }
                    }
                }
            }
        }
    }

    public void ThorBlessing()
    {
        thorBlessingCD.StartCooldown();
        partToRotate.transform.Rotate(new Vector3(0, 150 * Time.deltaTime, 0));


        if (thorBlessingCD.IsFinish())
        {
            foreach (Transform child in partToRotate.gameObject.transform)
            {
                child.GetComponent<DamageComponant>().caster = actor;
                child.gameObject.SetActive(true);

                thorBlessingCD.ResetCountdown();
            }
        }
    }

    public void OdinBlessing()
    {
        odinBlessingCD.StartCooldown();

        if (odinBlessingCD.IsFinish())
        {
            actor.Barrier.IncreaseCurrentValue(5);
            odinBlessingCD.ResetCountdown();
        }
    }

    public void LokiBlessing()
    {
        lokiBlessingCD.StartCooldown();

        if ( lokiBlessingCD.IsFinish() && !lokiDamageReady)
        {
            icon.SetActive(true);
            actor.CriticalChance.AddModifier(100);
            lokiDamageReady = true;
            lokiBlessingCD.ResetCountdown();
        }
        else if(lokiDamageReady && actor.AttackSpeed.GetCountdownValue() != 0)
        {
            icon.SetActive(false);
            actor.CriticalChance.RemoveModifier(100);
            lokiDamageReady = false;
        }
    }
}
