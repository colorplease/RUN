using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAwayPoint : MonoBehaviour
{
    [SerializeField]Transform myself;
     void OnTriggerStay(Collider other)
    {
        if (other.tag == "Waypoint")
        {
            Vector3 pos = new Vector3(Random.Range(-50.32f, -36.78f), 0.15f, Random.Range(-6.49f, 6.48f));
            myself.transform.position = pos;
        }
    }
}
