using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class BallOverlay : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;
    private RectTransform rect;
    public CanvasScaler canvas;

    
    private void Awake()
    {
        rect = gameObject.GetComponent<RectTransform>();
    }

    private void Start()
    {
        
    }
    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(target.transform.position+new Vector3(0,1,0));
        screenPosition.z = 0;
        rect.anchoredPosition = new Vector2(screenPosition.x / canvas.scaleFactor, screenPosition.y / canvas.scaleFactor );
        //rect.anchoredPosition= screenPosition * getScaleFactor();

    }

    private float getScaleFactor()
    {
        if (canvas == null)
        {
            return 1;
        }

        if (canvas.uiScaleMode == CanvasScaler.ScaleMode.ConstantPixelSize)
        {
            return 1f / canvas.scaleFactor;
        }

        if (canvas.uiScaleMode == CanvasScaler.ScaleMode.ScaleWithScreenSize)
        {
            return canvas.referenceResolution.x / Screen.width;
        }

        return 1;
    }
}
