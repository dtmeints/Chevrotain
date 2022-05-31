using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextDisplay : MonoBehaviour
{
    private TMP_Text displayText;
    private RectTransform textBG;

    private bool textDisplayOn;

    private Vector2 fullSize;
    private Vector2 noSize;
    public float openCloseSpeed = 50f;

    private void Awake()
    {
        displayText = transform.GetChild(0).Find("Content").GetComponent<TextMeshProUGUI>();
        textBG = transform.GetChild(0).GetComponent<RectTransform>();

        fullSize = textBG.sizeDelta;
        noSize = new Vector2(0f, fullSize.y);
        textBG.sizeDelta = noSize;
    }


    private void FixedUpdate()
    {
        if(textDisplayOn && textBG.sizeDelta.x < fullSize.x)
        {
            textBG.sizeDelta = new Vector2(textBG.sizeDelta.x + openCloseSpeed, fullSize.y);
        }
        if (!textDisplayOn && textBG.sizeDelta.x > noSize.x)
        {
            textBG.sizeDelta = new Vector2(textBG.sizeDelta.x - openCloseSpeed, fullSize.y);
        }
    }

    public IEnumerator DisplayText(string text)
    {
        yield return new WaitUntil(() => fullSize.x - textBG.sizeDelta.x < 0.3f);
        displayText.text = text;
    }

    public void ShowTextBox()
    {
        textDisplayOn = true;
    }

    public void HideTextBox()
    {
        displayText.text = " ";
        textDisplayOn = false;
    }
}
