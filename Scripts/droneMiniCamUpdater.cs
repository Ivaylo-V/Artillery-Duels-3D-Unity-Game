using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class droneMiniCamUpdater : MonoBehaviour
{
    [SerializeField]private GameObject noDroneCam;
    [SerializeField]private RenderTexture droneRenderTexture;

    private GameObject selected;
    private GameObject droneCam;
    private GameObject selectUI;
    private GameObject droneUI;
    private GameObject OtherUI;
    private void Start()
    {
        selectUI = GameObject.Find("ScreenCanvas").GetComponent<UIs>().selectUI;
        droneUI = GameObject.Find("ScreenCanvas").GetComponent<UIs>().droneUI;
        OtherUI = GameObject.Find("ScreenCanvas").GetComponent<UIs>().otherUI;
        noDroneCam.SetActive(true);
        noDroneCam.GetComponent<Camera>().targetTexture = droneRenderTexture;
    }

    void FixedUpdate()
    {
        if(Camera.main != null)
        {
            if(Camera.main.GetComponent<Interactor>().interactable != null)
            {
                selected = Camera.main.GetComponent<Interactor>().interactable.gameObject;
            }
            else
            {
                selected = null;
            }
        }

        if (selected != null && selected.GetComponent<life>().hasDrone == true)
        {
            if (droneCam != null)
            {
                droneCam.GetComponent<Camera>().enabled = false;
            }

            if (selected.CompareTag("Interactable") && selected.GetComponent<life>().team == GameObject.Find("TurnManager").GetComponent<turnManager>().currentTeam) 
            {
                if (selectUI.activeInHierarchy || droneUI.activeInHierarchy || OtherUI.activeInHierarchy)
                {
                    droneCam = GameObject.Find($"{selected.name}/Drone/DroneBody/CamHolder/DroneCam/DroneMiniCam").gameObject;
                    droneCam.GetComponent<Camera>().enabled = true;
                    droneCam.GetComponent<Camera>().targetTexture = droneRenderTexture;
                    noDroneCam.SetActive(false);
                }
                else if(droneCam != null)
                {
                    droneCam.GetComponent<Camera>().enabled = true;
                    droneCam.GetComponent<Camera>().targetTexture = droneRenderTexture;
                    noDroneCam.SetActive(false);
                }
            }
            else if (selected.CompareTag("Interactable") && selected.GetComponent<life>().team != GameObject.Find("TurnManager").GetComponent<turnManager>().currentTeam)
            {
                droneCam.GetComponent<Camera>().enabled = false;
                noDroneCam.SetActive(true);
                noDroneCam.GetComponent<Camera>().targetTexture = droneRenderTexture;
            }
        }
        else if (selected == null || selected.GetComponent<life>().hasDrone == false)
        {
            if (droneCam != null)
            {
                droneCam.GetComponent<Camera>().enabled = false;
            }

            noDroneCam.SetActive(true);
            noDroneCam.GetComponent<Camera>().targetTexture = droneRenderTexture;
        }
    }
}
