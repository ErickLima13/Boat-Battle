using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public TextMeshProUGUI scoreDisplay;
    public TextMeshProUGUI scoreDisplayFinal;
    public TextMeshProUGUI timeDisplay;

    public GameObject gameOverPanel;
    public GameObject pausePanel;
    public GameObject startDisplay;  

    private void Initialization()
    {
        GameManager.instance.onGameOver += GameOver;
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialization();
    }

    // Update is called once per frame
    void Update()
    {
        Started();
        ScoreUpdate();
        GameTime();
        PauseGame();
    }

    private void Started()
    {
        if (GameManager.instance.isGameActive)
        {
            startDisplay.SetActive(false);
        }        
    }

    private void ScoreUpdate()
    {
        scoreDisplay.text = GameManager.instance.score.ToString();
    }

    private void GameTime()
    {
        timeDisplay.text = GameManager.instance.time.ToString("f0");
    }

    private void GameOver()
    {
        gameOverPanel.SetActive(true);
        scoreDisplayFinal.text = "Your Score:" + GameManager.instance.score.ToString();
        
    }

    private void OnDisable()
    {
        GameManager.instance.onGameOver -= GameOver;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausePanel.activeSelf)
            {
                pausePanel.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                pausePanel.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
}
