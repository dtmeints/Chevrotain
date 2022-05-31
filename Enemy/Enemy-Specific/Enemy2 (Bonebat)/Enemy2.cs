using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Entity, IDamageable, ISlowable, IKnockable, IToxable
{
    public E2_PatrolState patrolState { get; private set; }
    public E2_AttackState attackState { get; private set; }
    public E2_HurtState hurtState { get; private set;}
    public E2_DeadState deadState { get; private set; }


    [SerializeField]
    private D_PatrolState patrolStateData;
    [SerializeField]
    private D_AttackState attackStateData;
    [SerializeField]
    private D_HurtState hurtStateData;
    [SerializeField]
    public D_DeadState deadStateData;

    public override void Start()
    {
        base.Start();
        currentHealth = entityData.MaxHealth;
        speedModifier = 1f;
        toxicModifier = 1f;

        patrolState = new E2_PatrolState(this, fsm, "patrol", patrolStateData, this);
        attackState = new E2_AttackState(this, fsm, "attack", attackStateData, this);
        hurtState = new E2_HurtState(this, fsm, "hurt", hurtStateData, this);
        deadState = new E2_DeadState(this, fsm, "dead", deadStateData, this);


        fsm.Initialize(patrolState);
    }
}
