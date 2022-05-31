using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyManager
{
    public static List<Entity> enemyList = new List<Entity>();
    public static Dictionary<string, Entity> enemyDict = new Dictionary<string, Entity>();

    public static void AddToDictionary(Entity entity)
    {
        if (enemyDict.ContainsKey(entity.GetID()))
        {
            enemyList.Remove(enemyDict[entity.GetID()]);
            enemyDict[entity.GetID()] = entity;
            enemyList.Add(entity);
            return;
        }
        enemyList.Add(entity);
        enemyDict.Add(entity.GetID(), entity);
    }

    public static void RespawnAllDead()
    {
        foreach(Entity entity in enemyList)
        {
            if (entity.GetStatus() == true && entity != null)
            {
                entity.Respawn();
            }
        }
    }
    
}
