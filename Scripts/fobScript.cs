using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fobScript : MonoBehaviour
{
    public int lvl;
    public float xp;
    public float totalXp;
    public float xpTarget;
    public int commandLvl;
    public GameObject unit1;
    public GameObject unit2;
    public GameObject unit3;
    public GameObject unit4;
    public GameObject unit5;
    public bool gameOver;
    public bool passiveXP = true;
    public int fobSkillPoints, unit1SkillPoints, unit2SkillPoints, unit3SkillPoints, unit4SkillPoints, unit5SkillPoints;

    private GameObject FobUI;

    private void Start()
    {
        FobUI = GameObject.Find("ScreenCanvas").GetComponent<UIs>().fobUI;
        lvl = 1;
        xp = 0;
        totalXp = 0;
        commandLvl = 0;
        fobSkillPoints = 1;
    }

    private void FixedUpdate()
    {
        targetXp();

        if (gameObject.GetComponent<life>().team != GameObject.Find("TurnManager").GetComponent<turnManager>().currentTeam)
        {
            passiveXP = true;
        }
        else if (passiveXP)
        {
            passiveXP = false;
            PassiveGain();
        }

    }

    private void targetXp()
    {
        xpTarget = lvl * 1000 + totalXp / lvl;

        if (xp >= xpTarget)
        {
            lvl++;
            fobSkillPoints++;
            totalXp += xp;
            xp = 0;
        }
    }

    public void PassiveGain()
    {
        GameObject[] cities = GameObject.FindGameObjectsWithTag("Small City");

        int cityXP = 0;

        foreach (GameObject city in cities)
        {
            if (city.GetComponent<life>().team == GameObject.Find("TurnManager").GetComponent<turnManager>().currentTeam)
            {
                cityXP += 100;
            }
        }

        xp += (lvl * 100) + cityXP;
    }

    public void FobSpawn()
    {
        FobUI.SetActive(true);
    }
}
