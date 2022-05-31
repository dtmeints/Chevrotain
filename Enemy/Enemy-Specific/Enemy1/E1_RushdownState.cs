using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_RushdownState : RushdownState
{
    private Enemy1 enemy;
    private bool isRushing = false;

    public E1_RushdownState(Entity entity, FiniteStateMachine fsm, string animBoolName, D_RushdownState stateData, Enemy1 enemy) : base(entity, fsm, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.SetVelocity(0f);
        isRushing = false;
    }

    public override void Exit()
    {
        base.Exit();
        enemy.anim.SetBool("isRushing", false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time > startTime + stateData.windupTime && !isRushing)
        {
            isRushing = true;
            enemy.anim.SetBool("isRushing", true);
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
        enemy.CheckLedge();
        if (isRushing)
        {
            enemy.SetVelocity(stateData.rushSpeed);
        }
    }
}
