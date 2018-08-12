using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour{

    static Player player;
    static MovingPlatform[] platforms;

    public GameObject pausedMenu;
    

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        platforms = FindObjectsOfType<MovingPlatform>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public static void GameOver()
    {

    }

    public static void MainMenu()
    {

    }

    public static void NextLevel()
    {
        
    }

    public static void PrevLevel()
    {
        
    }

    public void Pause()
    {
        if(player.enabled == true)
        {
            player.enabled = false;
            pausedMenu.SetActive(false);
            
            foreach (var p in platforms)
            {
                p.enabled = false;
            }
        }
        else
        {
            player.enabled = true;
            pausedMenu.SetActive(true);
            foreach (var p in platforms)
            {
                p.enabled = true;
            }
        }
    }
}
