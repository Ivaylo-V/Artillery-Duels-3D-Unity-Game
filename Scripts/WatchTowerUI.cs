using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchTowerUI : MonoBehaviour
{
    private GameObject selected;

    private void FixedUpdate()
    {
        selected = Camera.main.GetComponent<Interactor>().interactable.gameObject;
    }
    public void watchTowerCaptureClick()
    {
        selected.GetComponent<WatchTowerScript>().CaptureTower();
    }

    public void watchTowerRepairClick()
    {
        selected.GetComponent<WatchTowerScript>().RepairTower();
    }

    public void watchTowerDestroyClick()
    {
        selected.GetComponent<WatchTowerScript>().DestroyTower();
    }

    public void townBaseCaptureClick()
    {
        selected.GetComponent<TownBaseScript>().CaptureTown();
    }

    public void townBaseRepairClick()
    {
        selected.GetComponent<TownBaseScript>().RepairTowm();
    }

    public void townBaseResupplyClick()
    {
        selected.GetComponent<TownBaseScript>().ReasupplyUnits();
    }
}
