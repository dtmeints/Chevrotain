using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Permanents 
{
   public static List<IPermanent> permanents = new List<IPermanent>();

   public static Dictionary<string, IPermanent> permanentDict = new Dictionary<string, IPermanent>();

   public static void AddToList(IPermanent permanent)
    {
        if (permanentDict.ContainsKey(permanent.GetID())) return;
        permanents.Add(permanent);
        permanentDict.Add(permanent.GetID(), permanent);
    }
}
