using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CollectibleManager 
{
   public static List<ICollectible> collectibleList = new List<ICollectible>();
    public static Dictionary<string, ICollectible> collectibleDict = new Dictionary<string, ICollectible>();

    public static void AddToDict(ICollectible collectible)
    {
        if (collectibleDict.ContainsKey(collectible.GetID())) return;
        collectibleList.Add(collectible);
        collectibleDict.Add(collectible.GetID(), collectible);
    }
}
