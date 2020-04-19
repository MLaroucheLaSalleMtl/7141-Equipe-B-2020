using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
    [SerializeField] private Text description = null;
    public Image icon;
    [SerializeField] private int popupDuration = 7;
    [SerializeField] private GameObject popupBox = null;

    public void InitializeBox(Relic relic)
    {
        description.text = relic.discoveryDescription;
        icon.sprite = relic.icon;
        popupBox.SetActive(true);
        StartCoroutine(ClosePopup());
    }

    public string GetDescription()
    {
        return description.text;
    }

    public void SetDescription(string _text)
    {
        description.text = _text;
    }

    IEnumerator ClosePopup()
    {
        yield return new WaitForSeconds(popupDuration);
        popupBox.SetActive(false);
    }
}
