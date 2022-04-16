using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeClock : MonoBehaviour
{
    [SerializeField] GameObject clockText;
    public int day = 0;
    public class Clock
    {
        
        public int Hours = 8;
        public int Minutes = 0;

        public Clock()
        {
        }

        public int getHours()
        {
            return this.Hours;
        }
        public int getMinutes()
        {
            return this.Minutes;
        }

        
        
    }

    public Clock clock = new Clock();

    public string getTimeString()
    {
        string hours = clock.getHours().ToString();
        string minutes;

        if (clock.getMinutes() < 10)
        {
            minutes = "0" + clock.getMinutes().ToString();
        }

        else
        {
            minutes = clock.getMinutes().ToString();
        }
        return hours + ":" + minutes;
    }
    public void passTime(int hours, int minutes)
    {
        clock.Hours += hours;
        clock.Minutes += minutes;

	if(clock.Hours >= 24)
	{
	    clock.Hours = clock.Hours - 24;
	    day = day + 1;
	}

        clockText.GetComponent<TextMesh>().text = getTimeString();
    }
}
