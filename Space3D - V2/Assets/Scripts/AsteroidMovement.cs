using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AsteroidMovement : MonoBehaviour
{
    [SerializeField] public float speed;
    public float asteroidHealth;
    public bool isExploded;
    public GameObject explosiveParticle;

    private void Start()
    {
        asteroidHealth = 100f;
        isExploded = false;
    }

    private void Update()
    {
        if (!isExploded)
        {
            transform.Translate(Vector3.back * Time.deltaTime * speed, Space.World);
        }
    }

    public void Damage(int damageTaken)
    {
        asteroidHealth -= damageTaken;

        if (asteroidHealth <= 0)
        {
            isExploded = true;
            explode();
        }
    }

    public void explode()
    {
        GameObject explosion = (GameObject)Instantiate(explosiveParticle, transform.position, transform.rotation);
        Destroy(this.gameObject);
        Destroy(explosion, 1f);
    }
}
