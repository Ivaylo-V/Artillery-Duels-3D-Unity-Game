using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class life : MonoBehaviour
{
    public string type = "T100 Angara";
    public int team;
    public float armor = 100;
    public float MaxArmor = 100;
    public float speed = 6;
    public float normalSpeed = 6;
    public float bonusDMG = 10;
    public float accuracy = 0.1f;
    public float reloadTime = 3.6f;
    public float fieldOfVision = 30f;
    public float maximumForce = 150;
    public float trueForce = 150;
    public int MaxShots = 25;
    public int maximumShotsPerTurn = 2;
    public int currentShots = 2;
    public int ShotsLeft;
    public float driverHealth = 100;
    public float gunnerHealth = 100;
    public float commanderHealth = 100;
    public float engineerHealth = 100;
    public bool canDrive;
    public bool canShoot;
    public bool canRepair;
    public bool canCommand;
    public bool canDropGranades;
    public bool dugIn;
    public int defenceLvl;
    public int AirdefenceLvl;
    public bool camoflage;
    public bool signalJammer;
    public float fuel = 100;
    public float maxFuel = 100;
    public float fuelConsumptionPerKM = 20.55f;
    public float range = 10000;
    public float dailyDistance;
    public bool destroyed;
    public bool turnOver;
    public bool hasDrone;
    public bool finishedShooting;
    public bool counterMeasures;
    public bool triangulate;
    public bool specialAmmo;
    public bool Nuke;
    public int drones;
    public int moraleLVL;
    public bool xpBoost;
    public bool antiRad;
    public bool commandCentre;
    public GameObject owner;




    private void Start()
    {
        ShotsLeft = MaxShots;
        dugIn = false;
        destroyed = false;
        finishedShooting = false;
    }

    private void FixedUpdate()
    {
        if(owner != null)
        {
            if (gameObject.CompareTag("Drone"))
            {
                Vector3 distance = owner.transform.position - gameObject.transform.position;

                if(distance.magnitude >= range)
                {
                    Debug.Log("loosing signal. Go back or you will lose this unit");

                    if(distance.magnitude - range >= 200)
                    {
                        destroyed = true;
                        gameObject.transform.position = new Vector3(owner.transform.position.x + 3, owner.transform.position.y + 3, owner.transform.position.z + 3);
                        gameObject.SetActive(false);
                    }

                }
            }
        }

            tankLife();

        if(driverHealth <= 0)
        {
            canDrive = false;

        }

        if (gunnerHealth <= 0)
        {
            canShoot = false;
        }

        if(engineerHealth <= 0)
        {
            canRepair = false;
        }

        if(commanderHealth <= 0)
        {
            canCommand = false;
        }

        infoUpdate();
    }

    private void tankLife()
    {
        if(armor <= 0)
        {
            destroyed = true;
            driverHealth = 0;
            gunnerHealth = 0;
            engineerHealth = 0;
            commanderHealth = 0;
            canCommand = false;
            canDrive = false;
            canRepair = false;
            canShoot = false;

            Invoke(nameof(destroyDestroyed), 10);
        }
    }

    public void Repair()
    {
        if(armor < 100)
        {
            armor += 25;

            if (armor > 100)
            {
                armor = 100;
            }

            canCommand = true;
            canDrive = true;
            canRepair = true;
            canShoot = true;

            turnOver = true;
        }
    }

    private void infoUpdate()
    {
        gameObject.GetComponent<Text>().text = $@"{type}:
Armor:{armor}% 
Fuel: {fuel:f2}/{maxFuel}
Speed:{speed} 
Ammo capacity:{MaxShots}
Max velocity: {maximumForce}
Signal Jammer: {signalJammer}";
    }
    
    private void destroyDestroyed()
    {
        if (gameObject.CompareTag("Interactable"))
        {
            Destroy(gameObject);
        }
        else if (gameObject.CompareTag("Drone"))
        {
            gameObject.transform.parent.GetComponent<DroneActivator>().resetDroneLife();
            gameObject.transform.parent.GetComponent<life>().hasDrone = false;
        }
        else if (gameObject.CompareTag("FOB"))
        {
            GameObject.Find("TurnManager").GetComponent<turnManager>().GameOver();
        }
    }
}
