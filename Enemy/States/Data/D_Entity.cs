using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/Entity Data/Base Data")]
public class D_Entity : ScriptableObject
{
    public float wallCheckDistance = .2f;
    public float ledgeCheckDistance = .4f;

    public float noticeRadius = 3;
    public float escapeRadius = 8;

    public float knockbackModifier = 1;

    public int MaxHealth;
    public int hitSalt = 1;
    public int deathSalt = 3;

    public LayerMask whatIsGround;
    public LayerMask whatIsPlayer;

    public AwarenessType awarenessType;

    public GameObject hitParticles;
    public GameObject deathParticles;

    public enum AwarenessType
    {
        omnidirectional,
        forwardRay,
        floodlight,
        downwardRay,
    }
}
