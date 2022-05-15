using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    [SerializeField]float lowPixel;
    [SerializeField]float highPixel;
    [SerializeField]float targetScale;
    [SerializeField] PixelatedCamera pixelatedCamera;
    [SerializeField]float fluxSpeed;
    bool shouldFlux = true;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (targetScale == pixelatedCamera.screenScaleFactor)
        {
            targetScale = Random.Range(lowPixel, highPixel);
        }
        else
        {
            if (shouldFlux)
            {
                StartCoroutine(fluxSpeedTime());
                shouldFlux = false;
            }
        }
    }

    IEnumerator fluxSpeedTime()
    {
        yield return new WaitForSeconds(fluxSpeed);
        pixelatedCamera.screenScaleFactor = targetScale;
        shouldFlux = true;
    }
}
