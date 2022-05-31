using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArrowCustomization;

[System.Serializable]
public class SaveData 
{
    public int stoneBarbs;
    public int airBarbs;
    public int fireBarbs;
    public int iceBarbs;
    public int lodeBarbs;
    public int lightBarbs;
    public int toxicBarbs;
    public int rainBarbs;
    public int hollowBarbs;

    public int currentSalt;
    public int currentSaltJars;
    public int currentBarbs;

    public int currentHealth;
    public float[] currentPosition = new float[3];

    public List<string> permanentIDs = new List<string>();
    public List<bool> permanentStatuses = new List<bool>();

    public List<string> enemyIDs = new List<string>();
    public List<bool> enemyStatuses = new List<bool>();

    public List<string> collectibleIDs = new List<string>();
    public List<bool> collectibleStatuses = new List<bool>();

    public SaveData(Player player, Inventory inventory)
    {
        currentHealth = player.currentHealth;
        currentPosition[0] = player.transform.position.x;
        currentPosition[1] = player.transform.position.y;
        currentPosition[2] = player.transform.position.z;

        currentSalt = inventory._currentSalt;
        currentSaltJars = inventory._currentSaltJars;
        currentBarbs = inventory._currentBarbs;

        stoneBarbs = PlayerAugments.barbDictionary[BarbType.stone].numberEquipped;
        airBarbs = PlayerAugments.barbDictionary[BarbType.air].numberEquipped;
        fireBarbs = PlayerAugments.barbDictionary[BarbType.fire].numberEquipped;
        iceBarbs = PlayerAugments.barbDictionary[BarbType.ice].numberEquipped;
        lodeBarbs = PlayerAugments.barbDictionary[BarbType.lode].numberEquipped;
        lightBarbs = PlayerAugments.barbDictionary[BarbType.light].numberEquipped;
        toxicBarbs = PlayerAugments.barbDictionary[BarbType.toxic].numberEquipped;
        rainBarbs = PlayerAugments.barbDictionary[BarbType.rain].numberEquipped;
        hollowBarbs = PlayerAugments.barbDictionary[BarbType.hollow].numberEquipped;

        permanentIDs.Clear();
        permanentStatuses.Clear();
        foreach (IPermanent p in Permanents.permanents)
        {
            permanentIDs.Add(p.GetID());
            permanentStatuses.Add(p.GetStatus());
        }

        collectibleIDs.Clear();
        collectibleStatuses.Clear();
        foreach (ICollectible c in CollectibleManager.collectibleList)
        {
            collectibleIDs.Add(c.GetID());
            collectibleStatuses.Add(c.GetStatus());
        }
    }
}
