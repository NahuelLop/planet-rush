using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    public RowUI rowUI;
    public ScoreManager scoreManager;

    void Start()
    {
        var scores = scoreManager.GetHighScores().ToArray();

        for (int i = 0; i<scores.Length; i++)
        {
            
            //var row = Instantiate(rowUI, transform).GetComponent<RowUI>();
            RowUI row = Instantiate(rowUI, transform) as RowUI;
            row.rank.text = (i + 1).ToString();
            row.name.text = scores[i].name;
            row.score.text = scores[i].totalTime.ToString("F2");
        }
    }


}
