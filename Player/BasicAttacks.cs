using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerStates;

public class BasicAttacks : MonoBehaviour
{
//    [SerializeField] PlayerData _pd;
//    [SerializeField] PlayerInputManager pim;
//    [SerializeField] Animator anim;

//    Vector2 frontAttack3Size;
//    bool isHoldingUp;

//    void Awake()
//    {
//        _pd = GetComponent<PlayerData>();
//        frontAttack3Size = new Vector2(_pd.frontAttack3Width, _pd.frontAttack3Height);
//    }

//    private void Update()
//    {
//        CheckIsHoldingUp();
//    }

//    public void OnAttack(InputAction.CallbackContext context)
//    {
//        if (context.phase == InputActionPhase.Performed)
//        {
//            Attack();
//        }
//    }

//    public void CheckIsHoldingUp()
//    {
//        if (pim.moveInput.y > 0)
//        {
//            isHoldingUp = true;
//        }
//        else
//        {
//            isHoldingUp = false;
//        }
//    }

//    public void Attack()
//    {
//        if (isHoldingUp)
//        {
//            anim.SetBool("isUpAttacking", true);
//        }
//        else
//        {
//            anim.SetInteger("attackCombo", anim.GetInteger("attackCombo") + 1);
//        }
//    }

//    called by animator
//    public void HandlePlayerUpAttack()
//    {
//        Vector2 attackOrigin = _pd.upAttack.position;
//        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackOrigin, _pd.upAttackRadius, LayerMask.GetMask("Enemy"));
//        foreach (Collider2D collider in colliders)
//        {
//            IDamageable damageable = collider.gameObject.GetComponent<IDamageable>();
//            if (damageable == null) { return; }
//            else if (damageable != null)
//            {
//                HandleHit(collider, _pd.upAttackDamage);
//            }
//        }
//    }

//    called by animator
//    public void HandlePlayerAttack(int comboLevel)
//    {
//        if (comboLevel == 1)
//        {
//            Vector2 attackOrigin = _pd.frontAttack1.position;
//            Collider2D[] colliders = Physics2D.OverlapCircleAll(attackOrigin, _pd.frontAttack1Radius, LayerMask.GetMask("Enemy"));
//            foreach (Collider2D collider in colliders)
//            {
//                HandleHit(collider, _pd.frontAttack1Damage);
//            }
//        }
//        else if (comboLevel == 2)
//        {
//            Vector2 attackOrigin = _pd.frontAttack2.position;
//            Collider2D[] colliders = Physics2D.OverlapCircleAll(attackOrigin, _pd.frontAttack2Radius, LayerMask.GetMask("Enemy"));
//            foreach (Collider2D collider in colliders)
//            {
//                HandleHit(collider, _pd.frontAttack2Damage);
//            }
//        }
//        else if (comboLevel == 3)
//        {
//            Vector2 attackOrigin = _pd.frontAttack3.position;
//            Collider2D[] colliders = Physics2D.OverlapBoxAll(attackOrigin, frontAttack3Size, LayerMask.GetMask("Enemy"));
//            foreach (Collider2D collider in colliders)
//            {
//                HandleHit(collider, _pd.frontAttack3Damage);
//            }
//        }
//    }

//    void HandleHit(Collider2D collider, int damage)
//    {
//        IDamageable damageable = collider.gameObject.GetComponent<IDamageable>();
//        if (damageable == null) { return; }
//        else if (damageable != null)
//        {
//            damageable.DealDamage(damage);
//            Debug.Log("you dealt" + damage + "damage to" + collider.gameObject.name);
//            IKnockable knockable = collider.gameObject.GetComponent<IKnockable>();
//            if (knockable != null)
//            { HandleKnockback(collider, knockable); }
//        }
//    }

//    void HandleKnockback(Collider2D collided, IKnockable knockable)
//    {
//        float knockbackVectorY = collided.gameObject.transform.position.y - transform.position.y;
//        float knockbackVectorX = collided.gameObject.transform.position.x - transform.position.x;
//        float signOfY = Mathf.Sign(knockbackVectorY);
//        float signOfX = Mathf.Sign(knockbackVectorX);
//        if (Mathf.Abs(knockbackVectorY) > Mathf.Abs(knockbackVectorX))
//        {
//            knockbackVectorX /= knockbackVectorY;
//            knockbackVectorY = signOfY * _pd.knockback;
//        }
//        else if (Mathf.Abs(knockbackVectorX) > Mathf.Abs(knockbackVectorY))
//        {
//            knockbackVectorY /= knockbackVectorX;
//            knockbackVectorX = signOfX * _pd.knockback;
//        }
//        else
//        {
//            knockbackVectorX = signOfX * _pd.knockback;
//            knockbackVectorY = signOfY * _pd.knockback;
//        }
//        Vector2 knockbackForce = new Vector2(knockbackVectorX, knockbackVectorY);
//        knockable.TakeKnockback(knockbackForce);
//    }

//    public void OnDrawGizmosSelected()
//    {
//        Gizmos.DrawWireSphere(_pd.frontAttack1.position, _pd.frontAttack1Radius);
//        Gizmos.DrawWireSphere(_pd.frontAttack2.position, _pd.frontAttack2Radius);
//        Gizmos.DrawWireSphere(_pd.upAttack.position, _pd.upAttackRadius);
//        Gizmos.DrawWireCube(_pd.frontAttack3.position, new Vector3(_pd.frontAttack3Width, _pd.frontAttack3Height, 1f));
//    }
}
