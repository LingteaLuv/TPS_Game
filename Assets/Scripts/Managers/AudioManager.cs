using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignPatterns;

public class AudioManager : MonoBehaviour
{
    private AudioSource _bgmSource;
    // private ObjectPool<PlayerStatus> _pool = new();
    
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _bgmSource = GetComponent<AudioSource>();
    }

    public void BgmPlay(AudioClip source)
    {
        _bgmSource.Play();
    }
}
