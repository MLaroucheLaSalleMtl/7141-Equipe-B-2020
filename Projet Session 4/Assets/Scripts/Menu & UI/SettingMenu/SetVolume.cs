using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    #region Variables & Initialization
    private AudioMixer audioMixer = null;
    [SerializeField] private string nameParamater = null;
    private Slider slide;
    #endregion

    void Start()
    {
        audioMixer = GameObject.Find("AudioManager").GetComponent<AudioMixer>();
        slide = GetComponent<Slider>();
        float v = PlayerPrefs.GetFloat(nameParamater, 0);
        ModifyVolume(v);
    }

    public void ModifyVolume(float vol)
    {
        audioMixer.SetFloat(nameParamater, vol);
        slide.value = vol;
        PlayerPrefs.SetFloat(nameParamater, vol);
    }

}
