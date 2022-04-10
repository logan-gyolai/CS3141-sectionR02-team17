//Kaitlyn Klapatauskas

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

    public void QuitGame()
    {
        Application.Quit();
    }

}
