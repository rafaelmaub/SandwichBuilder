using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
public class SandwichRequester : MonoBehaviour
{
    
    [SerializeField] private List<Sandwich> _sandwichDatabase = new List<Sandwich>();
    [SerializeField] private List<Ingredient> _ingredientDatabase = new List<Ingredient>();
    [SerializeField] private List<SandwichOrder> _orders = new List<SandwichOrder>();
    [SerializeField] private float _matchTime = 120;
    private float _currentTimerToOrderAgain;
    private float _timeToNewOrder;
    [HideInInspector] public UnityEvent<SandwichOrder> OnNewSandwich = new UnityEvent<SandwichOrder>();
    [HideInInspector] public UnityEvent<float> OnTimeElapsed = new UnityEvent<float>();
    [HideInInspector] public UnityEvent OnMatchEnded = new UnityEvent();

    public List<SandwichOrder> Orders => _orders.Where(x => x._running).ToList();
    public List<Ingredient> IngredientDatabase => _ingredientDatabase;
    float _matchTimer;
    public bool _running { get; private set; }

    private void Start()
    {
        UIController.Instance.Countdown.OnFinishCountdown.AddListener(StartRequesting);

    }
    public void StartRequesting()
    {
        ResetOrderTimer();
        _matchTimer = 0;
        _running = true;
        _orders = new List<SandwichOrder>();
        NewRequest();

    }

    private void Update()
    {
        if(_running)
        {

            if (GameController.Instance._speedUp)
            {
                foreach (SandwichOrder order in Orders)
                {
                    order.EvaluateTime();
                }

                _currentTimerToOrderAgain += Time.deltaTime;
                if (_currentTimerToOrderAgain >= _timeToNewOrder)
                {
                    NewRequest();
                    ResetOrderTimer();
                }
                else if (Orders.Count == 0)
                {
                    NewRequest();
                }


            }

            _matchTimer += Time.deltaTime;
            OnTimeElapsed.Invoke(_matchTime - _matchTimer);

            if (_matchTimer >= _matchTime)
            {
                StopRequesting();
            }
        }


    }

    void ResetOrderTimer()
    {
        _currentTimerToOrderAgain = 0;
        _timeToNewOrder = Random.Range(10f, 20f);
    }
    public void NewRequest()
    {
        SandwichOrder order = new SandwichOrder(_sandwichDatabase[Random.Range(0, _sandwichDatabase.Count)], Random.Range(7f, 10f));
        _orders.Add(order);
        OnNewSandwich.Invoke(order);
        
    }
    public void StopRequesting()
    {  
        _running = false;
        foreach(SandwichOrder order in Orders)
        {
            order.Finish(false);
        }

        UIController.Instance.ShowResults();
        OnMatchEnded.Invoke();
    }
}

[System.Serializable]
public class SandwichOrder
{
    public Sandwich _sandwich;
    public List<Ingredient> Ingredients => _sandwich._RANDOM ? _overrideIngredients : _sandwich._ingredients;
    public float _time;
    public bool _running { get; private set; }
    private float _currentTimer;
    private List<Ingredient> _overrideIngredients = new List<Ingredient>();

    public UnityEvent<bool> OnOrderFinished = new UnityEvent<bool>();
    public UnityEvent<float> OnOrderTimeElapsed = new UnityEvent<float>();

    public void Finish(bool win)
    {
        _running = false;
        OnOrderFinished.Invoke(win);
        //Give Rewards?
    }
    public void EvaluateTime()
    {
        if(_running)
        {
            _currentTimer += Time.deltaTime;
            if (_currentTimer >= _time)
            {
                GameController.Instance.ScoreSystem.ChangeCurrentScore(GameController.Instance.ScoreLostOrder);
                Finish(false);
            }
            OnOrderTimeElapsed.Invoke(_currentTimer / _time);
        }

    }

    public SandwichOrder(Sandwich sand, float time)
    {
        _sandwich = sand;
        _time = time;
        _currentTimer = 0;
        _running = true;

        if(sand._RANDOM)
        {
            _overrideIngredients.AddRange(sand._ingredients);
            for (int i = 0; i < _overrideIngredients.Count; i++)
            {
                if (_overrideIngredients[i]._name == "Random")
                {
                    _overrideIngredients[i] = GameController.Instance.SandwichRequester.IngredientDatabase[Random.Range(0, GameController.Instance.SandwichRequester.IngredientDatabase.Count)];
                }
            }

        }
    }
}
