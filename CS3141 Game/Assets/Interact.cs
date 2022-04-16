using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Interact : MonoBehaviour
{
    public bool inRange = false;
    public KeyCode interactKey;
    public UnityEvent interact;
    [SerializeField] string interactMessage;
    [SerializeField] string resultMessage;
    [SerializeField] GameObject displayedText;
    [SerializeField] Stats stats;
    public int energyInc = 0; //how much this interaction will change this stat
    public int healthInc = 0;
    public int intelligenceInc = 0;
    public int socialInc = 0;
    public int drunkennessInc = 0;
    public int athleticnessInc = 0;
    public int coldnessInc = 0;
    public int decisionMakingInc = 0;
    public int moneyInc = 0;
    [SerializeField] TimeClock timeClock;
    public int timeCostHours = 0;
    public int timeCostMinutes = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange) 
        {
            if(Input.GetKeyDown(interactKey)) //true if interact key (space) is pressed down
            {
                //try 
                //{
                stats.energy.changeStat(energyInc);
                stats.intelligence.changeStat(intelligenceInc);
                stats.health.changeStat(healthInc);
                stats.social.changeStat(socialInc);
                stats.drunkenness.changeStat(drunkennessInc);
                stats.decisionMaking.changeStat(decisionMakingInc);
                stats.athleticness.changeStat(athleticnessInc);
                stats.coldness.changeStat(coldnessInc);
                stats.money.changeStat(moneyInc);

                timeClock.passTime(timeCostHours, timeCostMinutes);

                ShowMessage(resultMessage);

                interact.Invoke(); //invoke event
                //}
                //catch (exception statOutOfBounds) {
                    ShowMessage
                //}
            }
        }
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
