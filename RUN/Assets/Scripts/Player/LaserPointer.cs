using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour
{
    [SerializeField]Transform laserOrgin;
    [SerializeField]Transform laserPoint;
    [SerializeField]GameObject laserTrail;
    [SerializeField]PlayerMovementTutorial player;
    [SerializeField]CreatureAI creature;

    void Update()
    {
        if (Input.GetKey(player.weaponKey))
        {
            if (!creature.chase)
            {
                creature.target = laserPoint;
                creature.SetDestination();
                creature.ooshiny = true;
            }
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
            if (creature.target.gameObject.activeSelf == false)
            {
                creature.ooshiny = false;
                creature.target = creature.defaultTransform;
                creature.SetDestination();
            }
            laserPoint.gameObject.SetActive(false);
            laserTrail.SetActive(false);
        }
    }
}
