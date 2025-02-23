using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSleepMovementState : PlayerMovementState
{
    public PlayerSleepMovementState(PlayerMovementStateMachine _playerMovementStateMachine) : base(_playerMovementStateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();

        stateMachine.player.animController.AnimSetBool(stateMachine.player.animController.SleepHash, true);

        stateMachine.player.canJump = false;
    }

    public override void Exit()
    {
        base.Exit();

        stateMachine.player.animController.AnimSetBool(stateMachine.player.animController.SleepHash, false);

        stateMachine.player.canJump = true;
    }

    public override void Update()
    {
        if (GetMovementVector() != Vector3.zero)
        {
            stateMachine.ChangeState(stateMachine.normalState);
        }
    }

    public override void PhysicsUpdate()
    {

    }

    public override void HandleInput()
    {
        MoveInput();
        JumpInput();
    }
}
