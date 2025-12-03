using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenFlash : MonoBehaviour
{

    public Material mat;
    private Coroutine screenFlashTask;
    public static screenFlash instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public void ScreenFlashEffect(float intensity)
    {
        if(screenFlashTask != null)
        {
            StopCoroutine(screenFlashTask);
        }
        screenFlashTask = StartCoroutine(screenFlashDetail(intensity));
    }

    // void Update()
    // {
    //     if(Input.GetKeyDown(KeyCode.F))
    //     {
    //         Debug.Log("Flash Screen");
    //         ScreenFlashEffect(0.1f);
    //     }
    // }


    private IEnumerator screenFlashDetail (float intensity)
    {
        var targetRadius = Remap(intensity, 0f, 1f, 0.4f, -0.1f);
        float curRadius = 1;

        for(float t = 0; curRadius != targetRadius; t += Time.deltaTime*20f)
        {
            curRadius = Mathf.Lerp(1, targetRadius, t);
            mat.SetFloat("_Vradius", curRadius);
            yield return null;
        }
        for(float t = 0; curRadius < 1; t += Time.deltaTime*1.2f)
        {
            curRadius = Mathf.Lerp(targetRadius, 1, t);
            mat.SetFloat("_Vradius", curRadius);
            yield return null;
        }
    }

    private float Remap(float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        return Mathf.Lerp(toMin, toMax, Mathf.InverseLerp(fromMin, fromMax, value));
    }

    public static class SpecialEffects
    {
        public static void ScreenFlashEffect(float intensity)=>
            instance.ScreenFlashEffect(intensity);
    }
}
