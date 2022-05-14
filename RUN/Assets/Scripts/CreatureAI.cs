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
    [SerializeField]Light light2;
    [SerializeField]Rigidbody rb;
    [SerializeField]ProceduralWalk proceduralWalk;
    bool chase;
    bool actualChase;
    Vector3 lastPos;
    // Start is called before the first frame update
    void Awake()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        SetDestination();
    }

    public void SetDestination()
    {
        navMeshAgent.destination = target.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (actualChase)
        {
            //when chasing the player, look at the player
            Light.transform.LookAt(target.position);
            navMeshAgent.destination = target.position;
        }

       //check if enemy is moving
       Vector3 curPos = transform.position;
       if (curPos == lastPos)
       {
           SetDestination();
       }
       lastPos = curPos;

        RaycastHit hit;
        if (Physics.Raycast(Light.transform.position, Light.transform.forward, out hit, 1f))
        {
            //if the player is seen, drop everything and start the chase sequence
            if (hit.transform.tag == "Player")
            {
                chase = false;
                if (!chase)
                {
                    StopAllCoroutines();
                    Chase();  
                }
            }
        }
    }

    public void TargetPlayer()
    {
        //target the player
        target = player;
        if (actualChase)
        {
            StartCoroutine(seekTimeOut());
        }
    }

    IEnumerator seekTimeOut()
    {
        yield return new WaitForSeconds(5);
        target = defaultTransform;
    }

    void Chase()
    {
        //Start the chase sequence for the player
        chase = true;
        actualChase = true;
        if (actualChase)
        {
            StartCoroutine(realize());
        }
    }

    void Calm()
    {
        //calm down and stop the chase
        actualChase = false;
        chase = false;
        light.color = Color.white;
        light2.color = Color.white;
        target = defaultTransform;
        navMeshAgent.speed = 2;
        navMeshAgent.acceleration = 1;

    }

    IEnumerator realize()
    {
        //Turn the light color red, and then wait 2 seconds before going ape shit crazy on the player
        light.color = Color.red;
        light2.color = Color.red;
        yield return new WaitForSeconds(2);
        TargetPlayer();
        navMeshAgent.speed = 3;
        navMeshAgent.acceleration = 3;
        StartCoroutine(calm());
    }

    IEnumerator calm()
    {
        //Wait 5 seconds and then calm down the creature
        yield return new WaitForSeconds(10f);
        Calm();
    }

    void OnTriggerEnter(Collider other)
    {

    }
}
