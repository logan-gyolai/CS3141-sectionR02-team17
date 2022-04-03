using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeClock : MonoBehaviour
{
    [SerializeField] GameObject clock;
    public int day = 0;
    public class Clock
    {
        int Hours;
        int Minutes;

        public Clock(int hours, int minutes)
        {
            this.Hours = hours;
            this.Minutes = minutes;
        }

        public int getHours()
        {
            return this.Hours;
        }
        public int getMinutes()
        {
            return this.Minutes;
        }

        public string getTimeString()
        {
            string hours = this.getHours().ToString();
            string minutes;

            if (this.getMinutes() < 10) 
            {
                minutes = "0" + this.getMinutes().ToString();
            }

            else
            {
                minutes = this.getMinutes().ToString();
            }
            return hours + ":" + minutes;
        }
        public void passTime(int hours, int minutes)
        {

        }
    }
}
