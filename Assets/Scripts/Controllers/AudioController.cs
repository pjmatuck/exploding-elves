using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour, IGameService
{
    [SerializeField] AudioClip explosion_hit;
    [SerializeField] AudioClip simple_hit;

    AudioSource _audioSource;
    
    void Start()
    {
        ServiceLocator.Instance.Register(this);
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayExplosion()
    {
        _audioSource.clip = explosion_hit;
        _audioSource.Play();
    }

    public void PlaySimpleHit()
    {
        _audioSource.clip = simple_hit;
        _audioSource.Play();
    }
}
