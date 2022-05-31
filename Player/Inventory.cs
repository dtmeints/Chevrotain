using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ItemTypes;

public class Inventory : MonoBehaviour
{

    public int _currentSalt;
    private int _maxSalt;
    public int _currentSaltJars = 1;
    public int _currentBarbs;
    [SerializeField] private int saltJarCapacity = 8;
    
    
    private PlayerData _pd;
    private JarContainer _jarContainer;


    public static Action<int> OnSaltJarChange;
    public static Action<int> OnSaltChange;

    private void Awake()
    {
        _maxSalt = _currentSaltJars * saltJarCapacity;
        _pd = GetComponent<PlayerData>();
        _jarContainer = FindObjectOfType<JarContainer>();
    }
   
    public void ChangeItemsHeld(Collectible collectible, int amount = 1)
    {
        switch (collectible.type)
        {
            case ItemType.Salt:
                SetSalt(amount);
                break;
            case ItemType.SaltJar:
                _currentSaltJars += amount;
                _maxSalt = _currentSaltJars * saltJarCapacity;
                OnSaltJarChange?.Invoke(amount);
                break;
            case ItemType.Barb:
                _currentBarbs += amount;
                break;
        }    
    }

    public void SetSalt(int amount)
    {
        int adjustedAmount;
        if (amount > 0)
        {
            adjustedAmount = Mathf.FloorToInt(amount * _pd.saltGetModifier); 
        }
        else adjustedAmount = amount;
        Debug.Log(adjustedAmount);
        _currentSalt += adjustedAmount;
        if (_currentSalt > _maxSalt)
        {
            _currentSalt = _maxSalt;
            return;
        }
        OnSaltChange?.Invoke(amount);
    }

    public void AddHitSalt(Entity enemy)
    {
        SetSalt(enemy.entityData.hitSalt);
    }


    public void AddBarb(int amount)
    {
        _currentBarbs += amount;
    }

    public void SetCurrentBarbs(int amount)
    {
        _currentBarbs = amount;
    }

    public void LoadSalt(int amount)
    {
        _currentSalt = amount;
        _jarContainer.InitializeSaltAndJars(_currentSaltJars, _currentSalt);
    }

    public void LoadSaltJars(int amount)
    {
        _currentSaltJars = amount;
    }

    public void LoadCurrentBarbs(int amount)
    {
        _currentBarbs = amount;
    }



    public int CurrentSaltJars
    { get { return _currentSaltJars;} }

    public int Salt
    {
        get { return _currentSalt; }
    }

    public int MaxSalt
    {
        get { return _maxSalt; }
    }
}
