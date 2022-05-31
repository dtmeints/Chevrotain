using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerStates;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField] PlayerStateMachine psm;
    [SerializeField] PlayerInputManager pim;
    Animator anim;

    public const string CHEV_IDLING = "chev_idling";
    public const string CHEV_RUNNING = "chev_running";
    public const string CHEV_JUMPING = "chev_jumping";
    public const string CHEV_JUSTFALLING = "chev_hanging";
    public const string CHEV_DASHING = "chev_dashing";
    public const string CHEV_WALLSTUCK = "chev_wallstuck";
    public const string CHEV_WALLJUMPING = "chev_walljumping";
    public const string CHEV_ATTACKING_STILL = "chev_attacking_still";
    public const string CHEV_ATTACKING_MOVING = "chev_attacking_moving";
    public const string CHEV_UPATTACKING = "chev_upattacking";
    public const string CHEV_DOWNATTACKING = "chev_downattacking";
    public const string CHEV_DYING = "chev_dying";

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        FlipSprite();
        switch (psm.GetState())
        {
            case PlayerState.Idling:
                anim.Play(CHEV_IDLING);
                break;
            case PlayerState.Running:
                anim.Play(CHEV_RUNNING);
                break;
            case PlayerState.Jumping:
                anim.Play(CHEV_JUMPING);
                break;
            case PlayerState.AttackingStill:
                anim.Play(CHEV_ATTACKING_STILL);
                break;
            case PlayerState.AttackingMoving:
                anim.Play(CHEV_ATTACKING_MOVING);
                break;
            case PlayerState.UpAttacking:
                anim.Play(CHEV_UPATTACKING);
                break;
            case PlayerState.DownAttacking:
                anim.Play(CHEV_DOWNATTACKING);
                break;
            case PlayerState.Dashing:
                anim.Play(CHEV_DASHING);
                break;
            case PlayerState.StickingToWall:
                anim.Play(CHEV_WALLSTUCK);
                break;
            case PlayerState.WallJumping:
                anim.Play(CHEV_WALLJUMPING);
                break;
            case PlayerState.JustFalling:
                anim.Play(CHEV_JUSTFALLING);
                break;
            case PlayerState.Dying:
                anim.Play(CHEV_DYING);
                break;
        }

    }



    public void ResetAttackCombo()
    {
        anim.SetInteger("attackCombo", 0);
        anim.SetBool("isUpAttacking", false);
    }

    public void FlipSprite()
    {
        if (Mathf.Abs(pim.moveInput.x) > Mathf.Epsilon)
        { transform.localScale = new Vector2(-Mathf.Sign(pim.moveInput.x), 1); }
    }
}
