using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;

public class RandomGenerator : MonoBehaviour
{
    Random random = new Random();

    // generates random number from a normal distribution (generally between mean - 2*std and mean + 2*std)
    public double rNorm(double mean, double std)
    {
        double x1 = 1 - random.NextDouble();
        double x2 = 1 - random.NextDouble();

        double y1 = Math.Sqrt(-2.0 * Math.Log(x1)) * Math.Cos(2.0 * Math.PI * x2);
        return y1 * std + mean;
    }

    // simulates an event with probabilty of p
    public bool rBernoulli(double p)
    {
        double x = random.NextDouble();
        if(x < p)
        {
            return true;
        }

        else { return false; }
    }

}
