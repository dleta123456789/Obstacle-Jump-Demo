using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header ("Obstacles")]
    public GameObject[] obstaclePrefab;
    private int obstacleIndex;

    [Header("Spawn Manager")]
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    public float startDelay = 1.0f;
    public float repeatDelay = 3.0f;

    

    private PlayerController playerControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnObjects", startDelay, repeatDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnObjects()
    {
        //Stop Spawnining if gmae ends
        if (playerControllerScript.gameOver == false)
        {
            obstacleIndex = Random.Range(0, obstaclePrefab.Length);
            Instantiate(obstaclePrefab[obstacleIndex], spawnPos, obstaclePrefab[obstacleIndex].transform.rotation);

        }

    }

    
}
