using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xyRotationMouse : MonoBehaviour
{
    public float xRotation;
    public float yRotation;

    private int MouseSensitivity = 20;

    void Update()
    {
        float MouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float MouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;

        xRotation -= MouseY;
        xRotation = Mathf.Clamp(xRotation, -60, 5);
        yRotation -= MouseX;
    }
}
