using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    [Header("Global References")]
    [SerializeField] private SandwichRequester _sandwichRequester;
    [SerializeField] private GameScore _scoreSystem;
    [SerializeField] private IngredientPlate[] _plates;
    public bool _speedUp { get; private set; }

    [Header("Game Rules")]
    [SerializeField] private float _pointsForCorrectSandwich;
    [SerializeField] private float _pointsForWrongSandwich;
    [SerializeField] private float _pointsForFailingOrder;
    [SerializeField] private float _multiplierSpeedUp;
    public SandwichRequester SandwichRequester => _sandwichRequester;
    public GameScore ScoreSystem => _scoreSystem;
    public float ScoreSuccess => _pointsForCorrectSandwich;
    public float ScoreFail => _pointsForWrongSandwich;
    public float ScoreLostOrder => _pointsForFailingOrder;

    public bool MatchRunning => SandwichRequester._running;
    private void Start()
    {
        //StartMatch();
        SandwichRequester.OnMatchEnded.AddListener(EndMatch);
    }

    public void StartMatch()
    {
        ScoreSystem.StartCountingScore();
        UIController.Instance.Countdown.StartCountdown();
        UIController.Instance.ControlMainMenu(true);
    }

    void EndMatch()
    {
        ScoreSystem.FinishCountingScore();
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
            {
                ScoreSystem.ChangeCurrentScore(ScoreSuccess);
                return order;
            }
                
        }

        if(MatchRunning)
            ScoreSystem.ChangeCurrentScore(ScoreFail);

        return null;


    }

    public void CompleteOrder(SandwichOrder _order)
    {
        _order.Finish(true);
        //ScoreSystem.ChangeCurrentScore(ScoreSuccess);
        
        
    }
}
