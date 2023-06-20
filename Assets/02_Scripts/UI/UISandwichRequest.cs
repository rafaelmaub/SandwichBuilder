using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UISandwichRequest : MonoBehaviour
{
    [SerializeField] private UIIngredientSection _ingredientSectionPrefab;
    [SerializeField] private TextMeshProUGUI _sandwichName;
    

    private List<UIIngredientSection> _currentSections = new List<UIIngredientSection>();
    void Start()
    {
        GameController.Instance.SandwichRequester.OnNewSandwich.AddListener(UpdateSandwichUI);
    }

    void UpdateSandwichUI(Sandwich newSand)
    {
        foreach(UIIngredientSection section in _currentSections)
        {
            Destroy(section.gameObject);
        }

        _currentSections.Clear();

        foreach(Ingredient ing in newSand._ingredients)
        {
            UIIngredientSection _newSection = Instantiate(_ingredientSectionPrefab, transform);
            _newSection.SetupIngredient(ing);
            _currentSections.Add(_newSection);
        }

        _sandwichName.text = newSand._name;
    }
}
