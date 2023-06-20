using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Word3D : MonoBehaviour
{
    [SerializeField] private Letter3D[] _letters;
    [SerializeField] private Material _glowMaterial;
    [SerializeField] private Material _normalMaterial;
    public void GlowUp()
    {
        foreach(Letter3D letter in _letters)
        {
            letter.SetMaterial(_glowMaterial);
        }
    }

    public void GlowOff()
    {
        foreach (Letter3D letter in _letters)
        {
            letter.SetMaterial(_normalMaterial);
        }
    }
}
