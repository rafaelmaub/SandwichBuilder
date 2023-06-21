using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Tooltip3D : Singleton<Tooltip3D>
{
    [SerializeField] private GameObject _child;
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private Vector3 _offsetPos;
    bool _preparingToShow;
    float _timerToShow;
    public void ShowIngredientInfo(Ingredient ing, Vector3 pos)
    {
        _description.text = ing._description;
        _name.text = ing._name;
        _icon.sprite = ing._icon;

        transform.position = pos + _offsetPos;

        _preparingToShow = true;
    }

    private void Update()
    {
        if(_preparingToShow)
        {
            _timerToShow += Time.deltaTime;
            if(_timerToShow >= 1.5f)
            {
                _child.SetActive(true);
            }
        }
    }
    public void HideTooltip()
    {
        _preparingToShow = false;
        _timerToShow = 0;
        _child.SetActive(false);
    }
}
