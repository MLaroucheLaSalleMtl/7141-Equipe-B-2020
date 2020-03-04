using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class InteractionVsPlayer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    AttackSystem _attackSystem;

    public void Start()
    {
        _attackSystem = GameObject.Find("Player").GetComponent<AttackSystem>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _attackSystem.CanAttack = false;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _attackSystem.CanAttack = true;

    }

    void OnDestroy()
    {
        _attackSystem.CanAttack = true;

    }
}
