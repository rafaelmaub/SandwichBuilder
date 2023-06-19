using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IngredientObject : MonoBehaviour
{
    [SerializeField] private Ingredient _currentIngredient;
    [SerializeField] private Rigidbody _rigidBody;

    public Ingredient Ingredient => _currentIngredient;
    public Rigidbody RigidBody => _rigidBody;
    private void Awake()
    {
        _rigidBody.isKinematic = true;
        Vector3 scale = transform.localScale;
        transform.localScale = Vector3.zero;

        transform.DOScale(scale, 0.2f).SetEase(Ease.InOutBounce).OnComplete(() => _rigidBody.isKinematic = false);

        //Spawn Particles
    }

    public void Drop()
    {
        transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.InOutBounce).OnComplete(() => Destroy(gameObject, 0.2f));
    }

}
