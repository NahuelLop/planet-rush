using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUIController : MonoBehaviour
{

    float finishTime = 0f;
    float currentTime = 0f;
    Movement playerMovement;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] Text audioButtonText;
    // Start is called before the first frame update
    void Start()
    {
        timerText.text = currentTime.ToString("F2") + "s";
        playerMovement = FindObjectOfType<Movement>();

    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.StartedMoving())
        {
            currentTime += Time.deltaTime;
            timerText.text = currentTime.ToString("F2") + "s";
        }
    }


    public void StopCount()
    {
        finishTime = currentTime;
        currentTime = 0f;
    }

    public float GetFinishTime()
    {
        return finishTime;
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
