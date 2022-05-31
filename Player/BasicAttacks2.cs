using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttacks2 : MonoBehaviour
{

    //Cached References
    [SerializeField] PlayerData _pd;
    [SerializeField] PlayerInputManager pim;
    [SerializeField] Animator anim;

    AttackCollider frontAttackScript;
    AttackCollider upAttackScript;
    AttackCollider downAttackScript;
    AttackCollider chargeAttackScript;

    Collider2D frontAttackCollider;
    Collider2D upAttackCollider;
    Collider2D downAttackCollider;
    Collider2D chargeAttackCollider;

    [SerializeField] GameObject frontAttackSwoop;
    [SerializeField] GameObject upAttackSwoop;
    [SerializeField] GameObject downAttackSwoop;

    private ArrowParticles arrowParticles;

    public bool canAttack = true;
    private bool preCharging = false;

    private float chargeTimer;
    public float isCharged;

    void Awake()
    {
        _pd = GetComponent<PlayerData>();

        frontAttackCollider = _pd.frontAttackOrigin.GetComponent<Collider2D>();
        upAttackCollider = _pd.upAttackOrigin.GetComponent<Collider2D>();
        downAttackCollider = _pd.downAttackOrigin.GetComponent<Collider2D>();
        chargeAttackCollider = _pd.chargeAttackOrigin.GetComponent<Collider2D>();

        frontAttackScript = _pd.frontAttackOrigin.GetComponent<AttackCollider>();
        upAttackScript = _pd.upAttackOrigin.GetComponent<AttackCollider>();
        downAttackScript = _pd.downAttackOrigin.GetComponent<AttackCollider>();
        chargeAttackScript = _pd.chargeAttackOrigin.gameObject.GetComponent<AttackCollider>();

        frontAttackScript.damage = _pd.frontAttack1Damage;
        upAttackScript.damage = _pd.upAttackDamage;
        downAttackScript.damage = _pd.downAttackDamage;
        chargeAttackScript.damage = _pd.chargeAttackBaseDamage;

        arrowParticles = FindObjectOfType<ArrowParticles>();
    }

    public static Action<Player, Transform, GameObject> OnFrontAttack;
    public IEnumerator FrontAttack()
    {
        StartCoroutine(AttackCooldown());
        OnFrontAttack?.Invoke(GetComponent<Player>(), _pd.frontAttackOrigin.transform, frontAttackSwoop);
        arrowParticles.PlayArrowParticles();
        anim.SetBool("isFrontAttacking", true);
        frontAttackCollider.enabled = true;
        yield return new WaitForSeconds(.1f);
        anim.SetBool("isFrontAttacking", false);
        frontAttackCollider.enabled = false;
        yield return new WaitForSeconds(.2f);
        if (pim.isAttackPressed)
        {
            StartCoroutine(ChargeAttack());
        }
    }

    public static Action<Player, Transform, GameObject> OnUpAttack;
    public IEnumerator UpAttack()
    {
        StartCoroutine(AttackCooldown());
        OnUpAttack?.Invoke(GetComponent<Player>(), _pd.upAttackOrigin.transform, upAttackSwoop);
        arrowParticles.PlayArrowParticles();
        anim.SetBool("isUpAttacking", true);
        upAttackCollider.enabled = true;
        yield return new WaitForSeconds(.1f);
        anim.SetBool("isUpAttacking", false);
        upAttackCollider.enabled = false;
        yield return new WaitForSeconds(.2f);
        if (pim.isAttackPressed)
        {
            StartCoroutine(ChargeAttack());
        }
    }

    public static Action<Player,Transform, GameObject> OnDownAttack;
    public IEnumerator DownAttack()
    {
        StartCoroutine(AttackCooldown());
        OnDownAttack?.Invoke(GetComponent<Player>(), _pd.downAttackOrigin.transform, downAttackSwoop);
        arrowParticles.PlayArrowParticles();
        anim.SetBool("isDownAttacking", true);
        downAttackCollider.enabled = true;
        yield return new WaitForSeconds(.1f);
        anim.SetBool("isDownAttacking", false);
        downAttackCollider.enabled = false;
        yield return new WaitForSeconds(.2f);
        if (pim.isAttackPressed)
        {
            StartCoroutine(ChargeAttack());
        }
    }

    public IEnumerator ChargeAttack()
    {
        preCharging = true;
        yield return new WaitForSeconds(.5f);
        preCharging = false;
        anim.SetBool("isChargingAttack", true);
        while (chargeTimer < _pd.chargeAttackMaxTime && pim.isAttackPressed)
        {
            chargeTimer += Time.deltaTime;
            yield return null;
        }
        while (chargeTimer >= _pd.chargeAttackMaxTime && pim.isAttackPressed)
        {
            yield return null;
        }
        if (!pim.isAttackPressed)
        {
            StartCoroutine(ReleaseArrow());
            anim.SetBool("isChargingAttack", false);
        }
    }

    IEnumerator ReleaseArrow()
    {
        anim.SetBool("isChargeAttacking", true);
        chargeAttackCollider.enabled = true;
        yield return new WaitForSeconds(.1f);
        anim.SetBool("isChargeAttacking", false);
        yield return new WaitForSeconds(.5f);
        chargeAttackCollider.enabled = false; 
    }

    public void CancelAttack()
    {
        if (preCharging)
        {
            StopCoroutine(nameof(ChargeAttack));
            preCharging = false;
        }
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(_pd.attackCooldown);
        canAttack = true;
    }

    public void SetCanAttack(bool newCanAttack)
    {
        canAttack = newCanAttack;
    }
}
