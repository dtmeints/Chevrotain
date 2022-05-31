using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JarContainer : MonoBehaviour
{

    [SerializeField] List<UIJar> jars = new List<UIJar>();   

    [SerializeField] Texture[] saltSprites;
    [SerializeField] Inventory playerInventory;
    [SerializeField] List<GameObject> jarSlots = new List<GameObject>();
 
    UIJar currentJar;
    int currentJarIndex;

    private void Awake()
    {
        UpdateJars(playerInventory.CurrentSaltJars);
    }

    void UpdateJars(int numberChanged)
    {
        for (int i = 0; i < playerInventory.CurrentSaltJars; i++)
        {
            var jar = jarSlots[i].transform.GetChild(0).gameObject;
            jar.SetActive(true);
        }
    }

    void UpdateSalt(int numberChanged)
    {

        currentJarIndex = Mathf.FloorToInt(playerInventory.Salt / 8);
        if (playerInventory.Salt >= playerInventory.MaxSalt)
        {
            currentJar = jars[currentJarIndex - 1];
            currentJar.SetFilling(saltSprites[8]);
            currentJar.cork.SetActive(true);
            return;
        }
        currentJar = jars[currentJarIndex];
        StartCoroutine(SaltChangeVFX(numberChanged));
    }

    IEnumerator SaltChangeVFX(int numberChanged)
    {
        if (numberChanged > 0)
        {
            currentJar.saltPour.Play();
        }
        yield return new WaitForSeconds(.6f);
        int saltSpriteIndex = playerInventory.Salt % 8;
        currentJar.SetFilling(saltSprites[saltSpriteIndex]);
        currentJar.cork.SetActive(false);
        for (int i = 0; i < currentJarIndex; i++)
        {
            jars[i].SetFilling(saltSprites[8]);
            jars[i].cork.SetActive(true);
        }
        for (int i = playerInventory.CurrentSaltJars - 1; i > currentJarIndex; i--)
        {
            jars[i].SetFilling(saltSprites[0]);
            jars[i].cork.SetActive(false);
        }
    }

    public void InitializeSaltAndJars(int jarNumber, int saltNumber)
    {
        for (int i = 0; i < jarNumber; i++)
        {
            var jar = jarSlots[i].transform.GetChild(0).gameObject;
            jar.SetActive(true);
        }
        currentJarIndex = Mathf.FloorToInt(playerInventory.Salt / 8);
        int saltSpriteIndex = playerInventory.Salt % 8;
        currentJar = jars[currentJarIndex];
        currentJar.SetFilling(saltSprites[saltSpriteIndex]);
        currentJar.cork.SetActive(false);
        for (int i = 0; i < currentJarIndex; i++)
        {
            jars[i].SetFilling(saltSprites[8]);
            jars[i].cork.SetActive(true);
        }
        for (int i = playerInventory.CurrentSaltJars - 1; i > currentJarIndex; i--)
        {
            jars[i].SetFilling(saltSprites[0]);
            jars[i].cork.SetActive(false);
        }
    }

    private void OnEnable()
    {
        Inventory.OnSaltChange += UpdateSalt;
        Inventory.OnSaltJarChange += UpdateJars;
    }

    private void OnDisable()
    {
        Inventory.OnSaltChange -= UpdateSalt;
        Inventory.OnSaltJarChange -= UpdateJars;
    }
}
