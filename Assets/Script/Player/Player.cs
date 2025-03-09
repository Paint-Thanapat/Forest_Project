using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Player : MonoBehaviourPunCallbacks
{
    public GameObject model;
    [Header("Current Speed")]
    public float currentMoveSpeed = 5;
    [Header("Speed")]
    public float normalMoveSpeed = 5;
    public float runMoveSpeed = 8;
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
    public float jumpForce;
    public LayerMask jumpContractMask;
    public bool canJump;
    public PlayerMovementStateMachine movementStateMachine { get; private set; }

    public PlayerAnimatorController animController;

    public Rigidbody rb { get; private set; }
    public Animator anim { get; private set; }
    public PhotonView PV { get; private set; }

    [Header("Tree Interact")]
    [SerializeField] LayerMask treeInteractLayer;
    BuildableTree interactingBuildableTree;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        PV = GetComponent<PhotonView>();

        NetworkGameplayManager.player = this;

        if (PV.IsMine)
        {
            movementStateMachine = new PlayerMovementStateMachine(this);
            NetworkGameplayManager.LocalID = PV.ViewID;
            MainCamera.Instance.player = this.transform;
            GameManager.Instance.playerCharacter = this.gameObject;
        }
        else
        {
            rb.isKinematic = true;
        }

        animController.Initialize();

    }

    private void Start()
    {
        if (!PV.IsMine) return;

        movementStateMachine.ChangeState(movementStateMachine.normalState);
    }

    private void Update()
    {
        if (!PV.IsMine) return;

        movementStateMachine.HandleInput();
        movementStateMachine.Update();

        if (interactingBuildableTree != null)
        {
            HangManager.OnUpdatePreviewHang(model.transform.eulerAngles);
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (interactingBuildableTree != null)
            {
                HangManager.OnCancelPreviewHang?.Invoke();
                interactingBuildableTree = null;
            }

            if (Physics.Raycast(MainCamera.Instance.transform.position, MainCamera.Instance.transform.forward, out RaycastHit hit, 15f, treeInteractLayer))
            {
                BuildableTree temp_tree = hit.transform.GetComponent<BuildableTree>();
                if (temp_tree != null)
                {
                    if (!temp_tree.isPreviewToBuild)
                    {
                        // * Open UI
                        TreeInteractUIManager.Instance.interactTree = temp_tree;
                        TreeInteractUIManager.Instance.SetActivePanel(true);

                        interactingBuildableTree = temp_tree;
                    }
                    else
                    {
                        // * 
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (interactingBuildableTree != null)
            {
                HangManager.OnCreateHang?.Invoke(interactingBuildableTree, model.transform.eulerAngles);
                interactingBuildableTree = null;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!PV.IsMine) return;

        movementStateMachine.PhysicsUpdate();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!PV.IsMine) return;

        movementStateMachine.OnTriggerEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!PV.IsMine) return;

        movementStateMachine.OnTriggerExit(other);
    }

    public void OnMovementStateAnimationEnterEvent()
    {
        if (!PV.IsMine) return;

        movementStateMachine.OnAnimationEnterEvent();
    }
    public void OnMovementStateAnimationExitEvent()
    {
        if (!PV.IsMine) return;

        movementStateMachine.OnAnimationExitEvent();
    }
    public void OnMovementStateAnimationTransitionEvent()
    {
        if (!PV.IsMine) return;

        movementStateMachine.OnAnimationTransitionEvent();
    }

    public void ChangeState(IState newState)
    {
        movementStateMachine.ChangeState(newState);
    }
}
