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
    public GameObject InteractablesHolder;
    public GameObject InteractablePopup;
    public GameObject TeddyPopup;


    //Various UI elements
    public TextMeshProUGUI climbTextStart;

    //private variables
    

    private void Awake()
    {
        manager = GameObject.Find("Gamemanager").GetComponent<GameManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        InteractablesHolder.SetActive(false);
        WallClimbHolder.SetActive(false);
        InteractablePopup.SetActive(false);
        TeddyPopup.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showWallClimb()
    {
        
        ShowInteractables();
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

    public void ShowInteractablesUI()
    {
        
        ShowInteractables();
        InteractablePopup.SetActive(true);
    }

    public void HideWallClimb()
    {
        WallClimbHolder.SetActive(false);
    }
    public void HideInteractablesUI()
    {
        InteractablePopup.SetActive(false);
    }

    public void ShowInteractables()
    {
        InteractablesHolder.SetActive(true);
    }

    public void HideInteractables()
    {
        InteractablesHolder.SetActive(false);
    }

    public void showTeddy()
    {
        TeddyPopup.SetActive(true);
    }

    public void hideTeddy()
    {
        TeddyPopup.SetActive(false);
    }
}
