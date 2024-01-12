using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buffs : MonoBehaviour
{
    public float armor, speed, dmg, range, velocity, extraShots, MaxFuel, MaxAmmo, FieldOfView, accuracy, fuelConsumption, DroneAmmo, DroneBattery, drones, droneRange, droneFov, resupplyArmor, resupplySpeed, resupplyAmount;
    public bool granadeDropper, camoflage, specialAmmo, nuke, antiRad, counterMeasures, triangulate, xpBoost, resupplyCounterMeasures, signalJammer, commandCenter;
    public int moraleLVL, defenceLvl, airDefenceLvl, resupplyVehicleLVL, resupplyVehicleAmount;

    private GameObject unit;


    public void BuffMe()
    {
        unit = gameObject.GetComponent<playerScript>().unit;
        Debug.Log("Buffing unit");
        unit.GetComponent<life>().MaxArmor += gameObject.GetComponent<playerScript>().unit.GetComponent<life>().MaxArmor * armor;
        unit.GetComponent<life>().armor = gameObject.GetComponent<playerScript>().unit.GetComponent<life>().MaxArmor;
        unit.GetComponent<life>().normalSpeed += gameObject.GetComponent<playerScript>().unit.GetComponent<life>().normalSpeed * speed;
        unit.GetComponent<life>().speed = gameObject.GetComponent<playerScript>().unit.GetComponent<life>().normalSpeed;
        unit.GetComponent<life>().bonusDMG += dmg;
        unit.GetComponent<life>().range += range;
        unit.GetComponent<life>().maximumForce += velocity;
        unit.GetComponent<life>().maximumShotsPerTurn += (int)extraShots;
        unit.GetComponent<life>().maxFuel += MaxFuel;
        unit.GetComponent<life>().MaxShots += (int)MaxAmmo;
        unit.GetComponent<life>().fieldOfVision += FieldOfView;
        unit.GetComponent<life>().accuracy += accuracy;
        unit.GetComponent<life>().fuelConsumptionPerKM += gameObject.GetComponent<playerScript>().unit.GetComponent<life>().fuelConsumptionPerKM * fuelConsumption;
        unit.GetComponent<life>().maximumShotsPerTurn += moraleLVL;
        unit.GetComponent<life>().range += moraleLVL * 100;
        unit.GetComponent<life>().defenceLvl += defenceLvl;
        unit.GetComponent<life>().drones += (int)drones;
        unit.GetComponent<life>().camoflage = camoflage;
        unit.GetComponent<life>().specialAmmo = specialAmmo;
        unit.GetComponent<life>().Nuke = nuke;//To be implemented
        unit.GetComponent<life>().antiRad = antiRad;//To be implemented
        unit.GetComponent<life>().counterMeasures = counterMeasures;
        unit.GetComponent<life>().triangulate = triangulate;
        unit.GetComponent<life>().xpBoost = xpBoost;
        unit.GetComponent<life>().signalJammer = signalJammer;
        unit.GetComponent<DroneActivator>().drone.GetComponent<life>().maximumShotsPerTurn += (int)DroneAmmo;
        unit.GetComponent<DroneActivator>().drone.GetComponent<life>().maxFuel += DroneBattery;
        unit.GetComponent<DroneActivator>().drone.GetComponent<life>().fuel += DroneBattery;
        unit.GetComponent<DroneActivator>().drone.GetComponent<life>().range += droneRange;
        unit.GetComponent<DroneActivator>().drone.GetComponent<life>().fieldOfVision += droneFov;
        unit.GetComponent<DroneActivator>().drone.GetComponent<life>().canDropGranades = granadeDropper;
    }
}
