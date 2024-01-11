using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class droneBatteryDrain : MonoBehaviour
{
    Vector3 oldPos;
    [SerializeField]float distance = 0;
    [SerializeField] float currentBattery;

    void Awake()
    {
        oldPos = transform.position;
        currentBattery = gameObject.GetComponent<life>().fuel;
    }

    void Update()
    {
        if(currentBattery > 0)
        {
            batDrain();
        }
        else
        {
            Debug.Log("Not enough battery");
            gameObject.GetComponent<life>().fuel = currentBattery;
            currentBattery = gameObject.GetComponent<life>().maxFuel;
            gameObject.transform.position = new Vector3(transform.position.x, transform.position.y - transform.position.y + 3, transform.position.z);
            gameObject.GetComponent<droneBatteryDrain>().enabled = false;
        }

    }

    private void batDrain()
    {


            if (gameObject.transform.position.y > 3)
            {
                currentBattery -= 0.001f;
            }

            Vector3 distanceInVector = transform.position - oldPos;
            float distanceInCurrentFrame = distanceInVector.magnitude;
            distance += distanceInCurrentFrame;
            currentBattery -= (distanceInCurrentFrame * (gameObject.GetComponent<life>().fuelConsumptionPerKM * 0.01f));
            oldPos = transform.position;

            if (Input.GetButton("Fire2"))
            {
                distance = 0;
                gameObject.GetComponent<life>().fuel = currentBattery;
                gameObject.GetComponent<droneBatteryDrain>().enabled = false;
            }

    }
}
