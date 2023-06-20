using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameScore : MonoBehaviour
{
    [HideInInspector] public UnityEvent<float> OnCurrentScoreChanged = new UnityEvent<float>();
    [HideInInspector] public UnityEvent<float> OnNewBestHighScore = new UnityEvent<float>();
    [SerializeField] private float _lastScore;
    [SerializeField] private float _currentScore;
    public float HighestScore => PlayerPrefs.GetFloat(HIGH_SCORE);

    const string HIGH_SCORE = "HighScore";

    public void StartCountingScore()
    {
        _currentScore = 0;
        ChangeCurrentScore(0);
    }

    public void ChangeCurrentScore(float points)
    {
        _currentScore += points;
        OnCurrentScoreChanged.Invoke(_currentScore);
    }

    public bool FinishCountingScore() //RETURNS TRUE IF NEW HIGH SCORE
    {
        _lastScore = _currentScore;
        if(_lastScore > HighestScore)
        {
            OnNewBestHighScore.Invoke(_lastScore);
            SaveHighScore();
            return true;
        }

        return false;
    }

    public float GetLastScore()
    {
        return _lastScore;
    }
    void SaveHighScore()
    {
        PlayerPrefs.SetFloat(HIGH_SCORE, _lastScore);
    }

}
