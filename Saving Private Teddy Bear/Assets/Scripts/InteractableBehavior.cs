using System;
using System.Collections;
using System.Collections.Generic;
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
    }
}
