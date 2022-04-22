using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;


public class Interact : MonoBehaviour
{
    public bool inRange = false;
    public KeyCode interactKey;
    public UnityEvent interact;
    [SerializeField] string interactMessage;
    [SerializeField] string resultMessage;
    [SerializeField] GameObject displayedText;
    public int energyInc = 0; //how much this interaction will change this stat
    public int healthInc = 0;
    public int intelligenceInc = 0;
    public int socialInc = 0;
    public int athleticnessInc = 0;
    public int coldnessInc = 0;
    public int decisionMakingInc = 0;
    public int moneyInc = 0;
    [SerializeField] TimeClock timeClock;
    public int timeCostHours = 0;
    public int timeCostMinutes = 0;
    public int energyMod = 0; //used so you can sleep when energy == 0
    [SerializeField] GoOutsideMenu goOutsideMenu;
    [SerializeField] RandomGenerator randomGenerator;
    [SerializeField] String interactingWith; //name of what we are doing/interacting with
    bool gotChlamydia = false;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (inRange && !goOutsideMenu.isInMenu)
        {
            if (Input.GetKeyDown(interactKey)) //true if interact key (space) is pressed down
            {
               doTheThing();
            }
        }

        int day = timeClock.getDay();
        if (day > 5)
        {
            SceneManager.LoadScene(sceneName: "EndGame");
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

    public void doTheThing() {

        // Seed the random number generator so it is more random
        Random.InitState(Guid.NewGuid().GetHashCode());

        // check if we have enough energy
        if (Stats.energy.getStat() + energyMod == 0)
        {
            ShowMessage("You need to sleep");
            return;
        }

        // Work Interaction
        if (interactingWith.Equals("Work"))
        {
            // generate a random event at work
            int outcome = Random.Range(0, 100);
            if (outcome < 5) // real bad outcome
            {
                resultMessage = "You clogged the toilet at work and the manager made you pay for it. You lost $20";
                socialInc = -2;
                moneyInc = -20;
                energyInc = -5;
            }
            else if (outcome < 10)
            {
                resultMessage = "A child threw up on you, but her mother tipped you well.";
                socialInc = -1;
                moneyInc = 20;
                energyInc = -4;
            }
            else if (outcome < 15)
            {
                resultMessage = "Someone lit a fire in the bathroom. It may have been you.";
                moneyInc = 5;
                energyInc = -1;
            }
            else if (outcome < 90)
            {
                resultMessage = "You worked a shift. Nothing special happened.";
                moneyInc = 10;
                energyInc = -2;
            }
            else
            {
                resultMessage = "The big party must have liked you; they tipped big.";
                moneyInc = 30;
                socialInc = 1;
                energyInc = -2;
            }

        }

        // Broomball interaction
        if (interactingWith.Equals("Broomball"))
        {
            // Check the player's health, don't let them play if they're too injured.
            if (Stats.health.getStat() == 0)
            {
                ShowMessage("You're too hurt to play Broomball. You should rest");
                return;
            }

            // Initialize variables that will be used in this event
            int outcome = Random.Range(0, 100);
            int yourScore = 0;
            int opponentScore = 0;
            bool won = false;

            //generate scores
            while(yourScore == opponentScore)
            {
                yourScore = (int)Math.Round(Math.Abs(randomGenerator.rNorm(0, 4)), 0);
                opponentScore = (int)Math.Round(Math.Abs(randomGenerator.rNorm(0, 4)), 0);
            }

            //see who won
            if(yourScore > opponentScore)
            {
                won = true;
            }

            // Did the player get hurt?
            string injuryStr = "";
            if ( (outcome + Stats.athleticness.getStat()) < 10 )
            {
                injuryStr = ". Your ankle may also be broken.";
                healthInc = -10;
            }
            else if ( (outcome + Stats.athleticness.getStat()) < 20)
            {
                injuryStr = ". You also got a concussion.";
                healthInc = -5;
            }
            else if ( (outcome + (int)Stats.athleticness.getStat()) < 50)
            {
                injuryStr = ". It's gonna hurt to sit for a few days.";
                healthInc = -2;
            }

            //put message together
            if (won) 
            {
                resultMessage = "You won " + yourScore.ToString() + " to " + opponentScore.ToString() + injuryStr;
                athleticnessInc = 2;
            }

            else
            {
                resultMessage = "You lost " + yourScore.ToString() + " to " + opponentScore.ToString() + injuryStr;
                athleticnessInc = 1;
            }
        }

        // Hockey game interaction
        if (interactingWith.Equals("Hockey"))
        {
            resultMessage = "Tech beats NMU 10-0!";
        }

        // party interaction
        if (interactingWith.Equals("Party"))
        {
            gotChlamydia = randomGenerator.rBernoulli(0.1);
            if (gotChlamydia) { Die(); }
        }

        Stats.energy.changeStat(energyInc);
        Stats.intelligence.changeStat(intelligenceInc);
        Stats.health.changeStat(healthInc);
        Stats.social.changeStat(socialInc);
        Stats.decisionMaking.changeStat(decisionMakingInc);
        Stats.athleticness.changeStat(athleticnessInc);
        Stats.coldness.changeStat(coldnessInc);
        Stats.money.changeStat(moneyInc);

        timeClock.passTime(timeCostHours, timeCostMinutes);

        ShowMessage(resultMessage);

        interact.Invoke(); //invoke event
    }

    public void Die() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
