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
    bool actualChase;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.destination = target.position;
        if (actualChase)
        {
            //when chasing the player, look at the player
            Light.transform.LookAt(target.position);
        }

        RaycastHit hit;
        if (Physics.Raycast(Light.transform.position, Light.transform.forward, out hit, 1.5f))
        {
            //if the player is seen, drop everything and start the chase sequence
            if (hit.transform.tag == "Player")
            {
                chase = false;
                if (!chase)
                {
                    StopAllCoroutines();
                    print("hey1");
                    Chase();  
                }
            }
        }
    }

    public void TargetPlayer()
    {
        //target the player
        target = player;
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
        target = defaultTransform;
        navMeshAgent.speed = 2;
        navMeshAgent.acceleration = 1;
    }

    IEnumerator realize()
    {
        //Turn the light color red, and then wait 2 seconds before going ape shit crazy on the player
        light.color = Color.red;
        yield return new WaitForSeconds(2);
        TargetPlayer();
        navMeshAgent.speed = 3;
        navMeshAgent.acceleration = 3;
        StartCoroutine(calm());
    }

    IEnumerator calm()
    {
        //Wait 5 seconds and then calm down the creature
        yield return new WaitForSeconds(5f);
        Calm();
    }
}
