using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Current Speed")]
    public float currentMoveSpeed = 5;
    [Header("Speed")]
    public float normalMoveSpeed = 5;
    public float slowMoveSpeed = 2;

    [Header("Dash")]
    public float dashMoveSpeed = 20;
    public bool canDash;
    public bool isDashCooldown;
    public bool dashing;
    public float dashDuration = 0.5f;
    public float dashCooldown = 2;
    public LayerMask dashLayerMask;

    [Header("Declare Component Movement")]
    public Vector3 movementVector;
    public Transform cameraTransform;

    public PlayerMovementStateMachine movementStateMachine { get; private set; }

    public Rigidbody rb { get; private set; }
    public Animator anim { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();

        movementStateMachine = new PlayerMovementStateMachine(this);
    }

    private void Start()
    {
        movementStateMachine.ChangeState(movementStateMachine.normalState);
    }

    private void Update()
    {
        movementStateMachine.HandleInput();
        movementStateMachine.Update();
    }

    private void FixedUpdate()
    {
        movementStateMachine.PhysicsUpdate();
    }

    private void OnTriggerEnter(Collider other)
    {
        movementStateMachine.OnTriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        movementStateMachine.OnTriggerExit(other);
    }

    public void OnMovementStateAnimationEnterEvent()
    {
        movementStateMachine.OnAnimationEnterEvent();
    }
    public void OnMovementStateAnimationExitEvent()
    {
        movementStateMachine.OnAnimationExitEvent();
    }
    public void OnMovementStateAnimationTransitionEvent()
    {
        movementStateMachine.OnAnimationTransitionEvent();
    }

    public void ChangeState(IState newState)
    {
        movementStateMachine.ChangeState(newState);
    }
}
