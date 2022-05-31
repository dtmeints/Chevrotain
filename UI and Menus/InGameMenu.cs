using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] GameObject[] menuScreens;

    public void MenuOpen()
    {
        foreach (var menuScreen in menuScreens)
        {
            menuScreen.gameObject.SetActive(true);
        }
    }

    public void MenuClose()
    {
        foreach (var menuScreen in menuScreens)
        {
            menuScreen.gameObject.SetActive(false);
        }
    }
}
