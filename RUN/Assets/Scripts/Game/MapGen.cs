using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGen : MonoBehaviour
{
    [SerializeField]GameObject testSpawn;
    [SerializeField]float rotationAmount;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Instantiate(testSpawn, transform.position, Quaternion.Euler(0, rotationAmount, 0));
        }
    }
}
