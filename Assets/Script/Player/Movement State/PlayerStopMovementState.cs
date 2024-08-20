using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStopMovementState : MonoBehaviour, IPlayerMovementState
{
    private PlayerMovementController _playerMovementController;
    public void Handle(PlayerMovementController movementController)
    {
        if (!_playerMovementController) // if playermovementcontroller == null
        {
            _playerMovementController = movementController;
        }

        _playerMovementController.currentMoveSpeed = 0;
    }
}
