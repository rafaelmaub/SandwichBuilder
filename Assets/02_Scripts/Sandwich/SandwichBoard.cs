using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SandwichBoard : MonoBehaviour
{
    [SerializeField] private List<Ingredient> _currentSandwich = new List<Ingredient>();
    
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

        AddIngredient(ing.Ingredient);
    }
    public void AddIngredient(Ingredient ing)
    {


        _currentSandwich.Add(ing);
        //Instantiate ingredient above
        if (_currentSandwich.Count > 1 && ing._type == IngredientType.Bread)
        {
            //Finish sandwich
        }



    }
}
