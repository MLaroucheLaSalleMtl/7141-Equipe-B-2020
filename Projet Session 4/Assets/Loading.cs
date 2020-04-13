using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public Image fillingAmount;
    public Text numberText;
    public string sceneToLoad;
    // Start is called before the first frame update


    void Start()
    {
        GameManager.GameInitialization();
        StartCoroutine(LoadAsynch(sceneToLoad));
    }

    IEnumerator LoadAsynch(string level)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(level);

        while (!async.isDone)
        {
            float progress = Mathf.Clamp01(async.progress / 0.9f);
            fillingAmount.fillAmount = progress;
            numberText.text = progress*100 + "%";
            yield return null;
        }
    }
}
