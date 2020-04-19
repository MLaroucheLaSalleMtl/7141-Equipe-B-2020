using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource bgmusic = null;

    private void Start()
    {
        
    }

    public void ChangeMusic(AudioClip music)
    {
        if (bgmusic.clip.name == music.name || GameManager.NumberOfEnemy > 0)
            return;

        StartCoroutine(FadeOut(1.5f, music));

    }
    public void VictoryMusic(AudioClip music)
    {
        StartCoroutine(FadeOut(0f, music));
    }

    public IEnumerator FadeOut(float FadeTime, AudioClip music)
    {
        float startVolume = bgmusic.volume;

        while (bgmusic.volume > 0)
        {
            bgmusic.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        bgmusic.Stop();
        bgmusic.volume = startVolume;
        bgmusic.clip = music;
        bgmusic.Play();
    }
}
