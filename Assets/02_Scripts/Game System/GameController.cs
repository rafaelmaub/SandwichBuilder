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

    private void Start()
    {
        UIController.Instance.Countdown.StartCountdown();
    }

    public SandwichOrder IsSandwichCorrect(List<Ingredient> ingredients)
    {
        foreach(SandwichOrder order in SandwichRequester.Orders)
        {
            bool bCorrect = true;

            if (order._sandwich._ingredients.Count != ingredients.Count)
                bCorrect = false;

            foreach (Ingredient ing in ingredients)
            {
                if (!order._sandwich._ingredients.Contains(ing))
                {
                    bCorrect = false;
                }
            }

            foreach (Ingredient ing in order._sandwich._ingredients)
            {
                if (!ingredients.Contains(ing))
                {
                    bCorrect = false;
                }
            }

            if(bCorrect)
                return order;
        }

        return null;


    }

    public void CompleteOrder(SandwichOrder _order)
    {
        //give rewards
        _order.Finish(true);
        
    }
}
