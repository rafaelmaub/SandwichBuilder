using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class UITutorial : MonoBehaviour
{
    [SerializeField] private Collider[] _toDisable;
    [SerializeField] private CanvasGroup _alpha;
    public void OpenTutorial()
    {
        transform.DOScale(Vector3.one, 0.6f).SetEase(Ease.OutBack);
        _alpha.DOFade(1f, 0.6f);
        foreach (Collider col in _toDisable)
        {
            col.enabled = false;
        }
    }
    public void CloseTutorial()
    {
        transform.DOScale(Vector3.zero, 0.6f).SetEase(Ease.InBack);
        _alpha.DOFade(0f, 0.6f);
        foreach (Collider col in _toDisable)
        {
            col.enabled = true;
        }
    }
}
