using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour
{
    [SerializeField]Transform laserOrgin;
    [SerializeField]GameObject laserPoint;
    [SerializeField]PlayerMovementTutorial player;

    void Update()
    {
        if (Input.GetKey(player.weaponKey))
        {
             RaycastHit hit;
            if (Physics.Raycast(laserOrgin.transform.localPosition, laserOrgin.transform.forward, out hit, 100f))
            {
            laserPoint.SetActive(true);
            laserPoint.transform.localPosition = hit.transform.localPosition;
            }
        }
        else
        {
            laserPoint.SetActive(false);
        }
    }
}
