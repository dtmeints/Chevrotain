using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerStates;

public class Jump : MonoBehaviour
{
    // internal logic vars
    public float jumpTimer;
    public bool canJump;
    public float coyoteDelay = .05f;

    //cached references
    PlayerData _pd;
    Rigidbody2D myRigidbody;
    Animator anim;

    [SerializeField] PlayerInputManager pim;


    void Awake()
    {
        _pd = GetComponent<PlayerData>();
        anim = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        jumpTimer = _pd.jumpLength;
    }

    public IEnumerator DoJump()
    {
        if (pim.isJumpPressed
            && jumpTimer > 0
            && !anim.GetBool("isJumping")
            && canJump)
        {
            anim.SetBool("isJumping", true);
            myRigidbody.gravityScale = 0f;
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 0);
            yield return new WaitForSeconds(.05f);
            while (pim.isJumpPressed && jumpTimer > 0)
            {
                myRigidbody.velocity += Vector2.up * _pd.jumpSpeed * Time.deltaTime;
                jumpTimer -= Time.deltaTime;
                yield return null;
            }
            //reset
            anim.SetBool("isJumping", false); 
            jumpTimer = _pd.jumpLength;
            myRigidbody.gravityScale = _pd.playerGravityScale;
            yield break;
        }
    }

    //IEnumerator KeepJumping()
    //{
    //    myRigidbody.velocity += new Vector2(0, _pd.jumpSpeed);
    //    yield return new WaitForEndOfFrame();
    //    jumpTimer -= Time.deltaTime;
    //    if (pim.isJumpPressed && jumpTimer > 0)
    //    {
    //        StartCoroutine(KeepJumping());
    //        yield break;
    //    }
    //    else
    //    {
    //        if (anim.GetBool("isJumping")) { anim.SetBool("isJumping", false); }
    //        jumpTimer = _pd.jumpLength;
    //        myRigidbody.gravityScale = _pd.playerGravityScale;
    //        yield break;
    //    }
    //}

    private void SetCanJump(bool amGrounded)
    {
        if (amGrounded)
        {
            canJump = true;
        }
        if (!amGrounded)
        {
            StartCoroutine(WaitThenUnground());
        }
    }

    IEnumerator WaitThenUnground()
    {
        yield return new WaitForSeconds(coyoteDelay);
        canJump = false;
    }

    private void OnEnable()
    {
        GroundCheck.AmIGrounded += SetCanJump;
    }
    private void OnDisable()
    {
        GroundCheck.AmIGrounded -= SetCanJump;
    }

}
