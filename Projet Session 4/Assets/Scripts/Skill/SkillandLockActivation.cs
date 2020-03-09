using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillandLockActivation : MonoBehaviour
{
    [SerializeField] private GameObject _lock = null;
    [SerializeField] private GameObject _skill = null;
    private bool locked = true;
    public int levelNeeded = 0;
    private Player _Player;
    void Start()
    {
        _Player = GameObject.Find("Player").GetComponent<Player>();
        _skill.SetActive(false);
    }


    void Update()
    {
        if(locked)
        {
            gameObject.GetComponent<Button>().interactable = false;
        }
        if (_Player.LevelCurrent >= levelNeeded)
        {
            gameObject.GetComponent<Button>().interactable = true;
            _lock.SetActive(false);
            _skill.SetActive(true);
            locked = false;
        }
    }
}
