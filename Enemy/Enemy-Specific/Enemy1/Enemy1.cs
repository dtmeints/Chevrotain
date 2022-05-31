using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Entity, IDamageable, ISlowable, IKnockable, IToxable
{
    public E1_IdleState idleState { get; private set; }
    public E1_MoveState moveState { get; private set; }
    public E1_PlayerDetectedState playerDetectedState { get; private set; }
    public E1_RushdownState rushdownState { get; private set;}
    public E1_HurtState hurtState { get; private set; }
    public E1_DeadState deadState { get; private set; }

    [SerializeField]
    private D_IdleState idleStateData;
    [SerializeField]
    private D_MoveState moveStateData;
    [SerializeField]
    private D_PlayerDetectedState playerDetectedStateData;
    [SerializeField]
    private D_RushdownState rushdownStateData;
    [SerializeField]
    private D_HurtState hurtStateData;
    [SerializeField]
    private D_DeadState deadStateData;



    public override void Start()
    {
        base.Start();
        currentHealth = entityData.MaxHealth;
        speedModifier = 1f;
        toxicModifier = 1f;
        moveState = new E1_MoveState(this, fsm, "move", moveStateData, this);
        idleState = new E1_IdleState(this, fsm, "idle", idleStateData, this);
        playerDetectedState = new E1_PlayerDetectedState(this, fsm, "playerDetected", playerDetectedStateData, this);
        rushdownState = new E1_RushdownState(this, fsm, "rushdown", rushdownStateData, this);
        hurtState = new E1_HurtState(this, fsm, "hurt", hurtStateData, this);
        deadState = new E1_DeadState(this, fsm, "dead", deadStateData, this);
        fsm.Initialize(moveState);
    }
}
