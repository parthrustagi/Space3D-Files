using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootScript : MonoBehaviour
{
    public float shootRate;
    private float shootTime;
    public Ship shipComponent;
    public GameObject laserPrefab;

    RaycastHit hit;
    float range = 10000.0f;


    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Time.time > shootTime)
            {
                shootRay();
                shootTime = Time.time + shootRate;
            }
        }
    }

    void shootRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, range))
        {
            GameObject laser = GameObject.Instantiate(laserPrefab, transform.position, transform.rotation) as GameObject;
            laser.GetComponent<ShotBehavior>().setTarget(hit.point);
            print(hit.collider.gameObject.name);
            if (hit.collider.gameObject.CompareTag("Asteroid"))
            {
                hit.collider.gameObject.GetComponent<AsteroidMovement>().Damage(30);
            }
            else if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                hit.collider.gameObject.GetComponent<EnemiesFollow>().Damage(30);
            }
            GameObject.Destroy(laser, 2f);
        }
    }
}
