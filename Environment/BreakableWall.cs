using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour, IDamageable, IPermanent
{
    [SerializeField] private string id;
    private GameObject wholeWall;
    private GameObject breakingWall;
    private GameObject hider;
    private SpriteRenderer hiderSR;

    private ParticleSystem hurtParticles;
    private ParticleSystem breakParticles;

    private Color fadedColor = new Color(0, 0, 0, 0);
    public float fadeSpeed = .1f;

    public float recoilSpeed = .1f;
    public float hitDistance = .1f;

    public bool breaksTowardRight;
    public bool isBroken =  false;

    public Rigidbody2D rb;

    private int breakDirection;

    public int maxHealth = 3;
    public int currentHealth;

    
    private void Awake()
    {
        InitializeStatus();
        currentHealth = maxHealth;
        wholeWall = transform.Find("whole").gameObject;
        breakingWall = transform.Find("breaking").gameObject;
        hider = transform.Find("Hider").gameObject;
        hiderSR = hider.GetComponent<SpriteRenderer>();
        hurtParticles = transform.Find("Hurt Particles").GetComponent<ParticleSystem>();
        breakParticles = transform.Find("Break Particles").GetComponent<ParticleSystem>();
        rb = GetComponent<Rigidbody2D>();

        if (breaksTowardRight) breakDirection = 1;
        else breakDirection = -1;

    }

    public void DealDamage(int damage)
    {
        currentHealth--;
        if (currentHealth < maxHealth && currentHealth > 0)
        {
            hurtParticles.Play();
            wholeWall.SetActive(false);
            StartCoroutine(WallShove());
        }
        if (currentHealth <= 0)
        {
            StartCoroutine(WallShove());
            Die();
        }
    }

    private void Die()
    {
        breakingWall.SetActive(false);
        StartCoroutine(FadeHider());
        breakParticles.Play();
        isBroken = true;
       // SendToList();
    }

    IEnumerator FadeHider()
    {
        while (hiderSR.color != fadedColor)
        {
            hiderSR.color = Color.Lerp(hiderSR.color, fadedColor, fadeSpeed * Time.deltaTime);
            yield return null;
        }
        hider.SetActive(false);
    }


    IEnumerator WallShove()
    {
        var endPos = (Vector2)transform.position + Vector2.right * hitDistance * breakDirection;
        var origPos = (Vector2)transform.position;
        while ((Vector2)transform.position != endPos)
        {
            transform.position = Vector2.MoveTowards(transform.position, endPos, .1f);
            yield return null;
        }
        yield return new WaitForEndOfFrame();
        StartCoroutine(WallShoveBack(origPos));
    }

    IEnumerator WallShoveBack(Vector2 origPos)
    {
        while ((Vector2)transform.position != origPos)
        {
            transform.position = Vector2.MoveTowards(transform.position, origPos, .1f);
           // transform.position = Vector2.Lerp(transform.position, origPos, recoilSpeed);
            yield return null;
        }
    }

    public bool GetStatus()
    {
        return isBroken;
    }

    public void SetStatus(bool status)
    {
        isBroken = status;
        if (isBroken) gameObject.SetActive(false);
    }

    public string GetID()
    {
        return id;
    }

    public void InitializeStatus()
    {
        if (isBroken) Destroy(this.gameObject);
        else return;
    }

    public void SendToList()
    {
        Permanents.AddToList(this);
    }

    private void OnEnable()
    {
        SendToList();
    }
}
