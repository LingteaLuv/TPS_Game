using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DesignPatterns;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [field : SerializeField] [field : Range(0,10)] public float WalkSpeed { get; set; }
    [field : SerializeField] [field : Range(0,10)] public float RunSpeed { get; set; }
    [field : SerializeField] [field : Range(0,10)] public float RotateSpeed { get; set; }

    public ObservableProperty<bool> IsAiming { get; private set; } = new();
    public ObservableProperty<bool> IsMoving { get; private set; } = new();
    public ObservableProperty<bool> IsAttacking { get; private set; } = new();

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        
    }

    private void Init()
    {
        
    }
}
