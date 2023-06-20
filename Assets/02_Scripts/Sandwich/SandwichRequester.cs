using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SandwichRequester : MonoBehaviour
{
    
    [SerializeField] private List<Sandwich> _sandwichDatabase = new List<Sandwich>();
    [SerializeField] private List<SandwichOrder> _orders = new List<SandwichOrder>();
    [SerializeField] private SandwichOrder _firstOrder;
    [SerializeField] private float _matchTime = 120;

    [HideInInspector] public UnityEvent<SandwichOrder> OnNewSandwich = new UnityEvent<SandwichOrder>();

    public List<SandwichOrder> Orders => _orders;
    float _matchTimer;
    bool _running;

    private void Start()
    {
        UIController.Instance.Countdown.OnFinishCountdown.AddListener(StartRequesting);
    }
    public void StartRequesting()
    {
        _running = true;
        NewRequest();

        //hide menu
        //start countdown
    }

    private void Update()
    {
        if(_running)
        {
            _matchTimer += Time.deltaTime;
            if (_matchTimer >= _matchTime)
            {
                StopRequesting();
            }
        }

        if(false) //IF TIMED MODE
        {
            foreach (SandwichOrder order in _orders)
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

        //show menu
        //stop timer
    }

    void ClearList(bool finished)
    {
        List<SandwichOrder> toRemove = new List<SandwichOrder>();

        foreach(SandwichOrder order in _orders)
        {
            if(!order._running)
            {
                toRemove.Add(order);
            }
        }

        foreach(SandwichOrder r in toRemove)
        {
            Orders.Remove(r);
        }
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
        _currentTimer += Time.deltaTime;
        if(_currentTimer >= _time)
        {
            _running = false;
            Finish(false);
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
