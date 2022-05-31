using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ArrowHolder : MonoBehaviour
{
    [SerializeField] Animator playerAnim;
    [SerializeField] Animator arrowAnim;
    public float speedToPlayer = 15;
    Player player;
    [SerializeField] PlayerInputManager _pim;
    [SerializeField] PlayerData _pd;
    Rigidbody2D rb;
    public float arrowTimer = 0f;

    public Transform leverPos;
    public Lever currentLever { get; private set; }



    public static event Action<float> OnStaminaChange;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (arrowAnim.GetBool("isArrowSeparate"))
        {
            FlipHolder();
            ManageSeparateMove();
            ManageSeparateTimer();
            return;
        }
        //if arrow isn't separate, return to player, recharge stamina.
        else
        {
            arrowTimer = Mathf.Clamp(arrowTimer - (Time.deltaTime * _pd.staminaRegenRate), 0, 100);
            OnStaminaChange?.Invoke(1 - (arrowTimer / _pd.maxStamina));
            AdjustTowardPlayer();
        }
    }

    private void ManageSeparateMove()
    {
        //move, or stop moving
        if (_pim.arrowMoveInput != Vector2.zero)
        {
            rb.velocity += _pim.arrowMoveInput * Time.deltaTime * _pd.arrowMoveSpeed;
        }
        else if (_pim.arrowMoveInput == Vector2.zero)
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void ManageSeparateTimer()
    {
        //update timer
        arrowTimer += Time.deltaTime;
        OnStaminaChange?.Invoke(1 - (arrowTimer / _pd.maxStamina));
        //check if timer's up
        if (arrowTimer >= _pd.maxStamina)
        {
            playerAnim.SetBool("isArrowSeparate", false);
        }
    }   
    
    private void AdjustTowardPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speedToPlayer * Time.deltaTime);
        if (arrowAnim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "Arrow_ChargeAttack")
        {

        }
        if (arrowAnim.GetCurrentAnimatorClipInfo(0)[0].clip.name != "Arrow_ChargeAttack")
        {
            //flip to player alignment
            transform.localScale = player.transform.localScale;
            if (playerAnim.GetBool("isHoldingUp"))
            {
                transform.localRotation = Quaternion.Euler(Vector3.Lerp(Quaternion.identity.eulerAngles, new Vector3(0f, 0f, 180f * transform.localScale.x), .1f));
            }
            else
            {
                transform.localRotation = Quaternion.Euler(Vector3.Lerp(Quaternion.identity.eulerAngles, new Vector3(0f, 0f, 0f), .1f));
            }
        }
    }

    private void FlipHolder()
    {
        if (_pim.arrowMoveInput.x == 0)
        {
            return;
        }
        else if (_pim.arrowMoveInput.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (_pim.arrowMoveInput.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    //private void FlipHolderTowardCrank()
    //{
    //    transform.localScale = new Vector3(-currentLever.facingDirection, 1, 1);
    //}

    public void SetInLeverRange(bool inRange, Lever lever)
    {
        _pim.isArrowInLeverRange = inRange;
        
        if (inRange)
        {
            currentLever = lever;
            leverPos = lever.arrowHolderPos;
        }
    }

    public IEnumerator MoveToLever()
    {
        transform.localScale = new Vector3(-1,1,1);
        playerAnim.SetBool("isArrowSeparate", true);
        while (transform.position != leverPos.position || transform.rotation != leverPos.rotation)
        {
            transform.SetPositionAndRotation(Vector2.MoveTowards(transform.position, leverPos.position, .1f), Quaternion.Lerp(transform.rotation, currentLever.transform.rotation, 10f));
            yield return null;
        }
        if (!currentLever.isFlipped) playerAnim.SetBool("isCranking", true);
        else if (currentLever.isFlipped) playerAnim.SetBool("isUncranking", true);
        yield break;
    }
}
