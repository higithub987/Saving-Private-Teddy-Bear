using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    public float rayDistance;
    public LayerMask hitMask;
    public GameObject interactableUI;

    float xRotation;
    float yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        // getting mouse input
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // rotating camera and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

        //check for interactable objects
        Ray ray = new Ray(this.gameObject.transform.position, this.gameObject.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance, hitMask)) {
            interactableUI.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
                hit.collider.GetComponent<InteractableBehavior>().behavior();
        } else {
            interactableUI.SetActive(false);
        }
    }
}
