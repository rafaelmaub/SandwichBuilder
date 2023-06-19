using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientPlate : MonoBehaviour
{
    [Header("Visuals")]
    [SerializeField] private int _towerSize;

    [Header("Settings")]
    [SerializeField] private Ingredient _ingredient;

    IngredientObject IngredientObj => _ingredient._prefabMesh;

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
        return Instantiate(IngredientObj, transform.position, IngredientObj.transform.rotation);
    }



}
