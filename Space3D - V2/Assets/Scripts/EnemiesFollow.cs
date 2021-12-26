using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesFollow : MonoBehaviour
{
    public Transform Player;
    int move_Speed = 10;
    float max_Distance = 35f;
    int min_Distance = 10;

    public GameObject laserPrefab;
    public float shootRate = 1f;
    RaycastHit hit;

    private float shootTime;

    public float health = 100f;
    public GameObject explosiveParticle;
    public bool isDead;

    
    void Start()
    {
        isDead = false;
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (!isDead)
        {
            transform.LookAt(Player);

            if (Vector3.Distance(transform.position, Player.position) >= min_Distance)
            {
                transform.position += transform.forward * move_Speed * Time.deltaTime;

                if (Vector3.Distance(transform.position, Player.position) <= max_Distance)
                {
                    if (Time.time > shootTime)
                    {
                        shootRay();
                        shootTime = Time.time + shootRate;
                    }
                }
            }
        }
    }

    void shootRay()
    {
        Ray ray = new Ray (transform.position, Player.position - transform.position);
        if (Physics.Raycast(ray, out hit, max_Distance))
        {
            GameObject laser = GameObject.Instantiate(laserPrefab, transform.position, transform.rotation) as GameObject;
            laser.GetComponent<ShotBehavior>().setTarget(hit.point);
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                hit.collider.gameObject.GetComponent<Ship>().Damage(30);
            }
            GameObject.Destroy(laser, 2f);
        }
    }



    public void Damage(int dm)
    {
        health -= dm;

        if (health <= 0)
        {
            explode();
            isDead = true;
            ScoreSystem.incrementScore(50);
        }
    }
    public void explode()
    {
        GameObject explosion = (GameObject)Instantiate(explosiveParticle, transform.position, transform.rotation);
        Destroy(this.gameObject);
        Destroy(explosion, 1f);
    }
}
