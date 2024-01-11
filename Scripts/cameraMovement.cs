using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    private Transform drone;

    private Transform body;
    private float xRotation;
    private float yRotation;

    public Quaternion rotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        body = gameObject.transform.parent;
        xRotation = 0;
        yRotation = 0;

    }

    private void Update()
    {
        xRotation = gameObject.transform.parent.GetComponent<xyRotationMouse>().xRotation;
        yRotation = gameObject.transform.parent.GetComponent<xyRotationMouse>().yRotation;

        if(Cursor.lockState == CursorLockMode.None)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            body = gameObject.transform.parent;
        }

        if (gameObject.transform.parent.name == "CamHolder" && Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))
        {
            drone = gameObject.transform.parent.transform.parent.transform.parent.gameObject.transform;
            body.transform.rotation = Quaternion.Euler(xRotation, 0, 0);  
            drone.transform.rotation = Quaternion.Euler(0, -yRotation, 0);
        }
        else
        {
            drone = null;
            body.transform.rotation = Quaternion.Euler(xRotation, -yRotation, 0);
        }

    }

}
