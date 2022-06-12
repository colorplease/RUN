using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobController : MonoBehaviour
{
    [SerializeField]bool _enable = true;

    [SerializeField] float amplitude = 0.015f;
    [SerializeField, Range(0, 30)] float frequency = 10.0f;
    [SerializeField] float amplitudeReal;
    [SerializeField] Transform camera = null;
    [SerializeField] Transform cameraHolder = null;

    float toggleSpeed = 0.01f;
    Vector3 startPos;
    public PlayerMovementTutorial controller;
    // Start is called before the first frame update
    void Awake()
    {
        controller = GetComponentInParent<PlayerMovementTutorial>();
        startPos = camera.localPosition;
        amplitudeReal = amplitude * 0.11f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_enable) return;
         ResetPosition();
        CheckMotion();
        camera.LookAt(FocusTarget());
    }

    Vector3 FootStepMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * frequency) * amplitudeReal;
        pos.x += Mathf.Cos(Time.time * frequency / 2) * amplitudeReal ;
        return pos;
    }

    Vector3 FocusTarget()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + cameraHolder.localPosition.y, transform.position.z);
        pos += cameraHolder.forward * 15.0f;
        return pos;
    }

    void CheckMotion()
    {
        if (controller.isSprinting)
        {
        frequency = 15;
        }
        else
        {
            frequency = 10;
        }
        float speed = new Vector3(controller.rb.velocity.x * 0.01f, 0, controller.rb.velocity.z* 0.01f).magnitude;
        if(speed < toggleSpeed) return;
        PlayMotion(FootStepMotion());
    }

    void ResetPosition()
    {
        if (camera.localPosition == startPos) return;
        camera.localPosition = Vector3.Slerp(camera.localPosition, startPos, 1 * Time.deltaTime);
    }

    void PlayMotion(Vector3 motion){
camera.localPosition += motion; 
}


}