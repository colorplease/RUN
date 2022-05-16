using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public Transform[] spawnPointTransforms;
    public GameObject[] batteries;
    [SerializeField]GameObject orbPrefab;
    public int orbCount;
    int spawn;
    [SerializeField]int initalOrbCount;
    [SerializeField]MessageText messageText;
    void Awake()
    {
        orbCount = initalOrbCount;
        spawnPoints = GameObject.FindGameObjectsWithTag("WayPointChance");
        spawnPointTransforms = new Transform[spawnPoints.Length];
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPointTransforms[i] = spawnPoints[i].GetComponent<Transform>();
        }
    }

    public void Spawn()
    {
        spawn = Random.Range(0, spawnPoints.Length);
        Instantiate(orbPrefab, spawnPoints[spawn].transform.position, Quaternion.identity);
    }

    public void OrbCollected()
    {
        batteries = GameObject.FindGameObjectsWithTag("battery");
        orbCount = batteries.Length - 1;
        messageText.NewText(orbCount.ToString(), 0.5f);
    }

}
