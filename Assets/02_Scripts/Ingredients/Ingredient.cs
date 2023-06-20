using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ingredient", menuName = "Ingredients")]
public class Ingredient : ScriptableObject
{
    public string _name;
    public string _description;
    public Sprite _icon;
    public IngredientObject _prefabMesh;
    public IngredientType _type;
}

public enum IngredientType
{
    Additional, Bread
}
