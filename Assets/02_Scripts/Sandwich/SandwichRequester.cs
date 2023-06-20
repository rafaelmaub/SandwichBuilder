using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
public class SandwichRequester : MonoBehaviour
{
    
    [SerializeField] private List<Sandwich> _sandwichDatabase = new List<Sandwich>();
    [SerializeField] private List<SandwichOrder> _orders = new List<SandwichOrder>();
    [SerializeField] private SandwichOrder _firstOrder;
    [SerializeField] private float _matchTime = 120;

    [HideInInspector] public UnityEvent<SandwichOrder> OnNewSandwich = new UnityEvent<SandwichOrder>();
    [HideInInspector] public UnityEvent<float> OnTimeElapsed = new UnityEvent<float>();
    [HideInInspector] public UnityEvent OnMatchEnded = new UnityEvent();

    public List<SandwichOrder> Orders => _orders.Where(x => x._running).ToList();
    float _matchTimer;
    public bool _running { get; private set; }

    private void Start()
    {
        UIController.Instance.Countdown.OnFinishCountdown.AddListener(StartRequesting);
    }
    public void StartRequesting()
    {
        _matchTimer = 0;
        _running = true;
        _orders = new List<SandwichOrder>();
        NewRequest();
    }

    private void Update()
    {
        if(_running)
        {
            _matchTimer += Time.deltaTime;
            OnTimeElapsed.Invoke(_matchTime - _matchTimer);

            if (_matchTimer >= _matchTime)
            {
                StopRequesting();
            }
        }

        if(GameController.Instance._speedUp)
        {
            foreach (SandwichOrder order in Orders)
            {
                order.EvaluateTime();
            }
        }

    }

    public void NewRequest()
    {
        //_orders.Add()
        SandwichOrder order = new SandwichOrder(_sandwichDatabase[Random.Range(0, _sandwichDatabase.Count)], Random.Range(15f, 20f));
        order.OnOrderFinished.AddListener(ClearList);
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
        //show menu
        //stop timer
    }

    void ClearList(bool finished)
    {
        //List<SandwichOrder> toRemove = new List<SandwichOrder>();
        //foreach(SandwichOrder _oldOrder in Orders.Where(x => !x._running))
        //{
        //    Orders.Remove(_oldOrder);
        //}
        
    }
}

[System.Serializable]
public class SandwichOrder
{
    public Sandwich _sandwich;
    public float _time;
    public bool _running { get; private set; }
    private float _currentTimer;

    public UnityEvent<bool> OnOrderFinished = new UnityEvent<bool>();

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
        }

    }

    public SandwichOrder(Sandwich sand, float time)
    {
        _sandwich = sand;
        _time = time;
        _currentTimer = 0;
        _running = true;
    }
}
