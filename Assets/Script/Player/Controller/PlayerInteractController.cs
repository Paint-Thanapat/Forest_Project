using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractController : Subject
{
    public Vector3 aimPoint { get; private set; }

    //Observer
    private MainCamera _mainCamera;

    public GameObject model;

    [Header("Declare Component Interact")]
    public LayerMask aimLayer;
    private IPlayerInteractState _idleInteractState, _attackInteractState, _changeWeaponInteractState, _stopInteractState;
    private PlayerInteractStateContext _playerInteractStateContext;

    void Awake()
    {
        _mainCamera = (MainCamera)FindObjectOfType<MainCamera>();

        _playerInteractStateContext = new PlayerInteractStateContext(this);

        _idleInteractState = gameObject.AddComponent<PlayerIdleInteractState>();
        _attackInteractState = gameObject.AddComponent<PlayerAttackInteractState>();
        _changeWeaponInteractState = gameObject.AddComponent<PlayerChangeWeaponInteractState>();
        _stopInteractState = gameObject.AddComponent<PlayerStopInteractState>();

        TransitionToNormalState();
    }
    void OnEnable()
    {
        if (_mainCamera)
            Attach(_mainCamera);
    }
    void OnDisable()
    {
        if (_mainCamera)
            Detach(_mainCamera);
    }

    void Update()
    {
        //Debug.Log(_playerInteractStateContext.currentInteractState);

        PlayerInput();
    }

    public void TransitionToNormalState()
    {
        _playerInteractStateContext.Transition(_idleInteractState);
    }
    public void TransitionToStopState()
    {
        _playerInteractStateContext.Transition(_stopInteractState);
    }

    void PlayerInput()
    {
        if (_playerInteractStateContext.currentInteractState != _stopInteractState)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _playerInteractStateContext.Transition(_attackInteractState);
            }

            if (Input.GetMouseButtonUp(0))
            {
                _playerInteractStateContext.Transition(_idleInteractState);
                // playerWeaponHolder.ToggleFire();
            }
        }
    }

    public void CastAimPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000, aimLayer))
        {
            aimPoint = hit.point;
        }

        LookAtPoint(aimPoint);

        NotifyObservers();
    }
    void LookAtPoint(Vector3 point)
    {
        Vector3 lookPoint = new Vector3(point.x, transform.position.y, point.z);

        model.transform.LookAt(lookPoint);
    }

    public bool thisIsCurrentState(IPlayerInteractState state)
    {
        if (state == _playerInteractStateContext.currentInteractState)
        {
            return true;
        }

        return false;
    }
}
