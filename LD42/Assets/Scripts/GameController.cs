using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour{

    static Player player;
    static MovingPlatform[] platforms;

    public GameObject pausedMenu;
    public GameObject gameOverScreen;
    public GameObject victoryScreen;
    public GameObject creditsScreen;

    private void Start()
    {
        if(GameObject.Find("Player") != null)
        {
            player = GameObject.Find("Player").GetComponent<Player>();
        }
        
        platforms = FindObjectsOfType<MovingPlatform>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && gameOverScreen != null && victoryScreen != null)
        {
            if(!gameOverScreen.activeSelf && !victoryScreen.activeSelf)
                Pause();
        }

        if(Input.GetKeyDown(KeyCode.Escape) && creditsScreen != null)
        {
            creditsScreen.SetActive(false);
        }
    }

    public void GameOver()
    {
        Pause();
        pausedMenu.SetActive(false);
        gameOverScreen.SetActive(true);
    }

    public void Victory()
    {
        Pause();
        pausedMenu.SetActive(false);
        victoryScreen.SetActive(true);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Credits()
    {
        if(creditsScreen != null)
        {
            creditsScreen.SetActive(true);
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PrevLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Pause()
    {
        if(player.enabled == true)
        {
            player.enabled = false;
            pausedMenu.SetActive(true);
            
            foreach (var p in platforms)
            {
                p.enabled = false;
            }
        }
        else
        {
            player.enabled = true;
            pausedMenu.SetActive(false);
            foreach (var p in platforms)
            {
                p.enabled = true;
            }
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
