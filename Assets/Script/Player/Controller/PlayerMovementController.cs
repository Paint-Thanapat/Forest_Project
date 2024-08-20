using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
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
    private IPlayerMovementState _normalMovementState, _slowMovementState, _dashMovementState, _stopMovementState;
    private PlayerMovementStateContext _playerMovementStateContext;


    //Declare Normal Component
    [HideInInspector] public Rigidbody rb;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        _playerMovementStateContext = new PlayerMovementStateContext(this);

        _normalMovementState = gameObject.AddComponent<PlayerNormalMovementState>();
        _slowMovementState = gameObject.AddComponent<PlayerSlowMovementState>();
        _dashMovementState = gameObject.AddComponent<PlayerDashMovementState>();
        _stopMovementState = gameObject.AddComponent<PlayerStopMovementState>();

        TransitionToNormalState();

        Invoke(nameof(ReCooldownDash), dashCooldown);
    }

    void Update()
    {
        if (!cameraTransform)
            cameraTransform = GameManager.Instance.mainCamera.transform;

        UpdateWalkDirection();

        //Debug.Log(_playerMovementStateContext.currentMovementState);

        GroundCheck();
    }

    void PlayerInput()
    {
        //walk
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        movementVector = new Vector3(horizontal, 0f, vertical).normalized;

        //Dash
        if (Input.GetKeyDown(KeyCode.Space) && !isDashCooldown && canDash)
        {
            isDashCooldown = true;
            _playerMovementStateContext.Transition(_dashMovementState);
            StartCoroutine(ReCooldownDash());
        }
    }
    void UpdateWalkDirection()
    {
        PlayerInput();
    }

    void GroundCheck()
    {
        // RaycastHit hit;
        // if (Physics.Raycast(transform.position + Vector3.up * 0.1f, -Vector3.up, out hit, 1))
        // {
        //     if (_playerMovementStateContext.currentMovementState == _normalMovementState)
        //     {
        //         if (hit.collider.gameObject.CompareTag("SlowArea"))
        //         {
        //             _playerMovementStateContext.Transition(_slowMovementState);
        //         }
        //     }
        //     else if (_playerMovementStateContext.currentMovementState == _slowMovementState)
        //     {
        //         if (!hit.collider.gameObject.CompareTag("SlowArea"))
        //         {
        //             _playerMovementStateContext.Transition(_normalMovementState);
        //         }
        //     }
        // }
    }
    public void MoveCharacter()
    {
        if (movementVector.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(movementVector.x, movementVector.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            rb.MovePosition((Vector3)transform.position + (moveDir * currentMoveSpeed * Time.deltaTime));
        }
    }

    IEnumerator ReCooldownDash()
    {
        float countCooldown = dashCooldown;

        while (countCooldown > 0)
        {
            countCooldown -= Time.deltaTime;
            // GameManager.Instance.UIGameplay.playerDashFill.fillAmount = countCooldown / dashCooldown;
            // GameManager.Instance.UIGameplay.particleOnEnableDash.SetActive(false);
            yield return null;
        }

        // GameManager.Instance.UIGameplay.playerDashFill.fillAmount = 0;
        // GameManager.Instance.UIGameplay.particleOnEnableDash.SetActive(true);

        isDashCooldown = false;
    }
    public void TransitionToNormalState()
    {
        _playerMovementStateContext.Transition(_normalMovementState);
    }

    public void TransitionToStopState()
    {
        _playerMovementStateContext.Transition(_stopMovementState);
    }
}
