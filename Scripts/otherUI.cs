using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class otherUI : MonoBehaviour
{
    [SerializeField] private Button digIN;
    [SerializeField] private Button camoflage;
    [SerializeField] private Button _defences;
    [SerializeField] private Button signalJammer;
    [SerializeField] private Button drone;
    [SerializeField] private Button _range;
    [SerializeField] private Button granadeDropper;
    [SerializeField] private Button suicideDrone;
    [SerializeField] private Button repair;
    [SerializeField] private Button droneUI;

    private GameObject selected;

    private void FixedUpdate()
    {
        selected = Camera.main.GetComponent<Interactor>().interactable.gameObject;

        if (selected.GetComponent<life>().turnOver == false)
        {
            if (selected.GetComponent<life>().dugIn == false)
            {
                digIN.interactable = true;
            }
            else
            {
                digIN.interactable = false;
            }

            if (selected.GetComponent<life>().hasDrone == true)
            {
                droneUI.interactable = true;
            }
            else
            {
                droneUI.interactable = false;
            }

            if (selected.GetComponent<life>().canRepair)
            {
                repair.interactable = true;
            }
            else
            {
                repair.interactable = false;
            }

            if (selected.GetComponent<life>().canCommand)
            {
                if (selected.GetComponent<life>().hasDrone == false)
                {
                    drone.interactable = true;
                }
                else
                {
                    drone.interactable = false;
                }

                if (selected.GetComponent<life>().camoflage == false)
                {
                    camoflage.interactable = true;
                }
                else
                {
                    camoflage.interactable = false;
                }

                if (selected.GetComponent<life>().defenceLvl < 5)
                {
                    _defences.interactable = true;
                }
                else
                {
                    _defences.interactable = false;
                }

                if (selected.GetComponent<life>().signalJammer == false)
                {
                    signalJammer.interactable = true;
                }
                else
                {
                    signalJammer.interactable = false;
                }

                if (selected.GetComponent<life>().canDropGranades == false && selected.GetComponent<life>().hasDrone == true)
                {
                    granadeDropper.interactable = true;
                }
                else
                {
                    granadeDropper.interactable = false;
                }

                if (selected.GetComponent<life>().range < 6000 && selected.GetComponent<life>().hasDrone == true)
                {
                    _range.interactable = true;
                }
                else
                {
                    _range.interactable = false;
                }

            }
            else
            {
                digIN.interactable = false;
                camoflage.interactable = false;
                _defences.interactable = false;
                signalJammer.interactable = false;
                drone.interactable = false;
                _range.interactable = false;
                granadeDropper.interactable = false;
                suicideDrone.interactable = false;
                repair.interactable = false;
                droneUI.interactable = true;
            }
        }
        else
        {
            digIN.interactable = false;
            camoflage.interactable = false;
            _defences.interactable = false;
            signalJammer.interactable = false;
            drone.interactable = false;
            _range.interactable = false;
            granadeDropper.interactable = false;
            suicideDrone.interactable = false;
            repair.interactable = false;
            droneUI.interactable = true;
        }
    }


}
