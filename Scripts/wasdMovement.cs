using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wasdMovement : MonoBehaviour
{
    //Visable variables/fields
    [SerializeField] private CharacterController Controller;
    [SerializeField] private float speed = 12;
    [SerializeField] private float Maxspeed = 100;
    private float rotationAmount;
    Vector3 velocity;


    void Update()
    {
        if (gameObject.GetComponent<life>().fuel > 0)
        {
            Sprint();
            wasdMove();
            scroll();
            DroneRotation();

            if(gameObject.name == "Drone" && Input.GetButton("Fire2") && !Input.GetKey("w") && !Input.GetKey("a") && !Input.GetKey("s") && !Input.GetKey("d"))
            {
                endFlight();
            }
        }
        else
        {
            endFlight();
        }
    }

    private void wasdMove()
    {

        float Hinput = Input.GetAxis("Horizontal");
        float Vinput = Input.GetAxis("Vertical");

        Vector3 move = transform.right * Hinput + transform.forward * Vinput;
        Controller.Move(move * speed * Time.deltaTime);

        Controller.Move(velocity * Time.deltaTime);
    }

    private void Sprint()
    {
        if (Input.GetButton("Fire3"))
        {
            speed = Maxspeed;
        }
        else
        {
            speed = 12;
        }
    }

    private void scroll()
    {
        if(gameObject.transform.position.y <= 100 && gameObject.transform.position.y >= 3)
        {
            Vector2 scrollAmount = Input.mouseScrollDelta;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - scrollAmount.y, gameObject.transform.position.z);
        }
        else if(gameObject.transform.position.y > 100)
        {
            Vector2 scrollAmount = Input.mouseScrollDelta;

            if (scrollAmount.y > 0)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - scrollAmount.y, gameObject.transform.position.z);
            }
        }
        else if(gameObject.transform.position.y < 3)
        {
            Vector2 scrollAmount = Input.mouseScrollDelta;

            if (scrollAmount.y < 0)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - scrollAmount.y, gameObject.transform.position.z);
            }
        }

    }

    private void DroneRotation()
    {
        if(gameObject.name == "Drone")
        {

            if (Input.GetKey("q"))
            {
                rotationAmount -= 1;
            }
            if (Input.GetKey("e"))
            {
                rotationAmount += 1;
            }
                gameObject.transform.rotation = Quaternion.Euler(gameObject.transform.rotation.x, rotationAmount, gameObject.transform.rotation.z);
        }

    }

    public void endFlight()
    {
        gameObject.GetComponentInChildren<cameraMovement>().enabled = false;
        gameObject.GetComponent<wasdMovement>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameObject.GetComponentInChildren<Camera>().enabled = false;
        GameObject.Find($"{gameObject.transform.parent.name}/{gameObject.name}/DroneBody/CamHolder/DroneCam/DroneMiniCam").GetComponent<Camera>().enabled = true;
        GameObject.Find($"{gameObject.transform.parent.name}/{gameObject.name}/GranadeDropper").GetComponent<granadeDropper>().enabled = false;
        Camera.main.GetComponent<Camera>().enabled = true;
        Camera.main.GetComponent<Interactor>().interactable = null;
    }


}
