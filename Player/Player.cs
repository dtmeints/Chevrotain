using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using PlayerStates;
using Cinemachine;


public class Player : MonoBehaviour
{
    public PlayerData Pd { get; private set; }
    private PlayerInputManager pim;
    public int currentHealth;
    public float defaultInvulnerableTime = 2f;
    public bool invulnerable = false;
    public bool dying = false;
    public Rigidbody2D rb;
    Animator anim;

    public bool isImmobile;
    public Vector2 frozenSpot;

    public static Action<Player> OnHeal;
    public static Action OnDeath;
    public static Action OnRespawn;
    public static Action<Player> OnHurt;


    public CinemachineImpulseSource shakeSource;

    void Awake()
    {
        Pd = GetComponent<PlayerData>();
        pim = GetComponent<PlayerInputManager>();
        currentHealth = Pd.maxHealth;
        rb = GetComponent<Rigidbody2D>();
        //set physics properties
        rb.gravityScale = Pd.playerGravityScale;
        rb.drag = Pd.playerLinearDrag;
        rb.mass = Pd.playerMass;
        shakeSource = GetComponent<CinemachineImpulseSource>();
        
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        ManageImmobile();
    }

    private void Start()
    {
        OnRespawn?.Invoke();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer != 9 && other.gameObject.layer != 11) { return; }
        Debug.Log("Chev hit " + other.gameObject);
        if (!invulnerable)
        {
            GetHurt();
        }
    }

    public void GetHurt()
    {
        if (dying) { return; }
        currentHealth -= 1;
        OnHurt?.Invoke(this);
        shakeSource.GenerateImpulse();
        if (currentHealth <= 0)
        {
            Die();
            return;
        }
        StartCoroutine(HurtVFX());
    }

    
    public void GetHealed(int amount)
    {
        currentHealth += amount;
        if (currentHealth > Pd.maxHealth)
        {
            currentHealth = Pd.maxHealth;
        }
        OnHeal?.Invoke(this);
    }

    public void HealToFull()
    {
        var amountToHeal = Pd.maxHealth - currentHealth;
        for (int i = 0; i < amountToHeal; i++)
        {
            GetHealed(1);
        }
    }

   
    void Die()
    {
        anim.SetBool("isDying", true);
        dying = true;
        pim.CancelAllInputs();
        OnDeath?.Invoke();
    }

    IEnumerator HurtVFX()
    {
        Time.timeScale = .2f;
        yield return new WaitForSecondsRealtime(.3f);
        Time.timeScale = 1f;
    }

    public void Immobilize()
    {
        isImmobile = true;
        frozenSpot = new Vector2(transform.position.x, transform.position.y);
    }

    public void Mobilize()
    {
        isImmobile = false; 
        rb.velocity = Vector2.zero;
    }

    private void ManageImmobile()
    {
        if (isImmobile)
        {
            transform.position = frozenSpot;
            rb.velocity = Vector2.zero;
        }
    }
}
