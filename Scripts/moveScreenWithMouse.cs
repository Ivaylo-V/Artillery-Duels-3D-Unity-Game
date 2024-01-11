using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveScreenWithMouse : MonoBehaviour
{
    [SerializeField] private CharacterController Controller;
    [SerializeField] private GameObject skillTree;
    [SerializeField] private GameObject camHolder;

    private float speed = 3.0f;
    private float maxSpeed = 300f;
    private int mDelta = 10; //Size of area for movement in pixels
    private Vector3 rightDirection = Vector3.right;
    private Vector3 forwardDirection = Vector3.forward;

    private void Update()
    {

        if (skillTree.activeInHierarchy)
        {
            moveUI();
        }
        else
        {
            moveCamera();
        }

    }

    private void moveCamera()
    {
        Vector3 forwardDirection = Vector3.forward;

        if (Input.mousePosition.x >= Screen.width - mDelta)
        {
            if (speed <= maxSpeed)
            {
                speed += 0.5f;
            }


            if (gameObject.transform.position.x <= 4000)
            {
                Controller.Move(rightDirection * Time.deltaTime * speed);
            }

        }
        if (Input.mousePosition.x <= 0 + mDelta)
        {
            if (speed <= maxSpeed)
            {
                speed += 0.5f;
            }

            if (gameObject.transform.position.x >= -1000)
            {

                Controller.Move(-rightDirection * Time.deltaTime * speed);
            }
        }
        if (Input.mousePosition.y <= 0 + mDelta)
        {
            if (speed <= maxSpeed)
            {
                speed += 0.5f;
            }

            if (gameObject.transform.position.z >= 0)
            {
                Controller.Move(-forwardDirection * Time.deltaTime * speed);
            }
        }
        if (Input.mousePosition.y >= Screen.height + mDelta)
        {
            if (speed <= maxSpeed)
            {
                speed += 0.5f;
            }
            if (gameObject.transform.position.z <= 5000)
            {
                Controller.Move(forwardDirection * Time.deltaTime * speed);
            }
        }
        if (Input.mousePosition.x < Screen.width - mDelta && Input.mousePosition.x > 0 + mDelta && Input.mousePosition.y > 0 + mDelta && Input.mousePosition.y < Screen.height + mDelta)
        {

            speed = 10;
        }
    }

    private void moveUI()
    {
        if(gameObject.name != "camHolder")
        {

            forwardDirection = Vector3.up;


            if(gameObject.transform.position.x < -200|| gameObject.transform.position.x > 2000 || gameObject.transform.position.y < -800 || gameObject.transform.position.y > 1200)
            {
                speed = 0;
            }

            if (Input.mousePosition.x >= Screen.width - mDelta)
            {
                if (speed <= maxSpeed)
                {
                    speed += 5f;
                }

                    Controller.Move(-rightDirection * Time.deltaTime * speed);

            }
            if (Input.mousePosition.x <= 0 + mDelta)
            {
                if (speed <= maxSpeed)
                {
                    speed += 5f;
                }

                    Controller.Move(rightDirection * Time.deltaTime * speed);

            }
            if (Input.mousePosition.y <= 0 + mDelta)
            {
                if (speed <= maxSpeed)
                {
                    speed += 5f;
                }

                    Controller.Move(forwardDirection * Time.deltaTime * speed);

            }
            if (Input.mousePosition.y >= Screen.height + mDelta)
            {
                if (speed <= maxSpeed)
                {
                    speed += 5f;
                }

                    Controller.Move(-forwardDirection * Time.deltaTime * speed);

            }
            if (Input.mousePosition.x < Screen.width - mDelta && Input.mousePosition.x > 0 + mDelta && Input.mousePosition.y > 0 + mDelta && Input.mousePosition.y < Screen.height + mDelta)
            {

                speed = 10;
            }
        }
    }
}
