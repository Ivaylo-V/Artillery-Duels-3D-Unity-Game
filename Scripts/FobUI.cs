using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FobUI : MonoBehaviour
{
    [SerializeField] private Button unit1;
    [SerializeField] private Button unit2;
    [SerializeField] private Button unit3;
    [SerializeField] private Button unit4;
    [SerializeField] private Button unit5;

    private GameObject selected;

    private void FixedUpdate()
    {
        selected = Camera.main.GetComponent<Interactor>().interactable.gameObject;

        //Can be improved. No need to turn all keys true/false every frame. Maybe check if button is interactable and only if it is not && command lvl is enough - change all buttons (1 line every frame is still better than 5, but faaar from great).
        if(selected.GetComponent<fobScript>().commandLvl == 1)
        {
            unit1.interactable = true;
            unit2.interactable = false;
            unit3.interactable = false;
            unit4.interactable = false;
            unit5.interactable = false;

        }
        else if (selected.GetComponent<fobScript>().commandLvl == 2)
        {
            unit1.interactable = true;
            unit2.interactable = true;
            unit3.interactable = false;
            unit4.interactable = false;
            unit5.interactable = false;

        }
        else if (selected.GetComponent<fobScript>().commandLvl == 3)
        {
            unit1.interactable = true;
            unit2.interactable = true;
            unit3.interactable = true;
            unit4.interactable = false;
            unit5.interactable = false;

        }
        else if (selected.GetComponent<fobScript>().commandLvl == 4)
        {
            unit1.interactable = true;
            unit2.interactable = true;
            unit3.interactable = true;
            unit4.interactable = true;
            unit5.interactable = false;

        }
        else if (selected.GetComponent<fobScript>().commandLvl == 5)
        {
            unit1.interactable = true;
            unit2.interactable = true;
            unit3.interactable = true;
            unit4.interactable = true;
            unit5.interactable = true;
            unit5.interactable = true;
        }
        else
        {
            unit1.interactable = false;
            unit2.interactable = false;
            unit3.interactable = false;
            unit4.interactable = false;
            unit5.interactable = false;
        }
    }
}
