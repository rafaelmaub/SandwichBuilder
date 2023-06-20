using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class UISandwichRequest : MonoBehaviour
{
    [SerializeField] private UIIngredientSection _ingredientSectionPrefab;
    [SerializeField] private TextMeshProUGUI _sandwichName;

    SandwichOrder _myOrder;
    private List<UIIngredientSection> _currentSections = new List<UIIngredientSection>();

    private void Awake()
    {
        ((RectTransform)transform.GetChild(0)).anchoredPosition = new Vector2(0, 400);
        ((RectTransform)transform.GetChild(0)).DOAnchorPos(Vector2.zero, 0.4f).SetDelay(0.4f);
    }
    public void UpdateSandwichUI(SandwichOrder newSand)
    {
        _myOrder = newSand;
        foreach (UIIngredientSection section in _currentSections)
        {
            Destroy(section.gameObject);
        }
        _myOrder.OnOrderFinished.AddListener(FinishOrder);

        _currentSections.Clear();

        foreach(Ingredient ing in newSand._sandwich._ingredients)
        {
            UIIngredientSection _newSection = Instantiate(_ingredientSectionPrefab, transform.GetChild(0));
            _newSection.SetupIngredient(ing);
            _currentSections.Add(_newSection);
        }

        _sandwichName.text = newSand._sandwich._name;
    }

    void FinishOrder(bool win)
    {
        if (win && !GameController.Instance._speedUp) //if game is still running
        {
            GameController.Instance.SandwichRequester.NewRequest();
            
        }

        ((RectTransform)transform.GetChild(0)).DOAnchorPos(new Vector2(0, 400), 0.4f).OnComplete(() => Destroy(gameObject));
    }
    
}
