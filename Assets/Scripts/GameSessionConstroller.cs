using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement;

public class GameSessionConstroller : MonoBehaviour
{

    [SerializeField] int lastLevelUnblocked = 0;
    [SerializeField] TextMeshProUGUI totalGameTimeText;
    float globalGameTime = 0f;
    LevelSelectPlatform[] levels;
    GameUIController uiController;
    AudioSource audioSource;
    PlayedScene playedScenes;
    bool isAudioMuted = false;



    private void Awake()
    {
        if (FindObjectsOfType<GameSessionConstroller>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playedScenes = new PlayedScene();
        LoadMenuStatus();
    }

    private void Update()
    {
        LoadMenuStatus();
        //Load Menu screen when P key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }        
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void LoadMenuStatus()
    {
        
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            GetComponentInChildren<MenuUI>().GetComponent<Canvas>().enabled = true;
            GameUIController gameUIc = FindObjectOfType<GameUIController>();
            if (gameUIc) { gameUIc.GetComponent<Canvas>().enabled = false; }
            levels = FindObjectsOfType<LevelSelectPlatform>();
            for (int a = 0; a < levels.Length; a++)
            {
                if (levels[a].GetLevel() <= lastLevelUnblocked)
                {
                    levels[a].Unblock();
                }
                else
                {
                    levels[a].Block();
                }
            }
            totalGameTimeText.text = "Total Time: " + globalGameTime.ToString("F2") + "s";
        }
        else
        {
            GetComponentInChildren<MenuUI>().GetComponent<Canvas>().enabled = false;
            GameUIController gameUIc = FindObjectOfType<GameUIController>();
            if (gameUIc) { gameUIc.GetComponent<Canvas>().enabled = true; }
        }

        if (SceneManager.GetActiveScene().buildIndex == SceneManager.GetSceneByName("ScoreBoard").buildIndex)
        {
            audioSource.Stop();
        }
        else
        {
            if (!audioSource.isPlaying && !isAudioMuted)
            {
                audioSource.Play();
            }
        }
    }

    public void RestarGameSession()
    {
        lastLevelUnblocked = 1;
        globalGameTime = 0f;
       
        // Remove levels from played levels list
        for (int i = 0; i < playedScenes.sceneList.ToArray().Length; i++)
        {
            playedScenes.sceneList[i].sceneTime = 0f;
        }
    }

    public float GetTotalGameTime()
    {
        globalGameTime = 0f;

        List<SceneScore> sceneScores = playedScenes.sceneList.OrderBy(w => w.sceneIndex).ToList();
        playedScenes.sceneList = sceneScores;


        for (int i = 0; i < playedScenes.sceneList.ToArray().Length; i++)
        {
            globalGameTime += playedScenes.sceneList[i].sceneTime;
            
            //Debug.Log("Time for level " + playedScenes[i].sceneIndex + ": " + playedScenes[i].sceneTime);
        }
        return globalGameTime;
    }

    private void AddPlayedScene(int sceneIndex, float sceneTime)
    {
        //if level has been played already, remove it from the list to replace it with new finish time

        int idx = playedScenes.sceneList.FindIndex(w => w.sceneIndex == sceneIndex);

        // VER DE EN LUGAR DE REMOVE conviene Pisar el valor del tiempo en el lugar encontrado.
        if (idx >= 0)
        {
            Debug.Log("Removing level: " + idx + "playedScene: " + playedScenes.sceneList[idx].sceneIndex);
            //playedScenes.sceneList.Remove(playedScenes.sceneList[idx]);
            playedScenes.sceneList[idx].sceneIndex = sceneIndex;
            playedScenes.sceneList[idx].sceneTime = sceneTime;
            lastLevelUnblocked--;
        }
        else
        {
            SceneScore sc = new SceneScore(SceneManager.GetSceneByBuildIndex(sceneIndex).buildIndex, sceneTime);
            playedScenes.sceneList.Add(sc);
        }
    }


    public void CompleteLevel(int sceneIndex)
    {
        uiController = FindObjectOfType<GameUIController>();
        uiController.StopCount();
        AddPlayedScene(sceneIndex, uiController.GetFinishTime());
        lastLevelUnblocked++;
    }

    public float GetLevelTime(int index)
    {
        int idx = playedScenes.sceneList.FindIndex(w => w.sceneIndex == index);
        //Debug.Log("Remove scene: " + sceneIndex);
        if (idx >= 0)
        {
            return playedScenes.sceneList[idx].sceneTime;
        }
        else
        {
            return 0f;
        }
    }

    public void MuteMusic()
    {
        
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
            isAudioMuted = true;
        }
        else
        {
            audioSource.Play();
            isAudioMuted = false;
        }
        GetComponentInChildren<MenuUI>().ChangeMuteButton(audioSource.isPlaying);
        //FindObjectOfType<GameUIController>().ChangeMuteButton(audioSource.isPlaying);
    }

    public bool IsAudioMuted()
    {
        return isAudioMuted;
    }
}
