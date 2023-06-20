using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    [Header("Global References")]
    [SerializeField] private SandwichRequester _sandwichRequester;
    [SerializeField] private IngredientPlate[] _plates;
    [SerializeField] private bool _speedUp;

    [Header("Game Rules")]
    [SerializeField] private float _pointsForCorrectSandwich;
    [SerializeField] private float _pointsForWrongSandwich;
    [SerializeField] private float _pointsForFailingOrder;
    [SerializeField] private float _multiplierSpeedUp;
    public SandwichRequester SandwichRequester => _sandwichRequester;
    public float ScoreSuccess => _pointsForCorrectSandwich;
    public float ScoreFail => _pointsForWrongSandwich;
    public float ScoreLostOrder => _pointsForFailingOrder;

    private void Start()
    {
        //StartMatch();
    }

    public void StartMatch()
    {
        UIController.Instance.Countdown.StartCountdown();
        UIController.Instance.ControlMainMenu(true);
    }

    public void EndMatch()
    {
        TurnColliders(false);
    }

    public void TurnColliders(bool bSet)
    {
        foreach(IngredientPlate plate in _plates)
        {
            plate._enabled = bSet;
        }
    }

    public void SpeedUp(bool speed)
    {
        _speedUp = speed;
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
