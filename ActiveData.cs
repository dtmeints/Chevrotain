using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArrowCustomization;
using InventoryManagement;

[System.Serializable]
public class ActiveData : MonoBehaviour
{

    public static Dictionary<item, (int current, int max)> inventory = new Dictionary<item, (int current, int max)>();

    public bool airQuillHeld;
    public bool fireQuillHeld;
    public bool iceQuillHeld;

    public bool lightQuillHeld;
    public bool darkQuillHeld;
    public bool earthQuillHeld;

    public bool stillnessQuillHeld;
    public bool hasteQuillHeld;
    public bool flowQuillHeld;
}



