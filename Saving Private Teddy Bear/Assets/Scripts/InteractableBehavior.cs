using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class InteractableBehavior : MonoBehaviour
{
    public String objectName;
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
            rb.AddTorque(Vector3.right * 20, ForceMode.Impulse);
            StartCoroutine(ChangeVase());
            GameManager.distractions.Add(rb.transform.position, 3);        
        }
    }

    IEnumerator ChangeVase()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(GetComponent<Rigidbody>());
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<MeshCollider>().enabled = false;

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}
