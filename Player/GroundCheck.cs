using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] Transform feet;
    public bool amGrounded = true;
    public bool isOnSlope;

    Animator anim;
    public float footCircleRadius = .1f;

    private PlayerData _pd;
    private CapsuleCollider2D cc;
    private Vector2 colliderSize;
    private Rigidbody2D rb;

    private float slopeDownAngle;
    private float slopeDownAngleOld;
    public Vector2 slopeNormalPerp;

    public static Action<bool> AmIGrounded;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CapsuleCollider2D>();
        colliderSize = cc.size;
        _pd = GetComponent<PlayerData>();
    }
    void FixedUpdate()
    {
        CheckIsGrounded();
        SlopeCheck();
    }

    private void SlopeCheck()
    {
        Vector2 checkPos = transform.position - new Vector3(0.0f, colliderSize.y / 2);
        SlopeCheckVertical(checkPos);
    }

    private void SlopeCheckHorizontal(Vector2 checkPos)
    {

    }

    private void SlopeCheckVertical(Vector2 checkPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, _pd.slopeCheckDistance, _pd.whatIsGround);
        if (hit)
        {
            slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;
            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

           // if(slopeDownAngle != slopeDownAngleOld)
           if(slopeNormalPerp != Vector2.left)
            {
                isOnSlope = true;
            }
           else isOnSlope = false;

            slopeDownAngleOld = slopeDownAngle;
            Debug.DrawRay(hit.point, slopeNormalPerp, Color.red);
            Debug.DrawRay(hit.point, hit.normal, Color.green);
        }
    }

    void CheckIsGrounded()
    {
        if (Physics2D.OverlapCircle(feet.position, footCircleRadius, _pd.whatIsGround))
        {
            amGrounded = true;
            AmIGrounded?.Invoke(true);
            anim.SetBool("isGrounded", true);  
        }
        else if (!Physics2D.OverlapCircle(feet.position, footCircleRadius, _pd.whatIsGround))
        {
            amGrounded = false;
            AmIGrounded?.Invoke(false);
            anim.SetBool("isGrounded", false);
        }
    }

    public void SetFrictionMaterialForSlope(bool isRunning)
    {
        if (isRunning)
        {
            rb.sharedMaterial = _pd.noFriction;
            return;
        }
        else if (!isRunning)
        {
            if (isOnSlope)
            {
                rb.sharedMaterial = _pd.fullFriction;
            }
            else if (!isOnSlope)
            {
                rb.sharedMaterial = _pd.noFriction;
            }
        }
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(feet.position, footCircleRadius);
    }
}
