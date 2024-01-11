using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fobInfoBox : MonoBehaviour
{
    private GameObject selected;
    [SerializeField] GameObject ArmorBar;
    [SerializeField] GameObject FuelBar;
    [SerializeField] GameObject DailyTurnBar;
    [SerializeField] GameObject DailyShotsBar;
    [SerializeField] GameObject Interactor;
    private void FixedUpdate()
    {

        if(Interactor.GetComponent<Interactor>().interactable != null)
        {
            selected = Interactor.GetComponent<Interactor>().interactable.gameObject;
        }
        else
        {
            selected = null;
        }


        textUpdater();
        armorBar();
        FuelBars();
        DailyTurnBars();
        DailyShotsBars();

    }

    private void textUpdater()
    {

        if (selected != null)
        {
            gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = selected.GetComponent<life>().type;
        }
        else
        {
            GameObject[] FOBs = GameObject.FindGameObjectsWithTag("FOB");

            foreach (GameObject FOB in FOBs)
            {
                if (FOB.GetComponent<life>().team == GameObject.Find("TurnManager").GetComponent<turnManager>().currentTeam)
                {
                    gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = FOB.GetComponent<life>().type;
                }
            }
        }
    }

    private void armorBar()
    {
        if(selected != null)
        {
            float currentHealt = selected.GetComponent<life>().armor / selected.GetComponent<life>().MaxArmor;
            ArmorBar.GetComponent<InfoBar>().PercentFull = currentHealt;
        }
        else
        {
            GameObject[] FOBs = GameObject.FindGameObjectsWithTag("FOB");

            foreach (GameObject FOB in FOBs)
            {
                if (FOB.GetComponent<life>().team == GameObject.Find("TurnManager").GetComponent<turnManager>().currentTeam)
                {
                    float currentHealt = FOB.GetComponent<life>().armor / FOB.GetComponent<life>().MaxArmor;
                    ArmorBar.GetComponent<InfoBar>().PercentFull = currentHealt;
                }
            }
        }
    }

    private void FuelBars()
    {
        if (selected != null)
        {
            float currentHealt = selected.GetComponent<life>().fuel / selected.GetComponent<life>().maxFuel;
            FuelBar.GetComponent<InfoBar>().PercentFull = currentHealt;
        }
        else
        {
            float currentHealt = 1;
            FuelBar.GetComponent<InfoBar>().PercentFull = currentHealt;
        }
    }

    private void DailyTurnBars()
    {
        if (selected != null)
        {
            float currentHealt = 1 - (selected.GetComponent<life>().dailyDistance / selected.GetComponent<life>().range);
            DailyTurnBar.GetComponent<InfoBar>().PercentFull = currentHealt;
        }
        else
        {
            float currentHealt = 0;
            DailyTurnBar.GetComponent<InfoBar>().PercentFull = currentHealt;

        }
    }

    private void DailyShotsBars()
    {
        if (selected != null)
        {
            if(selected.GetComponent<gunnerScript>() != null)
            {
                if (selected.GetComponent<life>().turnOver)
                {
                    DailyShotsBar.GetComponent<InfoBar>().PercentFull = 0;
                }
                else
                {
                    float shotsLeft = selected.GetComponent<life>().maximumShotsPerTurn - selected.GetComponent<life>().currentShots;
                    float currentHealt = shotsLeft / selected.GetComponent<life>().maximumShotsPerTurn;
                    DailyShotsBar.GetComponent<InfoBar>().PercentFull = currentHealt;
                }
            }
            else
            {
                DailyShotsBar.GetComponent<InfoBar>().PercentFull = 0;
            }
        }
        else
        {
            DailyShotsBar.GetComponent<InfoBar>().PercentFull = 0;
        }
    }

}
