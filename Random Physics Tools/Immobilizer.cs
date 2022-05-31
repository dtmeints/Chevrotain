using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Immobilizer : MonoBehaviour
{
    public bool isImmobile = false;
    Vector2 frozenSpot;
    
    void Update()
    {
        if (isImmobile)
        {
            transform.position = frozenSpot;
        }
    }
    public void Immobilize()
    {
        isImmobile = true;
        frozenSpot = new Vector2(transform.position.x, transform.position.y);
    }

    public void Mobilize()
    {
        isImmobile = false;
    }

    public void OnEnable() => Player.OnDeath += Immobilize;
    public void OnDisable() => Player.OnDeath -= Immobilize;
}
