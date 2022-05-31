using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPools : MonoBehaviour
{
    public static ObjectPools SharedInstance;
    public List<GameObject> saltPool;
    public GameObject[] saltsToPool;
    public int saltAmountToPool;
    [SerializeField] Transform saltParent;

    private void Awake()
    {
        SharedInstance = this;
    }


    private void Start()
    {
        saltPool = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < saltAmountToPool; i++)
        {
            foreach (GameObject saltType in saltsToPool)
            {
                tmp = Instantiate(saltType, saltParent);
                tmp.SetActive(false);
                saltPool.Add(tmp);
            }
        }
    }

    public GameObject GetPooledSalt()
    {
        for (int i = 0;i < saltPool.Count; i++)
        {
            if (!saltPool[i].activeInHierarchy)
            {
                return saltPool[i];
            }
        }
        return null;
    }
}
