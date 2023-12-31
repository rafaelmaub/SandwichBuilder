using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UISandwichRequest : MonoBehaviour
{
    [SerializeField] private UIIngredientSection _ingredientSectionPrefab;
    [SerializeField] private TextMeshProUGUI _sandwichName;
    [SerializeField] private SmoothLayoutFollower _layoutFollower;
    [SerializeField] private Slider _sliderTimer;
    [SerializeField] private Graphic _timerColor;
    SandwichOrder _myOrder;
    private List<UIIngredientSection> _currentSections = new List<UIIngredientSection>();
    private Transform _link;
    private void Awake()
    {
        ((RectTransform)transform.GetChild(0)).anchoredPosition = new Vector2(0, 400);
        ((RectTransform)transform.GetChild(0)).DOAnchorPos(Vector2.zero, 0.4f).SetDelay(0.4f);
    }

    void UpdateTimer(float percentage)
    {
        if(!_sliderTimer.gameObject.activeInHierarchy)
            _sliderTimer.gameObject.SetActive(true);

        _sliderTimer.value = 1f - percentage;
        _timerColor.color = Color.Lerp(Color.red, Color.green, _sliderTimer.value);
    }
    public void UpdateSandwichUI(SandwichOrder newSand, Transform _targetFollow = null)
    {
        _myOrder = newSand;
        _link = _targetFollow;

        if (_layoutFollower && _link)
        {
            _layoutFollower.CreateLink(_link);
        }

        foreach (UIIngredientSection section in _currentSections)
        {
            Destroy(section.gameObject);
        }
        _myOrder.OnOrderFinished.AddListener(FinishOrder);
        newSand.OnOrderTimeElapsed.AddListener(UpdateTimer);

        _currentSections.Clear();

        foreach(Ingredient ing in newSand.Ingredients)
        {
            UIIngredientSection _newSection = Instantiate(_ingredientSectionPrefab, transform.GetChild(0));
            _newSection.SetupIngredient(ing);
            _currentSections.Add(_newSection);
        }

        _sandwichName.text = newSand._sandwich._name;
    }

    void FinishOrder(bool win)
    {
        if (win) //if game is still running
        {
            //if((GameController.Instance._speedUp && GameController.Instance.SandwichRequester.Orders.Count == 0) || !GameController.Instance._speedUp)
            GameController.Instance.SandwichRequester.NewRequest();
        }

        if (_layoutFollower)
        {
            //_layoutFollower.BreakLink();
        }

        ((RectTransform)transform.GetChild(0)).DOAnchorPos(new Vector2(0, 400), 0.4f).OnComplete(() => 
        {
            Destroy(gameObject);
            if (_layoutFollower)
            {
                _layoutFollower.BreakLink();
            }
        });
    }
    
}
