using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArrowCustomization;
using System.Linq;

public class SaveLoadButton : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Inventory inventory;
    [SerializeField] PlayerAugments playerAugments;
    [SerializeField] ArrowHolder arrowHolder;

    public void Save()
    {
        SaveSystem.CreateSaveData(player, inventory);
    }

    public void Load()
    {
        SaveData saveData = SaveSystem.LoadSaveData();

        inventory.LoadSaltJars(saveData.currentSaltJars);
        inventory.LoadSalt(saveData.currentSalt);
        inventory.SetCurrentBarbs(saveData.currentBarbs);

        Vector3 playerPosition = new Vector3(saveData.currentPosition[0], saveData.currentPosition[1], saveData.currentPosition[2]);
        player.transform.position = playerPosition;
        arrowHolder.transform.position = playerPosition;

        playerAugments.InitializeBarb(BarbType.stone, saveData.stoneBarbs);
        playerAugments.InitializeBarb(BarbType.air, saveData.airBarbs);
        playerAugments.InitializeBarb(BarbType.fire, saveData.fireBarbs);
        playerAugments.InitializeBarb(BarbType.ice, saveData.iceBarbs);
        playerAugments.InitializeBarb(BarbType.lode, saveData.lodeBarbs);
        playerAugments.InitializeBarb(BarbType.light, saveData.lightBarbs);
        playerAugments.InitializeBarb(BarbType.toxic, saveData.toxicBarbs);
        playerAugments.InitializeBarb(BarbType.rain, saveData.rainBarbs);
        playerAugments.InitializeBarb(BarbType.hollow, saveData.hollowBarbs);
        playerAugments.InitializeStatStages();
         

        //initialize permanents states
        for(int index = 0; index < saveData.permanentIDs.Count; index++)
        {
            if (Permanents.permanentDict[saveData.permanentIDs[index]] == null) break;
            else Permanents.permanentDict[saveData.permanentIDs[index]].SetStatus(saveData.permanentStatuses[index]);
        }

        for (int index = 0; index < saveData.collectibleIDs.Count; index++)
        {
            //Debug.Log(CollectibleManager.collectibleDict[saveData.collectibleIDs[index]].GetID());
            if (CollectibleManager.collectibleDict[saveData.collectibleIDs[index]] == null) break;
            else CollectibleManager.collectibleDict[saveData.collectibleIDs[index]].SetStatus(saveData.collectibleStatuses[index]);
        }

        EnemyManager.RespawnAllDead();

    }
}
