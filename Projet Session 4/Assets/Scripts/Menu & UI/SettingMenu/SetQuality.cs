using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetQuality : MonoBehaviour
{

    public void ModifyQuality (int QualityIndex)
    {
        QualitySettings.SetQualityLevel(QualityIndex);
    }

    public void ModifyFullScreen ( bool IsFullscreen)
    {
        Screen.fullScreen = IsFullscreen;
    }
}
