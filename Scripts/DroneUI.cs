using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class DroneUI : MonoBehaviour
{
    [SerializeField] private Button move;
    [SerializeField] private Button controlUI;
    [SerializeField] private Button suicide;
    [SerializeField] private GameObject DroneInfo;

    private GameObject selected;



    private void FixedUpdate()
    {
        selected = Camera.main.gameObject.GetComponent<Interactor>().interactable.gameObject;

        infoUpdate();

        if (selected.GetComponent<life>().destroyed || selected.GetComponent<life>().turnOver == true)
        {
            move.interactable = false;
            controlUI.interactable = false;
            suicide.interactable = false;
        }
        else if(selected.GetComponent<life>().turnOver == false && !selected.GetComponent<life>().destroyed)
        {
            if(selected.gameObject.GetComponent<life>().canDrive == true && selected.gameObject.GetComponent<life>().fuel > 0)
            {

                move.interactable = true;
                controlUI.interactable = true;

            }
            else
            {
                move.interactable = true;
                controlUI.interactable = true;
            }
        }

    }

    public void ControlAI()
    {
        selected.GetComponent<wasdMovement>().enabled = true;
        selected.GetComponent<droneBatteryDrain>().enabled = true;
        selected.GetComponent<NavMeshAgent>().enabled = false;
        GameObject.Find($"{selected.transform.parent.name}/{selected.name}/DroneBody/CamHolder/DroneCam").GetComponent<Camera>().enabled = true;
        GameObject.Find($"{selected.transform.parent.name}/{selected.name}/DroneBody/CamHolder/DroneCam/DroneMiniCam").GetComponent<Camera>().enabled = true;
        GameObject.Find($"{selected.transform.parent.name}/{selected.name}/DroneBody/CamHolder/DroneCam").GetComponent<cameraMovement>().enabled = true;
        GameObject.Find($"{selected.transform.parent.name}/{selected.name}/GranadeDropper").GetComponent<granadeDropper>().enabled = true;

    }

    public void MoveAI()
    {
        selected.transform.position = new Vector3(selected.transform.position.x, (selected.transform.position.y - selected.transform.position.y) + 3, selected.transform.position.z);
        selected.GetComponent<NavMeshAgent>().enabled = true;
        selected.GetComponent<evenSimplerAI>().enabled = true;

    }


    private void infoUpdate()
    {
        string name = selected.gameObject.GetComponent<life>().type;
        float armor = selected.gameObject.GetComponent<life>().armor;
        float speed = selected.gameObject.GetComponent<life>().speed;
        float battery = selected.gameObject.GetComponent<life>().fuel;
        float fullBattery = selected.gameObject.GetComponent<life>().maxFuel;
        int shots = selected.gameObject.GetComponent<life>().maximumShotsPerTurn;
        string granadeCapable;


        if (selected.gameObject.GetComponent<life>().canDropGranades)
        {
            granadeCapable = "Granade Dropper (Active)";
        }
        else
        {
            granadeCapable = "Granade Dropper (Disabled)";
        }


        DroneInfo.GetComponent<TMPro.TextMeshProUGUI>().text = $@"{name}:
armor: {armor:f2}
battery: {battery}/{fullBattery}
speed: {speed:f2}
{granadeCapable}
Max granades: {shots}";
    
    }
}
