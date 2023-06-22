using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothLayoutFollower : MonoBehaviour
{
    [SerializeField] private float _followSpeed;
    Transform link;
    public void CreateLink(Transform t)
    {
        link = t;
        ((RectTransform)transform).anchoredPosition = ((RectTransform)link.transform).anchoredPosition;
    }

    public void BreakLink()
    {
        
        Destroy(link.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(link)
        ((RectTransform)transform).anchoredPosition = Vector2.Lerp(
            ((RectTransform)transform).anchoredPosition, 
            ((RectTransform)link.transform).anchoredPosition, 
            Time.deltaTime * _followSpeed);
    }
}
