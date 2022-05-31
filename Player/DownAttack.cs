using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class DownAttack : MonoBehaviour
{
    //internal logic
    [SerializeField] private bool isHoldingDown = false;

    //cached references
    PlayerData _pd;
    [SerializeField] PlayerInputManager pim;
    Animator anim;
    [SerializeField] Arrow arrow;
    BoxCollider2D arrowCollider;
    Rigidbody2D myRigidbody;

    private void Awake()
    {
        _pd = GetComponent<PlayerData>();
        anim = GetComponent<Animator>();
        arrowCollider = arrow.GetComponent<BoxCollider2D>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckIsHoldingDown();
        ManageDownAttack();
    }

    void ManageDownAttack()
    {
        //if you're not downatttacking and not meeting any conditions to start downattacking, return
        if (!anim.GetBool("isDownAttacking") && (!isHoldingDown || anim.GetBool("isGrounded") || anim.GetBool("isWallStuck")))
        { return; }
        //if you're already downattacking and meeting conditions for downattacking, return
        if (anim.GetBool("isDownAttacking") && isHoldingDown && !anim.GetBool("isGrounded") && !anim.GetBool("isWallStuck")) 
            { return; }
        //if you are meeting conditions to start downattacking
        if (isHoldingDown && !anim.GetBool("isDownAttacking"))
        {
            anim.SetBool("isDownAttacking", true);
            arrowCollider.enabled = true;
            return;
        }
        //if you're downattacking and fail any conditions to keep downattacking, stop downattacking
        if (anim.GetBool("isDownAttacking") && (!isHoldingDown || anim.GetBool("isGrounded") || anim.GetBool("isWallStuck")))
        {
            anim.SetBool("isDownAttacking", false);
            arrowCollider.enabled = false;
        }
    }

    public static Action OnBounce;
    public void Bounce()
    {
        myRigidbody.velocity = Vector2.zero;
        myRigidbody.AddForce(new Vector2(myRigidbody.velocity.x, _pd.bounceSpeed), ForceMode2D.Impulse);
        OnBounce?.Invoke();
    }

    public void HandleHit(IDamageable damageable)
    {
        damageable.DealDamage(_pd.downAttackDamage);
    }

    public void HandleDownKnock(IKnockable knockable)
    {
        knockable.TakeKnockback(new Vector2(0f, -_pd.downwardKnockback));
    }

    void CheckIsHoldingDown()
    {
        if (pim.moveInput.y < 0)
        {
            isHoldingDown = true;
        }
        else
            isHoldingDown = false;
    }
    
}
