using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] float fuelForce = 1000f;
    [SerializeField] float rotationSpeed = 100f;
    bool startedMoving = false;

    [Header("Sound FX")]
    [SerializeField] AudioClip rocketSFX;
    [SerializeField] AudioClip rocketBreakSFX;

    [Header("Visual FX")]
    [SerializeField] ParticleSystem rocketFire;
    [SerializeField] ParticleSystem rocketLeftFire;
    [SerializeField] ParticleSystem rocketRightFire;
    [SerializeField] ParticleSystem rocketLeftBFire;
    [SerializeField] ParticleSystem rocketRightBFire;

    Rigidbody rocketRB;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rocketRB = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        ProcessRotation();
        ProcessBreak();

    }

        
    private void ProcessInput()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space))
        {
            StartThrustig();
        }
        else
        {
            StopThrusting();
        }
    }

    private void StartThrustig()
    {
        startedMoving = true;
        rocketRB.AddRelativeForce(Vector3.up * fuelForce * Time.deltaTime);
        PlaySFX(rocketSFX);

        if (!rocketFire.isPlaying) { rocketFire.Play(); };
    }
 

    private void StopThrusting()
    {
        StopSFX();
        rocketFire.Stop();
    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotation();
        }
    }

    private void RotateLeft()
    {
        ApplyRotation(rotationSpeed);
        if (!rocketRightFire.isPlaying) { rocketRightFire.Play(); }
    }

    private void RotateRight()
    {
        ApplyRotation(-rotationSpeed);
        if (!rocketLeftFire.isPlaying) { rocketLeftFire.Play(); }
    }

    private void StopRotation()
    {
        rocketLeftFire.Stop();
        rocketRightFire.Stop();
    }
    private void ApplyRotation(float rotationThisFrame)
    {
        rocketRB.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rocketRB.freezeRotation = false;
    }

    private void ProcessBreak()
    {
        if (Input.GetKey(KeyCode.S))
        {
            StartBreakThrustig();
        }
        else
        {
            StopBreakThrusting();
        }
    }

    private void StartBreakThrustig()
    {
        startedMoving = true;
        rocketRB.AddRelativeForce(Vector3.down * fuelForce/2 * Time.deltaTime);
        PlaySFX(rocketBreakSFX);

        if (!rocketLeftBFire.isPlaying) { rocketLeftBFire.Play(); };
        if (!rocketRightBFire.isPlaying) { rocketRightBFire.Play(); };
    }


    private void StopBreakThrusting()
    {
        StopBreakSFX();
        rocketRightBFire.Stop();
        rocketLeftBFire.Stop();
    }
    public bool StartedMoving()
    {
        return startedMoving;
    }

    public void StopMoving()
    {
        startedMoving = false;
    }

    void PlaySFX(AudioClip sfx)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = sfx;
            audioSource.PlayOneShot(sfx);
        }

    }

    void StopSFX()
    {
        if (audioSource.clip == rocketSFX)
        {
            audioSource.Stop();
        }
    }
    
    void StopBreakSFX()
    {
        if (audioSource.clip == rocketBreakSFX)
        {
            audioSource.Stop();
        }
    }

}
