using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMultipleScene : MonoBehaviour
{
    private static SoundMultipleScene instance = null;
    private static SoundMultipleScene Instance()
    {
        return instance;
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
