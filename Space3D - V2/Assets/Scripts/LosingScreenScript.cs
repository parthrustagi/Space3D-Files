﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosingScreenScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Flight");
        Time.timeScale = 1f;
        Cursor.visible = false;
        ScoreSystem.Awake();
    }
}
