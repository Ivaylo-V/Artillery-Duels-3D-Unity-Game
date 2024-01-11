using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class owner : MonoBehaviour
{
    [SerializeField] private GameObject Owner;
    private GameObject cameraMain;

    private void Start()
    {
        Owner = gameObject.transform.parent.gameObject;
    }
    public void setParent()
    {
        gameObject.transform.SetParent(Owner.transform);
    }

    public void onClick()
    {
        GameObject[] fobs = GameObject.FindGameObjectsWithTag("FOB");
        GameObject turnManager = GameObject.Find("TurnManager");

        foreach (GameObject fob in fobs)
        {
            if(fob.GetComponent<life>().team == turnManager.GetComponent<turnManager>().currentTeam)
            {
                if(gameObject.name == "Unit1")
                {
                    Owner = fob.GetComponent<fobScript>().unit1.GetComponent<playerScript>().unit;
                }
                else if (gameObject.name == "Unit2")
                {
                    Owner = fob.GetComponent<fobScript>().unit2.GetComponent<playerScript>().unit;
                }
                else if (gameObject.name == "Unit3")
                {
                    Owner = fob.GetComponent<fobScript>().unit3.GetComponent<playerScript>().unit;
                }
                else if (gameObject.name == "Unit4")
                {
                    Owner = fob.GetComponent<fobScript>().unit4.GetComponent<playerScript>().unit;
                }
                else if (gameObject.name == "Unit5")
                {
                    Owner = fob.GetComponent<fobScript>().unit5.GetComponent<playerScript>().unit;
                }
            }
        }
        cameraMain = Camera.main.transform.parent.gameObject;
        cameraMain.GetComponent<CharacterController>().enabled = false;
        cameraMain.transform.position = new Vector3(Owner.transform.position.x, cameraMain.transform.position.y, Owner.transform.position.z);
        cameraMain.GetComponent<CharacterController>().enabled = true;
    }
}
