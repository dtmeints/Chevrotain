using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_MoveState : MoveState
{
    private Enemy1 enemy;
    public E1_MoveState(Entity entity, FiniteStateMachine fsm, string animBoolName, D_MoveState stateData, Enemy1 enemy) : base(entity, fsm, animBoolName, stateData)
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
        if (isDetectingPlayer)
        {
            fsm.ChangeState(enemy.playerDetectedState);
        }
        if (!isDetectingLedge || isDetectingWall)
        {
            enemy.idleState.SetFlipAfterIdle(true);
            fsm.ChangeState(enemy.idleState);
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
