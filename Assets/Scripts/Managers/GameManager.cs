using System.Collections;
using System.Collections.Generic;
using DesignPatterns;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        
    }

    private void Init()
    {
        SingletonInit();
    }
}
