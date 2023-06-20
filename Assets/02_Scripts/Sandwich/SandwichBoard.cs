using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SandwichBoard : MonoBehaviour
{
    [SerializeField] private List<Ingredient> _currentSandwich = new List<Ingredient>();
    [SerializeField] private Transform _deliveryPoint;
    [SerializeField] private Transform _trashPoint;
    private GameObject _sandwichParent;
    public void PlaceIngredient(IngredientObject ing)
    {
        if (_currentSandwich.Count == 0 && ing.Ingredient._type != IngredientType.Bread)
        {
            ing.Drop();
            return;
        }

        ing.transform.DOMove(transform.position + (Vector3.up * 0.4f), 0.12f).OnComplete(() =>
        {
            ing.RigidBody.isKinematic = false;
        });

        if (_sandwichParent == null)
        {
            _sandwichParent = new GameObject("Sandwich");
            _sandwichParent.transform.SetParent(transform);
            _sandwichParent.transform.position = Vector3.zero;
        }

        ing.transform.SetParent(_sandwichParent.transform);
        AddIngredient(ing.Ingredient);
    }
    public void AddIngredient(Ingredient ing)
    {
        _currentSandwich.Add(ing);
        //Instantiate ingredient above
        if (_currentSandwich.Count > 1 && ing._type == IngredientType.Bread)
        {
            FinishSandwich();
        }
    }

    void FinishSandwich()
    {
        //check if its correct or not
        Transform target = _deliveryPoint;
        float score = GameController.Instance.ScoreSuccess;
        bool success = GameController.Instance.IsSandwichCorrect(_currentSandwich);

        if (!success)
        {
            target = _trashPoint;
            score = GameController.Instance.ScoreFail;

        }

        _sandwichParent.transform.DOMove(target.position, 1f).SetDelay(0.15f).SetEase(Ease.InOutElastic).OnComplete(() =>
        {
            Destroy(_sandwichParent);
            _currentSandwich.Clear();
            if(success)
            {
                GameController.Instance.SandwichRequester.NewRequest();
            }
        });
    }
}
