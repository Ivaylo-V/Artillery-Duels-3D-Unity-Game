using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private Button move;
    [SerializeField] private Button shoot;
    [SerializeField] private Button other;
    [SerializeField] private GameObject cam;
    [SerializeField] private GameObject topCam;
    [SerializeField] private GameObject noDroneCam;
    [SerializeField] private GameObject tankBody;
    [SerializeField] private GameObject otherUI;
    [SerializeField] private GameObject mapSplat;
    [SerializeField] private GameObject droneHolder;

    public GameObject selected;

    private int rangeLvl = 0;


    void FixedUpdate()
    {
        selected = Camera.main.GetComponent<Interactor>().interactable.gameObject;


        if (selected.GetComponent<life>().turnOver == false)
        {
            other.interactable = true;

            if (selected.GetComponent<life>().canShoot && selected.GetComponent<life>().ShotsLeft > 0)
            {
                shoot.interactable = true;
            }
            else
            {
                shoot.interactable = false;
            }

            if (selected.GetComponent<life>().canDrive)
            {
                move.interactable = true;
            }
            else
            {
                move.interactable = false;
            }

        }
        else
        {

            if (selected.GetComponent<life>().canDrive)
            {
                move.interactable = true;
            }
            else
            {
                move.interactable = false;
            }

            shoot.interactable = false;
            other.interactable = true;
        }

    }

    public void shootClick()
    {
        GameObject gunner = GameObject.Find($"{selected.name}/Gunner");
        GameObject cameraPos = GameObject.Find($"{selected.name}/Gunner/CameraPosGunner");
        gunner.transform.rotation = Quaternion.Euler(0, gunner.transform.rotation.y, gunner.transform.rotation.z);
        cameraPos.transform.rotation = Quaternion.Euler(0, cameraPos.transform.rotation.y, cameraPos.transform.rotation.z);
        mapSplat.GetComponent<pinToMap>().isActivated = false;
        cam.GetComponent<Camera>().enabled = true;
        cam.transform.SetParent(gunner.gameObject.transform);
        cam.transform.position = new Vector3(cameraPos.transform.position.x, cameraPos.transform.position.y, cameraPos.transform.position.z);
        cam.transform.rotation = gunner.transform.rotation;
        selected.GetComponent<evenSimplerAI>().DestinationReached();
        selected.GetComponent<gunnerScript>().enabled = true;
        selected.GetComponent<evenSimplerAI>().enabled = false;
        cam.GetComponent<cameraMovement>().enabled = true;
        cam.GetComponent<AudioListener>().enabled = true;
        GameObject.Find($"{selected.name}/Selection").gameObject.GetComponent<MeshRenderer>().enabled = false;


    }

    public void moveClick()
    {

        if (selected.GetComponent<NavMeshAgent>().hasPath && !selected.GetComponent<evenSimplerAI>().maxDistanceReached)
        {
            selected.GetComponent<evenSimplerAI>().DestinationReached();
            selected.GetComponent<evenSimplerAI>().enabled = false;
        }

        selected.GetComponent<evenSimplerAI>().enabled = true;
        selected.GetComponent<NavMeshAgent>().enabled = true;

        if (selected.GetComponent<life>().hasDrone == true)
        {
            GameObject.Find($"{selected.name}/Drone").transform.SetParent(droneHolder.transform);
        }

        if (selected.gameObject.GetComponent<life>().dugIn == true)
        {
            for (int i = 0; i <= 19; i++)
            {
                GameObject.Find($"{selected.name}/DugIn/Cube ({i})").GetComponent<MeshRenderer>().enabled = false;
                GameObject.Find($"{selected.name}/DugIn/Cube ({i})").GetComponent<MeshCollider>().enabled = true;
            }

            if(selected.GetComponent<life>().armor >= 100)
            {
                selected.GetComponent<life>().armor -= Mathf.Abs(100 - selected.GetComponent<life>().armor);
            }

            selected.gameObject.GetComponent<life>().dugIn = false;
        }
    }

    public void otherClick()
    {
        otherUI.SetActive(true);
        gameObject.SetActive(false);
    }

    public void digIn()
    {
        for (int i = 0; i <= 19; i++)
        {
            GameObject.Find($"{selected.name}/DugIn/Cube").GetComponent<MeshRenderer>().enabled = true;
            GameObject.Find($"{selected.name}/DugIn/Cube").GetComponent<MeshCollider>().enabled = true;
        }

        if(selected.GetComponent<life>().canRepair)
        {
            selected.GetComponent<life>().Repair();
        }

            selected.GetComponent<life>().armor += 200;
            selected.GetComponent<life>().dugIn = true;
            selected.GetComponent<life>().turnOver = true;
            GameObject.Find($"{selected.name}/Selection").gameObject.GetComponent<MeshRenderer>().enabled = false;

    }

    public void repair()
    {
        if (selected.GetComponent<life>().canRepair)
        {
            selected.GetComponent<life>().Repair();
        }

        GameObject.Find($"{selected.name}/Selection").gameObject.GetComponent<MeshRenderer>().enabled = false;

    }

    public void droneClick()
    {
        if (!selected.GetComponent<life>().hasDrone && selected.GetComponent<life>().drones > 0)
        {
            selected.GetComponent<life>().hasDrone = true;
        }
        else
        {
            Camera.main.GetComponent<Interactor>().findMyDrone();
        }
    }

    public void granadeDropper()
    {
        if(selected.GetComponent<life>().hasDrone == true)
        {
            GameObject.Find($"{selected.name}/Drone").GetComponent<life>().canDropGranades = true;
            selected.GetComponent<life>().turnOver = true;
        }
            GameObject.Find($"{selected.name}/Selection").gameObject.GetComponent<MeshRenderer>().enabled = false;
    }


    public void signalJammer()
    {
        selected.GetComponent<life>().signalJammer = true;
        selected.GetComponent<life>().turnOver = true;
    }

    public void rangeClick()
    {
        if (selected.GetComponent<life>().hasDrone == true && rangeLvl < 5)
        {
            GameObject.Find($"{selected.name}/Drone").GetComponent<life>().range += 1000;
            rangeLvl++;
            selected.GetComponent<life>().turnOver = true;
        }

        GameObject.Find($"{selected.name}/Selection").gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    public void CamoClick()
    {
        selected.GetComponent<life>().camoflage = true;
        selected.GetComponent<life>().turnOver = true;
    }

    public void defencesClick()
    {
        if(selected.GetComponent<life>().defenceLvl < 5)
        {
            selected.GetComponent<life>().defenceLvl++;
            selected.GetComponent<life>().turnOver = true;
        }
    }


}
