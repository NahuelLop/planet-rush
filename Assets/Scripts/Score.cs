using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Score
{
    public string name;
    public float totalTime;

    public Score(string playerName, float timeScore)
    {
        name = playerName;
        totalTime = timeScore;
    }


}
