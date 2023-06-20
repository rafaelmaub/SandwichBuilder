using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Toggle3D : MouseHoverExtension
{
    bool _value;
    public UnityEvent<bool> OnToggleChanged = new UnityEvent<bool>();
    [SerializeField] private Word3D _word;
    [SerializeField] private MeshRenderer _mesh;
    [SerializeField] private Material _onMaterial;
    [SerializeField] private Material _offMaterial;

    private void OnMouseDown()
    {
        _value = !_value;
        OnToggleChanged.Invoke(_value);

        if (_value)
            _mesh.material = _onMaterial;
        else
            _mesh.material = _offMaterial;
    }

    protected override void MouseStartedHover()
    {
        base.MouseStartedHover();
        _word.GlowUp();
    }
    protected override void MouseStoppedHover()
    {
        base.MouseStoppedHover();
        _word.GlowOff();
    }

}
