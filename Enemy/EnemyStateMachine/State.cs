using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State 
{
    protected FiniteStateMachine fsm;
    protected Entity entity;

    protected float startTime;
    protected string animBoolName;

    public State(Entity entity, FiniteStateMachine fsm, string animBoolName)
    {
        this.entity = entity;
        this.fsm = fsm;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        startTime = Time.time;
        entity.anim.SetBool(animBoolName, true);
    }

    public virtual void Exit()
    {
        entity.anim.SetBool(animBoolName, false);
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }
}
