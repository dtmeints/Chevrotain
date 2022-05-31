using System;
using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public FiniteStateMachine fsm;

    public string id = "NOTASSIGNED";

    public D_Entity entityData;
    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }
    public SpriteRenderer sr { get; private set; }
    public GameObject aliveGO { get; private set; }
    public GameObject deadGO { get; private set; }
    public Transform playerLocation { get; private set; }
    public Player player { get; private set; }

    private UnityEngine.Object enemyRef;

    public int facingDirection { get; private set; }
    public Vector2 startPos; //{ get; private set; }

    public ParticleSystem fireParticles;
    public ParticleSystem iceParticles;

    [SerializeField]
    private Transform wallCheck;
    [SerializeField]
    private Transform ledgeCheck;
    [SerializeField]
    private Transform playerCheck;

    public Vector2 closestPoint;
    private Vector2 velocityWorkspace;

    public static Action<Entity> OnEnemyDied;
    public static Action<Entity> OnEnemyHit;

    public float currentHealth;
    public float speedModifier;
    public float toxicModifier;

    public bool isDead = false;
    public bool isHurt = false;

    
    public virtual void Start()
    {
        enemyRef = Resources.Load("Enemies/" + id[0] + id[1]);
        EnemyManager.AddToDictionary(this);
        facingDirection = 1;

        aliveGO = transform.Find("Alive").gameObject;
        deadGO = transform.Find("Dead").gameObject;
        rb = aliveGO.GetComponent<Rigidbody2D>();
        sr = aliveGO.GetComponent<SpriteRenderer>();
        anim = aliveGO.GetComponent<Animator>();
        player = FindObjectOfType<Player>();
        playerLocation = player.transform;

        startPos = transform.position;

        fireParticles = aliveGO.transform.Find("Particles").transform.Find("Fire Particles").GetComponent<ParticleSystem>();
        iceParticles = aliveGO.transform.Find("Particles").transform.Find("Ice Particles").GetComponent<ParticleSystem>();

        fsm = new FiniteStateMachine();
    }

    public virtual void Update()
    {
        fsm.currentState.LogicUpdate();
    }

    public virtual void FixedUpdate()
    {
        fsm.currentState.PhysicsUpdate();
    }

    public void DealDamage(int damage)
    {
        int damageToDeal = Mathf.FloorToInt(damage * toxicModifier * player.Pd.damageModifier);
        Debug.Log("Dealt " + damageToDeal + " to " + gameObject);
        currentHealth -= damageToDeal;
        isHurt = true;
        closestPoint = aliveGO.GetComponent<Collider2D>().ClosestPoint(playerLocation.position);
        SpawnSalt(Mathf.FloorToInt(entityData.hitSalt * player.Pd.saltGetModifier));
        StartCoroutine(HurtVFX());
        OnEnemyHit?.Invoke(this);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void AffectSpeed(float newSpeedModifier)
    {
        StartCoroutine(SpeedThenUnspeed(newSpeedModifier));
    }

    IEnumerator SpeedThenUnspeed(float newSpeedModifier)
    {
        speedModifier *= newSpeedModifier;
        if (speedModifier > 1)
        {
            fireParticles.Play();
        }
        if (speedModifier < 1)
        {
            iceParticles.Play();
        }
        yield return new WaitForSeconds(5);
        speedModifier /= newSpeedModifier;
        if (Mathf.Epsilon <= Mathf.Abs(speedModifier - 1))
        {
            fireParticles.Stop();
        }
    }

    public virtual void TakeKnockback(Vector2 knockback)
    {
        rb.AddForce(knockback * entityData.knockbackModifier, ForceMode2D.Impulse);
    }

    void Die()
    {
        SpawnSalt(Mathf.FloorToInt(entityData.deathSalt * player.Pd.saltGetModifier));
        aliveGO.layer = 29;
        OnEnemyDied?.Invoke(this);
        isDead = true;
      //  Destroy(gameObject);
    }

    IEnumerator HurtVFX()
    {
        sr.color = Color.black;
        yield return new WaitForSeconds(.1f);
        sr.color = Color.white;
    }

    public void SetToxicModifier(float newModifier)
    {
        toxicModifier = newModifier;
    }

    public virtual void SetVelocity(float velocity)
    {
        velocityWorkspace.Set(facingDirection * velocity * speedModifier, rb.velocity.y);
        rb.velocity = velocityWorkspace;
    }

    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, aliveGO.transform.right, entityData.wallCheckDistance, entityData.whatIsGround);
    }

    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround);

    }

    public virtual bool CheckForPlayer()
    {
        if (entityData.awarenessType == D_Entity.AwarenessType.omnidirectional)
        {
            var rayDirection = playerLocation.position - playerCheck.position;
            return Physics2D.Raycast(playerCheck.position, rayDirection, entityData.noticeRadius, entityData.whatIsPlayer);
        }
        else if (entityData.awarenessType == D_Entity.AwarenessType.forwardRay)
        {
            return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.noticeRadius, entityData.whatIsPlayer);
        }
        else if (entityData.awarenessType == D_Entity.AwarenessType.floodlight)
            //TODO:learn how to code floodlight awareness
            return Physics2D.Raycast(playerCheck.position, (Vector3)(Vector2.right * facingDirection), entityData.noticeRadius, entityData.whatIsPlayer);
        else if (entityData.awarenessType == D_Entity.AwarenessType.downwardRay)
        {
            return Physics2D.Raycast(playerCheck.position, Vector2.down * entityData.noticeRadius, entityData.whatIsPlayer);
        }
        else return false;
    }

    public virtual float GetPlayerDistance()
    {
        return (playerLocation.position - playerCheck.position).magnitude;
    }

    public virtual Vector2 GetAngleToPlayer()
    {
        return playerLocation.position - playerCheck.position;
    }    

    public string GetID()
    {
        return id;
    }

    public void SetID(string newID)
    {
        id = newID;
    }

    public bool GetStatus()
    {
        return isDead;
    }

    public void SetStatus(bool deathStatus)
    {
        isDead = deathStatus;
        if (isDead)
        {
            aliveGO.SetActive(false);
            deadGO.SetActive(true);
        }
    }

    public virtual void Flip()
    {
        facingDirection *= -1;
        aliveGO.transform.Rotate(0f, 180f, 0);
        deadGO.transform.Rotate(0f, 180f, 0);
    }

    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.wallCheckDistance));
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down  * entityData.ledgeCheckDistance));
    }

    public virtual void Respawn()
    {
        Debug.Log("respawning " + name + " " + GetID());
        GameObject enemyClone = (GameObject)Instantiate(enemyRef, startPos, Quaternion.identity, transform.parent);
        enemyClone.name = name;
        enemyClone.transform.position = startPos;
        enemyClone.GetComponent<Entity>().SetID(id);
        Destroy(this.gameObject);
    }    

    public virtual void SpawnSalt(int numberToSpawn)
    {
        for (int i = 0; i < numberToSpawn; i++)
        {
            var salt = ObjectPools.SharedInstance.GetPooledSalt();
            if (salt == null) Debug.Log("no salt in pool");
            if (salt != null)
            {
                salt.transform.position = aliveGO.transform.position; //closestPoint;
                salt.SetActive(true);
                salt.GetComponent<Collectible>().StartLifeCountdown();
                salt.GetComponent<Rigidbody2D>().velocity = new Vector3(UnityEngine.Random.Range(-3f, 3f), UnityEngine.Random.Range(10f, 12f), 0f);
            }
        }
    }
}
