using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem
{
    public static int currentScore;
    public static void Awake()
    {
        currentScore = 0;
    }
    public static void incrementScore(int s)
    {
        currentScore += s;
    }
}
