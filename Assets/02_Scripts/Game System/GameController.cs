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
    public float ScoreSuccess => _speedUp ? _pointsForCorrectSandwich * _multiplierSpeedUp : _pointsForCorrectSandwich;
    public float ScoreFail => _speedUp ? _pointsForWrongSandwich * _multiplierSpeedUp : _pointsForWrongSandwich;
    public float ScoreLostOrder => _speedUp ? _pointsForFailingOrder * _multiplierSpeedUp : _pointsForFailingOrder;

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

    public void TurnColliders(bool bSet) //raycast purposes
    {
        foreach(IngredientPlate plate in _plates)
        {
            plate._enabled = bSet;
        }
    }

    public void SpeedUp(bool speed) //being called from main  menu, setting difficulty of game
    {
        _speedUp = speed;
    }
    public SandwichOrder IsSandwichCorrect(List<Ingredient> ingredients) //compare list of ingredients of current sandwich and active orders
    {
        foreach(SandwichOrder order in SandwichRequester.Orders)
        {
            bool bCorrect = true;

            if (order.Ingredients.Count != ingredients.Count)
                bCorrect = false;

            foreach (Ingredient ing in ingredients)
            {
                if (!order.Ingredients.Contains(ing))
                {
                    bCorrect = false;
                }
            }

            foreach (Ingredient ing in order.Ingredients)
            {
                if (!ingredients.Contains(ing))
                {
                    bCorrect = false;
                }
            }

            if(bCorrect)
            {
                ScoreSystem.ChangeCurrentScore(ScoreSuccess);
                SoundSystem.Instance.Play_CorrectSandwich();
                return order;
            }
                
        }

        if(MatchRunning)
        {
            ScoreSystem.ChangeCurrentScore(ScoreFail);
            SoundSystem.Instance.Play_WrongSandwich();
        }
            

        return null;


    }

    public void CompleteOrder(SandwichOrder _order)
    {
        _order.Finish(true);
        //ScoreSystem.ChangeCurrentScore(ScoreSuccess);
        
        
    }
}
