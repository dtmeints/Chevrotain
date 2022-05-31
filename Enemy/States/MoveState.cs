using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    protected D_MoveState stateData;

    protected bool isDetectingWall;
    protected bool isDetectingLedge;
    protected bool isDetectingPlayer;
    public MoveState(Entity entity, FiniteStateMachine fsm, string animBoolName, D_MoveState stateData) : base(entity, fsm, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void Enter()
    {
        base.Enter();
        entity.SetVelocity(stateData.movementSpeed);
        isDetectingLedge = entity.CheckLedge();
        isDetectingWall = entity.CheckWall();
        isDetectingPlayer = entity.CheckForPlayer();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        isDetectingPlayer = entity.CheckForPlayer();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        entity.SetVelocity(stateData.movementSpeed);
        isDetectingLedge = entity.CheckLedge();
        isDetectingWall = entity.CheckWall();
    }
}
