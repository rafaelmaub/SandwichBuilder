using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class AudioSourceExtension : MonoBehaviour
{
    [SerializeField] private AudioSource _source;
    [SerializeField] private bool _playOnAwake;
    [SerializeField] private float _startTime;

    // Start is called before the first frame update
    void Awake()
    {
        if(_playOnAwake)
        {
            _source.time = _startTime;
            _source.Play();
        }
    }
}
