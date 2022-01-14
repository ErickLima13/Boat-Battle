using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{  

    public void PlayAgain()
    {
        GameManager.instance.PlayGame();
        GameManager.instance.score = 0;
        
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
