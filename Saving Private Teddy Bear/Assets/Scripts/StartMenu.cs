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
        SoundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
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
        SoundManager.PressButton();
        Application.Quit();
    }

    public void StartGame()
    {
        SoundManager.PressButton();
        //SampleScene is our main scene, with index 1
        SceneManager.LoadScene(1);
    }

    public void Tutorial()
    {
        SoundManager.PressButton();
        //Tutorial scene has index 2
        SceneManager.LoadScene(2);
    }

    public void OpenSettings()
    {
        SoundManager.PressButton();
        MenuHolder.SetActive(false);
        SettingsHolder.SetActive(true);
    }

    public void BacktoMenu()
    {
        SoundManager.PressButton();
        MenuHolder.SetActive(true);
        SettingsHolder.SetActive(false);
    }

    public void ToggleWallClimb()
    {
        SoundManager.PressButton();
        manager.ToggleWall();
    }

    public void HoldWallClimb()
    {
        SoundManager.PressButton();
        manager.HoldWall();
    }

    public void AdjustSound()
    {
        SoundManager.SetVolume(VolumeSlider.value);
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
