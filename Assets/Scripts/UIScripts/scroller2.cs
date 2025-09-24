using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scroller2 : MonoBehaviour
{
     public Vector2 scrollSpeed = new Vector2(0.5f, 0f);
    private RawImage rawImage;
    private Vector2 uvOffset;

    void Awake()
    {
        rawImage = GetComponent<RawImage>();
    }

    void Update()
    {
        uvOffset += scrollSpeed * Time.deltaTime;
        
        Rect r = rawImage.uvRect;
        rawImage.uvRect = new Rect(uvOffset, r.size);
    }
}
