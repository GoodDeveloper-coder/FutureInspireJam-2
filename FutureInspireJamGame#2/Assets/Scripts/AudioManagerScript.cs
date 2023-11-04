using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    [SerializeField] private AudioSource musicPlayer1;
    [SerializeField] private AudioSource musicPlayer2;
    [SerializeField] private AudioSource ambiencePlayer1;
    [SerializeField] private AudioSource ambiencePlayer2;
    [SerializeField] private AudioSource sFXPlayer;

    [SerializeField] private AudioClip[] dayMusic;
    [SerializeField] private AudioClip[] nightMusic;
    [SerializeField] private AudioClip endMusic;

    [SerializeField] private AudioClip nightAmbience;
    [SerializeField] private AudioClip rainAmbience;
    [SerializeField] private AudioClip thunderAmbience;
    [SerializeField] private AudioClip plumberAmbience;

    [SerializeField] private float transitionDuration;

    private int dayIndex;
    private float transitionTime;
    private bool night;

    // Start is called before the first frame update
    void Start()
    {
        musicPlayer1.clip = dayMusic[0];
        musicPlayer1.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (transitionTime > 0)
        {
            transitionTime -= Time.deltaTime;
            if (transitionTime > 0)
            {
                musicPlayer1.volume = transitionTime / transitionDuration;
                ambiencePlayer1.volume = transitionTime / transitionDuration;
            }
            else
            {
                musicPlayer1.volume = 1;
                musicPlayer1.Stop();
                musicPlayer1.clip = night ? nightMusic[dayIndex] : dayIndex < dayMusic.Length ? dayMusic[dayIndex] : endMusic;
                musicPlayer1.Play();
                if (night)
                {
                    ambiencePlayer1.volume = 1;
                    ambiencePlayer1.Stop();
                    ambiencePlayer1.clip = nightAmbience;
                    ambiencePlayer1.Play();
                }
                else if (dayIndex == 1)
                {
                    ambiencePlayer1.Stop();
                    ambiencePlayer1.clip = rainAmbience;
                    ambiencePlayer1.Play();
                }
                else if (dayIndex == 2)
                {
                    ambiencePlayer1.Stop();
                    ambiencePlayer1.clip = thunderAmbience;
                    ambiencePlayer1.Play();
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.N)) SetToNight();
        if (Input.GetKeyDown(KeyCode.D)) SetToDay();
        if (Input.GetKeyDown(KeyCode.S)) StartMinigame();
        if (Input.GetKeyDown(KeyCode.E)) EndMinigame();
        if (Input.GetKeyDown(KeyCode.P)) ActivatePlumberAmbience();
        if (Input.GetKeyDown(KeyCode.O)) DeactivatePlumberAmbience();
    }

    public void ActivatePlumberAmbience()
    {
        ambiencePlayer2.Play();
    }

    public void DeactivatePlumberAmbience()
    {
        ambiencePlayer2.Stop();
    }

    public void StartMinigame()
    {
        musicPlayer1.Pause();
        musicPlayer2.Play();
    }

    public void EndMinigame()
    {
        musicPlayer2.Stop();
        musicPlayer1.Play();
    }

    public void SetToNight()
    {
        transitionTime = transitionDuration;
        night = true;
    }

    public void SetToDay()
    {
        dayIndex++;
        transitionTime = transitionDuration;
        night = false;
    }
}
