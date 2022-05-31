using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_HurtState : HurtState
{

    private Enemy1 enemy;
    public E1_HurtState(Entity entity, FiniteStateMachine fsm, string animBoolName, D_HurtState stateData, Enemy1 enemy) : base(entity, fsm, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        enemy.idleState.SetFlipAfterIdle(!CheckIfFacingPlayer());
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isHurtTimeOver && enemy.isDead)
        {
            fsm.ChangeState(enemy.deadState);
        }
        if (isHurtTimeOver && !enemy.isDead)
        {
            fsm.ChangeState(enemy.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
