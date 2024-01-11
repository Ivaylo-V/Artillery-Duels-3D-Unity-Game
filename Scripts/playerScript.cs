using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class playerScript : MonoBehaviour
{
    [SerializeField] private GameObject selectUI;

    public int lvl;
    public float xp;
    public float totalXp;
    public float targetXp;
    public float unitHP;
    public GameObject unit;
    public GameObject resupplyVehicle;
    public GameObject unitPrefab;
    public GameObject resupplyPrefab;
    public Vector3 distance;

    private bool passiveXP = true;
    private bool upgradingUnit = false;
    private bool ressuplyMissionActive = false;
    private bool alreadyDefended = false;
    private bool resetWalkpoint = false;
    private GameObject skillTree;

    [SerializeField]private GameObject _smokeGranadePrefab;
    [SerializeField]private GameObject[] vehiclePrefabs;
    [SerializeField]private int playerNum;


    private void Start()
    {
        skillTree = GameObject.Find("ScreenCanvas").GetComponent<UIs>().skillTree;

        if (unit != null)
        {
            resetWalkpoint = false;
            alreadyDefended = false;
            xp = 0;
            lvl = 1;
            unitHP = unit.GetComponent<life>().armor;
        }

        prefabUpdater();

    }

    private void FixedUpdate()
    {   
        targetXP();

        if (resetWalkpoint)
        {
            resetWalkpoint = false;
            CommanderDefence();
        }

        if (passiveXP)
        {
            passiveXP = false;
            StartCoroutine(passiveGain());
        }


        if (upgradingUnit)
        {
            distance = gameObject.transform.position - unit.transform.position;

            if (distance.magnitude <= 2.9f)
            {
                UpdateUnit();
            }
        }

        if (ressuplyMissionActive)
        {
            distance = resupplyVehicle.transform.position - unit.transform.position;

            if(resupplyVehicle.GetComponent<evenSimplerAI>().walkPoint != unit.transform.position)
            {
                resupplyVehicle.GetComponent<evenSimplerAI>().walkPoint = unit.transform.position;
            }

            if(distance.magnitude <= 2.9f)
            {
                ResupplyUnit();
            }
        }
     
    }

    private void targetXP()
    {
        if (unit != null && unit.GetComponent<life>().xpBoost)
        {
            targetXp = lvl * 500 + totalXp / lvl;
        }
        else
        {
            targetXp = lvl * 1000 + totalXp / lvl;
        }

        if(lvl == 20)
        {
            prefabUpdater();
        }
        else if(lvl < 20)
        {
            if (xp >= targetXp)
            {
                skillPointsIncrease();
                lvl++;
                totalXp += xp;
                gameObject.transform.parent.GetComponent<fobScript>().xp += xp;
                xp = 0;
                prefabUpdater();
            }
        }
    }

    IEnumerator passiveGain()
    {
        xp += lvl * 100;
        yield return new WaitForSeconds(100);
        passiveXP = true;
    }

    public void defence()
    {
        if(unit.GetComponent<life>().armor > 0 && !alreadyDefended)
        {
            alreadyDefended = true;
            CommanderDefence();
            Debug.Log("defence manuvers activated");
            xp += lvl * 250;
            unitHP = unit.GetComponent<life>().armor;
        }
        else if (unit.GetComponent<life>().armor <= 0)
        {
            Debug.Log("unit destroyed");
            unitHP = unit.GetComponent<life>().armor;
        }
    }

    public void attack()
    {
        xp += lvl * 250;
    }

    public void resupply()
    {
        if(resupplyVehicle == null)
        {
            resupplyVehicle = Instantiate(resupplyPrefab, gameObject.transform.position, Quaternion.identity);
            Camera.main.GetComponent<Interactor>().interactable = null;
            resupplyVehicle.GetComponent<life>().owner = gameObject;
            resupplyVehicle.GetComponent<life>().team = gameObject.transform.parent.GetComponent<life>().team;
            resupplyVehicle.GetComponent<evenSimplerAI>().enabled = true;
            resupplyVehicle.GetComponent<NavMeshAgent>().enabled = true;
            resupplyVehicle.GetComponent<evenSimplerAI>().walkPoint = unit.transform.position;
            resupplyVehicle.GetComponent<evenSimplerAI>().walkPointSet = true;
        }

        ressuplyMissionActive = true;
    }

    public void upgradeClass()
    {
        GameObject[] interactables = GameObject.FindGameObjectsWithTag("Interactable");

        if (unit == null)
        {

            RaycastHit hit;

            if (Physics.Raycast(gameObject.transform.position, -Vector3.up, out hit))
            {
                unit = Instantiate(unitPrefab, hit.point, Quaternion.identity);
                Camera.main.GetComponent<Interactor>().interactable = null;
                gameObject.GetComponent<playerScript>().enabled = true;
                unit.name = $"{unit.name} {interactables.Length}";
                unit.GetComponent<life>().owner = gameObject;
                unit.GetComponent<evenSimplerAI>().Start();
                unit.GetComponent<gunnerScript>().Start();
                unit.GetComponent<life>().team = GameObject.Find("TurnManager").GetComponent<turnManager>().currentTeam;
                gameObject.GetComponent<Buffs>().BuffMe();
            }
        }
        else
        {
            if (unit.gameObject.GetComponent<life>().dugIn == true)
            {
                GameObject.Find($"{unit.name}/DugIn/Cube").GetComponent<MeshRenderer>().enabled = false;
                GameObject.Find($"{unit.name}/DugIn/Cube").GetComponent<MeshCollider>().enabled = true;

                if (unit.GetComponent<life>().armor > unit.GetComponent<life>().MaxArmor)
                {
                    unit.GetComponent<life>().armor -= Mathf.Abs(unit.GetComponent<life>().MaxArmor - unit.GetComponent<life>().armor);
                }

                unit.gameObject.GetComponent<life>().dugIn = false;
            }

            unit.GetComponent<evenSimplerAI>().enabled = true;
            unit.GetComponent<NavMeshAgent>().enabled = true;
            unit.GetComponent<evenSimplerAI>().walkPoint = gameObject.transform.position;
            unit.GetComponent<evenSimplerAI>().walkPointSet = true;

            upgradingUnit = true;
            
        }

    }

    public void CommanderDefence()
    {
        if(unit.GetComponent<life>().commanderHealth > 0 && unit.GetComponent<life>().fuel > 0 && !unit.GetComponent<life>().dugIn && unit.GetComponent<NavMeshAgent>().hasPath == false && unit.GetComponent<life>().counterMeasures)
        {
            Vector3 escapePoint = new Vector3(unit.transform.position.x + Random.Range(-25, 25), 1000, unit.transform.position.z + Random.Range(-25, 25));
            Vector3 direction = new Vector3(escapePoint.x, escapePoint.y - 5, escapePoint.z) - escapePoint;
            RaycastHit hit;

            if(Physics.Raycast(escapePoint, direction.normalized, out hit))
            {
                if (hit.collider.CompareTag("Ground"))
                {
                    if (unit.GetComponent<life>().hasDrone == true && GameObject.Find($"{unit.name}/Drone"))
                    {
                        GameObject.Find($"{unit.name}/Drone").transform.SetParent(null);
                    }

                    unit.GetComponent<evenSimplerAI>().enabled = true;
                    unit.GetComponent<NavMeshAgent>().enabled = true;
                    unit.GetComponent<evenSimplerAI>().walkPoint = hit.point;
                    unit.GetComponent<evenSimplerAI>().walkPointSet = true;
                    Vector3 smokeSpawn = new Vector3(unit.transform.position.x, unit.transform.position.y + 3, unit.transform.position.z) - new Vector3(unit.transform.position.x, unit.transform.position.y + 4, unit.transform.position.z);
                    GameObject smoke = Instantiate(_smokeGranadePrefab, unit.transform.position, Quaternion.identity);
                    GameObject smoke2 = Instantiate(_smokeGranadePrefab, unit.transform.position, Quaternion.identity);
                    GameObject smoke3 = Instantiate(_smokeGranadePrefab, unit.transform.position, Quaternion.identity);
                    smoke.GetComponent<Rigidbody>().AddForce(smokeSpawn * 50, ForceMode.Impulse);
                    smoke2.GetComponent<Rigidbody>().AddForce(-smokeSpawn * 50, ForceMode.Impulse);
                    smoke3.GetComponent<Rigidbody>().AddForce(-smokeSpawn * 50, ForceMode.Impulse);

                    alreadyDefended = false;
                }
                else
                {
                    alreadyDefended = false;
                    resetWalkpoint = true;
                }
            }
        } 
    }
    private void skillPointsIncrease()
    {
        if (playerNum == 1)
        {
            gameObject.transform.parent.GetComponent<fobScript>().unit1SkillPoints++;
        }
        else if (playerNum == 2)
        {
            gameObject.transform.parent.GetComponent<fobScript>().unit2SkillPoints++;
        }
        else if (playerNum == 3)
        {
            gameObject.transform.parent.GetComponent<fobScript>().unit3SkillPoints++;
        }
        else if (playerNum == 4)
        {
            gameObject.transform.parent.GetComponent<fobScript>().unit4SkillPoints++;
        }
        else if (playerNum == 5)
        {
            gameObject.transform.parent.GetComponent<fobScript>().unit5SkillPoints++;
        }
    }

    private void prefabUpdater()
    {
        if (lvl <= 4)
        {
            unitPrefab = vehiclePrefabs[0];
        }
        else if (lvl <= 8)
        {
            unitPrefab = vehiclePrefabs[1];
        }
        else if (lvl <= 12)
        {
            unitPrefab = vehiclePrefabs[2];
        }
        else if (lvl <= 16)
        {
            unitPrefab = vehiclePrefabs[3];
        }
        else if (lvl <= 20)
        {
            unitPrefab = vehiclePrefabs[4];
        }

        if(lvl == 20 && gameObject.transform.parent.GetComponent<fobScript>().lvl >= 20)
        {
            unitPrefab = vehiclePrefabs[5];
        }

    }
        private void UpdateUnit()
        {
            upgradingUnit = false;
            GameObject[] interactables = GameObject.FindGameObjectsWithTag("Interactable");
            Destroy(unit);
            unit = Instantiate(unitPrefab, gameObject.transform.position, Quaternion.identity);
            unit.name = $"{unit.name} {interactables.Length}";
            unit.GetComponent<life>().owner = gameObject;
            unit.GetComponent<life>().team = gameObject.transform.parent.GetComponent<life>().team;
            unit.GetComponent<evenSimplerAI>().Start();
            unit.GetComponent<gunnerScript>().Start();
            unit.GetComponent<FieldOfView>().isVisable = true;
            gameObject.GetComponent<Buffs>().BuffMe();
        }

    private void ResupplyUnit()
    {
        Debug.Log("unit resupplied");
        unit.GetComponent<evenSimplerAI>().currentFuel = unit.GetComponent<life>().maxFuel;
        unit.GetComponent<evenSimplerAI>().startDistance = 0;
        unit.GetComponent<life>().fuel = unit.GetComponent<life>().maxFuel;
        unit.GetComponent<life>().ShotsLeft = unit.GetComponent<life>().MaxShots;
        unit.GetComponent<life>().gunnerHealth = 100;
        unit.GetComponent<life>().engineerHealth = 100;
        unit.GetComponent<life>().commanderHealth = 100;
        unit.GetComponent<life>().driverHealth = 100;
        unit.GetComponent<evenSimplerAI>().targetSplat.GetComponent<MeshRenderer>().enabled = false;
        Destroy(resupplyVehicle);
        ressuplyMissionActive = false;
    }
}
