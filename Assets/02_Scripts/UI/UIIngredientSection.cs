using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIIngredientSection : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _ingredientName;
    [SerializeField] private Image _ingredientIcon;

    public void SetupIngredient(Ingredient ing)
    {
        _ingredientIcon.sprite = ing._icon;
        _ingredientName.text = ing._name;
    }
}
