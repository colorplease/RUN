using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureAIRedux : MonoBehaviour
{
    [SerializeField]CameraShake cameraShake;
    [SerializeField]Transform player;
    [SerializeField]UnityEngine.AI.NavMeshAgent mNavMeshAgent;
    [SerializeField]Light light;
    [SerializeField]CheckIfInView check;
    [SerializeField]Color lightColor;
    public bool chase;
    [SerializeField]bool coolDown;
    bool targetPlayer;
    bool running;

    void Start()
    {
        targetPlayer = true;
        mNavMeshAgent.speed = 1;
        mNavMeshAgent.acceleration = 1;
        mNavMeshAgent.destination = player.position;
    }

    void FixedUpdate()
    {
        if (targetPlayer)
        {
            mNavMeshAgent.destination = player.position;
        }      
        if(check.seen && !coolDown)
        {
            if (!chase)
            {
                StartChase();
                chase = true;
            }
        }
    }

    void StartChase()
    {
        StopAllCoroutines();
        StartCoroutine(Realize());
    }  

    IEnumerator Realize()
    {
        targetPlayer = false;
        cameraShake.shouldShake = true;
        light.color = Color.red;
        mNavMeshAgent.destination = transform.position;
        yield return new WaitForSeconds(2);
        targetPlayer = true;
        mNavMeshAgent.speed = 3;
        mNavMeshAgent.acceleration = 3;
        StartCoroutine(Calm());
    }

    IEnumerator Calm()
    {
        yield return new WaitForSeconds(15);
        cameraShake.shouldShake = false;
        light.color = lightColor;
        mNavMeshAgent.speed = 1;
        mNavMeshAgent.acceleration = 1;
        chase = false;
        coolDown = true;
        yield return new WaitForSeconds(1);
        coolDown = false;
    }
}
