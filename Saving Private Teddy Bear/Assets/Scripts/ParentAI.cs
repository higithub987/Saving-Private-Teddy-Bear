using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ParentAI : MonoBehaviour
{
    public GameManager manager;

    public float distanceCutoff = 10.0f;
    public float cleanupDelay;
    public float distractionDelayScaling = 2.0f;

    [SerializeField]
    private NavMeshAgent parent; 

    //private stuff
    private bool activeTracking = false;
    private bool trackingObj = false;
    private bool cleaning = false; //jank way to stop the thing if it's cleaning without coroutines
    private Vector3 MaxValObj;
    private float timer = 0f;

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
        activeTracking = !(GameManager.distractions.Count == 0);
        //this is highest priority, do everything else in else if
        if (cleaning)
        {
            timer += Time.deltaTime;
            
            if(timer >= cleanupDelay)
            {
                cleaning = false;
                GameManager.distractions.Remove(MaxValObj);
            }
            return;
        }
        else if (activeTracking)
        {
            if (!trackingObj)
            {
                float maxval = 0f;
                foreach (Vector3 key in GameManager.distractions.Keys)
                {
                    float tempval = Vector3.Distance(transform.position, key)
                        * GameManager.distractions[key];
                    if (maxval < tempval)
                    {
                        maxval = tempval;
                        MaxValObj = key;
                    }
                }
                trackingObj = true;
                return;
            }
            else
            {
                parent.SetDestination(MaxValObj);
                foreach (Vector3 key in GameManager.distractions.Keys)
                {
                    float tempval = Vector3.Distance(transform.position, key)
                        * GameManager.distractions[key];
                    if (distanceCutoff < tempval)
                    {
                        MaxValObj = key;
                    }
                }
            }
            if(transform.position == MaxValObj)
            {
                cleanupDelay = GameManager.distractions[MaxValObj] * distractionDelayScaling;
                cleaning = true;
            }
        }
    }
}
