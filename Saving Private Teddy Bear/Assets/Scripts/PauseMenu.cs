using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    //declare public variables here
    public GameManager manager;
    

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
    private SoundManager SoundManager;
    private Color BaseColor;
    private Color SelectedColor = Color.gray;
    private bool isPaused;


    private void Awake()
    {
        SoundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        MenuHolder.SetActive(false);
        SettingsHolder.SetActive(false);
        BaseColor = HoldButton.image.color;
        ToggleButton.image.color = SelectedColor;
        VolumeSlider.value = SoundManager.GetVolume() / 100f;
    }

    // Update is called once per frame
    void Update()
    {
        //Tab for testing, change to Escape before building
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isPaused)
            {
                isPaused = false;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1;
                MenuHolder.SetActive(false);
                SettingsHolder.SetActive(false);
            }
            else
            {
                isPaused = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
                MenuHolder.SetActive(true);
                SettingsHolder.SetActive(false);
            }
        }
    }

    public void EndGame()
    {
        Application.Quit();
    }

    public void ResetGame()
    {
        //start scene has index 0
        SceneManager.LoadScene(0);
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
