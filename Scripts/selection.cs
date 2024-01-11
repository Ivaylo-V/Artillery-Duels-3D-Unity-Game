using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selection : MonoBehaviour
{
    public GameObject selectedPlayer;

    public void selectedUpgrade()
    {
        selectedPlayer.GetComponent<playerScript>().upgradeClass();
    }

    public void selectedResupply()
    {
        selectedPlayer.GetComponent<playerScript>().resupply();
    }
}
