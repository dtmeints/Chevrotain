using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_DeadState : DeadState
{
    private Enemy2 enemy;

    public E2_DeadState(Entity entity, FiniteStateMachine fsm, string animBoolName, D_DeadState stateData, Enemy2 enemy) : base(entity, fsm, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.aliveGO.GetComponent<CapsuleCollider2D>().size = new Vector2(.01f, .7f);
        enemy.aliveGO.GetComponent<Rigidbody2D>().gravityScale = 10;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (enemy.CheckLedge())
        {
            enemy.anim.SetBool("grounded", true);
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void SwitchAliveAndDead()
    {
        base.SwitchAliveAndDead();
    }
}
