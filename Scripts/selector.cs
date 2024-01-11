using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selector : MonoBehaviour
{
    [SerializeField] private GameObject fobPlayerUI;
    public int click = 0;

    public void playerSelector()
    {
        GameObject[] FOBs = GameObject.FindGameObjectsWithTag("FOB");

        foreach (GameObject FOB in FOBs)
        {
            if (FOB.GetComponent<life>().team == GameObject.Find("TurnManager").GetComponent<turnManager>().currentTeam)
            {
                fobPlayerUI.GetComponent<selection>().selectedPlayer = GameObject.Find($"{FOB.name}/Player ({click})");
            }
        }
    }

    public void click1()
    {
        click = 1;
        playerSelector();
    }

    public void click2()
    {
        click = 2;
        playerSelector();
    }

    public void click3()
    {
        click = 3;
        playerSelector();
    }

    public void click4()
    {
        click = 4;
        playerSelector();
    }

    public void click5()
    {
        click = 1;
        playerSelector();
    }

}
