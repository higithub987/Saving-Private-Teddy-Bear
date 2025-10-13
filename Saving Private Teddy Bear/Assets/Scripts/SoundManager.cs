using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    //declare variables here
    public float SoundVolume;

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

    public void SetVolume(float vol)
    {
        SoundVolume = vol;
    }
}
