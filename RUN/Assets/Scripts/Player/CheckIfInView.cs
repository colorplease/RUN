using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckIfInView : MonoBehaviour
{
    [SerializeField]Transform eyes;
    public bool seen;
    void FixedUpdate()
    {
        RaycastHit hit;
        Debug.DrawRay(eyes.position, eyes.forward, Color.green);
        if(Physics.Raycast(eyes.position, eyes.forward, out hit))
        {
            if (hit.transform.tag == "Creature")
            {
                seen = true;
            }
            else
            {
                seen = false;
            }
        }
    }
}
