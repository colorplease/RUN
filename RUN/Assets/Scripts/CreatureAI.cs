using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureAI : MonoBehaviour
{
    [SerializeField]UnityEngine.AI.NavMeshAgent navMeshAgent;
    public Transform target;
    [SerializeField]Transform defaultTransform;
    [SerializeField]Transform player;
    [SerializeField]Transform Light;
    [SerializeField]Light light;
    [SerializeField]Light light2;
    [SerializeField]Rigidbody rb;
    [SerializeField]ProceduralWalk proceduralWalk;
    [SerializeField]CameraShake cameraShake;
    [SerializeField]Color lightColor;
    [SerializeField]GameManager gameManager;
    [SerializeField]GameObject[] wayPoints;
    bool chase;
    bool actualChase;
    Vector3 lastPos;
    // Start is called before the first frame update
    void Awake()
    {
        wayPoints = GameObject.FindGameObjectsWithTag("Waypoint");
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        GenerateNewWanderPoint();
        SetDestination();
    }

    public void GenerateNewWanderPoint()
    {
        var spawn = Random.Range(0, wayPoints.Length);
        target = wayPoints[spawn].transform;
        defaultTransform = wayPoints[spawn].transform;
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
        cameraShake.shouldShake = false;
        actualChase = false;
        chase = false;
        light.color = lightColor;
        light2.color = lightColor;
        target = defaultTransform;
        navMeshAgent.speed = 2;
        navMeshAgent.acceleration = 1;

    }

    IEnumerator realize()
    {
        //Turn the light color red, and then wait 2 seconds before going ape shit crazy on the player
        cameraShake.shouldShake = true;
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
