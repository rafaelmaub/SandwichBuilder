using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOrders : MonoBehaviour
{
    [SerializeField] private UISandwichRequest _requestInfoPrefab;
    [SerializeField] private Transform _targetPosition;
    [SerializeField] private Transform _listParent;

    // Start is called before the first frame update
    void Start()
    {
        GameController.Instance.SandwichRequester.OnNewSandwich.AddListener(NewOrder);
    }

    void NewOrder(SandwichOrder _order)
    {
        Transform link = Instantiate(_targetPosition, _listParent);
        link.gameObject.SetActive(true);
        UISandwichRequest req = Instantiate(_requestInfoPrefab, _listParent);
        
        req.UpdateSandwichUI(_order, link);
    }

    
}
