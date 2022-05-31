using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Projectile : MonoBehaviour
{
    public GameObject destructParticles;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var collided = collision.gameObject;
        Destroy(gameObject);
    }

    public static event Action<Projectile> OnProjectileDestruct;

    public void DestroyProjectile()
    {
        OnProjectileDestruct?.Invoke(this);
        Destroy(gameObject);
    }
}
