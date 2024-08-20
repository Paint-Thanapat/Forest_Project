using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlowMovementState : MonoBehaviour, IPlayerMovementState
{
    private PlayerMovementController _playerMovementController;
    public void Handle(PlayerMovementController movementController)
    {
        if (!_playerMovementController) // if playermovementcontroller == null
        {
            _playerMovementController = movementController;
        }

        _playerMovementController.currentMoveSpeed = _playerMovementController.slowMoveSpeed;

        //_playerMovementController.canDash = false;
    }

    void FixedUpdate()
    {
        if (_playerMovementController)
        {
            if (_playerMovementController.currentMoveSpeed == _playerMovementController.slowMoveSpeed)
            {
                _playerMovementController.MoveCharacter();
            }
        }
    }
}
