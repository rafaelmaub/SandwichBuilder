using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class LightTurnerEffect : MonoBehaviour
{
    [SerializeField] bool isOn;
    [SerializeField] float _desireIntensity;
    [SerializeField] float _lerpSpeed;
    [SerializeField] Light _myLight;
    float currentValue;
    private void Awake()
    {
        currentValue = _myLight.intensity;
    }
    private void Update()
    {
        float _target = 0;
        if(isOn)
            _target = _desireIntensity;


        currentValue = Mathf.Lerp(currentValue, _target, Time.deltaTime * _lerpSpeed);
        _myLight.intensity = currentValue;
    }
    public void TurnLight(bool set)
    {
        isOn = set;
    }
}
