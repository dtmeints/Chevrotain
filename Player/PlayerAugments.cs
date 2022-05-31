using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArrowCustomization;
using System;

namespace ArrowCustomization
{
    public enum BarbType
    {
        fire,
        ice,
        stone,
        toxic,
        hollow,
        lode,
        rain,
        air,
        light
    }
}


public class PlayerAugments : MonoBehaviour
{
    public static Dictionary<BarbType, Barb> barbDictionary;

    public class Barb
    {
        public BarbType type;
        public int numberEquipped;
        
        public Barb(BarbType type, int numberEquipped)
        {
            this.type = type;
            this.numberEquipped = numberEquipped;
            barbDictionary.Add(type, this);
        }
    }

    [SerializeField] PlayerData _pd;

     public int knockbackStage = 6;
     public int attackSpeedStage = 6 ;
     public int arrowDamageStage = 6;
     public int heatStage = 6;
     public int chargeStage = 6;
     public int staminaStage = 6;
     public int toxicStage = 6;
     public int healStage = 6;
     public int saltGainStage = 6;

    public float knockbackMax;
    public float knockbackMin;
    public float attackCooldownMax;
    public float attackCooldownMin;
    public float damageMax;
    public float damageMin;
    public float heatMax;
    public float heatMin;
    public float chargeMax;
    public float chargeMin;
    public float staminaMax;
    public float staminaMin;
    public float toxicMax;
    public float toxicMin;
    public float healMin;
    public float healMax;
    public float saltGainMin;
    public float saltGainMax;

    private float knockbackIncrement, attackCooldownIncrement, damageIncrement, heatIncrement, chargeIncrement, staminaIncrement, 
        toxicIncrement, healIncrement, saltGainIncrement;

    public static event Action<BarbType> OnBarbChange;

    private void Awake()
    {
        if (barbDictionary == null)
        {
            barbDictionary = new Dictionary<BarbType, Barb>();
            if (barbDictionary.Count == 0)
            {
                foreach (BarbType barbType in Enum.GetValues(typeof(BarbType)))
                {
                    new Barb(barbType, 0);
                }
            }
        }
    }

    private void Start()
    {
        knockbackIncrement = (knockbackMax - knockbackMin) / 18;
        attackCooldownIncrement = (attackCooldownMax - attackCooldownMin) / 18;
        damageIncrement = (damageMax - damageMin) / 18;
        heatIncrement = (heatMax - heatMin) / 18;
        chargeIncrement = (chargeMax - chargeMin) / 18;
        staminaIncrement = (staminaMax - staminaMin) / 18;
        toxicIncrement = (toxicMax - toxicMin) / 18;
        healIncrement = (healMax - healMin) / 18;
        saltGainIncrement = (saltGainMax - saltGainMin) / 18;
        foreach(BarbType type in Enum.GetValues(typeof(BarbType)))
        {
            UpdateStages(type, barbDictionary[type].numberEquipped);
        }
    }
    public void EditBarb(BarbType type, int number)
    {
        var currentEquipped = barbDictionary[type].numberEquipped;
        barbDictionary[type].numberEquipped = Mathf.Clamp(barbDictionary[type].numberEquipped + number, 0, 6);
        if (currentEquipped == barbDictionary[type].numberEquipped)
        {
            UpdateStages(type, 0);
            return;
        }
        UpdateStages(type, number);
    }

    
   
    private void UpdateStages(BarbType type, int number)
    {
        switch (type)
        {
            case BarbType.stone:
                knockbackStage += 2 * number;
                attackSpeedStage -= 1 * number;
                break;
            case BarbType.air:
                attackSpeedStage += 2 * number;
                arrowDamageStage -= 1 * number;
                break;
            case BarbType.fire:
                arrowDamageStage += 2 * number;
                heatStage -= 1 * number;
                break;
            case BarbType.ice:
                heatStage += 2 * number;
                chargeStage -= 1 * number;
                break;
            case BarbType.lode:
                chargeStage += 2 * number;
                staminaStage -= 1 * number;
                break;
            case BarbType.light:
                staminaStage += 2 * number;
                toxicStage -= 1 * number;
                break;
            case BarbType.toxic:
                toxicStage += 2 * number;
                healStage -= 1 * number;
                break;
            case BarbType.rain:
                healStage += 2 * number;
                saltGainStage -= 1 * number;
                break;
            case BarbType.hollow:
                saltGainStage += 2 * number;
                knockbackStage -= 1 * number;
                break;
        }
        UpdateStats(type);
    }

    private void UpdateStats(BarbType type)
    {
        _pd.knockback = knockbackMin + (knockbackIncrement * knockbackStage);
        _pd.attackCooldown = attackCooldownMax - (attackCooldownIncrement * attackSpeedStage);
        _pd.damageModifier = damageMin + (damageIncrement * arrowDamageStage);
        _pd.heatModifier = heatMax - (heatIncrement * heatStage);
        _pd.chargeAttackMaxTime = chargeMax - (chargeIncrement * chargeStage);
        _pd.maxStamina = staminaMin + (staminaIncrement * staminaStage);
        _pd.maxToxicModifier = toxicMin + (toxicIncrement * toxicStage);
        _pd.circleLength = healMax - (healIncrement * healStage);
        _pd.saltGetModifier = saltGainMin + (saltGainIncrement * saltGainStage);
        OnBarbChange?.Invoke(type);
    }

    public void InitializeBarb(BarbType type, int number)
    {
        barbDictionary[type].numberEquipped = number;
    }

    public void InitializeStatStages()
    {
        foreach (BarbType barbType in Enum.GetValues(typeof(BarbType)))
        {
            switch (barbType)
            {
                case BarbType.stone:
                    knockbackStage += 2 * barbDictionary[barbType].numberEquipped;
                    attackSpeedStage -= 1 * barbDictionary[barbType].numberEquipped;
                    break;
                case BarbType.air:
                    attackSpeedStage += 2 * barbDictionary[barbType].numberEquipped;
                    arrowDamageStage -= 1 * barbDictionary[barbType].numberEquipped;
                    break;
                case BarbType.fire:
                    arrowDamageStage += 2 * barbDictionary[barbType].numberEquipped;
                    heatStage -= 1 * barbDictionary[barbType].numberEquipped;
                    break;
                case BarbType.ice:
                    heatStage += 2 * barbDictionary[barbType].numberEquipped;
                    chargeStage -= 1 * barbDictionary[barbType].numberEquipped;
                    break;
                case BarbType.lode:
                    chargeStage += 2 * barbDictionary[barbType].numberEquipped;
                    staminaStage -= 1 * barbDictionary[barbType].numberEquipped;
                    break;
                case BarbType.light:
                    staminaStage += 2 * barbDictionary[barbType].numberEquipped;
                    toxicStage -= 1 * barbDictionary[barbType].numberEquipped;
                    break;
                case BarbType.toxic:
                    toxicStage += 2 * barbDictionary[barbType].numberEquipped;
                    healStage -= 1 * barbDictionary[barbType].numberEquipped;
                    break;
                case BarbType.rain:
                    healStage += 2 * barbDictionary[barbType].numberEquipped;
                    saltGainStage -= 1 * barbDictionary[barbType].numberEquipped;
                    break;
                case BarbType.hollow:
                    saltGainStage += 2 * barbDictionary[barbType].numberEquipped;
                    knockbackStage -= 1 * barbDictionary[barbType].numberEquipped;
                    break;
            }
        }
        InitializeStats();
    }

    private void InitializeStats()
    {
        _pd.knockback = knockbackMin + (knockbackIncrement * knockbackStage);
        _pd.attackCooldown = attackCooldownMax - (attackCooldownIncrement * attackSpeedStage);
        _pd.damageModifier = damageMin + (damageIncrement * arrowDamageStage);
        _pd.heatModifier = heatMax - (heatIncrement * heatStage);
        _pd.chargeAttackMaxTime = chargeMax - (chargeIncrement * chargeStage);
        _pd.maxStamina = staminaMin + (staminaIncrement * staminaStage);
        _pd.maxToxicModifier = toxicMin + (toxicIncrement * toxicStage);
        _pd.circleLength = healMax - (healIncrement * healStage);
        _pd.saltGetModifier = saltGainMin + (saltGainIncrement * saltGainStage);
        foreach (BarbType barbType in Enum.GetValues(typeof(BarbType)))
        {
            OnBarbChange?.Invoke(barbType);
        }
    }
}
