using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WayPoint : MonoBehaviour
{
    [SerializeField]bool randomWanderer;
    GameManager gameManager;
    
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        if (randomWanderer)
        {
                NavMeshHit hit;
                Vector3 pos = new Vector3(Random.Range(-50.32f, -36.78f), 0.05f, Random.Range(-6.49f, 6.48f));
                NavMesh.SamplePosition(pos, out hit, Mathf.Infinity, NavMesh.AllAreas);
                transform.position = new Vector3(hit.position.x, 0.05f, hit.position.z);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!randomWanderer)
            {
                gameManager.OrbCollected();
                Destroy(gameObject);
            }
        }

        if (other.tag == "Creature")
        {
            if (randomWanderer)
            {
                NavMeshHit hit;
                Vector3 pos = new Vector3(Random.Range(-50.32f, -36.78f), 0.05f, Random.Range(-6.49f, 6.48f));
                NavMesh.SamplePosition(pos, out hit, Mathf.Infinity, NavMesh.AllAreas);
                transform.position = new Vector3(hit.position.x, 0.05f, hit.position.z);
            }
            if (transform == other.gameObject.GetComponentInParent<CreatureAI>().target)
            {
                other.gameObject.GetComponentInParent<CreatureAI>().GenerateNewWanderPoint();
            }
        }
    }
    

}
