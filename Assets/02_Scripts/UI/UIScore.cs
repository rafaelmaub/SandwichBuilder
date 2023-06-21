using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class UIScore : MonoBehaviour
{
    enum DisplayType { ShowHighest, ShowLatest, ShowCurrent }
    [SerializeField] private DisplayType _displayType;
    [SerializeField] private TextMeshPro _scoreText;
    [SerializeField] private Color _lostColor;
    [SerializeField] private Color _wonColor;
    
    Color _cacheColor;
    float _currentValue;
    Tween vTween;

    private void Start() //check which kind of score should be displaying and subscribe to required events
    {
        //everytime the score in the system changes, the UI display will change
        //this way it keeps the code modular and independent of each other

        if (_displayType == DisplayType.ShowCurrent)
            GameController.Instance.ScoreSystem.OnCurrentScoreChanged.AddListener(UpdateScoreText);
        else if (_displayType == DisplayType.ShowHighest)
        {
            GameController.Instance.ScoreSystem.OnNewBestHighScore.AddListener(IncreaseTextTween);
            IncreaseTextTween(GameController.Instance.ScoreSystem.HighestScore);
        }

        _cacheColor = _scoreText.color;
    }

    private void OnEnable()
    {
        if (_displayType == DisplayType.ShowLatest)
        {
            IncreaseTextTween(GameController.Instance.ScoreSystem.GetLastScore());
        }

    }

    void UpdateScoreText(float newValue) //used for increase value animation for current score
    {
        if (newValue == _currentValue)
        {
            IncreaseTextTween(_currentValue);
            return;
        }
            

        _scoreText.DOComplete();

        if (vTween != null)
            vTween.Complete();


        if(_currentValue > newValue)
        {
            _scoreText.DOColor(_lostColor, 0.2f);
        }
        else
        {
            _scoreText.DOColor(_wonColor, 0.2f);
        }

        _scoreText.DOColor(_cacheColor, 0.2f).SetDelay(0.2f);
        vTween = DOVirtual.Float(_currentValue, newValue, 0.4f, IncreaseTextTween);
    }

    void IncreaseTextTween(float value)
    {
        
        _scoreText.text = value.ToString("F2") + " $";
    }
}
