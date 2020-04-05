using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsComponant : MonoBehaviour
{
    [SerializeField] private Transform[] slots = null;
    [SerializeField] private bool[] isFull = null;
    [SerializeField] private GameObject image = null;
    [SerializeField] private float duration = 5;

    public GameObject Image { get => image; set => image = value; }
    public float Duration { get => duration; set => duration = value; }

    // [SerializeField] private int numberOfActiveEffect = 0;

    void Update()
    {


    }



    public void AddEffect()
    {
        if (isFull[5] == true) return;
        StartCoroutine(ShowTheEffect(Duration, Image));
    }


    public IEnumerator ShowTheEffect(float Duration, GameObject Image)
    {
        GameObject clone = null;
        int i = 0;

        for (i = 0; i < slots.Length; i++)
        {

            if (isFull[i] == false)
            {
                isFull[i] = true;

                clone = Instantiate(this.Image, slots[i].transform.position, Quaternion.identity, slots[i]);
                break;
            }
        }
        yield return new WaitForSeconds(Duration);
        isFull[i] = false;
        Destroy(clone.gameObject);
    }


}
