using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth;

    [Header("Run Settings")]
    public float runSpeed;
    public float maxXSpeed;

    [Header("Jump Settings")]
    public float jumpSpeed;
    public float jumpLength;

    [Header("Circle Settings")]
    public float circleLength;
    public int circleSaltCost;

    [Header("Dash Settings")]
    public bool canDash;
    public float dashSpeed;
    public float dashCooldown;
    public float maxDash;

    [Header("Wall Jump Settings")]
    public bool canWallJump;
    public float wallJumpSpeed;
    public float wallJumpLength;
    public float wallJumpKickoffSpeed;
    public float wallJumpKickoffSpeedY;

    [Header("Arrow Settings")]
    public float arrowMoveSpeed;
    public float maxStamina;
    public float staminaRegenRate;
    public float damageModifier;
    public float heatModifier;
    public float maxToxicModifier;
    public float saltGetModifier;

    [Header("Basic Attack Settings")]
    public float attackCooldown = .5f;

    [Header("Front Attack")]
    public int frontAttack1Damage;
    public float frontAttack1Radius;
    public GameObject frontAttackOrigin;
    public float frontAttackLength = .1f;

    [Header("Up Attack")]
    public int upAttackDamage = 1;
    public float upAttackRadius = 1f;
    public GameObject upAttackOrigin;
    public float upAttackLength =.1f;

    [Header("Basic Attack Knockback")]
    public float knockback = 3f;

    [Header("Down Attack Settings")]
    public int downAttackDamage = 1;
    public float bounceSpeed = 200f;
    public float bounceTime = .2f;
    public float downwardKnockback = 40f;
    public GameObject downAttackOrigin;
    public float downAttackLength = .1f;

    [Header("Charge Attack Settings")]
    public int chargeAttackBaseDamage = 1;
    public int chargeAttackDamageIncrements = 2;
    public float chargeAttackMaxTime;
    public GameObject chargeAttackOrigin;

    [Header("Physics Settings")]
    public float playerGravityScale = 12;
    public float playerLinearDrag = 20;
    public float playerMass = 1;
    public PhysicsMaterial2D noFriction;
    public PhysicsMaterial2D fullFriction;

    [Header("Checks")]
    public LayerMask whatIsGround;
    public float slopeCheckDistance;

}
