using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] PlayerInputManager pim;

    private void Update()
    {
        FlipSprite();
    }
    private void FlipSprite()
    {
        if (Mathf.Abs(pim.moveInput.x) > Mathf.Epsilon && !anim.GetBool("isDashing"))
        {
            gameObject.transform.localScale = new Vector2(Mathf.Sign(pim.moveInput.x), 1);
        }
        else return;
    }
    public void ResetAttackCombo()
    {
        anim.SetInteger("attackCombo", 0);
        anim.SetBool("isUpAttacking", false);
    }

    public void IncrementIdleCount()
    {
        anim.SetInteger("idleCount", anim.GetInteger("idleCount") + 1);
        if (anim.GetInteger("idleCount") >= 8)
        {
            anim.SetInteger("idleCount", 0);
        }
    }

    public void SetNotDying()
    {
        anim.SetBool("isDying", false);
    }

    public void IncrementHangingCounter()
    {
        anim.SetInteger("hangCounter", anim.GetInteger("hangCounter") + 1);
    }

    public void ResetHangingCounter()
    {
        anim.SetInteger("hangCounter", 0);
    }
}
