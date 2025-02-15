using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementState : IState
{
    protected PlayerMovementStateMachine stateMachine;

    public PlayerMovementState(PlayerMovementStateMachine _playerMovementStateMachine)
    {
        stateMachine = _playerMovementStateMachine;

        // movementData = stateMachine.player.data.groundedData;

        // airborneData = stateMachine.player.data.airborneData;

        // SetBaseCameraRecenteringData();

        InitializeData();
    }

    private void InitializeData()
    {
        // SetBaseRotationData();
    }

    #region IState Methods

    public virtual void Enter()
    {
        Debug.Log("State: " + GetType().Name);

        // AddInputActionsCallBacks();
    }

    public virtual void Exit()
    {
        // RemoveInputActionsCallBacks();
    }

    public virtual void HandleInput()
    {
        ReadMovementInput();
    }

    public virtual void Update()
    {
        UpdatePlayerLookForward();
    }

    public virtual void PhysicsUpdate()
    {
        Move();
    }

    public virtual void OnAnimationEnterEvent()
    {

    }

    public virtual void OnAnimationExitEvent()
    {

    }

    public virtual void OnAnimationTransitionEvent()
    {

    }
    public virtual void OnTriggerEnter(Collider other)
    {

    }
    public virtual void OnTriggerExit(Collider other)
    {

    }

    #endregion

    #region Public Methods


    #endregion

    #region Private Methods

    private void ReadMovementInput()
    {
        //walk
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        stateMachine.player.movementVector = new Vector3(horizontal, 0f, vertical).normalized;

        stateMachine.player.animController.AnimSetBool(stateMachine.player.animController.WalkHash, stateMachine.player.movementVector != Vector3.zero);

        //Dash
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(stateMachine.jumpState);
        }
    }

    private void Move()
    {
        if (stateMachine.player.movementVector.magnitude >= 0.1f)
        {
            Vector3 moveDir = GetMoveDirection();

            stateMachine.player.rb.MovePosition((Vector3)stateMachine.player.transform.position + (moveDir * stateMachine.player.currentMoveSpeed * Time.deltaTime));
        }
    }

    IEnumerator ReCooldownDash()
    {
        float countCooldown = stateMachine.player.dashCooldown;

        while (countCooldown > 0)
        {
            countCooldown -= Time.deltaTime;
            // GameManager.Instance.UIGameplay.playerDashFill.fillAmount = countCooldown / dashCooldown;
            // GameManager.Instance.UIGameplay.particleOnEnableDash.SetActive(false);
            yield return null;
        }

        // GameManager.Instance.UIGameplay.playerDashFill.fillAmount = 0;
        // GameManager.Instance.UIGameplay.particleOnEnableDash.SetActive(true);

        stateMachine.player.isDashCooldown = false;
    }

    protected Vector3 GetMoveDirection()
    {
        float targetAngle = Mathf.Atan2(stateMachine.player.movementVector.x, stateMachine.player.movementVector.z) * Mathf.Rad2Deg + stateMachine.player.cameraTransform.eulerAngles.y;
        return Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
    }

    private void UpdatePlayerLookForward()
    {
        float targetAngle = stateMachine.player.cameraTransform.eulerAngles.y;
        stateMachine.player.model.transform.LookAt(stateMachine.player.transform.position + Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward);
    }


    #endregion
}


