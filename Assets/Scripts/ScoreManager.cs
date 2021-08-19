using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScoreManager : MonoBehaviour
{
    private ScoreData scoreData;

    void Awake()
    {
        var json = PlayerPrefs.GetString("scores", "{}");
        scoreData = JsonUtility.FromJson<ScoreData>(json);
    }

    public List<Score> GetHighScores()
    {
        return scoreData.scores.OrderBy(e => e.totalTime).ToList();
    }

    public void AddScore(Score sd)
    {
        scoreData.scores.Add(sd);
    }

    private void OnDestroy()
    {
        SaveScore();
    }

    public void SaveScore()
    {
        var json = JsonUtility.ToJson(scoreData);
        PlayerPrefs.SetString("scores", json);
        //PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }
}
