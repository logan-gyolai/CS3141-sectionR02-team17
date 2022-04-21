using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [SerializeField] GameObject endText;
    [SerializeField] GameObject statText;

    // Start is called before the first frame update
    void Start()
    {
        printEndScreen();
    }

    public void printEndScreen()
    { 
        // I want to print out the stuff at the end screen
        Stats.printStats(statText);
    }
}
