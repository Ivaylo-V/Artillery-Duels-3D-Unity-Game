using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnManager : MonoBehaviour
{
    private GameObject[] interactables;
    private GameObject[] drones;
    private GameObject[] fob;

    public int currentTeam;
    public int maxTeams = 2;

    [SerializeField] private GameObject fobPrefab, _projector;
    [SerializeField] private Material[] _FoWMaterials;
    [SerializeField] private GameObject[] _playerFoWCams;
    [SerializeField] private GameObject _skillTree;
    [SerializeField] private GameObject _worldMap;

    public GameObject chosenQuadrant;

    private void Start()
    {
        currentTeam = 1;
        maxTeams = PlayerPrefs.GetInt("Players");
        _playerFoWCams[0].SetActive(true);
    }

    private void Update()
    {
        fob = GameObject.FindGameObjectsWithTag("FOB");

        if (chosenQuadrant == null)
        {
            _worldMap.SetActive(true);
        }
        else if (fob.Length < maxTeams && chosenQuadrant != null)
        {
            Camera.main.GetComponent<Interactor>().enabled = false;
            newGameBaby();
        }

    }

    public void newGameBaby()
    {
            GameObject fobSpawn = GameObject.Find($"{chosenQuadrant.name}/FobSpawn");
            GameObject fobInstance = (GameObject)Instantiate(fobPrefab, new Vector3(fobSpawn.transform.position.x, fobSpawn.transform.position.y + 10, fobSpawn.transform.position.z), Quaternion.identity);
            fobInstance.name = $"{fobInstance.name} {currentTeam}";
            fobInstance.GetComponent<life>().team = currentTeam;
            fobInstance.GetComponent<life>().owner = GameObject.Find("FOBHolder");
            Camera.main.GetComponent<Interactor>().enabled = true;
            fobSpawn.GetComponent<BoxCollider>().enabled = false;
            chosenQuadrant = null;
            manualNewTurn();

    }

    public void manualNewTurn()
    {
        _skillTree.GetComponent<SkillTree>().updateCurrents = true;
        Camera.main.GetComponent<Interactor>().interactable = null;

        if (currentTeam < maxTeams)
        {
            currentTeam++;
        }
        else
        {
            currentTeam = 1;
        }

        _skillTree.GetComponent<skillTreeUpdater>().skillTreePlayers();

        foreach (GameObject playerCam in _playerFoWCams)
        {
            playerCam.SetActive(false);
        }

        _playerFoWCams[currentTeam - 1].SetActive(true);

        _projector.GetComponent<Projector>().material = _FoWMaterials[currentTeam - 1];

        interactables = GameObject.FindGameObjectsWithTag("Interactable");
        drones = GameObject.FindGameObjectsWithTag("Drone");
        fob = GameObject.FindGameObjectsWithTag("FOB");

        foreach(GameObject Fob in fob)
        {
            Fob.GetComponent<FieldOfView>().startNewTurnBaby();
        }

        foreach (GameObject obj in interactables)
        {
            obj.GetComponent<FieldOfView>().startNewTurnBaby();

            if(obj.GetComponent<life>().team == currentTeam)
            {
                obj.GetComponent<life>().turnOver = false;
                obj.GetComponent<evenSimplerAI>().startDistance = 0;
                obj.GetComponent<evenSimplerAI>().dailyDistance = 0;
                obj.GetComponent<evenSimplerAI>().maxDistanceReached = false;
            }
            else
            {
                GameObject.Find($"{obj.name}/Selection").gameObject.GetComponent<MeshRenderer>().enabled = false;
            }
        }

        foreach (GameObject drone in drones)
        {
            drone.GetComponent<FieldOfView>().startNewTurnBaby();

            if (drone.GetComponent<life>().team == currentTeam)
            {

                drone.GetComponent<life>().turnOver = false;
                drone.GetComponent<life>().finishedShooting = false;
                drone.GetComponent<life>().fuel = drone.GetComponent<life>().maxFuel * 0.2f;
            }
            else
            {
                GameObject.Find($"{drone.name}/Selection").gameObject.GetComponent<MeshRenderer>().enabled = false;
            }
        }

    }

    public void GameOver()
    {
        Debug.Log("GG");
    }

}
