using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] AudioClip[] crashSFXs;
    [SerializeField] AudioClip successSFX;
    [SerializeField] ParticleSystem crashPS;
    [SerializeField] ParticleSystem successVFX;
    Movement playerMovement;
    RegisterScoreUI registerScoreUI;
    GameSessionConstroller gameSession;

    AudioSource audioSource;

    bool isTransitioning = false;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerMovement = GetComponent<Movement>();
        gameSession = FindObjectOfType<GameSessionConstroller>();

        registerScoreUI = FindObjectOfType<RegisterScoreUI>();
        if (registerScoreUI) { registerScoreUI.gameObject.SetActive(false); }
    }

    // Update is called once per frame
    void Update()
    {
        //CheckDebugKeys();
    }


    private void OnCollisionEnter(Collision collision)
    { 
        if (isTransitioning) { return; }

        string otherObjectTag = collision.gameObject.tag;
        switch(otherObjectTag)
        {
            case "Friendly":
                break;
            case "Finish":
                StartSuccessSequence();
                break;

            default:
                StartCrashSequence();
                break;
        }
    }

    private void StartCrashSequence()
    {
        isTransitioning = true;
        playerMovement.enabled = false;
        PlayCrashVFX();
        PlayCrashSFX();
        StartCoroutine(RestartLevel());
    }    
    
    private void StartSuccessSequence()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        isTransitioning = true;
        playerMovement.StopMoving();
        playerMovement.enabled = false;
        PlaySuccessSFX();
        PlaySuccessVFX();
        FindObjectOfType<GameSessionConstroller>().CompleteLevel(currentScene);
        Invoke("LoadNextLevel", 2f);
    }

    private void PlaySuccessSFX()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(successSFX);
    }    
    
    private void PlayCrashVFX()
    {
        ParticleSystem PS = Instantiate(crashPS,transform);
        PS.Play();
    }
        
    private void PlaySuccessVFX()
    {
        ParticleSystem PS = Instantiate(successVFX,transform);
        PS.Play();
    }

    private void PlayCrashSFX()
    {
        AudioClip crashSFX = crashSFXs[Random.Range(0, crashSFXs.Length)];
        audioSource.Stop();
        audioSource.PlayOneShot(crashSFX);
    }


    IEnumerator RestartLevel()
    {

        yield return new WaitForSeconds(2f);

        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    void LoadNextLevel()
    {


        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextScene < SceneManager.sceneCountInBuildSettings - 1)
        {
            //go to level selection menu
            SceneManager.LoadScene(0);
        }
        else
        {
            //show score registration UI
            registerScoreUI.gameObject.SetActive(true);
            registerScoreUI.SetGlobalTime(FindObjectOfType<GameSessionConstroller>().GetTotalGameTime());

        }
    }


    void CheckDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            isTransitioning = !isTransitioning;
            Debug.Log("Collision disabled");
        }
    }
}
