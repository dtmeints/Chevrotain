using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using ArrowCustomization;

public class ArrowCustomMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI fireText, iceText, stoneText, toxicText, hollowText, lightText, rainText, lodeText, airText;
    Dictionary<BarbType, TextMeshProUGUI> textMeshDict = new Dictionary<BarbType, TextMeshProUGUI>();

    private void Start()
    {
        textMeshDict.Add(BarbType.fire, fireText);
        textMeshDict.Add(BarbType.ice, iceText);
        textMeshDict.Add(BarbType.stone, stoneText);
        textMeshDict.Add(BarbType.lode, lodeText);
        textMeshDict.Add(BarbType.rain, rainText);
        textMeshDict.Add(BarbType.air, airText);
        textMeshDict.Add(BarbType.hollow, hollowText);
        textMeshDict.Add(BarbType.toxic, toxicText);
        textMeshDict.Add(BarbType.light, lightText);
        foreach (BarbType type in Enum.GetValues(typeof(BarbType)))
        {
            UpdateBarbText(type);
        }
    }
    private void UpdateBarbText(BarbType type)
    {
        textMeshDict[type].text = PlayerAugments.barbDictionary[type].numberEquipped.ToString() + " barbs";
    }
    private void OnEnable()
    {
        PlayerAugments.OnBarbChange += UpdateBarbText;
    }

    private void OnDisable()
    {
        PlayerAugments.OnBarbChange -= UpdateBarbText;
    }

}
