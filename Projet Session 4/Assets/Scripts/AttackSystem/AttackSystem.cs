using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackSystem : MonoBehaviour
{
    #region Variables & Attributs 
    private Actor actorManager;
    [SerializeField] private GameObject attackZone = null;
    private bool canAttack = true;
    // private float delayBetweenAttack = 2.5f;
    #endregion

    #region Unity's Methods
    void Start()
    {
        actorManager = GetComponent<Actor>();
    }
    #endregion

    #region Methods



    public IEnumerator UseBasicAttack()
    {
        canAttack = false;
        GameObject clone = Instantiate(attackZone, transform.position + (transform.forward * 2), transform.rotation);
        clone.GetComponent<DamageComponant>().BonusDamage = actorManager.PowerPhysical.GetValue();
        clone.GetComponent<DamageComponant>().ArmorPenatration = actorManager.DamagePenetration.GetValue();
        clone.GetComponent<DamageComponant>().CriticalChance = actorManager.CriticalChance.GetValue();
        clone.GetComponent<DamageComponant>().CriticalRatio = actorManager.CriticalDamage.GetValue();
        yield return new WaitForSeconds(actorManager.AttackSpeed.GetValue());
        canAttack = true;
    }
    public void Fire(InputAction.CallbackContext context)
    {
        if (context.started && canAttack == true)
        {
            if (canAttack)
            {
                StartCoroutine(actorManager.AttackRootEffect(0.15f));
                StartCoroutine(UseBasicAttack());
            }
        }
    }
    #endregion
}
