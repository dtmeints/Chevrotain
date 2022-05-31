using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] List<GameObject> shields = new List<GameObject>();
    List<RawImage> shieldImageComponents = new List<RawImage>();    
    [SerializeField] Texture[] shieldImages;
    PlayerData _pd;


    private void Awake()
    {
        _pd = FindObjectOfType<PlayerData>().GetComponent<PlayerData>();
        //construct list of shield image components
        for (int i = 0; i < shields.Count; i++)
        {
            shieldImageComponents.Add(shields[i].GetComponent<RawImage>());
        }
       UpdateMaxHealth(_pd.maxHealth);
    }

    private void UpdateMaxHealth(int maxHealth)
    {
        for (int i = 0; i < maxHealth; i++)
        {
            shields[i].SetActive(true);
        }
    }

    private void UpdateCurrentHealth(Player player)
    {
        for (int i = 0; i < player.currentHealth; i++)
        {
          shieldImageComponents[i].texture = shieldImages[1];
        }
        for (int i = player.currentHealth; i < _pd.maxHealth; i++)
        {
            shieldImageComponents[i].texture = shieldImages[0];
        }
    }

    private void OnEnable()
    {
        Player.OnHurt += UpdateCurrentHealth;
        Player.OnHeal += UpdateCurrentHealth;
    }

    private void OnDisable()
    {
        Player.OnHurt -= UpdateCurrentHealth;
        Player.OnHeal -= UpdateCurrentHealth;
    }
}
