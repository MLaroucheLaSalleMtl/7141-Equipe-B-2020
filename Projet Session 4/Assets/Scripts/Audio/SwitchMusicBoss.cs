using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMusicBoss : MonoBehaviour
{
    [SerializeField] private AudioClip newMusic = null;
    private AudioManager audioManager = null;


    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag != "Enemy" && other.gameObject.tag == "Player")
        {
            audioManager.ChangeMusic(newMusic);
        }
    }
}
