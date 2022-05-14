using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WayPoint : MonoBehaviour
{
    public bool isFriendly;
    void Awake()
    {
        NavMeshHit hit;
        Vector3 pos = new Vector3(Random.Range(-50.32f, -36.78f), 0.05f, Random.Range(-6.49f, 6.48f));
        NavMesh.SamplePosition(pos, out hit, Mathf.Infinity, NavMesh.AllAreas);
        transform.position = new Vector3(hit.position.x, 0.05f, hit.position.z);
    }

    void OnTriggerStay(Collider other)
    {

        if (other.tag == "Waypoint")
        {
            if (isFriendly)
            {
             NavMeshHit hit;
            Vector3 pos = new Vector3(Random.Range(-50.32f, -36.78f), 0.05f, Random.Range(-6.49f, 6.48f));
            NavMesh.SamplePosition(pos, out hit, Mathf.Infinity, NavMesh.AllAreas);
            transform.position = new Vector3(hit.position.x, 0.05f, hit.position.z);
            }
        }

        if (other.tag == "Creature")
        {
            if (!isFriendly)
            {
                StartCoroutine(wayPointGot(other.transform));
            }
        }

        if (other.tag == "Player")
        {
            if (isFriendly)
            {
                Destroy(gameObject);
            }
        }

        IEnumerator wayPointGot(Transform Creature)
        {
            NavMeshHit hit;
            Vector3 pos = new Vector3(Random.Range(-50.32f, -36.78f), 0.05f, Random.Range(-6.49f, 6.48f));
            NavMesh.SamplePosition(pos, out hit, Mathf.Infinity, NavMesh.AllAreas);
            transform.position = new Vector3(hit.position.x, 0.05f, hit.position.z);
            yield return new WaitForSeconds(1);
            Creature.gameObject.GetComponent<CreatureAI>().SetDestination();
        }
    }
    

}
