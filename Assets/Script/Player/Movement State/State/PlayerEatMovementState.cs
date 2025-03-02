using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEatMovementState : PlayerMovementState
{
    Coroutine eatIE;

    public PlayerEatMovementState(PlayerMovementStateMachine _playerMovementStateMachine) : base(_playerMovementStateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();

        stateMachine.player.animController.AnimSetBool(stateMachine.player.animController.EatHash, true);

        // * Eat item
        EatItem();

    }

    private void EatItem()
    {
        if (eatIE != null)
        {
            stateMachine.player.StopCoroutine(eatIE);
        }

        eatIE = stateMachine.player.StartCoroutine(EatDelay(5));
    }

    public override void Exit()
    {
        base.Exit();

        if (eatIE != null)
        {
            stateMachine.player.StopCoroutine(eatIE);
        }

        stateMachine.player.animController.AnimSetBool(stateMachine.player.animController.EatHash, false);
    }

    IEnumerator EatDelay(float delayTime)
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
