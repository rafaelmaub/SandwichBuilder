using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReader : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private IngredientPicker _picker;

    Camera _playerCam => _playerInput.camera;

    SandwichBuilder _inputAction;
    [SerializeField] Vector2 _aimPos;
    void Awake()
    {
        _inputAction = new SandwichBuilder();

        _inputAction.Gameplay.AimPosition.performed += AimPosition_performed;

        _inputAction.Gameplay.DragDrop.started += DragDrop_performed;
        _inputAction.Gameplay.DragDrop.canceled += DragDrop_canceled;

        _playerInput.actions = _inputAction.asset;
    }

    private void AimPosition_performed(InputAction.CallbackContext obj)
    {
        _aimPos = obj.ReadValue<Vector2>();
        _picker.MoveIngredient(_playerCam.ScreenToWorldPoint(new Vector3(_aimPos.x, _aimPos.y, 3f)));
    }


    private void DragDrop_canceled(InputAction.CallbackContext obj)
    {
        //_picker.StartPickIngredient();
        //CHECK IF THERE IS BOARD
        //IF THERE IS BOARD, SEND TO PICKER
        //IF NOT
        _picker.DropIngredient();
    }

    private void DragDrop_performed(InputAction.CallbackContext obj)
    {
        //_picker.MoveIngredient();
        RaycastHit hit;
        Ray ray = _playerCam.ScreenPointToRay(_aimPos);
        if(Physics.Raycast(ray, out hit, 1000f))
        {
            IngredientPlate plate = hit.collider.GetComponent<IngredientPlate>();
            if(plate)
            {
                _picker.StartPickIngredient(plate);
            }
            
        }
    }
}
