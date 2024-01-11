using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class findMyFob : MonoBehaviour
{
    private GameObject selected;
    public void FindMyFob()
    {
        if (Camera.main)
        {
            if (Camera.main.GetComponent<Interactor>().interactable != null)
            {
                selected = Camera.main.GetComponent<Interactor>().interactable.gameObject;
            }
            else
            {
                selected = null;
            }
        }
        else
        {
            selected = null;
        }

        if (selected != null)
        {
            Camera.main.transform.parent.GetComponent<CharacterController>().enabled = false;
            Camera.main.transform.parent.position = new Vector3(selected.transform.position.x, Camera.main.transform.position.y, selected.transform.position.z);
            Camera.main.transform.parent.GetComponent<CharacterController>().enabled = true;
        }
        else
        {
            GameObject[] FOBs = GameObject.FindGameObjectsWithTag("FOB");

            if (FOBs.Length == GameObject.Find("TurnManager").GetComponent<turnManager>().maxTeams)
            {
                foreach (GameObject FOB in FOBs)
                {
                    if (FOB.GetComponent<life>().team == GameObject.Find("TurnManager").GetComponent<turnManager>().currentTeam)
                    {
                        Camera.main.transform.parent.GetComponent<CharacterController>().enabled = false;
                        Camera.main.transform.parent.position = new Vector3(FOB.transform.position.x, Camera.main.transform.position.y, FOB.transform.position.z);
                        Camera.main.transform.parent.GetComponent<CharacterController>().enabled = true;
                    }
                }
            }
        }
    }
}
