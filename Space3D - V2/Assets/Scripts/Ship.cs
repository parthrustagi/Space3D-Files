using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ShipPhysics))]
[RequireComponent(typeof(ShipInput))]

public class Ship : MonoBehaviour
{    
    public bool isPlayer = false;

    private ShipInput shipInput;
    private ShipPhysics shipPhysics;    

    public static Ship PlayerShip { get { return playerShip; } }
    private static Ship playerShip;

    public bool mouseInputUsing { get { return shipInput.mouseUse; } }
    public Vector3 velocity { get { return shipPhysics.Rigidbody.velocity; } }
    public float throttle { get { return shipInput.throttleF; } }

    public float shiphealth;
    public GameObject explosiveParticle;
    public bool isDead;

    GameObject GamePanel;
    GameObject LosingPanel;
    GameObject GameWinPanel;

    [SerializeField]
    Text scoreText;

    public Text healthText;

    public float timeValue = 180;
    public Text timerText;

    public bool isGameOver;

    private void Awake()
    {
        shipInput = GetComponent<ShipInput>();
        shipPhysics = GetComponent<ShipPhysics>();
    }

    void Start()
    {
        isDead = false;
        shiphealth = 100f;
        GamePanel = GameObject.Find("Game Panel"); GamePanel.SetActive(true);
        LosingPanel = GameObject.Find("Game Lose Panel"); LosingPanel.SetActive(false);
        GameWinPanel = GameObject.Find("Game Win Panel"); GameWinPanel.SetActive(false);
    }

    void Update()
    {
        if (timeValue > 0)
        {
            if (!isDead)
            {
                shipPhysics.SetPhysicsInput(new Vector3(shipInput.strafeF, 0.0f, shipInput.throttleF), new Vector3(shipInput.pitchF, shipInput.yawF, shipInput.rollF));

                if (isPlayer)
                {
                    playerShip = this;
                }
                scoreText.text = "Score: " + ScoreSystem.currentScore.ToString();
                healthText.text = "Health: " + shiphealth;
            }
            timeValue -= Time.deltaTime;
        }

        else
        {
            timeValue = 0;
            explode();
            isDead = true;
            LosingPanel.SetActive(true);
            GamePanel.SetActive(false);
            Cursor.visible = true;
        }
         
        DisplayTime(timeValue);

        if (ScoreSystem.currentScore >= 500 && !isGameOver)
        {
            GameWinPanel.SetActive(true);
            LosingPanel.SetActive(false);
            GamePanel.SetActive(false);
            Time.timeScale = 0.2f;
            isGameOver = true;
            Cursor.visible = true;
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        if(timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Asteroid"))
        {
            explode();
            isDead = true;
        }
    }

    public void Damage(int damageTaken)
    {
        shiphealth -= damageTaken;
        if(shiphealth <= 0)
        {
            explode();
            isDead = true;
        }
    }

    public void explode()
    {
        GameObject explosion = (GameObject)Instantiate(explosiveParticle, transform.position, transform.rotation);
        Destroy(this.gameObject);
        Destroy(explosion, 1f);
        LosingPanel.SetActive(true);
        GamePanel.SetActive(false);
        Cursor.visible = true;
    }
}
