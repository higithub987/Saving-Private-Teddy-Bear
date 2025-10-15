using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{

    //declare public variables here
    public GameManager manager;
    public SoundManager SoundManager;

    //declare GameObjects here
    [SerializeField]
    private GameObject MenuHolder;
    [SerializeField]
    private GameObject SettingsHolder;
    [SerializeField]
    private Slider VolumeSlider;
    [SerializeField]
    private Button ToggleButton;
    [SerializeField]
    private Button HoldButton;


    //declare private variables here
    private Color BaseColor;
    private Color SelectedColor = Color.gray;


    private void Awake()
    {
        manager = GameObject.Find("Gamemanager").GetComponent<GameManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        MenuHolder.SetActive(true);
        SettingsHolder.SetActive(false);
        BaseColor = HoldButton.image.color;
        ToggleButton.image.color = SelectedColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        //SampleScene is our main scene, with index 1
        SceneManager.LoadScene(1);
    }

    public void Tutorial()
    {
        //Tutorial scene has index 2
        SceneManager.LoadScene(2);
    }

    public void OpenSettings()
    {
        MenuHolder.SetActive(false);
        SettingsHolder.SetActive(true);
    }

    public void BacktoMenu()
    {
        MenuHolder.SetActive(true);
        SettingsHolder.SetActive(false);
    }

    public void ToggleWallClimb()
    {
        manager.ToggleWall();
    }

    public void HoldWallClimb()
    {
        manager.HoldWall();
    }

    public void AdjustSound()
    {
        SoundManager.SetVolume(VolumeSlider.value * 100f);
    }

    public void ChangeColorToggle()
    {
        ToggleButton.image.color = SelectedColor;
        HoldButton.image.color = BaseColor;
    }

    public void ChangeColorHold()
    {
        ToggleButton.image.color = BaseColor;
        HoldButton.image.color = SelectedColor;
    }
}
