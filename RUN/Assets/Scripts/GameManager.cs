using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] spawnPoints;
    [SerializeField]GameObject orbPrefab;
    public int orbCount;
    int spawn;
    [SerializeField]int initalOrbCount;
    [SerializeField]MessageText messageText;
    void Start()
    {
        orbCount = initalOrbCount;
        spawnPoints = GameObject.FindGameObjectsWithTag("WayPointChance");
        for (int i = 0; i < initalOrbCount; i++)
        {
            spawn = Random.Range(0, spawnPoints.Length);
            if (spawnPoints[spawn] != null)
            {
                Instantiate(orbPrefab, spawnPoints[spawn].transform.position, Quaternion.identity);
                Destroy(spawnPoints[spawn]);
                spawnPoints = GameObject.FindGameObjectsWithTag("WayPointChance");
            }

        }
    }

    public void OrbCollected()
    {
        orbCount--;
        messageText.NewText(orbCount.ToString(), 0.5f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
