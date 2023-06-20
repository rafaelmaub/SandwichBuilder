using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    [SerializeField] private SandwichRequester _sandwichRequester;
    public SandwichRequester SandwichRequester => _sandwichRequester;
}
