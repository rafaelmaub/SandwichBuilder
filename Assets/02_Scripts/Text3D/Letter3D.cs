using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Letter3D : MonoBehaviour
{
    [SerializeField] private MeshRenderer _mesh;

    public void SetMaterial(Material _mat)
    {
        _mesh.material = _mat;
    }
}
