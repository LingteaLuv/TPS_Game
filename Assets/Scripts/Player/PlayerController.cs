using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Drag&Drop")]
    [SerializeField] private PlayerStatus status;
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private GameObject aimCam;
    [SerializeField] private GameObject mainCam;
    
    [SerializeField] private KeyCode aimKey = KeyCode.Mouse1;

    public bool IsControlActivate { get; set; } = true;
    private void Awake()
    {
        Init();
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }

    private void Update()
    {
        HandlePlayerControl();
    }

    private void Init()
    {
        status = GetComponent<PlayerStatus>();
        movement = GetComponent<PlayerMovement>();
    }

    private void HandlePlayerControl()
    {
        if (!IsControlActivate) return;
        
        HandleMovement();
        HandleAiming();
    }

    private void HandleMovement()
    {
        Vector3 camRotateDir = movement.SetAimRotation();

        float moveSpeed;
        if (status.IsAiming.Value) moveSpeed = status.WalkSpeed;
        else moveSpeed = status.RunSpeed;

        Vector3 moveDir = movement.SetMove(moveSpeed);
        status.IsMoving.Value = (moveDir != Vector3.zero);

        Vector3 avatarDir;
        if (status.IsAiming.Value) avatarDir = camRotateDir;
        else avatarDir = moveDir;
            
        movement.SetAvatarRotation(avatarDir);  
    }

    private void HandleAiming()
    {
        status.IsAiming.Value = Input.GetKey(aimKey);
    }

    public void SubscribeEvents()
    {
        status.IsAiming.Subscribe(value => SetActivateAimCamera(value));
    }

    public void UnSubscribeEvents()
    {
        status.IsAiming.Unsubscribe(value => SetActivateAimCamera(value));
    }

    private void SetActivateAimCamera(bool value)
    {
        aimCam.SetActive(value);
        mainCam.SetActive(!value);
    }
}
