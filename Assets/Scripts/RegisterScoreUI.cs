using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RegisterScoreUI : MonoBehaviour
{
    [SerializeField] InputField nameText;
    [SerializeField] Text totalTime;

    [SerializeField] ScoreManager scoreManager;
    Score currentScore;

    public void SetGlobalTime(float time)
    {
        totalTime.text = time.ToString("F2");
    }
    public void SaveRecord()
    {

        if (nameText.text == "")
        {
            GetComponentInChildren<PopUpSystem>().ShowPopUp("Please write your name!");
        }
        else
        {
            currentScore = new Score(nameText.text, float.Parse(totalTime.text));
            scoreManager.AddScore(currentScore);
            //Goes to main Menu
            FindObjectOfType<GameSessionConstroller>().RestarGameSession();
            SceneManager.LoadScene(0);
        }
    }

}
