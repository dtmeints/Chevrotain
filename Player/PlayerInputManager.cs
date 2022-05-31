using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputManager : MonoBehaviour
{
    public bool isJumpPressed = false;
    public bool isDashPressed = false;
    public bool isAttackPressed = false;
    public bool isCirclePressed = false;
    public bool isInteractPressed = false;
    public Vector2 moveInput;
    public Vector2 arrowMoveInput;
    public bool isSeparatePressed = false;
    public bool isArrowInLeverRange = false;

      private Player player;
      private Animator anim;
      private Run run;
      private Jump jump;
      private WallJump wallJump;
      private Dash dash;
      private Circle circle;
      private Read read;
      private Interact interact;
      public BasicAttacks2 basicAttacks2;

      public ArrowHolder arrowHolder;
      

    private void Awake()
    {
        player = GetComponent<Player>();
        anim = GetComponent<Animator>();
        run = GetComponent<Run>();
        jump = GetComponent<Jump>();
        wallJump = GetComponent<WallJump>();
        dash = GetComponent<Dash>();
        circle = GetComponent<Circle>();
        read = GetComponent<Read>();
        interact = GetComponent<Interact>();
        basicAttacks2 = GetComponent<BasicAttacks2>();

        arrowHolder = FindObjectOfType<ArrowHolder>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (player.dying) { return; }
        if (interact.IsInteracting) StartCoroutine(interact.EndInteract());
        if (player.isImmobile) { return; }
        if (context.phase == InputActionPhase.Started)
        {
            if (read.isReading)
            {
                read.AdvancePage();
                return;
            }
            isJumpPressed = true;
            if (anim.GetBool("isWallStuck"))
                wallJump.StartWallJump();
            if (anim.GetBool("isGrounded") || jump.canJump)
                StartCoroutine(jump.DoJump());
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            isJumpPressed = false;
            anim.SetBool("isJumping", false);
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (read.isReading) return;
        if (interact.IsInteracting) StartCoroutine(interact.EndInteract());
        if (anim.GetBool("isArrowSeparate")) return;
        if (player.dying) { return; }
        if (player.isImmobile) { return; }
        if (context.phase == InputActionPhase.Performed)
        {
            isDashPressed = true;
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            isDashPressed = false;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (read.isReading) return;
        if (interact.IsInteracting) return ;
        if (player.dying) { return; }
        if (player.isImmobile) { return; }
        moveInput = context.ReadValue<Vector2>();
        if (moveInput.y > .6)
        {
            anim.SetBool("isHoldingUp", true);
        }
        else 
        {
            anim.SetBool("isHoldingUp", false);
        }
        if (moveInput.y < -.6)
        {
            anim.SetBool("isHoldingDown", true);
        }
        else
        {
            anim.SetBool("isHoldingDown", false);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (read.isReading) return;
        if (interact.IsInteracting) return;
        if (context.phase == InputActionPhase.Canceled)
        {
            isAttackPressed = false;
            basicAttacks2.CancelAttack();
        }
        if (player.dying) { return; }
        if (player.isImmobile) { return; }
        if (anim.GetBool("isCranking")) return;
        if (anim.GetBool("isUncranking")) return;
        if (isArrowInLeverRange)
        {
            StartCoroutine(arrowHolder.MoveToLever());
            return;
        }
        if (context.phase == InputActionPhase.Performed)
        {
            isAttackPressed = true;
            if (!basicAttacks2.canAttack)
            {
                return;
            }
            if (anim.GetBool("isHoldingUp"))
            {
                StartCoroutine(basicAttacks2.UpAttack());
            }
            else if (anim.GetBool("isHoldingDown") && !anim.GetBool("isGrounded"))
            {
                StartCoroutine(basicAttacks2.DownAttack());
            }
            else
            {
                StartCoroutine(basicAttacks2.FrontAttack());
            }    
        }
        
    }

    public void OnCircle(InputAction.CallbackContext context)
    {
        if (read.isReading) return;
        if (interact.IsInteracting) return;
        if (player.dying) { return; }
        if (player.isImmobile) { return; }
        if (context.phase == InputActionPhase.Performed)
        {
            isCirclePressed = true;
            if (anim.GetBool("isGrounded"))
            {
                circle.StartCircle();
            } 
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            isCirclePressed = false;
        }
    }   

    public void OnSeparate(InputAction.CallbackContext context)
    {
        if (read.isReading) return;
        if (player.isImmobile) { return; }
        if (context.phase == InputActionPhase.Performed)
        {
            isSeparatePressed = true;
            //set arrowseparate bool to the opposite
            anim.SetBool("isArrowSeparate", !anim.GetBool("isArrowSeparate"));
        }
        if (context.phase == InputActionPhase.Canceled)
        {
            isSeparatePressed = false;
        }
    }

    public void OnArrowMove(InputAction.CallbackContext context)
    {
        if (read.isReading) return;
        if (player.isImmobile) { return; }
        if (!anim.GetBool("isArrowSeparate")) { return; }
        else
        {
            arrowMoveInput = context.ReadValue<Vector2>();
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {

        if (context.phase == InputActionPhase.Canceled)
        {
            isInteractPressed = false;
        }
        if (interact.IsInteracting) return;
        if (context.phase == InputActionPhase.Performed)
        {
            isInteractPressed = true;
            if (read.isInReadingRange) read.DoRead();
            if (interact.IsInInteractRange && !interact.IsInteracting) StartCoroutine(interact.DoInteract());
        }
        
    }

    public void CancelAllInputs()
    {
        isJumpPressed = false;
        isDashPressed = false;
        isAttackPressed = false;
        isCirclePressed = false;
        isSeparatePressed = false;
        isInteractPressed = false;
        moveInput = Vector2.zero;
        arrowMoveInput = Vector2.zero;
    }
}
