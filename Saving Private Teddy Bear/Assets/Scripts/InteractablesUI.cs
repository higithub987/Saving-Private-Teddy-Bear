using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InteractablesUI : MonoBehaviour
{
    //game manager
    public GameManager manager;

    //Various Menu Holders
    public GameObject WallClimbHolder;


    //Various UI elements
    public TextMeshProUGUI climbTextStart;

    //private variables
    private bool showingWallClimb;

    private void Awake()
    {
        manager = GameObject.Find("Gamemanager").GetComponent<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        WallClimbHolder.SetActive(false);
        showingWallClimb = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showWallClimb()
    {

        WallClimbHolder.SetActive(true);
        if (manager.ToggleHold())
        {
            climbTextStart.text = "Press ";
        }
        else
        {
            climbTextStart.text = "Hold ";
        }
    }
}
