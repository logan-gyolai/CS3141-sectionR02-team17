using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using TMPro;

public class GoOutsideMenu : MonoBehaviour
{
    public GameObject goOutsideMenu;
    public bool isInMenu = false;
    public KeyCode interactKey;
    public bool inRange;
    [SerializeField] string interactMessage;
    [SerializeField] GameObject displayedText;
    [SerializeField] GameObject titleText;

    // Start is called before the first frame update
    void Start()
    {
        goOutsideMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange)
        {
            if (Input.GetKeyDown(interactKey))
            {
                if (isInMenu)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }
        
    }

    public void PauseGame()
    {
        goOutsideMenu.SetActive(true);
        titleText.GetComponent<TextMeshProUGUI>().text = "Go Outside Menu";
        Time.timeScale = 0f;
        isInMenu = true;
    }

    public void ResumeGame()
    {
        goOutsideMenu.SetActive(false);
        Time.timeScale = 1f;
        isInMenu = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            inRange = true;
            ShowMessage(interactMessage);
            Debug.Log("Player in range");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = false;
            ShowMessage("");
            Debug.Log("Player not in range");
        }
    }

    public void ShowMessage(string text)
    {
        if (displayedText)
        {
            displayedText.GetComponent<TextMesh>().text = text;
        }
    }

    
}

