using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeddyAI : MonoBehaviour
{

    public GameManager manager;
    public bool followingHooman = false; 

    [SerializeField]
    public Vector3 offset = new Vector3(0.5f, 0.5f, 0.5f);
    [SerializeField]
    private float xwin1, xwin2, zwin1, zwin2;//1s are the smaller, 2s are the bigger

    //private stuff
    private GameObject hooman;  // what the teddy bear follows
    
    private void Awake()
    {
        manager = GameObject.Find("Gamemanager").GetComponent<GameManager>();
    }


    // Start is called before the first frame update
    void Start()
    {
        xwin1 = manager.getWinRoomX1();
        xwin2 = manager.getWinRoomX2();
        zwin1 = manager.getWinRoomZ1();
        zwin2 = manager.getWinRoomZ2();
    }

    // Update is called once per frame
    void Update()
    {
        if(hooman != null)
        {
            transform.position = hooman.transform.position + offset;
        }
        if(transform.position.x >= xwin1 && transform.position.x <= xwin2
            && transform.position.z >= zwin1 && transform.position.z <= zwin2)
        {
            manager.winGame();
        }
    }

    public void SetHooman(GameObject target)
    {
        hooman = target;
        followingHooman = true;
    }
}
