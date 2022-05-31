using UnityEngine;
using PlayerStates;

public class PlayerStateMachine : MonoBehaviour
{
    [SerializeField] PlayerState currentState = PlayerState.Empty;
    [SerializeField] PlayerInputManager pim;

    Rigidbody2D rb;

    public bool isGrounded = false;
    public bool isFalling = false;
    public bool isMovingX = false;
    public bool isAttacking = false;
    public bool isUpAttacking = false;
    public bool isDownAttacking = false;

    public Jump jump;
    public Dash dash;
    public WallJump wallJump;
    public BasicAttacks basicAttacks;

    private void Awake()
    {
      rb = GetComponent<Rigidbody2D>();  
    }
    private void Update()
    {
        CheckFalling(); 
        switch (currentState)
        {
            case PlayerState.Idling:
                break;
            case PlayerState.Running:
                break;
            case PlayerState.Jumping:
                //jump.DoJump();
                break;
            case PlayerState.AttackingStill:
                break;
            case PlayerState.AttackingMoving:
                break;
            case PlayerState.UpAttacking:
                break;
            case PlayerState.DownAttacking:
                break;
            case PlayerState.Dashing:
                break;
            case PlayerState.StickingToWall:
                break;
            case PlayerState.WallJumping:
                break;
            case PlayerState.JustFalling:
                break;
            case PlayerState.Dying:
                break;
            case PlayerState.Empty:
                ChooseState();
                break;
        }
    }

    private void ChooseState()
    {
        if (isGrounded)
        {
            if (isMovingX)
            {
                if (pim.isJumpPressed)
                {
                    SetState(PlayerState.Jumping);
                    return;
                }
                if (isAttacking)
                {
                    SetState(PlayerState.AttackingMoving);
                    return;
                }
                else if (!isAttacking)
                {
                    SetState(PlayerState.Running);
                    return;
                }
            }
        }
    }
    public bool CheckState(PlayerState checkingFor)
    {
        if (currentState == checkingFor) return true;
        else return false;
    }
    public PlayerState GetState()
    {
        return currentState;
    }
    public void SetState(PlayerState stateToSet)
    {
        currentState = stateToSet;
    }

    public void CheckFalling()
    {
        if (rb.velocity.y < 0)
            isFalling = true;
        else
            isFalling = false;
    }

    private void SetGrounded(bool groundedness)
    {
        isGrounded = groundedness;
    }

    private void OnEnable()
    {
        GroundCheck.AmIGrounded += SetGrounded;
    }
    private void OnDisable()
    {
        GroundCheck.AmIGrounded -= SetGrounded;
    }
}
