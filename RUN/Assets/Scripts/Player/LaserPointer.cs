using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour
{
    [SerializeField]Transform laserOrgin;
    [SerializeField]LineRenderer lr;
    [SerializeField]PlayerMovementTutorial player;

    void Update()
    {
        if (Input.GetKey(player.weaponKey))
        {
            Debug.DrawRay(laserOrgin.position, laserOrgin.transform.forward, Color.red);
            RaycastHit hit;
            if (Physics.Raycast(laserOrgin.transform.position, laserOrgin.transform.forward, out hit, 10f))
            {
                print(hit.transform.name);
                    lr.SetPosition(1, new Vector3(0,0, hit.distance));
            }
            else
            {
                lr.SetPosition(1, new Vector3(0,0,5000));
            }
        }
        else
        {
            lr.SetPosition(1, new Vector3(0,0,5000));
        }
    }
}
