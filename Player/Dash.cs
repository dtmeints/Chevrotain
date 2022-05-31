using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerStates;

public class Dash : MonoBehaviour
{
    //external vars
    public DashState dashState = DashState.Ready;

    //internal logic vars
    private float dashTimer;
    [SerializeField] private bool isGroundReset = true;

    //cached references
    PlayerData _pd;
    Rigidbody2D rb;
    [SerializeField] PlayerInputManager pim;
    Animator anim;

    private Transform arrowHolder;
    private BasicAttacks2 basicAttacks2;

    private void Awake()
    {
        _pd = GetComponent<PlayerData>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        arrowHolder = pim.arrowHolder.transform;
        basicAttacks2 = GetComponent<BasicAttacks2>();
    }
    private void Update()
    {
        if (_pd.canDash)
        {
            ManageDash();
        }
    }

    private void ManageDash()
    {
        switch (dashState)
        {
            case DashState.Ready:
                if (pim.isDashPressed)
                {
                    //dash vector construction
                    Vector2 dashVector;
                    if (pim.moveInput == Vector2.zero)
                    {
                        dashVector = new Vector2(transform.localScale.x * _pd.dashSpeed, 0f);

                    }
                    else { dashVector = new Vector2(Mathf.Round(pim.moveInput.x) * _pd.dashSpeed, Mathf.Round(pim.moveInput.y) * _pd.dashSpeed); }

                    rb.velocity = dashVector;
                    rb.gravityScale = 0f;
                    rb.drag = 0f;
                    dashTimer = 0;
                    dashState = DashState.Dashing;
                    anim.SetBool("isDashing", true);
                    basicAttacks2.canAttack = false;
                }
                break;
            case DashState.Dashing:
                dashTimer += Time.deltaTime;
                if (dashTimer >= _pd.maxDash)
                {
                    isGroundReset = false;
                    dashTimer = 0;
                    rb.velocity = Vector2.zero;
                    rb.gravityScale = _pd.playerGravityScale;
                    rb.drag = _pd.playerLinearDrag;
                    dashState = DashState.Cooldown;
                    arrowHolder.position = transform.position;
                    anim.SetBool("isDashing", false);
                    basicAttacks2.canAttack = true;
                }
                break;
            case DashState.Cooldown:
                dashTimer += Time.deltaTime;
                if (dashTimer >= _pd.dashCooldown && isGroundReset)
                {
                    dashTimer = 0;
                    dashState = DashState.Ready;
                }
                break;
        }
    }

    private void SetGroundReset(bool isGrounded = true)
    {
        if (!isGrounded)
        { return; }
        else
            isGroundReset = true;
    }

    private void DashReset()
    {
         isGroundReset = true;
    }

    public enum DashState
    {
        Ready,
        Dashing,
        Cooldown
    }

    private void OnEnable()
    {
        GroundCheck.AmIGrounded += SetGroundReset;
        DownAttack.OnBounce += DashReset;
        WallJump.OnWallStuck += DashReset;
    }

    private void OnDisable()
    {
        GroundCheck.AmIGrounded -= SetGroundReset;
        DownAttack.OnBounce -= DashReset;
        WallJump.OnWallStuck -= DashReset;
    }

}
