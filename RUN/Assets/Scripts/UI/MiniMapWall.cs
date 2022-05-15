using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapWall : MonoBehaviour
{
    [SerializeField]Material lit;
    [SerializeField]Material unlit;
    [SerializeField]Color colorDistance;
    MeshRenderer meshRenderer;
    public float fadeOffStrength; //has to be a decimal btw
    [SerializeField]float distance;
    [SerializeField]float colorValue;
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "MiniMapWall")
        {
            distance = 1/Vector3.Distance(other.transform.position, transform.position) * fadeOffStrength;
            meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.material = lit;
            colorValue = 255 * distance;
            colorDistance = new Color32 ((byte)colorValue, (byte)colorValue, (byte)colorValue, 255);
            meshRenderer.material.color = colorDistance;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "MiniMapWall")
        {
            meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.material = unlit;
            meshRenderer.material.color = Color.white;
        }
    }

}
