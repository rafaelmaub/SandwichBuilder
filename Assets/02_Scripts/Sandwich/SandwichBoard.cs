using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandwichBoard : MonoBehaviour
{
    [SerializeField] private List<Ingredient> _currentSandwich = new List<Ingredient>();

    public void AddIngredient(IngredientObject ing)
    {
        if(_currentSandwich.Count == 0 && ing.Ingredient._type != IngredientType.Bread)
        {
            //Ignore
            return;
        }

        _currentSandwich.Add(ing.Ingredient);
        //Instantiate ingredient above
        if (_currentSandwich.Count > 1 && ing.Ingredient._type == IngredientType.Bread)
        {
            //Finish sandwich
        }



    }
}
