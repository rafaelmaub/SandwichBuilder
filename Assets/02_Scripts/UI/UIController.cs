using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script allows global access to more important UIs and commands
public class UIController : Singleton<UIController>
{
    [SerializeField] private UICountdown _countdown;
    [SerializeField] private UIMatchTimer _matchTimer;
    [SerializeField] private UI3DMenu _mainMenu;

    public UICountdown Countdown => _countdown;
    public UIMatchTimer MatchTimer => _matchTimer;

    private void Start()
    {
        ControlMainMenu(false);
    }
    public void ShowResults()
    {
        MatchTimer.gameObject.SetActive(false);
        ControlMainMenu(false);
    }

    public void ShowMainGameplayHud()
    {
        MatchTimer.gameObject.SetActive(true);
    }

    public void ControlMainMenu(bool hide)
    {
        if (hide)
            _mainMenu.Hide();
        else
            _mainMenu.Show();
    }

}
