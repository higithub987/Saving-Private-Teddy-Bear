using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //Declare Stuff here
    public bool wallClimbing;
    public bool ToggleHoldWallClimb; //true when toggle, false when hold

    // Start is called before the first frame update
    void Start()
    {

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
}
