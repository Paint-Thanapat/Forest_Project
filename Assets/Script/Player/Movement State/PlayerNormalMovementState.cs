using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNormalMovementState : MonoBehaviour, IPlayerMovementState
{
    private PlayerMovementController _playerMovementController;
    public void Handle(PlayerMovementController movementController)
    {
        if (!_playerMovementController) // if playermovementcontroller == null
        {
            _playerMovementController = movementController;
        }

        _playerMovementController.currentMoveSpeed = _playerMovementController.normalMoveSpeed;

        _playerMovementController.canDash = true;
    }

    void FixedUpdate()
    {
        if (_playerMovementController)
        {
            if (_playerMovementController.currentMoveSpeed == _playerMovementController.normalMoveSpeed)
            {
                _playerMovementController.MoveCharacter();
            }
        }
    }
}
