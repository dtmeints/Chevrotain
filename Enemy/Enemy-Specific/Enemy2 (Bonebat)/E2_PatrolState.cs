using UnityEngine;

public class E2_PatrolState : PatrolState
{
    private Enemy2 enemy;
    public E2_PatrolState(Entity entity, FiniteStateMachine fsm, string animBoolName, D_PatrolState stateData, Enemy2 enemy) : base(entity, fsm, animBoolName, stateData)
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
        if (entity.CheckForPlayer())
        {
            fsm.ChangeState(enemy.attackState);
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
