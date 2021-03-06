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

    // Initialize stats
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
    


    // Initialize trackers for all the stats
    static int broomballWins = 0;
    static int SculptureTime = 0;

    // Achievements
    public static bool BroomballChampion    = false;
    public static bool BrokeBitch           = false;
    public static bool Workaholic           = false;
    public static bool CompleteSculpture    = false;
    public static bool SoupTime             = false;
    public static bool gotChlamydia         = false;
    public static bool Ouch                 = false;
    


    // Start is called before the first frame update
    void Start()
    {
        // Seed the random number generator so it is more random
        Random.InitState(Guid.NewGuid().GetHashCode());
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

        // Set all the increases back to 0. There was an oversight where we were setting the Inc value to an amount 
        // and then sometimes it would affect the stat regardless of what was happening. 
        // Now all stat changes should happen here instead of the Unity Interact script menu because I think these might overwrite those values and therefore
        // stats might not change how they're supposed to.
        energyInc = 0; //how much this interaction will change this stat
        healthInc = 0;
        intelligenceInc = 0;
        socialInc = 0;
        athleticnessInc = 0;
        coldnessInc = 0;
        moneyInc = 0;

        // check if we have enough energy
        if (Stats.energy.getStat() + energyMod == 0)
        {
            ShowMessage("You need to sleep");
            return;
        }
        else if( (interactingWith.Equals("Broomball") | interactingWith.Equals("Snow Sculpture")) & (Stats.coldness.getStat() == 10) )
        {
            ShowMessage( "You're a little cold. You should rest or eat soup" );
            return;
        }

        // Study Interaction
        if (interactingWith.Equals("Desk"))
        {
            energyInc = -1;
            intelligenceInc = 1;
        }

        // Bed Interaction
        if (interactingWith.Equals("Bed"))
        {
            energyInc = 16;
            healthInc = 2;
            coldnessInc = -8;
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

            if (Stats.money.getStat() < -95)        { BrokeBitch = true; }
            else if (Stats.money.getStat() > 95)    { Workaholic = true; }

        }

        // Broomball interaction
        if (interactingWith.Equals("Broomball"))
        {
            energyInc = -3;
            coldnessInc = 3;

            // Check the player's health, don't let them play if they're too injured.
            if (Stats.health.getStat() <= 0)
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
                Ouch = true;
                healthInc = -20;
            }
            else if ( (outcome + Stats.athleticness.getStat()) < 20)
            {
                injuryStr = ". You also got a concussion.";
                Ouch = true;
                healthInc = -5;
                decisionMakingInc = -3;
            }
            else if ( (outcome + (int)Stats.athleticness.getStat()) < 50)
            {
                injuryStr = ". You're gonna be sore for a few days.";
                healthInc = -2;
            }
            else { healthInc = 0;  }

            //put message together
            if (won) 
            {
                resultMessage = "You won " + yourScore.ToString() + " to " + opponentScore.ToString() + injuryStr;
                athleticnessInc = 2;
                broomballWins++;
            }

            else
            {
                resultMessage = "You lost " + yourScore.ToString() + " to " + opponentScore.ToString() + injuryStr;
                athleticnessInc = 1;
                broomballWins = -100;
            }

            if(broomballWins == 3) 
            { 
                BroomballChampion = true;
                resultMessage = "You've won the Broomball Championship!";
            }
        }

        // party interaction
        if (interactingWith.Equals("Party"))
        {
            int outcome = Random.Range(0, 100);

            energyInc = -2;

            if(outcome < 3 || outcome == 91)
            {
                resultMessage = "You disappointed the Dean...";
                gotChlamydia = true;
                socialInc = -25;
                energyInc = -4; // This activity is probably a tad more physically tiring
            }
            else if( (outcome + Stats.social.getStat()) < 10 )
            {
                resultMessage = "Did you spill a drink or pee your pants? Either way you looked like a fool";
                socialInc = -5;
            }
            else if ((outcome + Stats.social.getStat()) < 20)
            {
                resultMessage = "You rocked karaoke night";
                socialInc = 2;
            }
            else if ((outcome + Stats.social.getStat()) < 27)
            {
                resultMessage = "One tequilla, two tequilla, three tequilla, floor";
                socialInc = 1;
                healthInc = -1;
                timeClock.passTime(5,0); // Because you passed out
            }
            else if ((outcome + Stats.social.getStat()) < 40)
            {
                resultMessage = "Meh. The party was okay.";
                socialInc = 1;
            }
            else if ((outcome + Stats.social.getStat()) < 45)
            {
                resultMessage = "You won a pushup contest";
                athleticnessInc = 2;
            }
            else if ((outcome + Stats.social.getStat()) < 50)
            {
                resultMessage = "Some guy talked about partial physics for three hours";
                intelligenceInc = 3;
            }
            else if ((outcome + Stats.social.getStat()) < 60)
            {
                resultMessage = "Your conspiracy theories were the life of the party";
                socialInc = 7;
            }
            else if ((outcome + Stats.social.getStat()) < 65)
            {
                resultMessage = "Your \"breathe fire\" trick didn't quite go as planned...";
                socialInc = -10;
                moneyInc = -25;
            }
            else if ((outcome + Stats.social.getStat()) < 70)
            {
                resultMessage = "Your dance moves attracted lots of attention.s And dollar bills";
                socialInc = 3;
                moneyInc = 12;
            }
            else if ((outcome + Stats.social.getStat()) < 75)
            {
                resultMessage = "Your bagpipes weren't a hit with the others";
                socialInc = -3;
            }
            else if ((outcome + Stats.social.getStat()) < 80)
            {
                resultMessage = "Your bagpipe playing was a huge hit";
                socialInc = 5;
            }
            else if ((outcome + Stats.social.getStat()) < 90)
            {
                resultMessage = "You made some new friends";
                socialInc = 3;
            }
            else if ((outcome + Stats.social.getStat()) < 100)
            {
                resultMessage = "Regrettable decisions were made";
                socialInc = -3;
            }
            else if ((outcome + Stats.social.getStat()) < 110)
            {
                resultMessage = "You were the life of the party";
                socialInc = 3;
            }
        }

        // Hockey game interaction
        if (interactingWith.Equals("Hockey"))
        {
            energyInc = -1;
            socialInc = 2;
            resultMessage = "Tech beats NMU 10-0!";
        }

        

        // Building Snow Sculpture
        if (interactingWith.Equals("Snow Sculpture"))
        {
            coldnessInc = 5;
            energyInc = -3;
            SculptureTime += 3;
            resultMessage = "You made progress and had a great time";

            if(SculptureTime > 10) { 
                CompleteSculpture = true;
                resultMessage = "You finished the statue!";
            }
        }

        if (interactingWith.Equals("Consume Soup"))
        {
            coldnessInc = -5;
            energyInc = 1;
            SoupTime = true;
        }

        Stats.energy.changeStat(energyInc);
        Stats.intelligence.changeStat(intelligenceInc);
        Stats.health.changeStat(healthInc);
        Stats.social.changeStat(socialInc);
        Stats.athleticness.changeStat(athleticnessInc);
        Stats.coldness.changeStat(coldnessInc);
        Stats.money.changeStat(moneyInc);

        timeClock.passTime(timeCostHours, timeCostMinutes);

        ShowMessage(resultMessage);

        interact.Invoke(); //invoke event
    }

    public void Sleep()
    {
        Stats.energy.changeStat(16);
    }
}
