using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UI3DMenu : MonoBehaviour
{
    [SerializeField] private float _animDuration;
    [SerializeField] private Ease _animEase;
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Vector3 _targetPosition;

    [SerializeField] private Graphic[] _highScoreTexts;


    private void Start()
    {
        transform.position = _startPosition;
        foreach (Graphic scoreText in _highScoreTexts)
        {
            scoreText.DOFade(0f, 0f);
        }

    }
    private void OnEnable()
    {
        transform.position = _startPosition;
        transform.DOMove(_targetPosition, _animDuration).SetEase(_animEase);
        foreach (Graphic scoreText in _highScoreTexts)
        {
            scoreText.DOFade(1f, 0.45f);
        }
    }
    public void Hide()
    {
        transform.DOKill();
        transform.DOMove(_startPosition, _animDuration).SetEase(_animEase);
        foreach(Graphic scoreText in _highScoreTexts)
        {
            scoreText.DOFade(0f, 0.45f);
        }
    }

    public void Show()
    {
        transform.DOKill();
        transform.DOMove(_targetPosition, _animDuration).SetEase(_animEase);
        foreach (Graphic scoreText in _highScoreTexts)
        {
            scoreText.DOFade(1f, 0.45f);
        }
    }
}
