using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class evenSimplerAI : MonoBehaviour
{
    //visable Variables
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private LineRenderer lineRend;

    //Invisable variables
    private NavMeshAgent player;
    private Vector3 distanceToWalk;
    private float timeToArrival;

    public float trueDistance;
    public float dailyDistance;
    public float startDistance;
    public Vector3 walkPoint;
    public bool walkPointSet = false;
    public GameObject targetSplat;
    public GameObject selectUI;
    public GameObject fobPlayerUI;
    public float currentFuel;
    public bool maxDistanceReached;

    public void Start()
    {
        targetSplat = GameObject.Find("WalkSplat");
        selectUI = GameObject.Find("ScreenCanvas").GetComponent<UIs>().selectUI;
        fobPlayerUI = GameObject.Find("ScreenCanvas").GetComponent<UIs>().fobPlayerUI;
        lineRend.positionCount = 0;
        dailyDistance = 0;

        if (Camera.main)
        {
            if(Camera.main.GetComponent<Interactor>().interactable != null)
            {
                player = Camera.main.GetComponent<Interactor>().interactable.gameObject.GetComponent<NavMeshAgent>();
            }
            else
            {
                if (gameObject.CompareTag("Interactable"))
                {
                    player = fobPlayerUI.GetComponent<selection>().selectedPlayer.GetComponent<playerScript>().unit.gameObject.GetComponent<NavMeshAgent>();
                }
                else if (gameObject.CompareTag("Transport Truck"))
                {
                    player = fobPlayerUI.GetComponent<selection>().selectedPlayer.GetComponent<playerScript>().resupplyVehicle.gameObject.GetComponent<NavMeshAgent>();
                }
            }
        }
        else
        {
            player = gameObject.GetComponent<NavMeshAgent>();
        }

       
        if(player != null)
        {
            player.ResetPath();
            player.speed = player.gameObject.GetComponent<life>().speed;
            currentFuel = player.gameObject.GetComponent<life>().fuel;
        }
 
    }

    void FixedUpdate()
    {
        player.speed = player.gameObject.GetComponent<life>().speed;

        if (currentFuel > 0)
        {
            if (!walkPointSet)
            {
                RaycastHit hit;
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = 1500f;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);

                if (Physics.Raycast(Camera.main.transform.position, mousePos - transform.position, out hit, 1500, interactableLayer))
                {
                    if (hit.collider.CompareTag("Ground") && Input.GetButton("Fire1"))
                    {
                        walkPoint = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                        walkPointSet = true;
                    }

                }
            }
           else if(walkPointSet)
           {
                player.SetDestination(walkPoint);

                if(player.pathStatus == NavMeshPathStatus.PathInvalid)
                {
                    DestinationReached();
                }

                targetSplat.transform.SetParent(null);
                targetSplat.transform.position = new Vector3(walkPoint.x -  1f, walkPoint.y, walkPoint.z + 0.9f);

                if(gameObject.GetComponent<life>().team == GameObject.Find("TurnManager").GetComponent<turnManager>().currentTeam)
                {
                    targetSplat.GetComponent<MeshRenderer>().enabled = true;
                }
                else
                {
                    targetSplat.GetComponent<MeshRenderer>().enabled = false;
                }

                if (player.hasPath && player.gameObject.GetComponent<life>().team == GameObject.Find("TurnManager").GetComponent<turnManager>().currentTeam)
                {
                    lineRend.enabled = true;
                    drawPath();
                }
                else
                {
                    lineRend.enabled = false;
                }

                distanceToWalk = player.transform.position - walkPoint;

                float consumptionPerM = player.gameObject.GetComponent<life>().fuelConsumptionPerKM * 0.01f;

                float neededFuel = startDistance * consumptionPerM;

                if(player.gameObject.GetComponent<life>().fuel >= neededFuel)
                {
                    currentFuel = player.gameObject.GetComponent<life>().fuel - ((startDistance - trueDistance) * consumptionPerM);

                    player.GetComponent<life>().dailyDistance = (startDistance - trueDistance) + dailyDistance;

                    if ((startDistance - trueDistance) + dailyDistance > gameObject.GetComponent<life>().range)
                    {
                        player.speed = 0;
                        targetSplat.GetComponent<MeshRenderer>().enabled = false;

                        if (!maxDistanceReached)
                        {
                            maxDistanceReached = true;
                            gameObject.GetComponent<life>().fuel = currentFuel;
                        }
                    }
                    else
                    {
                        if (distanceToWalk.magnitude <= player.stoppingDistance && walkPointSet && !gameObject.CompareTag("Drone"))
                        {
                            DestinationReached();
                        }
                        else if(distanceToWalk.magnitude <= 3f && walkPointSet && gameObject.CompareTag("Drone"))
                        {
                            startDistance = 0;
                            DestinationReached();
                        }
                    }
                }
                else if(currentFuel < neededFuel)
                {
                    currentFuel = player.gameObject.GetComponent<life>().fuel - ((startDistance - trueDistance) * consumptionPerM);

                    player.GetComponent<life>().dailyDistance = (startDistance - trueDistance) + dailyDistance;

                    if ((startDistance - trueDistance) + dailyDistance > player.gameObject.GetComponent<life>().range)
                    {
                        player.speed = 0;
                        targetSplat.GetComponent<MeshRenderer>().enabled = false;

                        if (!maxDistanceReached)
                        {
                            maxDistanceReached = true;
                            gameObject.GetComponent<life>().fuel = currentFuel;
                        }
                    }
                    else if(currentFuel <= 0)
                    {
                        player.gameObject.GetComponent<life>().fuel = 0;
                        Debug.Log("Not enough fuel");
                    }



                }
           }
        }
        else
        {
            player.speed = 0;
        }

        if (gameObject.GetComponent<life>().destroyed)
        {
            DestinationReached();
        }
    }

    void drawPath()
    {
        lineRend.positionCount = player.path.corners.Length;
        lineRend.SetPosition(0, player.transform.position);

        if(player.path.corners.Length < 2)
        {
            return;
        }

        trueDistance = 0;

        for (int i = 1; i < player.path.corners.Length; i++)
        {
            Vector3 pointPos = new Vector3(player.path.corners[i].x, player.path.corners[i].y, player.path.corners[i].z);
            trueDistance += Vector3.Distance(player.path.corners[i - 1], player.path.corners[i]);//calculates distance between all points
            lineRend.SetPosition(i, pointPos);
        }

        timeToArrival = trueDistance / gameObject.GetComponent<life>().speed;

        if (startDistance == 0)
        {
            startDistance = trueDistance;
        }

    }

    public void DestinationReached()
    {
        if(gameObject.GetComponent<life>().speed != gameObject.GetComponent<life>().normalSpeed)
        {
            gameObject.GetComponent<life>().speed = gameObject.GetComponent<life>().normalSpeed;
        }

        dailyDistance = (startDistance - trueDistance) + dailyDistance;
        startDistance = 0;
        walkPointSet = false;
        lineRend.enabled = false;
        player.ResetPath();
        targetSplat.GetComponent<MeshRenderer>().enabled = false;
        player.gameObject.GetComponent<life>().fuel = currentFuel;
        returnOwners();
        player.GetComponent<evenSimplerAI>().enabled = false;
    }

    private void returnOwners()
    {
        GameObject[] drones = GameObject.FindGameObjectsWithTag("Drone");

        foreach (GameObject drone in drones)
        {
            drone.GetComponent<owner>().setParent();
        }
    }

    public void selectUIActivator()
    {
        selectUI.SetActive(true);
    }
}
