using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TutorialParent : MonoBehaviour
{
    public GameManager manager;

    public float distanceCutoff = 10.0f;
    public float cleanupDelay;
    public float distractionDelayScaling = 2.0f;

    [SerializeField]
    private NavMeshAgent parent;
    [SerializeField]
    private float xmax, ymax;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private LayerMask playerLayer;
    [SerializeField]
    private float rangeCheck;
    [SerializeField]
    private TutorialUI tutorialUI;

    //private stuff
    private bool activeTracking = true;
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
        parent.SetDestination(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (cleaning)
        {
            timer += Time.deltaTime;
            if (timer >= cleanupDelay)
            {
                cleaning = false;
                Destroy(GameManager.distractions[MaxValObj].Item2);
                GameManager.distractions.Remove(MaxValObj);
                TutorialUI.ParentDistracted = true;
                cleaning = false;
            }
            return;

        }
        else if (activeTracking)
        {
            if (!trackingObj && GameManager.distractions.Count != 0)
            {
                float maxval = 0f;
                foreach (Vector3 key in GameManager.distractions.Keys)
                {
                    float tempval = Vector3.Distance(transform.position, key)
                        * GameManager.distractions[key].Item1;
                    if (maxval < tempval)
                    {
                        maxval = tempval;
                        MaxValObj = key;
                    }
                }
                trackingObj = true;
                return;
            }
            else if(trackingObj)
            {
                parent.SetDestination(MaxValObj);
                foreach (Vector3 key in GameManager.distractions.Keys)
                {
                    float tempval = Vector3.Distance(transform.position, key)
                        * GameManager.distractions[key].Item1;
                    if (distanceCutoff < tempval)
                    {
                        MaxValObj = key;
                    }
                }
                float distance = Vector3.Distance(transform.position, MaxValObj);
                if (distance <= rangeCheck)
                {
                    cleanupDelay = GameManager.distractions[MaxValObj].Item1 * distractionDelayScaling;
                    trackingObj = false;

                    cleaning = true;
                }
            }
            
        }
    }


}
