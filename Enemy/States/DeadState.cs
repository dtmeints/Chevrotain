using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : State
{
    protected D_DeadState stateData;

    protected float deathAnimClipLength;
    public DeadState(Entity entity, FiniteStateMachine fsm, string animBoolName, D_DeadState stateData) : base(entity, fsm, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        entity.aliveGO.layer = 29;
        deathAnimClipLength = entity.anim.GetCurrentAnimatorClipInfo(0)[0].clip.length * 10;
    }

    public override void Exit()
    {
        SwitchAliveAndDead();
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time > startTime + deathAnimClipLength)
        {
            SwitchAliveAndDead();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public virtual void SwitchAliveAndDead()
    {
        entity.deadGO.transform.position = entity.aliveGO.transform.position;
        entity.deadGO.SetActive(true);
        entity.aliveGO.SetActive(false);
        
    }
}
