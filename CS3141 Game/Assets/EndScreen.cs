using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] GameObject statText;
    [SerializeField] GameObject achievementText;

    // Start is called before the first frame update
    void Start()
    {
        if (Stats.money.getStat() <= 0) { Interact.BrokeBitch = true; }
        printEndScreen();
    }

    public void printEndScreen()
    { 
        // I want to print out the stuff at the end screen
        Stats.printStats(statText);
        printAchievements();
    }

    public void printAchievements()
    {
        string text = "Achievements: \n";
        if (Interact.BroomballChampion)
        {
            text = text + "Champion \n";
        }
        if (Interact.BrokeBitch)
        {
            text = text + "Go broke \n";
        }
        if (Interact.Workaholic)
        {
            text = text + "Workaholic \n";
        }
        if (Interact.CompleteSculpture)
        {
            text = text + "Builder \n";
        }
        if (Interact.SoupTime)
        {
            text = text + "Soup \n";
        }
        if (Interact.gotChlamydia)
        {
            text = text + "Intimate Encounter \n";
        }
        achievementText.GetComponent<TextMeshProUGUI>().text = text;
    }
}
