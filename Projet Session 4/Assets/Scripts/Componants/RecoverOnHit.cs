using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfRecover { Health, Barrier, Mana }

public class RecoverOnHit : MonoBehaviour
{
    public Actor caster;
    [SerializeField] protected TypeOfTarget typeOfTarget = 0;
    [SerializeField] private float amount = 0;
    [SerializeField] bool health = false;
    [SerializeField] bool barrier = false;
    [SerializeField] bool mana = false;

    private bool isDirty = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != typeOfTarget.ToString() || isDirty) return;

        Collider[] colliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale/2, transform.rotation, LayerMask.GetMask("Target"));
        Steal(colliders);

        isDirty = true;
    }

    public void Steal(Collider[] _colliders)
    {
        foreach (Collider collider in _colliders)
        {
            if (health == true)
                caster.Health.IncreaseCurrentValue(amount);
            if (barrier == true)
                caster.Barrier.IncreaseCurrentValue(amount);
            if (mana == true)
                caster.Mana.IncreaseCurrentValue(amount);

        }
    }
}
