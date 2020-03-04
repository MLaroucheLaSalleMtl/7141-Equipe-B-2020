using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyStatComponant : MonoBehaviour
{
    private Player _player;
    public float duration;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            _player = other.GetComponent<Player>();
            StartCoroutine(_player.TemporaryDebuff(_player.MovementSpeed, duration, -_player.MovementSpeed.GetValue()));
            
        }
    }

    void OnDestroy()
    {
        StopAllCoroutines();
    }

}
