using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class droneTeamUpdate : MonoBehaviour
{
    private GameObject droneUI;

    private void Start()
    {
        gameObject.GetComponent<life>().team = gameObject.transform.parent.GetComponent<life>().team;
    }

    public void droneUIFinder()
    {
        droneUI = GameObject.Find("ScreenCanvas").GetComponent<UIs>().droneUI;
        droneUI.SetActive(true);
    }
}
