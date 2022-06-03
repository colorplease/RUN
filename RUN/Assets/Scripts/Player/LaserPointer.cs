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
    [SerializeField]float scaleFactorDistance;
    [SerializeField]float minimumScale;

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
                    var distance = Vector3.Distance(laserOrgin.position, laserPoint.position);
                    var distanceScaleFactor = Mathf.Clamp(distance * scaleFactorDistance, minimumScale, 20);
                    laserPoint.localScale = new Vector3(distanceScaleFactor, distanceScaleFactor, distanceScaleFactor);
                    laserPoint.position = hit.point;
                    laserPoint.rotation = Quaternion.LookRotation(hit.normal);
            }
        }
        else
        {
            if (creature.target.gameObject.activeSelf == false)
            {
                creature.ooshiny = false;
                creature.target = creature.GetClosestSpawn();
                creature.SetDestination();
            }
            laserPoint.gameObject.SetActive(false);
            laserTrail.SetActive(false);
        }
    }
}
