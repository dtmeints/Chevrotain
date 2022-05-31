using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtState : State
{
    protected D_HurtState stateData;

    protected float hurtTime;
    protected bool isHurtTimeOver;
    public HurtState(Entity entity, FiniteStateMachine fsm, string animBoolName, D_HurtState stateData) : base(entity, fsm, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        hurtTime = stateData.hurtTime;
        isHurtTimeOver = false;
    }

    public override void Exit()
    {
        base.Exit();
        entity.isHurt = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + hurtTime)
        {
            isHurtTimeOver = true;
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    protected bool CheckIfFacingPlayer()
    {
        int playerSide = (int)Mathf.Sign(entity.GetAngleToPlayer().x);
        return (playerSide == entity.facingDirection);
    }
}
