using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneActivator : MonoBehaviour
{

    public GameObject drone;

    void FixedUpdate()
    {
        if (gameObject.GetComponent<life>().hasDrone == true)
        {
            drone.SetActive(true);
        }
        else
        {
            drone.SetActive(false);
        }
    }

    public void resetDroneLife()
    {
        drone.GetComponent<life>().destroyed = false;
        drone.GetComponent<life>().canDrive = true;
        drone.GetComponent<life>().canDropGranades = false;;
        drone.transform.position = new Vector3(drone.transform.parent.position.x - 5, drone.transform.parent.position.y + 3, drone.transform.parent.position.z - 5);
        drone.GetComponent<life>().armor = 10;
        drone.GetComponent<life>().fuel = drone.GetComponent<life>().maxFuel;
    }
}
