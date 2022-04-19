//Authors:
//James Henthorn
//Blake Hawes
//Kaitlyn Klapatauskas
//Logan Gyolai
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //move to next scene (main game)
    }

    public void HowToPlay()
    {
        // I want to open up a menu that tells the concept of the game and how it works/how to play. Unsure how to do this at this moment in time.
        SceneManager.LoadScene(sceneName: "Scene_HowToPlay");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
