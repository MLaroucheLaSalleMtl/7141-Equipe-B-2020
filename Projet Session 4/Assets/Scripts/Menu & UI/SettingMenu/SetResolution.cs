using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SetResolution : MonoBehaviour
{
    Resolution[] resolutions;
    public Dropdown resolutionDropdown;

    void start ()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            Debug.Log("Resolution : " + resolutions[i].width);
        }
        resolutionDropdown.AddOptions(options);
    }

}
