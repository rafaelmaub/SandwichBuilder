using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientPlate : MouseHoverExtension
{
    [Header("Visuals")]
    [SerializeField] private int _towerSize;

    [Header("Settings")]
    [SerializeField] private Ingredient _ingredient;

    IngredientObject IngredientObj => _ingredient._prefabMesh;
    public bool _enabled;
    private void Awake()
    {
        StartCoroutine(BuildTowerOfIngredients());
    }

    IEnumerator BuildTowerOfIngredients() //visual effects
    {
        WaitForSecondsRealtime delay = new WaitForSecondsRealtime(1f / _towerSize);
        float yDifference = 0.05f;
        Vector3 spawnPos = Vector3.zero;
        for(int i = 0; i < _towerSize; i++)
        {
            IngredientObject mesh = Instantiate(IngredientObj, transform.GetChild(0));
            mesh.transform.localPosition = spawnPos;
            spawnPos.y += yDifference;
            yield return delay;
        }
    }

    public IngredientObject GetIngredient() //gives the raycast an ingredient object to hold
    {
        if(_enabled)
            return Instantiate(IngredientObj, transform.position, IngredientObj.transform.rotation);
        else
            return null;
    }

    //if mouse over collider, calls the tooltip to its position
    protected override void MouseStartedHover()
    {
        if (!_enabled)
            return;

        base.MouseStartedHover();
        Tooltip3D.Instance.ShowIngredientInfo(_ingredient, transform.position);
    }

    protected override void MouseStoppedHover()
    {
        base.MouseStoppedHover();
        Tooltip3D.Instance.HideTooltip();
    }


}
