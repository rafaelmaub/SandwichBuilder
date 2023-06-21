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

    private void Start()
    {
        GameController.Instance.SandwichRequester.OnMatchEnded.AddListener(FinishSandwich);
    }
    public void PlaceIngredient(IngredientObject ing) //puts an ingredient as child object of a "current sandwich" object
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
    public void AddIngredient(Ingredient ing) //add the ingredient data to the list
    {
        _currentSandwich.Add(ing);
        //Instantiate ingredient above
        if (_currentSandwich.Count > 1 && ing._type == IngredientType.Bread)
        {
            FinishSandwich();
        }
    }

    void FinishSandwich() //sends sandwich to right or wrong position with animation
    {
        //check if its correct or not
        Transform target = _deliveryPoint;
        SandwichOrder success = GameController.Instance.IsSandwichCorrect(_currentSandwich);

        if (success == null)
        {
            target = _trashPoint;
        }
        _currentSandwich.Clear();

        if (!_sandwichParent)
        {
            return;
        }
        GameObject copy = _sandwichParent;
        _sandwichParent = null;
        foreach(Transform t in copy.transform)
        {
            t.gameObject.layer = 3;
        }
        copy.transform.DOMove(target.position, 1f).SetDelay(0.15f).SetEase(Ease.InOutElastic).OnComplete(() =>
        {
            if(success != null)
            {
                GameController.Instance.CompleteOrder(success);
            }
            
            Destroy(copy);
        });
    }
}
