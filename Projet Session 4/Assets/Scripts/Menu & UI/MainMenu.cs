using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour 
{
    #region Variables
    [Header("Panel Toggle")]
    [SerializeField] private GameObject[] panels = null;
    [SerializeField] private Selectable[] defaultButtons = null;
    #endregion

    #region Methods
    public void PanelToggle(int position)
    {
        Input.ResetInputAxes();
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(position == i);
            if (position == i)
            {
                defaultButtons[i].Select();
            }

        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadScene(string level)
    {
        SceneManager.LoadScene(level);
    }
    #endregion
}
