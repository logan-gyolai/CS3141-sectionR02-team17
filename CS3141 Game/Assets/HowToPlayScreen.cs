//Authors:
//James Henthorn
//Blake Hawes
//Kaitlyn Klapatauskas
//Logan Gyolai
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlayScreen : MonoBehaviour
{
    public void returnToMainMenu()
    {
        SceneManager.LoadScene(sceneName: "StartGame");
    }

}
