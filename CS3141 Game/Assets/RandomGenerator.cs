using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;

public class RandomGenerator : MonoBehaviour
{
    public Random random;
    public double rNorm(double mean, double std)
    {
        double x1 = 1 - random.NextDouble();
        double x2 = 1 - random.NextDouble();

        double y1 = Math.Sqrt(-2.0 * Math.Log(x1)) * Math.Cos(2.0 * Math.PI * x2);
        return y1 * std + mean;
    }

    public double rChiSq(int df)
    {
        return 0;
    }

    public int rBernoulli()
    {
        return 0;
    }

}
