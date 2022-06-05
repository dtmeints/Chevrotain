using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ItemTypes;
using InventoryManagement;

public static class Inventory
{

    public const int saltJarCapacity = 8;
    public const int maxSaltJars = 12;
    
    //private JarContainer _jarContainer;

      
    public static event Action<int> OnSaltJarChange;
    public static event Action<int> OnSaltChange;
    public static event Action<int> OnBarbChange;
    public static event Action OnInitializeHUD;

    public static void AddItem(item type, int amount = 1)
    {
        switch (type)
        {
            case item.salt:
                AddSalt(amount);
                break;
            case item.saltJar:
                AddSaltJar(amount);
                break;
            case item.barb:
               AddBarb(amount);
                break;
        }    
    }

    public static void AddSalt(int amount)
    {
        if (ActiveData.inventory[item.salt].current + amount > ActiveData.inventory[item.salt].max)
        {
            amount = 0;
        }
        OnSaltChange?.Invoke(amount);
    }


    public static void AddBarb(int amount)
    {
        ActiveData.inventory[item.barb] = (ActiveData.inventory[item.barb].current + amount, ActiveData.inventory[item.barb].max);
    }

    private static void AddSaltJar(int amount)
    {
        if (ActiveData.inventory[item.saltJar].current + amount > ActiveData.inventory[item.saltJar].max)
        {
            amount = 0;
        }
        OnSaltChange?.Invoke(amount);
    }

    public static void LoadSalt(int amount)
    {
        ActiveData.inventory[item.salt] = (ActiveData.inventory[item.salt].current + amount, ActiveData.inventory[item.salt].max);
        OnInitializeHUD?.Invoke();
    }

    public static void LoadSaltJars(int amount)
    {
        ActiveData.inventory[item.saltJar] = (amount, maxSaltJars);
    }

    public static void LoadCurrentBarbs(int amount)
    {
        ActiveData.inventory[item.barb] = (amount, ActiveData.inventory[item.barb].max);
    }
}

namespace InventoryManagement
{
    public enum item
    {
        salt,
        saltJar,
        barb
    }
}