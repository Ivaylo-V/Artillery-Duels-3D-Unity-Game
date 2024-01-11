using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buffs : MonoBehaviour
{
    public float armor, speed, dmg, range, velocity, extraShots, MaxFuel, MaxAmmo, FieldOfView, accuracy, fuelConsumption, DroneAmmo, DroneBattery, drones, droneRange, droneFov, resupplyArmor, resupplySpeed, resupplyAmount;
    public bool granadeDropper, camoflage, specialAmmo, nuke, antiRad, counterMeasures, triangulate, xpBoost, resupplyCounterMeasures, signalJammer, commandCenter;
    public int moraleLVL, defenceLvl, airDefenceLvl, resupplyVehicleLVL, resupplyVehicleAmount;

    public void BuffMe()
    {
        Debug.Log("Buffing unit");
        gameObject.GetComponent<playerScript>().unit.GetComponent<life>().MaxArmor += gameObject.GetComponent<playerScript>().unit.GetComponent<life>().MaxArmor * armor;
        gameObject.GetComponent<playerScript>().unit.GetComponent<life>().armor = gameObject.GetComponent<playerScript>().unit.GetComponent<life>().MaxArmor;
        gameObject.GetComponent<playerScript>().unit.GetComponent<life>().normalSpeed += gameObject.GetComponent<playerScript>().unit.GetComponent<life>().normalSpeed * speed;
        gameObject.GetComponent<playerScript>().unit.GetComponent<life>().speed = gameObject.GetComponent<playerScript>().unit.GetComponent<life>().normalSpeed;
        gameObject.GetComponent<playerScript>().unit.GetComponent<life>().bonusDMG += dmg;
        gameObject.GetComponent<playerScript>().unit.GetComponent<life>().range += range;
        gameObject.GetComponent<playerScript>().unit.GetComponent<life>().maximumForce += velocity;
        gameObject.GetComponent<playerScript>().unit.GetComponent<life>().maximumShotsPerTurn += (int)extraShots;
        gameObject.GetComponent<playerScript>().unit.GetComponent<life>().maxFuel += MaxFuel;
        gameObject.GetComponent<playerScript>().unit.GetComponent<life>().MaxShots += (int)MaxAmmo;
        gameObject.GetComponent<playerScript>().unit.GetComponent<life>().fieldOfVision += FieldOfView;
        gameObject.GetComponent<playerScript>().unit.GetComponent<life>().accuracy += accuracy;
        gameObject.GetComponent<playerScript>().unit.GetComponent<life>().fuelConsumptionPerKM += gameObject.GetComponent<playerScript>().unit.GetComponent<life>().fuelConsumptionPerKM * fuelConsumption;
        gameObject.GetComponent<playerScript>().unit.GetComponent<life>().maximumShotsPerTurn += moraleLVL;
        gameObject.GetComponent<playerScript>().unit.GetComponent<life>().range += moraleLVL * 100;
        gameObject.GetComponent<playerScript>().unit.GetComponent<life>().defenceLvl += defenceLvl;
        gameObject.GetComponent<playerScript>().unit.GetComponent<life>().drones += (int)drones;
        gameObject.GetComponent<playerScript>().unit.GetComponent<life>().camoflage = camoflage;
        gameObject.GetComponent<playerScript>().unit.GetComponent<life>().specialAmmo = specialAmmo;
        gameObject.GetComponent<playerScript>().unit.GetComponent<life>().Nuke = nuke;//To be implemented
        gameObject.GetComponent<playerScript>().unit.GetComponent<life>().antiRad = antiRad;//To be implemented
        gameObject.GetComponent<playerScript>().unit.GetComponent<life>().counterMeasures = counterMeasures;
        gameObject.GetComponent<playerScript>().unit.GetComponent<life>().triangulate = triangulate;
        gameObject.GetComponent<playerScript>().unit.GetComponent<life>().xpBoost = xpBoost;
        gameObject.GetComponent<playerScript>().unit.GetComponent<life>().signalJammer = signalJammer;
        gameObject.GetComponent<playerScript>().unit.GetComponent<DroneActivator>().drone.GetComponent<life>().maximumShotsPerTurn += (int)DroneAmmo;
        gameObject.GetComponent<playerScript>().unit.GetComponent<DroneActivator>().drone.GetComponent<life>().maxFuel += DroneBattery;
        gameObject.GetComponent<playerScript>().unit.GetComponent<DroneActivator>().drone.GetComponent<life>().fuel += DroneBattery;
        gameObject.GetComponent<playerScript>().unit.GetComponent<DroneActivator>().drone.GetComponent<life>().range += droneRange;
        gameObject.GetComponent<playerScript>().unit.GetComponent<DroneActivator>().drone.GetComponent<life>().fieldOfVision += droneFov;
        gameObject.GetComponent<playerScript>().unit.GetComponent<DroneActivator>().drone.GetComponent<life>().canDropGranades = granadeDropper;
    }
}
