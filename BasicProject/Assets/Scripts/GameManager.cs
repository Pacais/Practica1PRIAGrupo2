using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scripts : MonoBehaviour
{
    private bool isSpawning;   
    private float minWait;
    private float maxWait;
    public GameObject obstaculoPrefab;
    public GameObject spawnerObstaculos;
    void Start()
    {
        isSpawning = false;
        minWait = 1f;
        maxWait = 2.5f;
    }

    
    void Update()
    {
     if (!isSpawning)
     {
        float timer = Random.Range(minWait, maxWait);
        Invoke("SpawnObjects", timer);
        isSpawning = true;
     }
    }

    private void SpawnObjects()
    {
        Instantiate(obstaculoPrefab, spawnerObstaculos.transform.position, Quaternion.identity);
        isSpawning = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
