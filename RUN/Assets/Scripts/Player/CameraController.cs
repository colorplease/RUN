using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void FixedUpdate()
    {

    float mouseX = Input.GetAxisRaw("Mouse X") * Time.fixedDeltaTime * sensX;
    float mouseY = Input.GetAxisRaw("Mouse Y") * Time.fixedDeltaTime * sensY;
    yRotation += mouseX;
    xRotation -= mouseY;
    xRotation = Mathf.Clamp(xRotation, -85f, 85f);

    transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
    
}