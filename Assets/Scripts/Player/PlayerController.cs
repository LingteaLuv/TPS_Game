using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    [Header("Drag&Drop")]
    [SerializeField] private PlayerStatus status;
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private CinemachineVirtualCamera aimCam;
    [SerializeField] private CinemachineVirtualCamera mainCam;
    
    [SerializeField] private KeyCode aimKey = KeyCode.Mouse1;

    [SerializeField] private Animator animator;

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
        animator = GetComponent<Animator>();
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
        status.IsAiming.Subscribe(aimCam.gameObject.SetActive);
        status.IsAiming.Subscribe(SetAimAnimation);
    }

    public void UnSubscribeEvents()
    {
        status.IsAiming.Unsubscribe(aimCam.gameObject.SetActive);
        status.IsAiming.Unsubscribe(SetAimAnimation);
    }

    private void SetAimAnimation(bool value)
    {
        animator.SetBool("IsAim", value);
    }
}
