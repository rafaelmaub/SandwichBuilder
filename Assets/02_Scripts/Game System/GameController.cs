using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    [Header("Global References")]
    [SerializeField] private SandwichRequester _sandwichRequester;

    [Header("Game Rules")]
    [SerializeField] private float _pointsForCorrect;
    [SerializeField] private float _pointsForError;
    public SandwichRequester SandwichRequester => _sandwichRequester;
    public float ScoreSuccess => _pointsForCorrect;
    public float ScoreFail => _pointsForError;

    public bool IsSandwichCorrect(List<Ingredient> ingredients)
    {
        bool bCorrect = true;

        if (SandwichRequester.CurrentSandwich._ingredients.Count != ingredients.Count)
            bCorrect = false;

        foreach(Ingredient ing in ingredients)
        {
            if(!SandwichRequester.CurrentSandwich._ingredients.Contains(ing))
            {
                bCorrect = false;
            }
        }

        foreach (Ingredient ing in SandwichRequester.CurrentSandwich._ingredients)
        {
            if (!ingredients.Contains(ing))
            {
                bCorrect = false;
            }
        }

        return bCorrect;

    }
}
