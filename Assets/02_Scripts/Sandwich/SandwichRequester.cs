using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SandwichRequester : MonoBehaviour
{
    [SerializeField] private List<Sandwich> _sandwichDatabase = new List<Sandwich>();
    [SerializeField] private List<Sandwich> _orders = new List<Sandwich>();
    [SerializeField] private Sandwich _currentSandwich;
    [SerializeField] private float _matchTime = 120;

    public Sandwich CurrentSandwich => _currentSandwich;
    [HideInInspector] public UnityEvent<Sandwich> OnNewSandwich = new UnityEvent<Sandwich>();

    float _matchTimer;
    bool _running;

    private void Start()
    {
        StartRequesting();
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
    }

    public void NewRequest()
    {
        _currentSandwich = _sandwichDatabase[Random.Range(0, _sandwichDatabase.Count)];
        OnNewSandwich.Invoke(_currentSandwich);
        
    }
    public void StopRequesting()
    {
        _running = false;

        //show menu
        //stop timer
    }
}
