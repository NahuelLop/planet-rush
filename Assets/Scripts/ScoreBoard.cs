using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreBoard : MonoBehaviour
{
    GameSessionConstroller gameSession;

    private void Start()
    {
        gameSession = FindObjectOfType<GameSessionConstroller>();
        if (gameSession.IsAudioMuted())
        {
            GetComponent<AudioSource>().Stop();
        }
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
}
