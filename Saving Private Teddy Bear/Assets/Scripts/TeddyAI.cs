using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeddyAI : MonoBehaviour
{

    public GameManager manager;
    public bool followingHooman = false; 

    [SerializeField]
    public Vector3 offset = new Vector3(0.5f, 0.5f, 0.5f);

    //private stuff
    private GameObject hooman;  // what the teddy bear follows
    
    private void Awake()
    {
        manager = GameObject.Find("Gamemanager").GetComponent<GameManager>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hooman != null)
        {
            transform.position = hooman.transform.position + offset;
        }
    }

    public void SetHooman(GameObject target)
    {
        hooman = target;
        followingHooman = true;
    }
}
