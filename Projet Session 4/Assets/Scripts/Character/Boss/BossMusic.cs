using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMusic : MonoBehaviour
{
    [Header("BossMusic")]
    [SerializeField] private AudioClip newMusic = null;
    private AudioManager audioManager = null;

    // Start is called before the first frame update
    void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        audioManager.ChangeMusic(newMusic);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
