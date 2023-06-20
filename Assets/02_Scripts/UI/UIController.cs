using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : Singleton<UIController>
{
    [SerializeField] private UICountdown _countdown;

    public UICountdown Countdown => _countdown;

}
