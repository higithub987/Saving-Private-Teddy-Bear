using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //Declare Stuff here
    public bool wallClimbing;


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
}
