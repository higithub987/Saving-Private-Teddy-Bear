using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameUI : MonoBehaviour
{

    public SoundManager SoundManager;
    // Start is called before the first frame update
    void Start()
    {
        SoundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart()
    {
        SoundManager.PressButton();
        SceneManager.LoadScene(0);
        
    }

    public void EndGame()
    {
        SoundManager.PressButton();
        Application.Quit();
    }
}
