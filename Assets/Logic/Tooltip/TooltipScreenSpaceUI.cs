using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NaughtyAttributes;
using System;
using Unity.VisualScripting;

public class TooltipScreenSpaceUI : MonoBehaviour
{
    public static TooltipScreenSpaceUI instance;
    [HideInInspector]public GameObject targetObject;
    [SerializeField] public GameObject downTriangle;
    [SerializeField] public GameObject upTriangle;

    [SerializeField] private RectTransform canvasRectTransform;
 
    [SerializeField]private RectTransform backgroundRectTransform;
    private RectTransform rectTransform;
   [SerializeField] private TextMeshProUGUI textMeshPro;

    [HideInInspector]public float textPaddingXSize;
    [HideInInspector]public float textPaddingYSize;

    private void Awake()
    {
        instance = this;

        backgroundRectTransform = transform.Find("TooltipBackground").GetComponent<RectTransform>();
        rectTransform = transform.GetComponent<RectTransform>();
    }

    private void Start()
    {
        HideTooltip();
    }

    private void Update()
    {
        Vector2 anchoredPosition = targetObject.transform.position / canvasRectTransform.localScale.x;
        anchoredPosition.y += 50;
        
        downTriangle.SetActive(true);
        upTriangle.SetActive(false);

        if (anchoredPosition.x + backgroundRectTransform.rect.width > canvasRectTransform.rect.width)
        {
            anchoredPosition.x = canvasRectTransform.rect.width ;
        }
        if (anchoredPosition.y + backgroundRectTransform.rect.height > canvasRectTransform.rect.height)
        {
            anchoredPosition.y = (targetObject.transform.position.y / canvasRectTransform.localScale.y - 60);
            downTriangle.SetActive(false);
            upTriangle.SetActive(true);
        } 

        rectTransform.anchoredPosition = anchoredPosition;
    }

    private void SetText(string tooltipText)
    {   
        textMeshPro.SetText(tooltipText);
        textMeshPro.ForceMeshUpdate();

        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 paddingSize = new Vector2(textPaddingXSize, textPaddingYSize);
        Vector2 exitButtonSize = new Vector2(0f, 50f);

        //backgroundRectTransform.sizeDelta = textSize + paddingSize + exitButtonSize;
    }

    public void ShowTooltip(string tooltipText)
    {
        HideTooltip();
        gameObject.SetActive(true);
        SetText(tooltipText);
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }

}
