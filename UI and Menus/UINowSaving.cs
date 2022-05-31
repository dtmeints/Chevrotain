using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UINowSaving : MonoBehaviour
{

    Animator anim;
    TextMeshProUGUI tmp;
    string nowSaving = "Now Saving...";
    public float typingSpeed;

    bool isSaving = false;

    private void Awake()
    {
        anim = transform.Find("SaveIcon").GetComponent<Animator>();
        tmp = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void DisplaySave()
    {
        if (!isSaving)
        {
            isSaving = true;
            anim.SetBool("animate", true);
            StartCoroutine(PrintText());
        }
    }

    IEnumerator PrintText()
    {
        for (int i = 0; i < nowSaving.Length; i++)
        {
            tmp.text += nowSaving[i];
            yield return new WaitForSeconds(typingSpeed);
        }
        tmp.text = " ";
        anim.SetBool("animate", false);
        isSaving = false;
    }

    private void OnEnable()
    {
        SaveSystem.OnSave += DisplaySave;
    }

    private void OnDisable()
    {
        SaveSystem.OnSave -= DisplaySave;
    }
}
