using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureAI : MonoBehaviour
{
    [SerializeField]UnityEngine.AI.NavMeshAgent navMeshAgent;
    [SerializeField]Transform target;
    [SerializeField]Transform defaultTransform;
    [SerializeField]Transform player;
    [SerializeField]Transform Light;
    [SerializeField]Light light;
    bool chase;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.destination = target.position;
        if (target.transform.tag == "Waypoint")
        {
            Light.transform.LookAt(transform.position);
        }
        else
        {
            Light.transform.LookAt(target.position);
        }

        RaycastHit hit;
        if (Physics.Raycast(Light.transform.position, Light.transform.forward, out hit, 25f))
        {
            if (hit.transform.tag == "Player")
            {
                if (!chase)
                {
                    Chase();
                }
            }
        }
    }

    public void TargetPlayer()
    {
        target = player;
    }

    void Chase()
    {
        StartCoroutine(realize());
    }

    void Calm()
    {
        light.color = Color.white;
        target = default;
        navMeshAgent.speed = 2;
        navMeshAgent.acceleration = 1;
    }

    IEnumerator realize()
    {
        chase = false;
        light.color = Color.red;
        yield return new WaitForSeconds(2);
        TargetPlayer();
        navMeshAgent.speed = 3;
        navMeshAgent.acceleration = 3;
        StartCoroutine(calm());
    }

    IEnumerator calm()
    {
        yield return new WaitForSeconds(10f);
        Calm();
    }
}
