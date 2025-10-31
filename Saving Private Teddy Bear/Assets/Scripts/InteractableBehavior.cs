using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class InteractableBehavior : MonoBehaviour
{
    public String objectName;
    public Camera cam;
    public static bool tutorialInteracted = false;
    public void behavior()
    {
        if (objectName == "tester")
        {
            Destroy(gameObject);
        }
        else if (objectName == "vase" || objectName == "tutorvase")
        {
            Rigidbody rb;
            rb = gameObject.AddComponent<Rigidbody>();
            rb.AddTorque(transform.forward * 20, ForceMode.VelocityChange);
            rb.AddForce(transform.up * 0.1f, ForceMode.VelocityChange);
            StartCoroutine(ChangeVase());
            GameManager.distractions.Add(rb.transform.position, 3);        
            if (objectName == "tutorvase")
            {
                tutorialInteracted = true;
            }
        } else if (objectName == "cup")
        {
            Rigidbody rb;
            rb = gameObject.transform.parent.gameObject.AddComponent<Rigidbody>();
            rb.AddTorque(transform.up * 20, ForceMode.VelocityChange);
            rb.AddForce(transform.forward * 1.25f, ForceMode.VelocityChange);
            StartCoroutine(ChangeCup());
            GameManager.distractions.Add(rb.transform.position, 2);
        }
    }

    IEnumerator ChangeVase()
    {
        yield return new WaitForSeconds(0.6f);
        Destroy(GetComponent<Rigidbody>());
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<MeshCollider>().enabled = false;

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }
    IEnumerator ChangeCup()
    {
        yield return new WaitForSeconds(0.6f);
        Destroy(GetComponent<Rigidbody>());
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<MeshCollider>().enabled = false;
        transform.parent.Find("Torus").gameObject.SetActive(false);
    

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}
