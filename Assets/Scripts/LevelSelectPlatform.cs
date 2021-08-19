using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectPlatform : MonoBehaviour
{

    [SerializeField] int goToLevel;
    [SerializeField] bool unlocked = false;
    GameSessionConstroller gs;
    [SerializeField] Text levelTimeText;

    private void Start()
    {
        gs = FindObjectOfType<GameSessionConstroller>();
        if (gs.GetLevelTime(goToLevel) > 0f)
        {
            levelTimeText.text = gs.GetLevelTime(goToLevel).ToString("F2") + "s";
        }
        else
        {
            levelTimeText.text = "";
        }

    }
    public int GetLevel()
    {
        return goToLevel;
    }

    public void Block()
    {
        unlocked = false;
        GetComponentInChildren<Light>().enabled = false;
    }
    public void Unblock()
    {
        unlocked = true;
        GetComponentInChildren<Light>().enabled = true;
    }

    public bool Unblocked()
    {
        return unlocked;
    }

}
