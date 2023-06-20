using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class UI3DMenu : MonoBehaviour
{
    [SerializeField] private float _animDuration;
    [SerializeField] private Ease _animEase;
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private Vector3 _targetPosition;

    private void Start()
    {
        transform.position = _startPosition;

    }
    public void Hide()
    {
        transform.DOKill();
        transform.DOMove(_startPosition, _animDuration).SetEase(_animEase);
    }

    public void Show()
    {
        transform.DOKill();
        transform.DOMove(_targetPosition, _animDuration).SetEase(_animEase);
    }
}
