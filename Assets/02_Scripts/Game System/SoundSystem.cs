using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSystem : Singleton<SoundSystem>
{
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _rightSound;
    [SerializeField] private AudioClip _wrongSound;

    public void Play_WrongSandwich()
    {
        _source.PlayOneShot(_wrongSound);
    }
    public void Play_CorrectSandwich()
    {
        _source.PlayOneShot(_rightSound);
    }
}
