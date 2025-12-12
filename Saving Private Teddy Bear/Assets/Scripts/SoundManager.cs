using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{

    //declare variables here
    public float SoundVolume;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioSource movementSource, climbSource;
    [SerializeField]
    private AudioClip buttonSound, vaseSound, ballSound, cupSound, footStep, jump, wallClimb;
    [SerializeField]
    private AudioMixer mixer;

    private float timer1, timer2 = 0f;
    
    //Singleton
    public static SoundManager Instance;

    // Start is called before the first frame update
    void Start()
    {
        SoundVolume = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(this.gameObject == null);
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    public void SetVolume(float vol)
    {
        SoundVolume = vol;
        mixer.SetFloat("Volume", Mathf.Log10(vol) * 20);
    }

    public float GetVolume()
    {
        return SoundVolume;
    }

    public void PressButton()
    {
        audioSource.PlayOneShot(buttonSound, SoundVolume);
    }

    public void BreakVase()
    {
        audioSource.PlayOneShot(vaseSound, SoundVolume);
    }

    public void BounceBall()
    {
        audioSource.PlayOneShot(ballSound, SoundVolume);
    }

    public void BreakCup()
    {
        audioSource.PlayOneShot(cupSound, SoundVolume);
    }

    public void PlayFootStep()
    {
        climbSource.loop = false;
        climbSource.Stop();
        timer1 += Time.deltaTime;
        if (timer1 <= movementSource.clip.length)
        {
            return;
        }
        else
        {
            movementSource.volume = SoundVolume;
            movementSource.Play();
            timer1 = 0;
        }
        
    }

    public void jumpSound()
    {
        audioSource.PlayOneShot(jump, SoundVolume);
    }

    public void climb()
    {
        movementSource.loop = false;
        movementSource.Stop();
        timer2 += Time.deltaTime;
        if(timer2 <= climbSource.clip.length)
        {
            return;
        }
        else
        {
            climbSource.volume = SoundVolume;
            climbSource.Play();
            timer2 = 0;
        }
        
    }
}
