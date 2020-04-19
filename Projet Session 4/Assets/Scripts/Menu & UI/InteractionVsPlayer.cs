using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class InteractionVsPlayer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    Actor _Actor;

    public void Start()
    {
        _Actor = GameObject.Find("Player").GetComponent<Actor>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _Actor.CanAttack = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _Actor.CanAttack = true;

    }

    void OnDisable()
    {
        _Actor.CanAttack = true;
    }

    void OnDestroy()
    {
        _Actor.CanAttack = true;

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _Actor.CanAttack = false;

    }
}
