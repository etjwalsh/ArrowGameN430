using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIPanDrag : MonoBehaviour
{
    private RectTransform rectTransform;
    private Vector2 lastMousePos;
    private bool dragging;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            dragging = true;
            lastMousePos = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(1))
        {
            dragging = false;
        }

        if (dragging)
        {
            Vector2 mousePos = Input.mousePosition;
            Vector2 delta = mousePos - lastMousePos;

            rectTransform.anchoredPosition += delta;

            lastMousePos = mousePos;
        }
        
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            float zoom = Mathf.Clamp(rectTransform.localScale.x + scroll, 0.5f, 3f);
            rectTransform.localScale = Vector3.one * zoom;
        }

    }
}
