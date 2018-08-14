using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public Vector2 relTopLeftCorner;
    public Vector2 relBottomRightCorner;
    public GameObject prefab;
    public float spawnInterval;

    Vector2 topLeftCorner;
    Vector2 bottomRightCorner;
    float nextSpawnTime;

    private void Start()
    {
        topLeftCorner = (Vector2)transform.position + relTopLeftCorner;
        bottomRightCorner = (Vector2)transform.position + relBottomRightCorner;
    }

    private void Update()
    {
        if(Time.time > spawnInterval)
        {
            Vector2 randomPosition = new Vector2(Random.Range(bottomRightCorner.x, topLeftCorner.x), Random.Range(bottomRightCorner.y, topLeftCorner.y));
            //randomPosition = V
        }
    }
}
