using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class E1_IdleState : IdleState
{
    private Enemy1 enemy;
    public E1_IdleState(Entity entity, FiniteStateMachine fsm, string animBoolName, D_IdleState stateData, Enemy1 enemy) : base(entity, fsm, animBoolName, stateData)
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
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isDetectingPlayer) fsm.ChangeState(enemy.playerDetectedState);
        if (isIdleTimeOver)
        {
            fsm.ChangeState(enemy.moveState);
        }
        if (enemy.isHurt)
        {
            fsm.ChangeState(enemy.hurtState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
