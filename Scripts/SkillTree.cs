using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillTree : MonoBehaviour
{
    private GameObject[] FOBs;
    private GameObject unit1;
    private GameObject unit2;
    private GameObject unit3;
    private GameObject unit4;
    private GameObject unit5;
    private GameObject currentFob;

    [SerializeField] private TextMeshProUGUI _fobInfo;
    [SerializeField] private TextMeshProUGUI _unit1Info;
    [SerializeField] private TextMeshProUGUI _unit2Info;
    [SerializeField] private TextMeshProUGUI _unit3Info;
    [SerializeField] private TextMeshProUGUI _unit4Info;
    [SerializeField] private TextMeshProUGUI _unit5Info;

    public int CurrentSkillPointsFob;
    public int CurrentUnit1SkillPoints;
    public int CurrentUnit2SkillPoints;
    public int CurrentUnit3SkillPoints;
    public int CurrentUnit4SkillPoints;
    public int CurrentUnit5SkillPoints;
    public bool updateCurrents = true;
    public GameObject currentButton;


    private void FixedUpdate()
    {
        if (updateCurrents)
        {
            FOBs = GameObject.FindGameObjectsWithTag("FOB");

            foreach(GameObject FOB in FOBs)
            {
                if(FOB.GetComponent<life>().team == GameObject.Find("TurnManager").GetComponent<turnManager>().currentTeam)
                {
                    currentFob = FOB.gameObject;
                    unit1 = FOB.GetComponent<fobScript>().unit1;
                    unit2 = FOB.GetComponent<fobScript>().unit2;
                    unit3 = FOB.GetComponent<fobScript>().unit3;
                    unit4 = FOB.GetComponent<fobScript>().unit4;
                    unit5 = FOB.GetComponent<fobScript>().unit5;
                    updateCurrents = false;
                }
            }
        }
        else
        {
            CurrentSkillPointsFob = currentFob.GetComponent<fobScript>().fobSkillPoints;
            CurrentUnit1SkillPoints = currentFob.GetComponent<fobScript>().unit1SkillPoints;
            CurrentUnit2SkillPoints = currentFob.GetComponent<fobScript>().unit2SkillPoints;
            CurrentUnit3SkillPoints = currentFob.GetComponent<fobScript>().unit3SkillPoints;
            CurrentUnit4SkillPoints = currentFob.GetComponent<fobScript>().unit4SkillPoints;
            CurrentUnit5SkillPoints = currentFob.GetComponent<fobScript>().unit5SkillPoints;
            infoUpdater();
        }
    }

    private void infoUpdater()
    {
        _fobInfo.text = $"lvl: {currentFob.GetComponent<fobScript>().lvl} XP: {currentFob.GetComponent<fobScript>().xp} Target XP: {currentFob.GetComponent<fobScript>().xpTarget} Total XP: {currentFob.GetComponent<fobScript>().totalXp}  Skill points: {currentFob.GetComponent<fobScript>().fobSkillPoints}";
        _unit1Info.text = $"lvl: {unit1.GetComponent<playerScript>().lvl} XP: {unit1.GetComponent<playerScript>().xp} Target XP: {unit1.GetComponent<playerScript>().targetXp} Total XP: {unit1.GetComponent<playerScript>().totalXp} Skill points: {currentFob.GetComponent<fobScript>().unit1SkillPoints}";
        _unit2Info.text = $"lvl: {unit2.GetComponent<playerScript>().lvl} XP: {unit2.GetComponent<playerScript>().xp} Target XP: {unit2.GetComponent<playerScript>().targetXp} Total XP: {unit2.GetComponent<playerScript>().totalXp} Skill points: {currentFob.GetComponent<fobScript>().unit2SkillPoints}";
        _unit3Info.text = $"lvl: {unit3.GetComponent<playerScript>().lvl} XP: {unit3.GetComponent<playerScript>().xp} Target XP: {unit3.GetComponent<playerScript>().targetXp} Total XP: {unit3.GetComponent<playerScript>().totalXp} Skill points: {currentFob.GetComponent<fobScript>().unit3SkillPoints}";
        _unit4Info.text = $"lvl: {unit4.GetComponent<playerScript>().lvl} XP: {unit4.GetComponent<playerScript>().xp} Target XP: {unit4.GetComponent<playerScript>().targetXp} Total XP: {unit4.GetComponent<playerScript>().totalXp} Skill points: {currentFob.GetComponent<fobScript>().unit4SkillPoints}";
        _unit5Info.text = $"lvl: {unit5.GetComponent<playerScript>().lvl} XP: {unit5.GetComponent<playerScript>().xp} Target XP: {unit5.GetComponent<playerScript>().targetXp} Total XP: {unit5.GetComponent<playerScript>().totalXp} Skill points: {currentFob.GetComponent<fobScript>().unit5SkillPoints}";
    }

    public void commandClick(GameObject unitSkillTree)
    {
        if(currentFob.GetComponent<fobScript>().fobSkillPoints > 0)
        {
            unitSkillTree.SetActive(true);
            currentFob.GetComponent<fobScript>().fobSkillPoints--;
            currentFob.GetComponent<fobScript>().commandLvl++;
        }
    }


    public void fobSpeedClick()
    {
        GameObject[] units = { currentFob.GetComponent<fobScript>().unit1, currentFob.GetComponent<fobScript>().unit2, currentFob.GetComponent<fobScript>().unit3, currentFob.GetComponent<fobScript>().unit4, currentFob.GetComponent<fobScript>().unit5 };

        if(currentFob.GetComponent<fobScript>().fobSkillPoints > 0)
        {
            currentFob.GetComponent<Buffs>().speed += 0.1f;

            foreach (GameObject unit in units)
            {
                if(unit.GetComponent<playerScript>().unit != null)
                {
                    unit.GetComponent<playerScript>().unit.GetComponent<life>().normalSpeed += unit.GetComponent<playerScript>().unit.GetComponent<life>().normalSpeed * currentFob.GetComponent<Buffs>().speed;
                    unit.GetComponent<playerScript>().unit.GetComponent<life>().speed = unit.GetComponent<playerScript>().unit.GetComponent<life>().normalSpeed;
                }
            }

            currentFob.GetComponent<fobScript>().fobSkillPoints--;
        }
    }

    public void fobFuelConsumptClick()
    {
        GameObject[] units = { currentFob.GetComponent<fobScript>().unit1, currentFob.GetComponent<fobScript>().unit2, currentFob.GetComponent<fobScript>().unit3, currentFob.GetComponent<fobScript>().unit4, currentFob.GetComponent<fobScript>().unit5 };

        if (currentFob.GetComponent<fobScript>().fobSkillPoints > 0)
        {
            currentFob.GetComponent<Buffs>().fuelConsumption += 0.05f;

            foreach (GameObject unit in units)
            {
                if (unit.GetComponent<playerScript>().unit != null)
                {
                    unit.GetComponent<playerScript>().unit.GetComponent<life>().fuelConsumptionPerKM -= unit.GetComponent<playerScript>().unit.GetComponent<life>().fuelConsumptionPerKM * currentFob.GetComponent<Buffs>().fuelConsumption;
                }
            }

            currentFob.GetComponent<fobScript>().fobSkillPoints--;
        }
    }

    public void fobRangeClick()
    {
        GameObject[] units = { currentFob.GetComponent<fobScript>().unit1, currentFob.GetComponent<fobScript>().unit2, currentFob.GetComponent<fobScript>().unit3, currentFob.GetComponent<fobScript>().unit4, currentFob.GetComponent<fobScript>().unit5 };

        if (currentFob.GetComponent<fobScript>().fobSkillPoints > 0)
        {
            currentFob.GetComponent<Buffs>().range += 250;

            foreach (GameObject unit in units)
            {
                if (unit.GetComponent<playerScript>().unit != null)
                {
                    unit.GetComponent<playerScript>().unit.GetComponent<life>().range += 250;
                }
            }

            currentFob.GetComponent<fobScript>().fobSkillPoints--;
        }
    }

    public void FobArmorClick()
    {

        if (currentFob.GetComponent<fobScript>().fobSkillPoints > 0)
        {
            currentFob.GetComponent<Buffs>().armor += 500;
            currentFob.GetComponent<life>().armor += 500;
            currentFob.GetComponent<fobScript>().fobSkillPoints--;
        }
    }

    public void fobRadarClick()
    {

        if (currentFob.GetComponent<fobScript>().fobSkillPoints > 0)
        {
            currentFob.GetComponent<Buffs>().FieldOfView += 5;
            currentFob.GetComponent<life>().fieldOfVision += 5;
            currentFob.GetComponent<fobScript>().fobSkillPoints--;
        }
    }

    public void fobSupplySpeed()
    {
        GameObject[] units = GameObject.FindGameObjectsWithTag("Transport Truck");

        if (currentFob.GetComponent<fobScript>().fobSkillPoints > 0)
        {
            currentFob.GetComponent<Buffs>().resupplySpeed += 0.2f;

            if(units.Length != 0)
            {
                foreach (GameObject unit in units)
                {
                    unit.GetComponent<life>().speed += unit.GetComponent<life>().speed * currentFob.GetComponent<Buffs>().resupplySpeed;
                    unit.GetComponent<life>().normalSpeed = unit.GetComponent<life>().speed;
                }
            }

            currentFob.GetComponent<fobScript>().fobSkillPoints--;
        }
    }

    public void FobSupplyArmor()
    {
        GameObject[] units = GameObject.FindGameObjectsWithTag("Transport Truck");

        if (currentFob.GetComponent<fobScript>().fobSkillPoints > 0)
        {
            currentFob.GetComponent<Buffs>().resupplyArmor += 20;

            if(units.Length != 0)
            {
                foreach (GameObject unit in units)
                {
                    unit.GetComponent<life>().MaxArmor += currentFob.GetComponent<Buffs>().resupplyArmor;
                    unit.GetComponent<life>().armor += currentFob.GetComponent<Buffs>().resupplyArmor;
                }
            }

            currentFob.GetComponent<fobScript>().fobSkillPoints--;

        }
    }

    public void FobSupplyCounterMeasures()
    {
        GameObject[] units = GameObject.FindGameObjectsWithTag("Transport Truck");

        if (currentFob.GetComponent<fobScript>().fobSkillPoints > 0)
        {
            currentFob.GetComponent<Buffs>().counterMeasures = true;

            if (units.Length != 0)
            {
                foreach (GameObject unit in units)
                {
                    unit.GetComponent<life>().counterMeasures = true;
                }
            }

            currentFob.GetComponent<fobScript>().fobSkillPoints--;

        }
    }

    public void FobSupplySupplies()
    {
        GameObject[] units = { currentFob.GetComponent<fobScript>().unit1, currentFob.GetComponent<fobScript>().unit2, currentFob.GetComponent<fobScript>().unit3, currentFob.GetComponent<fobScript>().unit4, currentFob.GetComponent<fobScript>().unit5 };

        if (currentFob.GetComponent<fobScript>().fobSkillPoints > 0)
        {
            currentFob.GetComponent<Buffs>().MaxAmmo += 2;
            currentFob.GetComponent<Buffs>().MaxFuel += 50;

            foreach (GameObject unit in units)
            {
                unit.GetComponent<life>().MaxShots += (int)currentFob.GetComponent<Buffs>().MaxAmmo;
                unit.GetComponent<life>().maxFuel += (int)currentFob.GetComponent<Buffs>().MaxFuel;
            }

            currentFob.GetComponent<fobScript>().fobSkillPoints--;

        }
    }



    public void fobMoraleClick()
    {
        GameObject[] units = { currentFob.GetComponent<fobScript>().unit1, currentFob.GetComponent<fobScript>().unit2, currentFob.GetComponent<fobScript>().unit3, currentFob.GetComponent<fobScript>().unit4, currentFob.GetComponent<fobScript>().unit5 };

        if (currentFob.GetComponent<fobScript>().fobSkillPoints > 0)
        {
            currentFob.GetComponent<Buffs>().moraleLVL += 1;

            foreach (GameObject unit in units)
            {
                if (unit.GetComponent<playerScript>().unit != null)
                {
                    unit.GetComponent<playerScript>().unit.GetComponent<life>().moraleLVL += 1;
                }
            }

            currentFob.GetComponent<fobScript>().fobSkillPoints--;
        }
    }

    public void fobXpBoost()
    {
        GameObject[] units = { currentFob.GetComponent<fobScript>().unit1, currentFob.GetComponent<fobScript>().unit2, currentFob.GetComponent<fobScript>().unit3, currentFob.GetComponent<fobScript>().unit4, currentFob.GetComponent<fobScript>().unit5 };

        if (currentFob.GetComponent<fobScript>().fobSkillPoints > 0)
        {
            currentFob.GetComponent<Buffs>().xpBoost = true;

            foreach (GameObject unit in units)
            {
                if (unit.GetComponent<playerScript>().unit != null)
                {
                    unit.GetComponent<playerScript>().unit.GetComponent<life>().xpBoost = true;
                }
            }

            currentFob.GetComponent<fobScript>().fobSkillPoints--;
        }
    }

    public void fobSignalJammer()
    {
      

        if (currentFob.GetComponent<fobScript>().fobSkillPoints > 0)
        {
            currentFob.GetComponent<Buffs>().signalJammer = true;
            currentFob.GetComponent<signalJammer>().enabled = true;
            currentFob.GetComponent<fobScript>().fobSkillPoints--;
        }
    }

    public void fobAntiRad()
    {


        if (currentFob.GetComponent<fobScript>().fobSkillPoints > 0)
        {
            currentFob.GetComponent<Buffs>().antiRad = true;
            currentFob.GetComponent<life>().antiRad = true;
            currentFob.GetComponent<fobScript>().fobSkillPoints--;
        }
    }
    public void fobResupplyVehicleLVL()
    {


        if (currentFob.GetComponent<fobScript>().fobSkillPoints > 0)
        {
            currentFob.GetComponent<Buffs>().resupplyVehicleLVL++;
            currentFob.GetComponent<fobScript>().fobSkillPoints--;
        }
    }

    public void fobResupplyVehicleAmount()
    {


        if (currentFob.GetComponent<fobScript>().fobSkillPoints > 0)
        {
            currentFob.GetComponent<Buffs>().resupplyVehicleAmount++;
            currentFob.GetComponent<fobScript>().fobSkillPoints--;
        }
    }
    public void fobAirDefence()
    {


        if (currentFob.GetComponent<fobScript>().fobSkillPoints > 0)
        {
            currentFob.GetComponent<Buffs>().airDefenceLvl += 1;
            currentFob.GetComponent<life>().AirdefenceLvl += 1;
            currentFob.GetComponent<fobScript>().fobSkillPoints--;
        }
    }

    public void fobCommandCenter()
    {


        if (currentFob.GetComponent<fobScript>().fobSkillPoints > 0)
        {
            currentFob.GetComponent<Buffs>().commandCenter = true;
            currentFob.GetComponent<life>().commandCentre = true;
            currentFob.GetComponent<fobScript>().fobSkillPoints--;
        }
    }

    public void UnitArmor()
    {
        if (currentButton.GetComponent<SkillUnlocker>()._owner == 1)
        {
            if (currentFob.GetComponent<fobScript>().unit1SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit1.gameObject.GetComponent<Buffs>().armor += 0.2f;
                currentFob.GetComponent<fobScript>().unit1.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().armor += unit1.GetComponent<playerScript>().unit.GetComponent<life>().MaxArmor * unit1.GetComponent<Buffs>().armor;
                currentFob.GetComponent<fobScript>().unit1.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().MaxArmor += unit1.GetComponent<playerScript>().unit.GetComponent<life>().MaxArmor * unit1.GetComponent<Buffs>().armor;
                currentFob.GetComponent<fobScript>().unit1SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 2)
        {
            if (currentFob.GetComponent<fobScript>().unit2SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit2.gameObject.GetComponent<Buffs>().armor += 0.2f;
                currentFob.GetComponent<fobScript>().unit2.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().armor += unit2.GetComponent<playerScript>().unit.GetComponent<life>().MaxArmor * unit2.GetComponent<Buffs>().armor;
                currentFob.GetComponent<fobScript>().unit2.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().MaxArmor += unit2.GetComponent<playerScript>().unit.GetComponent<life>().MaxArmor * unit2.GetComponent<Buffs>().armor;
                currentFob.GetComponent<fobScript>().unit2SkillPoints--;
            }

        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 3)
        {
            if (currentFob.GetComponent<fobScript>().unit3SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit3.gameObject.GetComponent<Buffs>().armor += 0.2f;
                currentFob.GetComponent<fobScript>().unit3.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().armor += unit3.GetComponent<playerScript>().unit.GetComponent<life>().MaxArmor * unit3.GetComponent<Buffs>().armor;
                currentFob.GetComponent<fobScript>().unit3.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().MaxArmor += unit3.GetComponent<playerScript>().unit.GetComponent<life>().MaxArmor * unit3.GetComponent<Buffs>().armor;
                currentFob.GetComponent<fobScript>().unit3SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 4)
        {

            if (currentFob.GetComponent<fobScript>().unit4SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit4.gameObject.GetComponent<Buffs>().armor += 0.2f;
                currentFob.GetComponent<fobScript>().unit4.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().armor += unit4.GetComponent<playerScript>().unit.GetComponent<life>().MaxArmor * unit4.GetComponent<Buffs>().armor;
                currentFob.GetComponent<fobScript>().unit4.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().MaxArmor += unit4.GetComponent<playerScript>().unit.GetComponent<life>().MaxArmor * unit4.GetComponent<Buffs>().armor;
                currentFob.GetComponent<fobScript>().unit4SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 5)
        {

            if (currentFob.GetComponent<fobScript>().unit5SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit5.gameObject.GetComponent<Buffs>().armor += 0.2f;
                currentFob.GetComponent<fobScript>().unit5.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().armor += unit5.GetComponent<playerScript>().unit.GetComponent<life>().MaxArmor * unit5.GetComponent<Buffs>().armor;
                currentFob.GetComponent<fobScript>().unit5.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().MaxArmor += unit5.GetComponent<playerScript>().unit.GetComponent<life>().MaxArmor * unit5.GetComponent<Buffs>().armor;
                currentFob.GetComponent<fobScript>().unit5SkillPoints--;
            }
        }
    }

    public void UnitCamo()
    {
        if (currentButton.GetComponent<SkillUnlocker>()._owner == 1)
        {
            if (currentFob.GetComponent<fobScript>().unit1SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit1.gameObject.GetComponent<Buffs>().camoflage = true;
                currentFob.GetComponent<fobScript>().unit1.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().camoflage = true;
                currentFob.GetComponent<fobScript>().unit1SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 2)
        {
            if (currentFob.GetComponent<fobScript>().unit2SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit2.gameObject.GetComponent<Buffs>().camoflage = true;
                currentFob.GetComponent<fobScript>().unit2.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().camoflage = true;
                currentFob.GetComponent<fobScript>().unit2SkillPoints--;
            }

        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 3)
        {
            if (currentFob.GetComponent<fobScript>().unit3SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit3.gameObject.GetComponent<Buffs>().camoflage = true;
                currentFob.GetComponent<fobScript>().unit3.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().camoflage = true;
                currentFob.GetComponent<fobScript>().unit3SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 4)
        {

            if (currentFob.GetComponent<fobScript>().unit4SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit4.gameObject.GetComponent<Buffs>().camoflage = true;
                currentFob.GetComponent<fobScript>().unit4.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().camoflage = true;
                currentFob.GetComponent<fobScript>().unit4SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 5)
        {

            if (currentFob.GetComponent<fobScript>().unit5SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit5.gameObject.GetComponent<Buffs>().camoflage = true;
                currentFob.GetComponent<fobScript>().unit5.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().camoflage = true;
                currentFob.GetComponent<fobScript>().unit5SkillPoints--;
            }
        }
    }

    public void UnitSignalJammer()
    {
        if (currentButton.GetComponent<SkillUnlocker>()._owner == 1)
        {
            if (currentFob.GetComponent<fobScript>().unit1SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit1.gameObject.GetComponent<Buffs>().signalJammer = true;
                currentFob.GetComponent<fobScript>().unit1.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().signalJammer = true;
                currentFob.GetComponent<fobScript>().unit1SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 2)
        {
            if (currentFob.GetComponent<fobScript>().unit2SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit2.gameObject.GetComponent<Buffs>().signalJammer = true;
                currentFob.GetComponent<fobScript>().unit2.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().signalJammer = true;
                currentFob.GetComponent<fobScript>().unit2SkillPoints--;
            }

        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 3)
        {
            if (currentFob.GetComponent<fobScript>().unit3SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit3.gameObject.GetComponent<Buffs>().signalJammer = true;
                currentFob.GetComponent<fobScript>().unit3.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().signalJammer = true;
                currentFob.GetComponent<fobScript>().unit3SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 4)
        {

            if (currentFob.GetComponent<fobScript>().unit4SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit4.gameObject.GetComponent<Buffs>().signalJammer = true;
                currentFob.GetComponent<fobScript>().unit4.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().signalJammer = true;
                currentFob.GetComponent<fobScript>().unit4SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 5)
        {

            if (currentFob.GetComponent<fobScript>().unit5SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit5.gameObject.GetComponent<Buffs>().signalJammer = true;
                currentFob.GetComponent<fobScript>().unit5.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().signalJammer = true;
                currentFob.GetComponent<fobScript>().unit5SkillPoints--;
            }
        }
    }

    public void UnitRadar()
    {
        if (currentButton.GetComponent<SkillUnlocker>()._owner == 1)
        {
            if (currentFob.GetComponent<fobScript>().unit1SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit1.gameObject.GetComponent<Buffs>().FieldOfView += 2;
                currentFob.GetComponent<fobScript>().unit1.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().fieldOfVision += 2;
                currentFob.GetComponent<fobScript>().unit1SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 2)
        {
            if (currentFob.GetComponent<fobScript>().unit2SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit2.gameObject.GetComponent<Buffs>().FieldOfView += 2;
                currentFob.GetComponent<fobScript>().unit2.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().fieldOfVision += 2;
                currentFob.GetComponent<fobScript>().unit2SkillPoints--;
            }

        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 3)
        {
            if (currentFob.GetComponent<fobScript>().unit3SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit3.gameObject.GetComponent<Buffs>().FieldOfView += 2;
                currentFob.GetComponent<fobScript>().unit3.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().fieldOfVision += 2;
                currentFob.GetComponent<fobScript>().unit3SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 4)
        {

            if (currentFob.GetComponent<fobScript>().unit4SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit4.gameObject.GetComponent<Buffs>().FieldOfView += 2;
                currentFob.GetComponent<fobScript>().unit4.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().fieldOfVision += 2;
                currentFob.GetComponent<fobScript>().unit4SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 5)
        {

            if (currentFob.GetComponent<fobScript>().unit5SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit5.gameObject.GetComponent<Buffs>().FieldOfView += 2;
                currentFob.GetComponent<fobScript>().unit5.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().fieldOfVision += 2;
                currentFob.GetComponent<fobScript>().unit5SkillPoints--;
            }
        }
    }

    public void UnitFortify()
    {
        if (currentButton.GetComponent<SkillUnlocker>()._owner == 1)
        {
            if (currentFob.GetComponent<fobScript>().unit1SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit1.gameObject.GetComponent<Buffs>().defenceLvl += 1;
                currentFob.GetComponent<fobScript>().unit1.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().defenceLvl += 1;
                currentFob.GetComponent<fobScript>().unit1SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 2)
        {
            if (currentFob.GetComponent<fobScript>().unit2SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit2.gameObject.GetComponent<Buffs>().defenceLvl += 1;
                currentFob.GetComponent<fobScript>().unit2.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().defenceLvl += 1;
                currentFob.GetComponent<fobScript>().unit2SkillPoints--;
            }

        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 3)
        {
            if (currentFob.GetComponent<fobScript>().unit3SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit3.gameObject.GetComponent<Buffs>().defenceLvl += 1;
                currentFob.GetComponent<fobScript>().unit3.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().defenceLvl += 1;
                currentFob.GetComponent<fobScript>().unit3SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 4)
        {

            if (currentFob.GetComponent<fobScript>().unit4SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit4.gameObject.GetComponent<Buffs>().defenceLvl += 1;
                currentFob.GetComponent<fobScript>().unit4.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().defenceLvl += 1;
                currentFob.GetComponent<fobScript>().unit4SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 5)
        {

            if (currentFob.GetComponent<fobScript>().unit5SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit5.gameObject.GetComponent<Buffs>().defenceLvl += 1;
                currentFob.GetComponent<fobScript>().unit5.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().defenceLvl += 1;
                currentFob.GetComponent<fobScript>().unit5SkillPoints--;
            }
        }
    }

    public void UnitCounterMeasures()
    {
        if (currentButton.GetComponent<SkillUnlocker>()._owner == 1)
        {
            if (currentFob.GetComponent<fobScript>().unit1SkillPoints > 0)
            {
                if (!currentFob.GetComponent<fobScript>().unit1.gameObject.GetComponent<Buffs>().counterMeasures)
                {
                    currentFob.GetComponent<fobScript>().unit1.gameObject.GetComponent<Buffs>().counterMeasures = true;
                    currentFob.GetComponent<fobScript>().unit1.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().counterMeasures = true;
                    currentFob.GetComponent<fobScript>().unit1SkillPoints--;
                }
                else
                {
                    currentFob.GetComponent<fobScript>().unit1.gameObject.GetComponent<Buffs>().triangulate = true;
                    currentFob.GetComponent<fobScript>().unit1.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().triangulate = true;
                    currentFob.GetComponent<fobScript>().unit1SkillPoints--;
                }
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 2)
        {
            if (!currentFob.GetComponent<fobScript>().unit2.gameObject.GetComponent<Buffs>().counterMeasures)
            {
                currentFob.GetComponent<fobScript>().unit2.gameObject.GetComponent<Buffs>().counterMeasures = true;
                currentFob.GetComponent<fobScript>().unit2.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().counterMeasures = true;
                currentFob.GetComponent<fobScript>().unit2SkillPoints--;
            }
            else
            {
                currentFob.GetComponent<fobScript>().unit2.gameObject.GetComponent<Buffs>().triangulate = true;
                currentFob.GetComponent<fobScript>().unit2.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().triangulate = true;
                currentFob.GetComponent<fobScript>().unit2SkillPoints--;
            }

        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 3)
        {
            if (!currentFob.GetComponent<fobScript>().unit3.gameObject.GetComponent<Buffs>().counterMeasures)
            {
                currentFob.GetComponent<fobScript>().unit3.gameObject.GetComponent<Buffs>().counterMeasures = true;
                currentFob.GetComponent<fobScript>().unit3.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().counterMeasures = true;
                currentFob.GetComponent<fobScript>().unit3SkillPoints--;
            }
            else
            {
                currentFob.GetComponent<fobScript>().unit3.gameObject.GetComponent<Buffs>().triangulate = true;
                currentFob.GetComponent<fobScript>().unit3.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().triangulate = true;
                currentFob.GetComponent<fobScript>().unit3SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 4)
        {

            if (!currentFob.GetComponent<fobScript>().unit4.gameObject.GetComponent<Buffs>().counterMeasures)
            {
                currentFob.GetComponent<fobScript>().unit4.gameObject.GetComponent<Buffs>().counterMeasures = true;
                currentFob.GetComponent<fobScript>().unit4.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().counterMeasures = true;
                currentFob.GetComponent<fobScript>().unit4SkillPoints--;
            }
            else
            {
                currentFob.GetComponent<fobScript>().unit4.gameObject.GetComponent<Buffs>().triangulate = true;
                currentFob.GetComponent<fobScript>().unit4.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().triangulate = true;
                currentFob.GetComponent<fobScript>().unit4SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 5)
        {

            if (!currentFob.GetComponent<fobScript>().unit5.gameObject.GetComponent<Buffs>().counterMeasures)
            {
                currentFob.GetComponent<fobScript>().unit5.gameObject.GetComponent<Buffs>().counterMeasures = true;
                currentFob.GetComponent<fobScript>().unit5.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().counterMeasures = true;
                currentFob.GetComponent<fobScript>().unit5SkillPoints--;
            }
            else
            {
                currentFob.GetComponent<fobScript>().unit5.gameObject.GetComponent<Buffs>().triangulate = true;
                currentFob.GetComponent<fobScript>().unit5.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().triangulate = true;
                currentFob.GetComponent<fobScript>().unit5SkillPoints--;
            }
        }
    }

    public void UnitDroneActivator()
    {
        if (currentButton.GetComponent<SkillUnlocker>()._owner == 1)
        {
            if (currentFob.GetComponent<fobScript>().unit1SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit1.gameObject.GetComponent<Buffs>().drones += 1;
                currentFob.GetComponent<fobScript>().unit1.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().drones += 1;
                currentFob.GetComponent<fobScript>().unit1SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 2)
        {
            if (currentFob.GetComponent<fobScript>().unit2SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit2.gameObject.GetComponent<Buffs>().drones += 1;
                currentFob.GetComponent<fobScript>().unit2.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().drones += 1;
                currentFob.GetComponent<fobScript>().unit2SkillPoints--;
            }

        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 3)
        {
            if (currentFob.GetComponent<fobScript>().unit3SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit3.gameObject.GetComponent<Buffs>().drones += 1;
                currentFob.GetComponent<fobScript>().unit3.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().drones += 1;
                currentFob.GetComponent<fobScript>().unit3SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 4)
        {

            if (currentFob.GetComponent<fobScript>().unit4SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit4.gameObject.GetComponent<Buffs>().drones += 1;
                currentFob.GetComponent<fobScript>().unit4.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().drones += 1;
                currentFob.GetComponent<fobScript>().unit4SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 5)
        {

            if (currentFob.GetComponent<fobScript>().unit5SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit5.gameObject.GetComponent<Buffs>().drones += 1;
                currentFob.GetComponent<fobScript>().unit5.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().drones += 1;
                currentFob.GetComponent<fobScript>().unit5SkillPoints--;
            }
        }
    }

    public void UnitDroneRange()
    {
        if (currentButton.GetComponent<SkillUnlocker>()._owner == 1)
        {
            if (currentFob.GetComponent<fobScript>().unit1SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit1.gameObject.GetComponent<Buffs>().droneRange += 500;
                currentFob.GetComponent<fobScript>().unit1.gameObject.GetComponent<playerScript>().unit.GetComponent<DroneActivator>().drone.GetComponent<life>().range += 500;
                currentFob.GetComponent<fobScript>().unit1SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 2)
        {
            if (currentFob.GetComponent<fobScript>().unit2SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit2.gameObject.GetComponent<Buffs>().droneRange += 500;
                currentFob.GetComponent<fobScript>().unit2.gameObject.GetComponent<playerScript>().unit.GetComponent<DroneActivator>().drone.GetComponent<life>().range += 500;
                currentFob.GetComponent<fobScript>().unit2SkillPoints--;
            }

        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 3)
        {
            if (currentFob.GetComponent<fobScript>().unit3SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit3.gameObject.GetComponent<Buffs>().droneRange += 500;
                currentFob.GetComponent<fobScript>().unit3.gameObject.GetComponent<playerScript>().unit.GetComponent<DroneActivator>().drone.GetComponent<life>().range += 500;
                currentFob.GetComponent<fobScript>().unit3SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 4)
        {

            if (currentFob.GetComponent<fobScript>().unit4SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit4.gameObject.GetComponent<Buffs>().droneRange += 500;
                currentFob.GetComponent<fobScript>().unit4.gameObject.GetComponent<playerScript>().unit.GetComponent<DroneActivator>().drone.GetComponent<life>().range += 500;
                currentFob.GetComponent<fobScript>().unit4SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 5)
        {

            if (currentFob.GetComponent<fobScript>().unit5SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit5.gameObject.GetComponent<Buffs>().droneRange += 500;
                currentFob.GetComponent<fobScript>().unit5.gameObject.GetComponent<playerScript>().unit.GetComponent<DroneActivator>().drone.GetComponent<life>().range += 500;
                currentFob.GetComponent<fobScript>().unit5SkillPoints--;
            }
        }
    }

    public void UnitDroneBattery()
    {
        if (currentButton.GetComponent<SkillUnlocker>()._owner == 1)
        {
            if (currentFob.GetComponent<fobScript>().unit1SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit1.gameObject.GetComponent<Buffs>().DroneBattery += 50;
                currentFob.GetComponent<fobScript>().unit1.gameObject.GetComponent<playerScript>().unit.GetComponent<DroneActivator>().drone.GetComponent<life>().maxFuel += 50;
                currentFob.GetComponent<fobScript>().unit1.gameObject.GetComponent<playerScript>().unit.GetComponent<DroneActivator>().drone.GetComponent<life>().fuel += 50;
                currentFob.GetComponent<fobScript>().unit1SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 2)
        {
            if (currentFob.GetComponent<fobScript>().unit2SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit2.gameObject.GetComponent<Buffs>().DroneBattery += 50;
                currentFob.GetComponent<fobScript>().unit2.gameObject.GetComponent<playerScript>().unit.GetComponent<DroneActivator>().drone.GetComponent<life>().maxFuel += 50;
                currentFob.GetComponent<fobScript>().unit2.gameObject.GetComponent<playerScript>().unit.GetComponent<DroneActivator>().drone.GetComponent<life>().fuel += 50;
                currentFob.GetComponent<fobScript>().unit2SkillPoints--;
            }

        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 3)
        {
            if (currentFob.GetComponent<fobScript>().unit3SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit3.gameObject.GetComponent<Buffs>().DroneBattery += 50;
                currentFob.GetComponent<fobScript>().unit3.gameObject.GetComponent<playerScript>().unit.GetComponent<DroneActivator>().drone.GetComponent<life>().maxFuel += 50;
                currentFob.GetComponent<fobScript>().unit3.gameObject.GetComponent<playerScript>().unit.GetComponent<DroneActivator>().drone.GetComponent<life>().fuel += 50;
                currentFob.GetComponent<fobScript>().unit3SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 4)
        {

            if (currentFob.GetComponent<fobScript>().unit4SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit4.gameObject.GetComponent<Buffs>().DroneBattery += 50;
                currentFob.GetComponent<fobScript>().unit4.gameObject.GetComponent<playerScript>().unit.GetComponent<DroneActivator>().drone.GetComponent<life>().maxFuel += 50;
                currentFob.GetComponent<fobScript>().unit4.gameObject.GetComponent<playerScript>().unit.GetComponent<DroneActivator>().drone.GetComponent<life>().fuel += 50;
                currentFob.GetComponent<fobScript>().unit4SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 5)
        {

            if (currentFob.GetComponent<fobScript>().unit5SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit5.gameObject.GetComponent<Buffs>().DroneBattery += 50;
                currentFob.GetComponent<fobScript>().unit5.gameObject.GetComponent<playerScript>().unit.GetComponent<DroneActivator>().drone.GetComponent<life>().maxFuel += 50;
                currentFob.GetComponent<fobScript>().unit5.gameObject.GetComponent<playerScript>().unit.GetComponent<DroneActivator>().drone.GetComponent<life>().fuel += 50;
                currentFob.GetComponent<fobScript>().unit5SkillPoints--;
            }
        }
    }

    public void UnitDroneFieldOfVision()
    {
        if (currentButton.GetComponent<SkillUnlocker>()._owner == 1)
        {
            if (currentFob.GetComponent<fobScript>().unit1SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit1.gameObject.GetComponent<Buffs>().droneFov += 5;
                currentFob.GetComponent<fobScript>().unit1.gameObject.GetComponent<playerScript>().unit.GetComponent<DroneActivator>().drone.GetComponent<life>().fieldOfVision += 5;
                currentFob.GetComponent<fobScript>().unit1SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 2)
        {
            if (currentFob.GetComponent<fobScript>().unit2SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit2.gameObject.GetComponent<Buffs>().droneFov += 5;
                currentFob.GetComponent<fobScript>().unit2.gameObject.GetComponent<playerScript>().unit.GetComponent<DroneActivator>().drone.GetComponent<life>().fieldOfVision += 5;
                currentFob.GetComponent<fobScript>().unit2SkillPoints--;
            }

        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 3)
        {
            if (currentFob.GetComponent<fobScript>().unit3SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit3.gameObject.GetComponent<Buffs>().droneFov += 5;
                currentFob.GetComponent<fobScript>().unit3.gameObject.GetComponent<playerScript>().unit.GetComponent<DroneActivator>().drone.GetComponent<life>().fieldOfVision += 5;
                currentFob.GetComponent<fobScript>().unit3SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 4)
        {

            if (currentFob.GetComponent<fobScript>().unit4SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit4.gameObject.GetComponent<Buffs>().droneFov += 5;
                currentFob.GetComponent<fobScript>().unit4.gameObject.GetComponent<playerScript>().unit.GetComponent<DroneActivator>().drone.GetComponent<life>().fieldOfVision += 5;
                currentFob.GetComponent<fobScript>().unit4SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 5)
        {

            if (currentFob.GetComponent<fobScript>().unit5SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit5.gameObject.GetComponent<Buffs>().droneFov += 5;
                currentFob.GetComponent<fobScript>().unit5.gameObject.GetComponent<playerScript>().unit.GetComponent<DroneActivator>().drone.GetComponent<life>().fieldOfVision += 5;
                currentFob.GetComponent<fobScript>().unit5SkillPoints--;
            }
        }
    }

    public void UnitDroneGranadeDropper()
    {
        if (currentButton.GetComponent<SkillUnlocker>()._owner == 1)
        {
            if (currentFob.GetComponent<fobScript>().unit1SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit1.gameObject.GetComponent<Buffs>().granadeDropper = true;
                currentFob.GetComponent<fobScript>().unit1.gameObject.GetComponent<playerScript>().unit.GetComponent<DroneActivator>().drone.GetComponent<life>().canDropGranades = true;
                currentFob.GetComponent<fobScript>().unit1SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 2)
        {
            if (currentFob.GetComponent<fobScript>().unit2SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit2.gameObject.GetComponent<Buffs>().granadeDropper = true;
                currentFob.GetComponent<fobScript>().unit2.gameObject.GetComponent<playerScript>().unit.GetComponent<DroneActivator>().drone.GetComponent<life>().canDropGranades = true;
                currentFob.GetComponent<fobScript>().unit2SkillPoints--;
            }

        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 3)
        {
            if (currentFob.GetComponent<fobScript>().unit3SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit3.gameObject.GetComponent<Buffs>().granadeDropper = true;
                currentFob.GetComponent<fobScript>().unit3.gameObject.GetComponent<playerScript>().unit.GetComponent<DroneActivator>().drone.GetComponent<life>().canDropGranades = true;
                currentFob.GetComponent<fobScript>().unit3SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 4)
        {

            if (currentFob.GetComponent<fobScript>().unit4SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit4.gameObject.GetComponent<Buffs>().granadeDropper = true;
                currentFob.GetComponent<fobScript>().unit4.gameObject.GetComponent<playerScript>().unit.GetComponent<DroneActivator>().drone.GetComponent<life>().canDropGranades = true;
                currentFob.GetComponent<fobScript>().unit4SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 5)
        {

            if (currentFob.GetComponent<fobScript>().unit5SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit5.gameObject.GetComponent<Buffs>().granadeDropper = true;
                currentFob.GetComponent<fobScript>().unit5.gameObject.GetComponent<playerScript>().unit.GetComponent<DroneActivator>().drone.GetComponent<life>().canDropGranades = true;
                currentFob.GetComponent<fobScript>().unit5SkillPoints--;
            }
        }
    }

    public void UnitDroneGranadeAmmo()
    {
        if (currentButton.GetComponent<SkillUnlocker>()._owner == 1)
        {
            if (currentFob.GetComponent<fobScript>().unit1SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit1.gameObject.GetComponent<Buffs>().DroneAmmo += 1;
                currentFob.GetComponent<fobScript>().unit1.gameObject.GetComponent<playerScript>().unit.GetComponent<DroneActivator>().drone.GetComponent<life>().maximumShotsPerTurn += 1;
                currentFob.GetComponent<fobScript>().unit1SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 2)
        {
            if (currentFob.GetComponent<fobScript>().unit2SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit2.gameObject.GetComponent<Buffs>().DroneAmmo += 1;
                currentFob.GetComponent<fobScript>().unit2.gameObject.GetComponent<playerScript>().unit.GetComponent<DroneActivator>().drone.GetComponent<life>().maximumShotsPerTurn += 1;
                currentFob.GetComponent<fobScript>().unit2SkillPoints--;
            }

        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 3)
        {
            if (currentFob.GetComponent<fobScript>().unit3SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit3.gameObject.GetComponent<Buffs>().DroneAmmo += 1;
                currentFob.GetComponent<fobScript>().unit3.gameObject.GetComponent<playerScript>().unit.GetComponent<DroneActivator>().drone.GetComponent<life>().maximumShotsPerTurn += 1;
                currentFob.GetComponent<fobScript>().unit3SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 4)
        {

            if (currentFob.GetComponent<fobScript>().unit4SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit4.gameObject.GetComponent<Buffs>().DroneAmmo += 1;
                currentFob.GetComponent<fobScript>().unit4.gameObject.GetComponent<playerScript>().unit.GetComponent<DroneActivator>().drone.GetComponent<life>().maximumShotsPerTurn += 1;
                currentFob.GetComponent<fobScript>().unit4SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 5)
        {

            if (currentFob.GetComponent<fobScript>().unit5SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit5.gameObject.GetComponent<Buffs>().DroneAmmo += 1;
                currentFob.GetComponent<fobScript>().unit5.gameObject.GetComponent<playerScript>().unit.GetComponent<DroneActivator>().drone.GetComponent<life>().maximumShotsPerTurn += 1;
                currentFob.GetComponent<fobScript>().unit5SkillPoints--;
            }
        }
    }

    public void UnitDmg()
    {
        if (currentButton.GetComponent<SkillUnlocker>()._owner == 1)
        {
            if (currentFob.GetComponent<fobScript>().unit1SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit1.gameObject.GetComponent<Buffs>().dmg += 10;
                currentFob.GetComponent<fobScript>().unit1.GetComponent<playerScript>().unit.GetComponent<life>().bonusDMG += 10;
                currentFob.GetComponent<fobScript>().unit1SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 2)
        {
            if (currentFob.GetComponent<fobScript>().unit2SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit2.gameObject.GetComponent<Buffs>().dmg += 10;
                currentFob.GetComponent<fobScript>().unit2.GetComponent<playerScript>().unit.GetComponent<life>().bonusDMG += 10;
                currentFob.GetComponent<fobScript>().unit2SkillPoints--;
            }

        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 3)
        {
            if (currentFob.GetComponent<fobScript>().unit3SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit3.gameObject.GetComponent<Buffs>().dmg += 10;
                currentFob.GetComponent<fobScript>().unit3.GetComponent<playerScript>().unit.GetComponent<life>().bonusDMG += 10;
                currentFob.GetComponent<fobScript>().unit3SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 4)
        {

            if (currentFob.GetComponent<fobScript>().unit4SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit4.gameObject.GetComponent<Buffs>().dmg += 10;
                currentFob.GetComponent<fobScript>().unit4.GetComponent<playerScript>().unit.GetComponent<life>().bonusDMG += 10;
                currentFob.GetComponent<fobScript>().unit4SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 5)
        {

            if (currentFob.GetComponent<fobScript>().unit5SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit5.gameObject.GetComponent<Buffs>().dmg += 10;
                currentFob.GetComponent<fobScript>().unit5.GetComponent<playerScript>().unit.GetComponent<life>().bonusDMG += 10;
                currentFob.GetComponent<fobScript>().unit5SkillPoints--;
            }
        }
    }

    public void UnitVelocity()
    {
        if (currentButton.GetComponent<SkillUnlocker>()._owner == 1)
        {
            if (currentFob.GetComponent<fobScript>().unit1SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit1.gameObject.GetComponent<Buffs>().velocity += 50;
                currentFob.GetComponent<fobScript>().unit1.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().maximumForce += 50;
                currentFob.GetComponent<fobScript>().unit1SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 2)
        {
            if (currentFob.GetComponent<fobScript>().unit2SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit2.gameObject.GetComponent<Buffs>().velocity += 50;
                currentFob.GetComponent<fobScript>().unit2.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().maximumForce += 50;
                currentFob.GetComponent<fobScript>().unit2SkillPoints--;
            }

        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 3)
        {
            if (currentFob.GetComponent<fobScript>().unit3SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit3.gameObject.GetComponent<Buffs>().velocity += 50;
                currentFob.GetComponent<fobScript>().unit3.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().maximumForce += 50;
                currentFob.GetComponent<fobScript>().unit3SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 4)
        {

            if (currentFob.GetComponent<fobScript>().unit4SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit4.gameObject.GetComponent<Buffs>().velocity += 50;
                currentFob.GetComponent<fobScript>().unit4.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().maximumForce += 50;
                currentFob.GetComponent<fobScript>().unit4SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 5)
        {

            if (currentFob.GetComponent<fobScript>().unit5SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit5.gameObject.GetComponent<Buffs>().velocity += 50;
                currentFob.GetComponent<fobScript>().unit5.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().maximumForce += 50;
                currentFob.GetComponent<fobScript>().unit5SkillPoints--;
            }
        }
    }

    public void UnitAccuracy()//needs more work
    {
        if (currentButton.GetComponent<SkillUnlocker>()._owner == 1)
        {
            if (currentFob.GetComponent<fobScript>().unit1SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit1.gameObject.GetComponent<Buffs>().accuracy += 0.05f;
                currentFob.GetComponent<fobScript>().unit1.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().accuracy += 0.05f;
                currentFob.GetComponent<fobScript>().unit1SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 2)
        {
            if (currentFob.GetComponent<fobScript>().unit2SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit2.gameObject.GetComponent<Buffs>().accuracy += 0.05f;
                currentFob.GetComponent<fobScript>().unit2.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().accuracy += 0.05f;
                currentFob.GetComponent<fobScript>().unit2SkillPoints--;
            }

        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 3)
        {
            if (currentFob.GetComponent<fobScript>().unit3SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit3.gameObject.GetComponent<Buffs>().accuracy += 0.05f;
                currentFob.GetComponent<fobScript>().unit3.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().accuracy += 0.05f;
                currentFob.GetComponent<fobScript>().unit3SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 4)
        {

            if (currentFob.GetComponent<fobScript>().unit4SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit4.gameObject.GetComponent<Buffs>().accuracy += 0.05f;
                currentFob.GetComponent<fobScript>().unit4.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().accuracy += 0.05f;
                currentFob.GetComponent<fobScript>().unit4SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 5)
        {

            if (currentFob.GetComponent<fobScript>().unit5SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit5.gameObject.GetComponent<Buffs>().accuracy += 0.05f;
                currentFob.GetComponent<fobScript>().unit5.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().accuracy += 0.05f;
                currentFob.GetComponent<fobScript>().unit5SkillPoints--;
            }
        }
    }

    public void UnitShotsPerTurn()
    {
        if (currentButton.GetComponent<SkillUnlocker>()._owner == 1)
        {
            if (currentFob.GetComponent<fobScript>().unit1SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit1.gameObject.GetComponent<Buffs>().extraShots += 1;
                currentFob.GetComponent<fobScript>().unit1.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().maximumShotsPerTurn += 1;
                currentFob.GetComponent<fobScript>().unit1SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 2)
        {
            if (currentFob.GetComponent<fobScript>().unit2SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit2.gameObject.GetComponent<Buffs>().extraShots += 1;
                currentFob.GetComponent<fobScript>().unit2.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().maximumShotsPerTurn += 1;
                currentFob.GetComponent<fobScript>().unit2SkillPoints--;
            }

        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 3)
        {
            if (currentFob.GetComponent<fobScript>().unit3SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit3.gameObject.GetComponent<Buffs>().extraShots += 1;
                currentFob.GetComponent<fobScript>().unit3.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().maximumShotsPerTurn += 1;
                currentFob.GetComponent<fobScript>().unit3SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 4)
        {

            if (currentFob.GetComponent<fobScript>().unit4SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit4.gameObject.GetComponent<Buffs>().extraShots += 1;
                currentFob.GetComponent<fobScript>().unit4.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().maximumShotsPerTurn += 1;
                currentFob.GetComponent<fobScript>().unit4SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 5)
        {

            if (currentFob.GetComponent<fobScript>().unit5SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit5.gameObject.GetComponent<Buffs>().extraShots += 1;
                currentFob.GetComponent<fobScript>().unit5.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().maximumShotsPerTurn += 1;
                currentFob.GetComponent<fobScript>().unit5SkillPoints--;
            }
        }
    }

    public void ClusterAmmo()
    {
        if (currentButton.GetComponent<SkillUnlocker>()._owner == 1)
        {
            if (currentFob.GetComponent<fobScript>().unit1SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit1.gameObject.GetComponent<Buffs>().specialAmmo = true;
                currentFob.GetComponent<fobScript>().unit1.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().specialAmmo = true;
                currentFob.GetComponent<fobScript>().unit1SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 2)
        {
            if (currentFob.GetComponent<fobScript>().unit2SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit2.gameObject.GetComponent<Buffs>().specialAmmo = true;
                currentFob.GetComponent<fobScript>().unit2.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().specialAmmo = true;
                currentFob.GetComponent<fobScript>().unit2SkillPoints--;
            }

        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 3)
        {
            if (currentFob.GetComponent<fobScript>().unit3SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit3.gameObject.GetComponent<Buffs>().specialAmmo = true;
                currentFob.GetComponent<fobScript>().unit3.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().specialAmmo = true;
                currentFob.GetComponent<fobScript>().unit3SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 4)
        {

            if (currentFob.GetComponent<fobScript>().unit4SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit4.gameObject.GetComponent<Buffs>().specialAmmo = true;
                currentFob.GetComponent<fobScript>().unit4.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().specialAmmo = true;
                currentFob.GetComponent<fobScript>().unit4SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 5)
        {

            if (currentFob.GetComponent<fobScript>().unit5SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit5.gameObject.GetComponent<Buffs>().specialAmmo = true;
                currentFob.GetComponent<fobScript>().unit5.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().specialAmmo = true;
                currentFob.GetComponent<fobScript>().unit5SkillPoints--;
            }
        }
    }

    public void Nuke()
    {
        if (currentButton.GetComponent<SkillUnlocker>()._owner == 1)
        {
            if (currentFob.GetComponent<fobScript>().unit1SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit1.gameObject.GetComponent<Buffs>().nuke = true;
                currentFob.GetComponent<fobScript>().unit1.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().Nuke = true;
                currentFob.GetComponent<fobScript>().unit1SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 2)
        {
            if (currentFob.GetComponent<fobScript>().unit2SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit2.gameObject.GetComponent<Buffs>().nuke = true;
                currentFob.GetComponent<fobScript>().unit2.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().Nuke = true;
                currentFob.GetComponent<fobScript>().unit2SkillPoints--;
            }

        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 3)
        {
            if (currentFob.GetComponent<fobScript>().unit3SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit3.gameObject.GetComponent<Buffs>().nuke = true;
                currentFob.GetComponent<fobScript>().unit3.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().Nuke = true;
                currentFob.GetComponent<fobScript>().unit3SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 4)
        {

            if (currentFob.GetComponent<fobScript>().unit4SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit4.gameObject.GetComponent<Buffs>().nuke = true;
                currentFob.GetComponent<fobScript>().unit4.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().Nuke = true;
                currentFob.GetComponent<fobScript>().unit4SkillPoints--;
            }
        }
        else if (currentButton.GetComponent<SkillUnlocker>()._owner == 5)
        {

            if (currentFob.GetComponent<fobScript>().unit5SkillPoints > 0)
            {
                currentFob.GetComponent<fobScript>().unit5.gameObject.GetComponent<Buffs>().nuke = true;
                currentFob.GetComponent<fobScript>().unit5.gameObject.GetComponent<playerScript>().unit.GetComponent<life>().Nuke = true;
                currentFob.GetComponent<fobScript>().unit5SkillPoints--;
            }
        }
    }
}
