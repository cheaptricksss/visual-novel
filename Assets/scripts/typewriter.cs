using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class typewriter : MonoBehaviour
{
    public float writingSpeed = 1f;


    public void Run(string txtToType, TMP_Text label)
    {
        StartCoroutine(TypeText(txtToType, label));
    }

    private IEnumerator TypeText(string txtToType, TMP_Text label)
    {
        float time = 0;
        int charIndex = 0;

        while (charIndex < txtToType.Length)
        {
            //time value increments with time
            time += Time.deltaTime *  writingSpeed;
            //will store the floored down value of time
            charIndex = Mathf.FloorToInt(time);
            // to keep charIndex smaller then length
            charIndex = Mathf.Clamp(charIndex, 0, txtToType.Length);

            // tells how many chars of the text will be written at each frame
            label.text = txtToType.Substring(0, charIndex);

            yield return null;
        }

        label.text = txtToType;

    }
}
