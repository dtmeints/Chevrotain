using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_PlayerDetectedState : PlayerDetectedState
{
    private Enemy1 enemy;

    public E1_PlayerDetectedState(Entity entity, FiniteStateMachine fsm, string animBoolName, D_PlayerDetectedState stateData, Enemy1 enemy) : base(entity, fsm, animBoolName, stateData)
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
        if (enemy.isHurt)
        {
            fsm.ChangeState(enemy.hurtState);
        }
        fsm.ChangeState(enemy.rushdownState);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
