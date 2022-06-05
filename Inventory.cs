using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using InventoryManagement;
using Quills;

public static class Inventory
{
    public static Dictionary<ItemType, (int current, int max)> ActiveInventory = new Dictionary<ItemType, (int current, int max)>();
    public static Dictionary<QuillType, bool> Quills = new Dictionary<QuillType, bool>();

    public const int saltJarCapacity = 8;
    public const int maxSaltJars = 12;

    //private JarContainer _jarContainer;


    public static event Action OnInitializeHUD;

    public static event Action<ItemType, int> OnInventoryChange;

    public static void AddToInventory(ItemType itemType, int amount = 1)
    {
        var itemsInInventory = ActiveInventory[itemType];
        if (itemsInInventory.current + amount > itemsInInventory.max)
        {
            amount = 0;
        }

        OnInventoryChange?.Invoke(itemType, amount);
    }

    public static void LoadInventory(ItemType itemType, int amount)
    {
        var itemToLoad = ActiveInventory[itemType];

        switch (itemType)
        {
            case ItemType.salt:
                itemToLoad = (itemToLoad.current += amount, itemToLoad.max);
                OnInitializeHUD?.Invoke();
                break;
            case ItemType.saltJar:
                itemToLoad = (amount, maxSaltJars);
                break;
            case ItemType.barb:
                itemToLoad = (amount, itemToLoad.max);
                break;
        }
    }
}

namespace InventoryManagement
{
    public enum ItemType
    {
        salt,
        saltJar,
        barb
    }
}
