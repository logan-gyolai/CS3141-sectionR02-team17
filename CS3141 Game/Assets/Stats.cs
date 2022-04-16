using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stats : MonoBehaviour
{
    [SerializeField] GameObject statText;
    public class Stat
    {
        int Value;
        int MinValue;
        int MaxValue;

        //Constructor
        public Stat(int value, int minValue, int maxValue)
        {
            this.Value = value;
            this.MinValue = minValue;
            this.MaxValue = maxValue;
        }

        //Unsure if I should add check for greater than max or less than min
        public void setStat(int value) 
        {
            this.Value = value;
        }

        public int getStat()
        {
            return this.Value;
        }

        public string getStatString()
        {
            return this.Value.ToString() + " ";
        }

        public void changeStat(int value)
        {
            if (inRange(this.Value + value, this.MinValue, this.MaxValue))
            {
                this.Value += value;
            }
            else if ((this.Value + value) < this.MinValue)
            {
                this.Value = this.MinValue;
            }
            else if ((this.Value + value) > this.MaxValue)
            {
                this.Value = this.MaxValue;
            }
            else
            {
                //how did you get here?S
            }
        }

        public bool inRange(int newVal, int min, int max)
        {
            if ((newVal < min) || (newVal > max))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    public Stat energy = new Stat(10, 0, 20);
    public Stat health = new Stat(10, 0, 10);
    public Stat intelligence = new Stat(5, 1, 10);
    public Stat social = new Stat(5, 0, 10);
    public Stat drunkenness = new Stat(0, 0, 10);
    public Stat decisionMaking = new Stat(10, 0, 10);
    public Stat athleticness = new Stat(5, 0, 10);
    public Stat coldness = new Stat(0, 0, 10);
    public Stat money = new Stat(0, 0, 99);

    public void printStats()
    { 
        string text = "Energy: " + energy.getStatString() +
            "\n Health: " + health.getStatString() +
            "\n Intelligence: " + intelligence.getStatString() +
            "\n Social: " + social.getStatString() +
            "\n Drunkenness: " + drunkenness.getStatString() +
            "\n Decision Making: " + decisionMaking.getStatString() +
            "\n Athleticness: " + athleticness.getStatString() +
            "\n Coldness: " + coldness.getStatString() +
            "\n Money: " + money.getStatString()
            ;

        statText.GetComponent<TextMeshProUGUI>().text = text;
    }
    

    
}
