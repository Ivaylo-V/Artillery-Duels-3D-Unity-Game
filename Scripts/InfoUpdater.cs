using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoUpdater : MonoBehaviour
{

    [SerializeField] private GameObject vehicleText;
    [SerializeField] private GameObject droneText;

    private GameObject selected;

    private void Start()
    {
    }
    void FixedUpdate()
    {
        if(vehicleText.activeInHierarchy == true)
        {
            selected = Camera.main.GetComponent<Interactor>().interactable.gameObject;
            vehicleUpdate();
        }
    }

    private void vehicleUpdate()
    {
        string name = selected.GetComponent<life>().type;
        float armor = selected.GetComponent<life>().armor;
        float fuel = selected.GetComponent<life>().fuel;
        float maxfuel = selected.GetComponent<life>().maxFuel;
        float shotsLeft = selected.GetComponent<life>().ShotsLeft;
        float maxShots = selected.GetComponent<life>().MaxShots;
        float accuracy = selected.GetComponent<life>().accuracy;
        float FoV = selected.GetComponent<life>().fieldOfVision;
        float maxForce = selected.GetComponent<life>().maximumForce;
        float dmgMultiplier = selected.GetComponent<life>().bonusDMG;
        float driverHealth = selected.GetComponent<life>().driverHealth;
        float gunnerHealth = selected.GetComponent<life>().gunnerHealth;
        float engineerHealth = selected.GetComponent<life>().engineerHealth;
        float commanderHealth = selected.GetComponent<life>().commanderHealth;
        bool counterMeasures = selected.GetComponent<life>().counterMeasures;
        bool triangulate = selected.GetComponent<life>().triangulate;
        bool specialAmmo = selected.GetComponent<life>().specialAmmo;
        bool nuke = selected.GetComponent<life>().Nuke;
        bool signalJammer = selected.GetComponent<life>().signalJammer;
        bool camo = selected.GetComponent<life>().camoflage;
        bool AntiRad = selected.GetComponent<life>().antiRad;
        string tracks;
        string gun;
        string supplies;
        string command;
        string dugIn;

        if (selected.GetComponent<life>().canDrive)
        {
            tracks = "Tracks (Active)";
        }
        else
        {
            tracks = "Tracks (Disabled)";
        }

        if (selected.GetComponent<life>().canShoot)
        {
            gun = "Main Gun (Active)";
        }
        else
        {
            gun = "Main Gun (Disabled)";
        }

        if (selected.GetComponent<life>().canRepair)
        {
            supplies = "Supplies (Intact)";
        }
        else
        {
            supplies = "Supplies (Damaged)";
        }

        if (selected.GetComponent<life>().canCommand)
        {
            command = "Command (Active)";
        }
        else
        {
            command = "Command (Disabled)";
        }

        if (selected.GetComponent<life>().dugIn)
        {
            dugIn = "Vehicle is dug in.";
        }
        else
        {
            dugIn = "Vehicle is not dug in.";
        }

        vehicleText.GetComponent<TMPro.TextMeshProUGUI>().text = $@"{name}:
armor: {armor:f2}
fuel: {fuel:f2}/{maxfuel}
Ammo: {shotsLeft}/{maxShots}
Max Force: {maxForce}
Accuracy: {accuracy} - DMG Multiplier: {dmgMultiplier}
driver: {driverHealth:f2}% gunner: {gunnerHealth:f2}% engineer: {engineerHealth:f2}% commander: {commanderHealth:f2}%
{tracks}, {gun}, {supplies}, {command}, 
counter-measures:{counterMeasures} Triangulate:{triangulate} Signal Jammer:{signalJammer}
Special Ammo:{specialAmmo} Nuke:{nuke} Camo:{camo} Anti-Rad:{AntiRad}
{dugIn}";

    }

    private void DroneUpdate()
    {
        if(selected.GetComponent<life>().hasDrone == true)
        {

            string name = GameObject.Find($"{selected.name}/Drone").GetComponent<life>().type;
            float armor = GameObject.Find($"{selected.name}/Drone").GetComponent<life>().armor;
            float speed = GameObject.Find($"{selected.name}/Drone").GetComponent<life>().speed;
            float battery = GameObject.Find($"{selected.name}/Drone").GetComponent<life>().fuel;
            float fullBattery = GameObject.Find($"{selected.name}/Drone").GetComponent<life>().maxFuel;
            int shots = GameObject.Find($"{selected.name}/Drone").GetComponent<life>().maximumShotsPerTurn;
            int currentShots = GameObject.Find($"{selected.name}/Drone").GetComponent<life>().ShotsLeft;
            string granadeCapable;

            if (GameObject.Find($"{selected.name}/Drone").GetComponent<life>().canDropGranades)
            {
                granadeCapable = "Granade Dropper (Active)";
            }
            else
            {
                granadeCapable = "Granade Dropper (Disabled)";
            }


            droneText.GetComponent<TMPro.TextMeshProUGUI>().text = $@"{name}:
armor: {armor:f2}
battery: {battery}/{fullBattery}
speed: {speed:f2}
{granadeCapable}
Granades: {currentShots}/{shots}";
        }
        else
        {
            droneText.GetComponent<TMPro.TextMeshProUGUI>().text = "No Drone Available.";
        }
    }
}
