using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_HurtState : HurtState
{
    private Enemy2 enemy;

    public E2_HurtState(Entity entity, FiniteStateMachine fsm, string animBoolName, D_HurtState stateData, Enemy2 enemy) : base(entity, fsm, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.aliveGO.GetComponent<Rigidbody2D>().gravityScale = 1;
    }

    public override void Exit()
    {
        base.Exit();
        enemy.aliveGO.GetComponent<Rigidbody2D>().gravityScale = 0;
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
            fsm.ChangeState(enemy.attackState);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
