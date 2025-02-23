using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollectMovementState : PlayerMovementState
{
    Coroutine collectIE;

    public PlayerCollectMovementState(PlayerMovementStateMachine _playerMovementStateMachine) : base(_playerMovementStateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();

        stateMachine.player.animController.AnimSetBool(stateMachine.player.animController.CollectHash, true);

        // * Collect item
        CollectItem();

    }

    private void CollectItem()
    {
        if (collectIE != null)
        {
            stateMachine.player.StopCoroutine(collectIE);
        }

        collectIE = stateMachine.player.StartCoroutine(CollectDelay(4 + (18 / 24)));
    }

    public override void Exit()
    {
        base.Exit();

        if (collectIE != null)
        {
            stateMachine.player.StopCoroutine(collectIE);
        }

        stateMachine.player.animController.AnimSetBool(stateMachine.player.animController.CollectHash, false);
    }

    IEnumerator CollectDelay(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        stateMachine.ChangeState(stateMachine.normalState);
    }

    public override void Update()
    {

    }

    public override void PhysicsUpdate()
    {

    }

    public override void HandleInput()
    {

    }
}
