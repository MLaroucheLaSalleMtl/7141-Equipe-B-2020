using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum TypeOfDamage { Physical, Magical, True }
public class DamageComponant : MonoBehaviour
{
    #region Variables & Attributs
    [Header("Damage Componant")]
    [SerializeField] private float baseDamage = 0;
    [Range(0,2)]
    [SerializeField] private float damageRatio = 0;
    [SerializeField] private TypeOfDamage damageType = 0;
    [SerializeField] private TypeOfTarget typeOfTarget = 0;
    private float bonusDamage;
    private float armorPenatration;
    private float criticalChance;
    private float criticalRatio;

    private bool isDirty = false;
    #region Properties
    public float BonusDamage { get => bonusDamage; set => bonusDamage = value; }
    public float ArmorPenatration { get => armorPenatration; set => armorPenatration = value; }
    public float CriticalChance { get => criticalChance; set => criticalChance = value; }
    public float CriticalRatio { get => criticalRatio; set => criticalRatio = value; }
    #endregion
    #endregion
    private void OnTriggerEnter(Collider collision)
    {
        float calculatedDamage = baseDamage + (bonusDamage * damageRatio);


        if (collision.gameObject.tag == typeOfTarget.ToString() && !isDirty)
        {
            collision.gameObject.GetComponent<Actor>().TakeDamage(calculatedDamage,armorPenatration,criticalChance,criticalRatio,damageType.ToString());
            isDirty = true;
        }
    }
}
