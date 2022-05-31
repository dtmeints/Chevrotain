using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Readable : MonoBehaviour
{

    public D_ReadableText textData;

    private int currentTextIndex;
    public string currentText;

    private Read reader;

    private TextDisplay textDisplay;

    private void Awake()
    {
        textDisplay = FindObjectOfType<TextDisplay>();
        reader = FindObjectOfType<Read>();

        currentTextIndex = 0;
        currentText = textData.texts[currentTextIndex];
    }

    public void ShowText()
    {
        currentText = textData.texts[currentTextIndex];
        textDisplay.ShowTextBox();
        StartCoroutine(textDisplay.DisplayText(currentText));
    }

    public void AdvanceText()
    {
        Debug.Log(currentTextIndex);
        currentTextIndex++;
        if (currentTextIndex >= textData.texts.Count)
        {
            textDisplay.HideTextBox();
            currentTextIndex = 0;
            reader.SetIsReading(false);
            return;
        }
        currentText = textData.texts[currentTextIndex];
        StartCoroutine(textDisplay.DisplayText(currentText));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            reader.SetCurrentReadable(this);
            reader.SetInReadingRange(true);
        }
        else return;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            if (reader.isReading)
            {
                StartCoroutine(WaitThenNullifySign());
                return;
            }
            reader.SetCurrentReadable(null);
            reader.SetInReadingRange(false);
        }
        else return;
    }

    IEnumerator WaitThenNullifySign()
    {
        yield return new WaitUntil(() => !reader.isReading);
        reader.SetCurrentReadable(null);
        reader.SetInReadingRange(false);
    }
}
