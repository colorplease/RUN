using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float power = 0.7f;
    public Transform camera;
    public bool shouldShake = false;
    public bool screenShakeEnabled;

    Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        if (!screenShakeEnabled)
        {
            power = 0;
        }
        startPosition = camera.localPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(shouldShake)
        {
                camera.localPosition = startPosition+Random.insideUnitSphere*power;
        }
        else
        {
                camera.localPosition = startPosition;
        }
    }
    }

