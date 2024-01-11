using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillTreeUpdater : MonoBehaviour
{
    [SerializeField] private GameObject turnManager;
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField] private GameObject player3;
    [SerializeField] private GameObject player4;

    public void skillTreePlayers()
    {
        if(turnManager.GetComponent<turnManager>().currentTeam == 1)
        {
            player1.SetActive(true);
            player2.SetActive(false);
            player3.SetActive(false);
            player4.SetActive(false);

        }
        else if (turnManager.GetComponent<turnManager>().currentTeam == 2)
        {
            player1.SetActive(false);
            player2.SetActive(true);
            player3.SetActive(false);
            player4.SetActive(false);

        }
        else if (turnManager.GetComponent<turnManager>().currentTeam == 3)
        {
            player1.SetActive(false);
            player2.SetActive(false);
            player3.SetActive(true);
            player4.SetActive(false);

        }
        else if (turnManager.GetComponent<turnManager>().currentTeam == 4)
        {
            player1.SetActive(false);
            player2.SetActive(false);
            player3.SetActive(false);
            player4.SetActive(true);
        }
        else
        {
            player1.SetActive(false);
            player2.SetActive(false);
            player3.SetActive(false);
            player4.SetActive(false);

        }
    }
}
