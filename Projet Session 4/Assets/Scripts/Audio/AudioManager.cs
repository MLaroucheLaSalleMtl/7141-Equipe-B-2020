using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource bgmusic;

    private void Start()
    {
        
    }

    public void ChangeMusic(AudioClip music)
    {
        if (bgmusic.clip.name == music.name || GameManager.NumberOfEnemy > 0)
            return;

        bgmusic.Stop();
        bgmusic.clip = music;
        bgmusic.Play();
    }
}
