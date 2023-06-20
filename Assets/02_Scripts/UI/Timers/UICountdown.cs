using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using DG.Tweening;
public class UICountdown : MonoBehaviour
{
    public UnityEvent OnFinishCountdown = new UnityEvent();
    [SerializeField] private TextMeshProUGUI _countDownText;
    [SerializeField] private float _timeToStart;

    private int _currentCountdownSecond;

    private float _currentTimer;
    private bool _counting;

    public void StartCountdown()
    {
        _currentCountdownSecond = (int)_timeToStart;
        _currentTimer = _timeToStart;
        _counting = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(_counting)
        {
            if (_currentTimer > 0)
            {
                _currentTimer -= Time.deltaTime;

                UpdateTimerText((int)_currentTimer);
            }
            else
            {
                StopCountdown();
            }
        }
       
    }

    void UpdateTimerText(int second)
    {
        if(second < _currentCountdownSecond)
        {
            _countDownText.text = second.ToString();
            _currentCountdownSecond = second;
            PopEffectTimerText();
        }
    }
    void StopCountdown()
    {
        _counting = false;
        OnFinishCountdown.Invoke();
    }

    void PopEffectTimerText()
    {
        _countDownText.transform.localScale = Vector3.one;
        _countDownText.transform.DOScale(1.5f, 1f);
        _countDownText.DOFade(1f, 0.5f).OnComplete(() =>
        {
            _countDownText.DOFade(0f, 0.5f);
        });
    }
}
