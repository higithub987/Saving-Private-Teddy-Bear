using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{

    //Singleton
    public static GameManager Instance;

    //Declare Stuff here
    public bool wallClimbing;
    public bool ToggleHoldWallClimb; //true when toggle, false when hold
    public static Dictionary<Vector3, Tuple<int, GameObject>> distractions = new Dictionary<Vector3, Tuple<int, GameObject>>();
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

    // Start is called before the first frame update
    void Start()
    {
        ToggleHoldWallClimb = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setClimbing(bool status)
    {
        wallClimbing = status;
    }

    public void ToggleWall()
    {
        ToggleHoldWallClimb = true;
    }

    public void HoldWall()
    {
        ToggleHoldWallClimb = false;
    }
    public bool getWallClimb()
    {
        return wallClimbing;
    }

    public bool ToggleHold()
    {
        return ToggleHoldWallClimb;
    }
}
