using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button3D : MouseHoverExtension
{
    public UnityEvent OnButtonClick = new UnityEvent();
    [SerializeField] private Word3D _word;

    private void Awake()
    {
        MouseStoppedHover();
    }

    private void OnMouseDown()
    {
        OnButtonClick.Invoke();
    }

    protected override void MouseStartedHover()
    {
        base.MouseStartedHover();
        Debug.Log("TESTE");
        _word.GlowUp();
    }
    protected override void MouseStoppedHover()
    {
        base.MouseStoppedHover();
        _word.GlowOff();
    }

}
