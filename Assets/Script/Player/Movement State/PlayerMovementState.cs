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

        //Dash
        if (Input.GetKeyDown(KeyCode.Space) && !stateMachine.player.isDashCooldown && stateMachine.player.canDash)
        {
            stateMachine.player.isDashCooldown = true;
            stateMachine.ChangeState(stateMachine.dashState);
            stateMachine.player.StartCoroutine(ReCooldownDash());
        }
    }

    private void Move()
    {
        if (stateMachine.player.movementVector.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(stateMachine.player.movementVector.x, stateMachine.player.movementVector.z) * Mathf.Rad2Deg + stateMachine.player.cameraTransform.eulerAngles.y;

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

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

    #endregion
}


