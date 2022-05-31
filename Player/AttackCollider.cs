using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AttackCollider : MonoBehaviour
{
    PlayerData _pd;
    Transform player;
    Rigidbody2D playerRB;
    public int damage;
    public float damageModifier = 1;
    Bouncer bouncer;
    Toxifier toxifier;

    private List<GameObject> currentlyBeingHit = new List<GameObject>();

    private CinemachineImpulseSource shakeSource;

    private void Awake()
    {
        player = FindObjectOfType<Player>().transform;
        _pd = player.GetComponent<PlayerData>();
        playerRB = player.GetComponent<Rigidbody2D>();
        TryGetComponent(out bouncer);
        toxifier = player.GetComponent<Toxifier>();
        shakeSource = GetComponent<CinemachineImpulseSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;

        //prevent dupe hits
        if (currentlyBeingHit.Contains(other)) return;
        StartCoroutine(PreventDuplicateHits(other));

        //checkforparent
        if (other.transform.parent == null) return;

        //get components
        other.transform.parent.TryGetComponent(out Projectile projectile);
        other.transform.parent.TryGetComponent(out IDamageable damageable);
        other.transform.parent.TryGetComponent(out IKnockable knockable);
        other.transform.parent.TryGetComponent(out ISlowable slowable);
        other.transform.parent.TryGetComponent(out IToxable toxable);

        //execute on components
        if (bouncer != null && damageable != null && !bouncer.isBouncing)
            StartCoroutine(bouncer.Bounce(playerRB, _pd.bounceSpeed, _pd.bounceTime));
        if (toxable != null)
        {
            toxifier.ManageToxified(toxable);
        }
        if (damageable != null)
        {
            shakeSource.GenerateImpulse();
            damageable.DealDamage(Mathf.FloorToInt(damage * damageModifier));
        }
        if (knockable != null)
        {
            HitHandler.HandleKnockback(other, knockable, player, _pd.knockback);
        }
        if (projectile != null)
        {
            projectile.DestroyProjectile();
        }
        if (slowable != null)
        {
            slowable.AffectSpeed(_pd.heatModifier);
        }
    }

    IEnumerator PreventDuplicateHits(GameObject other)
    {
        currentlyBeingHit.Add(other);
        yield return new WaitForSeconds(.1f);
        currentlyBeingHit.Remove(other);
    }


}
