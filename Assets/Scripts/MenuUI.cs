using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    [SerializeField] Text audioButtonText;
    public void LoadScoreBoard()
    {
        SceneManager.LoadScene("ScoreBoard");
    }

    public void ChangeMuteButton(bool playing)
    {
        if (playing)
        {
            audioButtonText.text = "Music Off";
        }
        else
        {
            audioButtonText.text = "Music On";
        }
    }
}
