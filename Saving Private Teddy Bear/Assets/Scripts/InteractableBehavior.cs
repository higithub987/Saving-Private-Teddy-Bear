using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class InteractableBehavior : MonoBehaviour
{
    public String objectName;
    public Camera cam;
    public void behavior()
    {
        if (objectName == "tester")
        {
            Destroy(gameObject);
        }
        else if (objectName == "vase")
        {
            Rigidbody rb;
            rb = gameObject.AddComponent<Rigidbody>();
            rb.AddTorque(transform.forward * 20, ForceMode.VelocityChange);
            rb.AddForce(transform.up * 0.1f, ForceMode.VelocityChange);
            StartCoroutine(ChangeVase());
            GameManager.distractions.Add(rb.transform.position, 3);
        }
        else if (objectName == "cup")
        {
            Rigidbody rb;
            rb = gameObject.transform.parent.gameObject.AddComponent<Rigidbody>();
            rb.AddTorque(transform.up * 20, ForceMode.VelocityChange);
            rb.AddForce(transform.forward * 1.25f, ForceMode.VelocityChange);
            StartCoroutine(ChangeCup());
            GameManager.distractions.Add(rb.transform.position, 2);
        }
        else if (objectName == "ball")
        {
            Rigidbody rb;
            Vector3 forceApplicationDirection = new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z).normalized;
            rb = gameObject.AddComponent<Rigidbody>();
            rb.AddTorque(transform.forward * -20, ForceMode.VelocityChange);
            rb.AddForce(forceApplicationDirection * 2f, ForceMode.VelocityChange);
            rb.transform.gameObject.layer = LayerMask.NameToLayer("Player");
            GameManager.distractions.Add(rb.transform.position, 1);
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
