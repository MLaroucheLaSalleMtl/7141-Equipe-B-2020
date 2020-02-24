using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackSystem : MonoBehaviour
{
    #region Variables & Attributs 
    private Actor _actorManager;
    [SerializeField] private GameObject attackZone = null;
    private bool canAttack = true;
    // private float delayBetweenAttack = 2.5f;
    #endregion

    #region Unity's Methods
    void Start()
    {
        _actorManager = GetComponent<Actor>();
    }
    #endregion

    #region Methods



    public IEnumerator UseBasicAttack()
    {
        canAttack = false;
        GameObject clone = Instantiate(attackZone, transform.position + (transform.forward * 2), transform.rotation);
        clone.GetComponent<DamageComponant>().caster = GetComponent<Actor>();

        yield return new WaitForSeconds(_actorManager.AttackSpeed.GetValue());
        canAttack = true;
    }
    public void Fire(InputAction.CallbackContext context)
    {
        if (context.started && canAttack == true)
        {
            if (canAttack)
            {
                StartCoroutine(_actorManager.AttackRootEffect(0.15f));
                StartCoroutine(UseBasicAttack());
            }
        }
    }
    #endregion
}
