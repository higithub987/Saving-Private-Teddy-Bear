using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
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
    [SerializeField]
    private TextMeshProUGUI Tutorialtext;
    [SerializeField]
    private GameObject TutorialWall;
    [SerializeField]
    private GameObject WallClimbStuff;
    [SerializeField]
    private GameObject InteractableVase;
    [SerializeField]
    private GameObject parent;


    //declare private variables here
    private SoundManager SoundManager;
    private Color BaseColor;
    private Color SelectedColor = Color.gray;
    private bool isPaused;
    private bool WPressed, APressed, SPressed, DPressed, SpacePressed = false;
    private bool WallClimbed, InteractableUsed =false;
    public static bool ParentDistracted = false;
    private List<string> texts = new List<string> {
        "Welcome to Saving Private Teddy Bear!\n Press W to move forward and start the game!",//0
        "Press A to move right. ",//1
        "Press D to move left. ",//2
        "Press S to move backwards",//3
        "Press Space to Jump. ",//4
        "Try to climb the wall! ",//5
        "Walk up to the vase and see what happens!", //6
        "Notice how the parent is now distracted? ",//7
        "Congratulations on finishing the tutorial! Press Escape and start the game! "//8
    };



    private void Awake()
    {
        SoundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        manager = GameObject.Find("Gamemanager").GetComponent<GameManager>();
    }


    // Start is called before the first frame update
    void Start()
    {
        TutorialWall.SetActive(false);
        MenuHolder.SetActive(false);
        SettingsHolder.SetActive(false);
        BaseColor = HoldButton.image.color;
        ToggleButton.image.color = SelectedColor;
        VolumeSlider.value = SoundManager.GetVolume();
        InteractableVase.SetActive(false);
        SetTutorialText();
        parent.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        int textind = SetTutorialText();
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
        if (Input.GetKeyDown(KeyCode.W))
        {
            WPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            APressed = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            SPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            DPressed = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpacePressed = true;
        }
        if(textind >= 5)
        {
            TutorialWall.SetActive(true);
        }
        if(textind >= 5 && manager.getWallClimb())
        {
            WallClimbed = true;
        }
        InteractableUsed = InteractableBehavior.tutorialInteracted;
        if(textind >= 6)
        {
            InteractableVase.SetActive(true);
            parent.SetActive(true);
        }
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

    public void ResetGame()
    {
        SoundManager.PressButton();
        //start scene has index 0
        SceneManager.LoadScene(0);
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
        manager.ToggleWall();
    }

    public void HoldWallClimb()
    {
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

    private int SetTutorialText()
    {
        int WInt = WPressed ? 1 : 0;
        int AInt = APressed ? 1 : 0;
        int DInt = DPressed ? 1 : 0;
        int SInt = SPressed ? 1 : 0;
        int SpaceInt = SpacePressed ? 1 : 0;
        int InteractablesInt = InteractableUsed ? 1 : 0;
        int WallInt = WallClimbed ? 1 : 0;
        int ParentInt = ParentDistracted ? 1 : 0;
        int textindex = WInt + AInt + DInt + SInt + InteractablesInt + WallInt + ParentInt + SpaceInt;
        Tutorialtext.text = texts[textindex];
        return textindex;
    }
}
