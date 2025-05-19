using System.Collections;
using System.Collections.Generic;
using DesignPatterns;
using UnityEngine;

public class PoolTest : MonoBehaviour
{
    [Header("Drag&Drop")] 
    [SerializeField] private AudioManager audioManager;
    
    private ObjectPool<AudioManager> _pool;
    private AudioManager _temp;

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _temp = _pool.Get();
            _temp.transform.parent = transform;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            _pool.ReturnPool(_temp);
        }
    }

    private void Init()
    {
        _pool = new ObjectPool<AudioManager>(audioManager, 10);
    }
}
