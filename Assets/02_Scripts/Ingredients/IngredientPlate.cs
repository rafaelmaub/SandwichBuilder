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

    IEnumerator BuildTowerOfIngredients()
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

    public IngredientObject GetIngredient()
    {
        if(_enabled)
            return Instantiate(IngredientObj, transform.position, IngredientObj.transform.rotation);
        else
            return null;
    }

    protected override void MouseStartedHover()
    {
        base.MouseStartedHover();
        Tooltip3D.Instance.ShowIngredientInfo(_ingredient, transform.position);
    }

    protected override void MouseStoppedHover()
    {
        base.MouseStoppedHover();
        Tooltip3D.Instance.HideTooltip();
    }


}
