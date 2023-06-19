using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sandwich", menuName = "Sandwich")]
public class Sandwich : ScriptableObject
{
    public bool _RANDOM;
    public string _name;
    public float _time;
    public Ingredient[] _ingredients;
}
