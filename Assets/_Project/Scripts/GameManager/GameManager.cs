using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    [Header("Main Menu")]
    public GameObject optionsPanel;
    public GameObject menuPanel;
    public TMP_Dropdown sessionTime;
    public TMP_Dropdown spawnTime;

    public delegate void HandlerGameOver();
    public HandlerGameOver onGameOver;

    public delegate void HandlerStartGame();
    public HandlerStartGame onStartGame;     

    public static GameManager instance;

    public bool isGameActive;
    
    public int score = 0;
    public float time;
    public float spawnRate = 3;

    public int multiplier = 1;

    public void SetSpawnTime()
    {
        switch (spawnTime.value)
        {
            case 0:
                spawnRate = 3;
                break;
            case 1:
                spawnRate = 4;
                break;
            case 2:
                spawnRate = 5;
                break;
            case 3:
                spawnRate = 6;
                break;
            case 4:
                spawnRate = 7;
                break;
        }
    }

    public void Initialization()
    {
        DontDestroyOnLoad(gameObject);
        instance = this;
        SetGameTime();
        SetSpawnTime();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialization();   
    }

    // Update is called once per frame
    void Update()
    {
        if (time == 0)
        {
            isGameActive = false;
        }

        if (isGameActive)
        {
            SetGameTime();            
        }

        if (SceneManager.GetActiveScene().name == "Lvl 1")
        {
            optionsPanel = null;
            menuPanel = null;        
        }      
    }


    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Options()
    {
        optionsPanel.SetActive(true);
        menuPanel.SetActive(false);
    }

    public void BackToMenu()
    {
        optionsPanel.SetActive(false);
        menuPanel.SetActive(true);
    }    

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        print("Ponto");
    }

    public void SetGameTime()
    {
        switch (sessionTime.value)
        {
            case 0:
                multiplier = 1;
                break;
            case 1:
                multiplier = 2;
                break;
            case 2:
                multiplier = 3;
                break;
        }

        time -= Time.deltaTime;        

        if(time == 0)
        {
            GameOver();
        }
    }    

    public void GameOver()
    {
        isGameActive = false;
        onGameOver?.Invoke();        
    }

    public void StartGame()
    {
        isGameActive = true;
        onStartGame?.Invoke();
        time = 60 * multiplier;
    }
        
}
