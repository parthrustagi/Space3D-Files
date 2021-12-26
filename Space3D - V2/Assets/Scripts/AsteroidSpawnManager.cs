using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AsteroidSpawnManager : MonoBehaviour
{
    public GameObject[] Asteroids;
    public float radius = 14.0f;

    void Start()
    {
        InvokeRepeating("SpawnRandomArea", 2.0f, 1.5f);
    }

    void update()
    {

    }

    void SpawnRandomArea()
    {
        int asteroidIndex = Random.Range(0, Asteroids.Length);
        Vector3 spanwPosition = this.transform.position + Random.insideUnitSphere * radius;
        Instantiate(Asteroids[asteroidIndex], spanwPosition, Quaternion.identity);
    }
}