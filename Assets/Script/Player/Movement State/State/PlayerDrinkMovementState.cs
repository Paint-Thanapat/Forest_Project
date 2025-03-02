using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDrinkMovementState : PlayerMovementState
{
    Coroutine drinkIE;

    public PlayerDrinkMovementState(PlayerMovementStateMachine _playerMovementStateMachine) : base(_playerMovementStateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();

        stateMachine.player.animController.AnimSetBool(stateMachine.player.animController.DrinkHash, true);

        // * Drink item
        DrinkItem();
    }

    private void DrinkItem()
    {
        if (drinkIE != null)
        {
            stateMachine.player.StopCoroutine(drinkIE);
        }

        drinkIE = stateMachine.player.StartCoroutine(DrinkDelay(5));
    }

    public override void Exit()
    {
        base.Exit();

        if (drinkIE != null)
        {
            stateMachine.player.StopCoroutine(drinkIE);
        }

        stateMachine.player.animController.AnimSetBool(stateMachine.player.animController.DrinkHash, false);
    }

    IEnumerator DrinkDelay(float delayTime)
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
