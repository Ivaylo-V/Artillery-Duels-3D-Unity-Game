using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.AI;

public class Interactor : MonoBehaviour
{
    [SerializeField] private LayerMask InteractableLayerMask;
    [SerializeField] private GameObject DroneUI;
    [SerializeField] private GameObject SelectUI;
    [SerializeField] private GameObject unitUI;
    [SerializeField] private GameObject FobUI;
    [SerializeField] private GameObject FobPlayerUI;
    [SerializeField] private GameObject skillTreeUI;
    [SerializeField] private GameObject ShellUI;
    [SerializeField] private GameObject WatchTowerUI;
    [SerializeField] private GameObject TownBaseUI;
    [SerializeField] private GameObject worldMap;
    [SerializeField] private GameObject infoBox;
    [SerializeField] private GameObject otherUINew;
    [SerializeField] private GameObject enemyInfo;
    [SerializeField] private GameObject mapSplat;
    [SerializeField] private GameObject turnManager;

    public Interactable interactable = null;

    private void Start()
    {
        interactable = null;
    }
    void Update()
    {


        RaycastHit hit;


        if (!SelectUI.activeInHierarchy && !worldMap.activeInHierarchy && !TownBaseUI.activeInHierarchy && !ShellUI.activeInHierarchy && !WatchTowerUI.activeInHierarchy && !otherUINew.activeInHierarchy && !skillTreeUI.activeInHierarchy && !DroneUI.activeInHierarchy && !enemyInfo.activeInHierarchy && !FobUI.activeInHierarchy && !FobPlayerUI.activeInHierarchy)//can be improved
        {

            if (Input.GetButton("Fire1") && Cursor.lockState != CursorLockMode.Locked)
            {
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = 1500f;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);

                if (Physics.Raycast(transform.position, mousePos - transform.position, out hit, 1500, InteractableLayerMask))
                {
                    if (hit.collider.CompareTag("Ground"))
                    {
                        return;
                    }

                    if (interactable == null || interactable.ID != hit.collider.GetComponent<Interactable>().ID)
                    {
                            interactable = hit.collider.GetComponent<Interactable>();
                    }

                    if( interactable != null && interactable.GetComponent<life>().team == turnManager.GetComponent<turnManager>().currentTeam)
                    {
                        if (hit.collider.CompareTag("FOB"))
                        {
                            GameObject.Find($"{interactable.name}/Selection").gameObject.GetComponent<MeshRenderer>().enabled = true;
                            Camera.main.transform.parent.gameObject.transform.position = new Vector3(hit.collider.transform.position.x, Camera.main.transform.parent.gameObject.transform.position.y, hit.collider.transform.position.z);
                            interactable.onInteract.Invoke();
                        }
                        else if (hit.collider.CompareTag("Interactable") && !hit.collider.GetComponent<NavMeshAgent>().hasPath)
                        {
                            GameObject.Find($"{interactable.name}/Selection").gameObject.GetComponent<MeshRenderer>().enabled = true;
                            infoBox.GetComponent<TMPro.TextMeshProUGUI>().text = hit.collider.GetComponent<Text>().text;
                            Camera.main.transform.parent.gameObject.transform.position = new Vector3(hit.collider.transform.position.x, Camera.main.transform.parent.gameObject.transform.position.y, hit.collider.transform.position.z);
                            interactable.onInteract.Invoke();
                        }
                        else if (hit.collider.GetComponent<NavMeshAgent>().hasPath && hit.collider.CompareTag("Interactable"))
                        {
                            if(hit.collider.GetComponent<evenSimplerAI>().startDistance + hit.collider.GetComponent<evenSimplerAI>().dailyDistance <= hit.collider.GetComponent<life>().range)
                            { 
                                YesNoBox.Instance.ShowQuestion("Would you like to fast travel to the selected location?", () =>
                                {
                                    hit.collider.GetComponent<life>().speed = 100;

                                }, () =>
                                {
                                    GameObject.Find($"{interactable.name}/Selection").gameObject.GetComponent<MeshRenderer>().enabled = true;
                                    infoBox.GetComponent<TMPro.TextMeshProUGUI>().text = hit.collider.GetComponent<Text>().text;
                                    Camera.main.transform.parent.gameObject.transform.position = new Vector3(hit.collider.transform.position.x, Camera.main.transform.parent.gameObject.transform.position.y, hit.collider.transform.position.z);
                                    interactable.onInteract.Invoke();
                                });
                            }
                            else
                            {
                                GameObject.Find($"{interactable.name}/Selection").gameObject.GetComponent<MeshRenderer>().enabled = true;
                                infoBox.GetComponent<TMPro.TextMeshProUGUI>().text = hit.collider.GetComponent<Text>().text;
                                Camera.main.transform.parent.gameObject.transform.position = new Vector3(hit.collider.transform.position.x, Camera.main.transform.parent.gameObject.transform.position.y, hit.collider.transform.position.z);
                                interactable.onInteract.Invoke();
                            }
                        }
                        else if(hit.collider.CompareTag("Transport Truck"))
                        {
                            enemyInfo.SetActive(true);
                            GameObject.Find($"{interactable.name}/Selection").gameObject.GetComponent<MeshRenderer>().enabled = true;
                            enemyInfo.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = hit.collider.GetComponent<Text>().text;
                            Camera.main.transform.parent.gameObject.transform.position = new Vector3(hit.collider.transform.position.x, Camera.main.transform.parent.gameObject.transform.position.y, hit.collider.transform.position.z);
                        }
                        else if (hit.collider.CompareTag("Drone"))
                        {
                            GameObject.Find($"{interactable.name}/Selection").gameObject.GetComponent<MeshRenderer>().enabled = true;
                            infoBox.GetComponent<TMPro.TextMeshProUGUI>().text = hit.collider.GetComponent<Text>().text;
                            Camera.main.transform.parent.gameObject.transform.position = new Vector3(hit.collider.transform.position.x, Camera.main.transform.parent.gameObject.transform.position.y, hit.collider.transform.position.z);
                            interactable.onInteract.Invoke();
                        }
                        else if (hit.collider.CompareTag("Watch Tower"))
                        {
                            GameObject.Find($"{interactable.name}/Selection").gameObject.GetComponent<MeshRenderer>().enabled = true;
                            infoBox.GetComponent<TMPro.TextMeshProUGUI>().text = hit.collider.GetComponent<Text>().text;
                            Camera.main.transform.parent.gameObject.transform.position = new Vector3(hit.collider.transform.position.x, Camera.main.transform.parent.gameObject.transform.position.y, hit.collider.transform.position.z);
                            interactable.onInteract.Invoke();
                        }
                        else if (hit.collider.CompareTag("Town Base"))
                        {
                            GameObject.Find($"{interactable.name}/Selection").gameObject.GetComponent<MeshRenderer>().enabled = true;
                            infoBox.GetComponent<TMPro.TextMeshProUGUI>().text = hit.collider.GetComponent<Text>().text;
                            Camera.main.transform.parent.gameObject.transform.position = new Vector3(hit.collider.transform.position.x, Camera.main.transform.parent.gameObject.transform.position.y, hit.collider.transform.position.z);
                            interactable.onInteract.Invoke();
                        }
                        else if (hit.collider.CompareTag("Small City"))
                        {
                            enemyInfo.SetActive(true);
                            GameObject.Find($"{interactable.name}/Selection").gameObject.GetComponent<MeshRenderer>().enabled = true;
                            enemyInfo.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = hit.collider.GetComponent<Text>().text;
                            Camera.main.transform.parent.gameObject.transform.position = new Vector3(hit.collider.transform.position.x, Camera.main.transform.parent.gameObject.transform.position.y, hit.collider.transform.position.z);
                        }

                    }
                    else if(interactable != null)
                    {
                        if(interactable.GetComponent<life>().team == 0)
                        {
                            if (hit.collider.CompareTag("Watch Tower"))
                            {
                                GameObject.Find($"{interactable.name}/Selection").gameObject.GetComponent<MeshRenderer>().enabled = true;
                                infoBox.GetComponent<TMPro.TextMeshProUGUI>().text = hit.collider.GetComponent<Text>().text;
                                Camera.main.transform.parent.gameObject.transform.position = new Vector3(hit.collider.transform.position.x, Camera.main.transform.parent.gameObject.transform.position.y, hit.collider.transform.position.z);
                                interactable.onInteract.Invoke();
                            }
                            else if (hit.collider.CompareTag("Town Base"))
                            {
                                GameObject.Find($"{interactable.name}/Selection").gameObject.GetComponent<MeshRenderer>().enabled = true;
                                infoBox.GetComponent<TMPro.TextMeshProUGUI>().text = hit.collider.GetComponent<Text>().text;
                                Camera.main.transform.parent.gameObject.transform.position = new Vector3(hit.collider.transform.position.x, Camera.main.transform.parent.gameObject.transform.position.y, hit.collider.transform.position.z);
                                interactable.onInteract.Invoke();
                            }
                            else if (hit.collider.CompareTag("Small City"))
                            {
                                GameObject.Find($"{interactable.name}/Selection").gameObject.GetComponent<MeshRenderer>().enabled = true;
                                infoBox.GetComponent<TMPro.TextMeshProUGUI>().text = hit.collider.GetComponent<Text>().text;
                                Camera.main.transform.parent.gameObject.transform.position = new Vector3(hit.collider.transform.position.x, Camera.main.transform.parent.gameObject.transform.position.y, hit.collider.transform.position.z);
                                interactable.onInteract.Invoke();
                            }
                            else if (hit.collider.CompareTag("Cross Border Checkpoint"))
                            {
                                GameObject.Find($"{interactable.name}/Selection").gameObject.GetComponent<MeshRenderer>().enabled = true;
                                infoBox.GetComponent<TMPro.TextMeshProUGUI>().text = hit.collider.GetComponent<Text>().text;
                                Camera.main.transform.parent.gameObject.transform.position = new Vector3(hit.collider.transform.position.x, Camera.main.transform.parent.gameObject.transform.position.y, hit.collider.transform.position.z);
                                interactable.onInteract.Invoke();
                            }
                        }
                        else
                        {
                            enemyInfo.SetActive(true);
                            GameObject.Find($"{interactable.name}/Selection").gameObject.GetComponent<MeshRenderer>().enabled = true;
                            enemyInfo.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = hit.collider.GetComponent<Text>().text;
                            Camera.main.transform.parent.gameObject.transform.position = new Vector3(hit.collider.transform.position.x, Camera.main.transform.parent.gameObject.transform.position.y, hit.collider.transform.position.z);
                        }
                    }
                }
            }
        }
        else
        {
            if (Input.GetButton("Fire2"))
            {
                if(interactable != null)
                {
                    GameObject.Find($"{interactable.name}/Selection").gameObject.GetComponent<MeshRenderer>().enabled = false;
                }

                interactable = null;
                closeUI();
            }
        }

    }
    public void closeUI()
    {
        skillTreeUI.SetActive(false);
        DroneUI.SetActive(false);
        SelectUI.SetActive(false);
        enemyInfo.SetActive(false);
        FobUI.SetActive(false);
        FobPlayerUI.SetActive(false);
        unitUI.SetActive(true);
        otherUINew.SetActive(false);
        ShellUI.SetActive(false);
        WatchTowerUI.SetActive(false);
        TownBaseUI.SetActive(false);
        worldMap.SetActive(false);
    }

    public void findMyDrone()
    {
        if (interactable != null)
        {
            GameObject.Find($"{interactable.name}/Selection").gameObject.GetComponent<MeshRenderer>().enabled = false;
            interactable = GameObject.Find($"{interactable.name}/Drone").gameObject.GetComponent<Interactable>();
            GameObject.Find($"{interactable.name}/Selection").gameObject.GetComponent<MeshRenderer>().enabled = true;
            Camera.main.transform.parent.gameObject.GetComponent<CharacterController>().enabled = false;
            Camera.main.transform.parent.gameObject.transform.position = new Vector3(interactable.gameObject.transform.position.x, Camera.main.transform.parent.gameObject.transform.position.y, interactable.gameObject.transform.position.z);
            Camera.main.transform.parent.gameObject.GetComponent<CharacterController>().enabled = true;
        }
    }
}

