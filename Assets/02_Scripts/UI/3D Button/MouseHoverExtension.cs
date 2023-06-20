using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHoverExtension : MonoBehaviour
{
    private void OnMouseEnter()
    {
        MouseStartedHover();
    }

    private void OnMouseExit()
    {
        MouseStoppedHover();
    }


    protected virtual void MouseStartedHover()
    {

    }

    protected virtual void MouseStoppedHover()
    {

    }
}
