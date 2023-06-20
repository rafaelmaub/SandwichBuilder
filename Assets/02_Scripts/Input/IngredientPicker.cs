using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientPicker : MonoBehaviour
{
    [SerializeField] private float _followHeight;
    [SerializeField] private float _minHeight;

    Rigidbody _rigidBody;
    IngredientObject _currentIngredient;
    Vector3 _targetedPosition;

    private void Awake()
    {

    }

    private void Update()
    {
        if(_currentIngredient)
        {
            _currentIngredient.transform.position = Vector3.LerpUnclamped(_currentIngredient.transform.position, _targetedPosition, Time.deltaTime * 10f);

            if(!_currentIngredient.RigidBody.isKinematic)
                _currentIngredient.RigidBody.isKinematic = true;
        }
        
    }
    public void StartPickIngredient(IngredientPlate plate)
    {
        _currentIngredient = plate.GetIngredient();
        if(_currentIngredient)
            _currentIngredient.transform.position += new Vector3(0, _followHeight, 0);
        //_minHeight = _currentIngredient.transform.position.y;
        
    }

    public void MoveIngredient(Vector3 targetedPosition)
    {
        //_targetedPosition = targetedPosition;
        //Debug.Log(targetedPosition);
        _targetedPosition.x = targetedPosition.x;
        _targetedPosition.z = targetedPosition.z;
        _targetedPosition.y = Mathf.Clamp(targetedPosition.y, _minHeight, 2f);
    }

    public void DropIngredient()
    {
        if(_currentIngredient)
        {
            _currentIngredient.Drop();
            _currentIngredient = null;
        }
    }

    public void PlaceOnBoard(SandwichBoard board)
    {
        if(_currentIngredient)
        {
            board.PlaceIngredient(_currentIngredient);
            _currentIngredient = null;
        }
        
    }
}
