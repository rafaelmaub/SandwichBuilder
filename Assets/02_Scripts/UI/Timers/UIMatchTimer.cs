using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIMatchTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timerText;

    private void OnEnable()
    {
        _timerText.color = Color.white;

    }
    void UpdateTimer(float secondsRemaining)
    {
        TimeSpan formatedTime = new TimeSpan(0, 0, (int)secondsRemaining);
        _timerText.text = formatedTime.ToString(@"mm\:ss");
        if(secondsRemaining < 0)
        {
            _timerText.color = Color.red;
        }
    }
}
