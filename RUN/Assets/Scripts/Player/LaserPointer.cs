using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour
{
    [SerializeField]Transform laserOrgin;
    [SerializeField]Transform laserPoint;
    [SerializeField]GameObject laserTrail;
    [SerializeField]PlayerMovementTutorial player;

    void Update()
    {
        if (Input.GetKey(player.weaponKey))
        {
            laserTrail.SetActive(true);
            laserPoint.gameObject.SetActive(true);
            Debug.DrawRay(laserOrgin.position, laserOrgin.transform.forward, Color.red);
            RaycastHit hit;
            if (Physics.Raycast(laserOrgin.transform.position, laserOrgin.transform.forward, out hit))
            {
                    laserPoint.position = hit.point;
                    laserPoint.rotation = Quaternion.LookRotation(hit.normal);
            }
        }
        else
        {
            laserPoint.gameObject.SetActive(false);
            laserTrail.SetActive(false);
        }
    }
}
