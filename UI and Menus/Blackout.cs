using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blackout : MonoBehaviour
{
    [SerializeField] RawImage blackSquareImage;

    void OnEnable() => UIActions.FadeToBlack += FadeInOrOut;


    void OnDisable() => UIActions.FadeToBlack -= FadeInOrOut;
    
    void FadeInOrOut(bool toBlack)
    {
        if (toBlack)
        {
            StartCoroutine(FadeToBlack());
        }
    }

    IEnumerator FadeToBlack()
    {
        while (blackSquareImage.color.a < 1f)
        {
            blackSquareImage.color = new Color(0f, 0f, 0f, blackSquareImage.color.a + Time.deltaTime);
        }
        yield return new WaitForSeconds(.2f);
        while (blackSquareImage.color.a > 0f)
        {
            blackSquareImage.color = new Color(0f, 0f, 0f, blackSquareImage.color.a - Time.deltaTime);
        }
    }
}
