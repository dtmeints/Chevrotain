using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_AttackState : AttackState
{
    public Enemy2 enemy;
    public E2_AttackState(Entity entity, FiniteStateMachine fsm, string animBoolName, D_AttackState stateData, Enemy2 enemy) : base(entity, fsm, animBoolName, stateData)
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
        if (entity.GetPlayerDistance() > entity.entityData.escapeRadius)
        {
            fsm.ChangeState(enemy.patrolState);
        }
        if (enemy.isHurt)
        {
            fsm.ChangeState(enemy.hurtState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        enemy.aliveGO.transform.position = Vector2.MoveTowards(enemy.aliveGO.transform.position, enemy.playerLocation.position, stateData.attackSpeed * entity.speedModifier * Time.deltaTime);
    }
}
