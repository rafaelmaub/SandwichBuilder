using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SandwichRequester : MonoBehaviour
{
    [SerializeField] private List<Sandwich> _sandwichDatabase = new List<Sandwich>();
    [SerializeField] private Sandwich _currentSandwich;
    [SerializeField] private float _matchTime = 120;

    public Sandwich CurrentSandwich => _currentSandwich;
    [HideInInspector] public UnityEvent OnNewSandwich = new UnityEvent();

    float _matchTimer;
    bool _running;

    public void StartRequesting()
    {
        _running = true;
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
        OnNewSandwich.Invoke();
        
    }
    public void StopRequesting()
    {
        _running = false;

        //show menu
        //stop timer
    }
}
