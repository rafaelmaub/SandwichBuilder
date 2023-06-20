using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOrders : MonoBehaviour
{
    [SerializeField] private UISandwichRequest _requestInfoPrefab;
    [SerializeField] private Transform _listParent;

    // Start is called before the first frame update
    void Start()
    {
        GameController.Instance.SandwichRequester.OnNewSandwich.AddListener(NewOrder);
    }

    void NewOrder(SandwichOrder _order)
    {
        Instantiate(_requestInfoPrefab, _listParent).UpdateSandwichUI(_order);
    }

    
}
