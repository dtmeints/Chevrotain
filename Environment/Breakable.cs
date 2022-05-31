using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour, IDamageable
{

    public int maxHealth;
    public int currentHealth;

    private ParticleSystem hurtParticles;
    private Collider2D col;
    private Animator anim;

    private void Awake()
    {
        currentHealth = maxHealth;
        anim = transform.GetChild(0).GetComponent<Animator>();
        col = transform.GetChild(0).GetComponent<Collider2D>();
        transform.GetChild(0).Find("Particles").TryGetComponent(out hurtParticles);
    }

    public void DealDamage(int damage)
    {
        currentHealth--;
        if (currentHealth <= 0)
        {
            Die();            
        }
        if (currentHealth > 0)
        {
            HurtVFX();
        }
    }

    private void Die()
    {
        anim.SetBool("isDying", true);
        col.enabled = false;
    }

    private void HurtVFX()
    {
        anim.SetBool("isHurt", true);
        if (hurtParticles != null)
        {
            hurtParticles.Play();
        }
    }
}
