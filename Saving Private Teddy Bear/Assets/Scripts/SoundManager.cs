using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    //declare variables here
    public float SoundVolume;

    //Singleton
    public static SoundManager Instance;

    // Start is called before the first frame update
    void Start()
    {
        SoundVolume = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(SoundVolume);
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
    }

    public float GetVolume()
    {
        return SoundVolume;
    }
}
