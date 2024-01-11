using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WatchTowerScript : MonoBehaviour
{
    [SerializeField] private LayerMask _interactableLayer;
    [SerializeField] private GameObject ResupplyVehicle;
    [SerializeField] private GameObject ResupplyVehiclePrefab;
    private GameObject WatchTowerUI;
    private GameObject FOB;
    private bool RepairMissionActive;
    private bool Captured;
    private Vector3 distance;


    void Start()
    {
        WatchTowerUI = GameObject.Find("ScreenCanvas").GetComponent<UIs>().WatchtowerUI;
    }

    private void FixedUpdate()
    {
        if (gameObject.GetComponent<life>().destroyed)
        {
            gameObject.GetComponent<life>().team = 0;
            gameObject.GetComponent<FieldOfView>().enabled = false;
            gameObject.GetComponent<FieldOfView>()._fieldOfView.SetActive(false);
        }
        else
        {
            gameObject.GetComponent<FieldOfView>().enabled = true;
            gameObject.GetComponent<FieldOfView>()._fieldOfView.SetActive(true);
        }

        if (RepairMissionActive)
        {
            distance = ResupplyVehicle.transform.position - gameObject.transform.position;


            if (distance.magnitude <= 2.9f)
            {
                Debug.Log("unit resupplied");
                gameObject.GetComponent<life>().armor = 1000;
                gameObject.GetComponent<life>().destroyed = false;
                gameObject.GetComponent<life>().team = ResupplyVehicle.GetComponent<life>().team;
                Destroy(ResupplyVehicle);
                RepairMissionActive = false;
            }
        }
    }

    public void watchTowerClick()
    {
        WatchTowerUI.SetActive(true);
    }

    public void CaptureTower()
    {
        Collider[] CaptureCandidates = Physics.OverlapSphere(transform.position, 15, _interactableLayer);

        if (CaptureCandidates.Length != 0)
        {
            foreach(Collider hit in CaptureCandidates)
            {
                if(hit.GetComponent<life>() != null)
                {
                    if (hit.gameObject.GetComponent<life>().team == GameObject.Find("TurnManager").GetComponent<turnManager>().currentTeam && hit.CompareTag("Interactable"))
                    {
                        gameObject.GetComponent<life>().team = GameObject.Find("TurnManager").GetComponent<turnManager>().currentTeam;
                    }
                }
            }
        }
    }

    public void RepairTower()
    {
        if (gameObject.GetComponent<life>().destroyed)
        {
            GameObject[] FOBs = GameObject.FindGameObjectsWithTag("FOB");

            foreach(GameObject Fob in FOBs)
            {
                if(Fob.GetComponent<life>().team == GameObject.Find("TurnManager").GetComponent<turnManager>().currentTeam)
                {
                    FOB = Fob;
                }
            }

            if (ResupplyVehicle == null)
            {
                ResupplyVehicle = Instantiate(ResupplyVehiclePrefab, new Vector3(FOB.transform.position.x - 3.7f, FOB.transform.position.y, FOB.transform.position.z - 20) , Quaternion.identity);
                Camera.main.GetComponent<Interactor>().interactable = ResupplyVehicle.GetComponent<Interactable>();
                ResupplyVehicle.GetComponent<life>().owner = gameObject;
                ResupplyVehicle.GetComponent<life>().team = GameObject.Find("TurnManager").GetComponent<turnManager>().currentTeam;
                ResupplyVehicle.GetComponent<evenSimplerAI>().enabled = true;
                ResupplyVehicle.GetComponent<NavMeshAgent>().enabled = true;
                ResupplyVehicle.GetComponent<evenSimplerAI>().walkPoint = gameObject.transform.position;
                ResupplyVehicle.GetComponent<evenSimplerAI>().walkPointSet = true;
            }

            RepairMissionActive = true;
        }

    }

    public void DestroyTower()
    {
        gameObject.GetComponent<life>().armor = 0;
    }

}
