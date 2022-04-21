using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    
    
    public GameObject pauseMenu;
    public bool isPaused = false;
    public KeyCode pauseKey;
    [SerializeField] GameObject statText;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        Debug.Log("entered pausegame");
        pauseMenu.SetActive(true);
        Debug.Log("setactive true");
        Time.timeScale = 0f;
        Debug.Log("timescale 0");
        isPaused = true;
        Debug.Log("isPaused true");
        Stats.printStats(statText) ;
        Debug.Log("printed stats");
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
}
