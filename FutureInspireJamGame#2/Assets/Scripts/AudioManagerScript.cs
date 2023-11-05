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
    [SerializeField] private AudioClip windAmbience;

    [SerializeField] private AudioClip doorKnockSFX;

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
                if (night || dayIndex > 0 && dayIndex < 4)
                {
                    ambiencePlayer1.volume = 1;
                    ambiencePlayer1.Stop();
                    ambiencePlayer1.clip = night ? nightAmbience : dayIndex == 1 ? rainAmbience : dayIndex == 2 ? thunderAmbience : windAmbience;
                    ambiencePlayer1.Play();
                }
            }
        }
        //if (Input.GetKeyDown(KeyCode.N)) SetToNight();
        //if (Input.GetKeyDown(KeyCode.D)) SetToDay();
        //if (Input.GetKeyDown(KeyCode.S)) StartMinigame();
        //if (Input.GetKeyDown(KeyCode.E)) EndMinigame();
        //if (Input.GetKeyDown(KeyCode.P)) ActivatePlumberAmbience();
        //if (Input.GetKeyDown(KeyCode.O)) DeactivatePlumberAmbience();
        //if (Input.GetKeyDown(KeyCode.K)) PlayDoorKnock();
    }

    public void PlayDoorKnock()
    {
        sFXPlayer.PlayOneShot(doorKnockSFX);
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
        ambiencePlayer1.Pause();
        ambiencePlayer2.volume = 0;
        musicPlayer2.Play();
    }

    public void EndMinigame()
    {
        musicPlayer2.Stop();
        musicPlayer1.Play();
        ambiencePlayer1.Play();
        ambiencePlayer2.volume = 1;
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
