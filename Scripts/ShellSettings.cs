using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShellSettings : MonoBehaviour
{
    [SerializeField] GameObject[] ShellType;
    [SerializeField] private int ForceVariations;
    [SerializeField] GameObject forceSlider;
    [SerializeField] Button[] attackTypes;
    [SerializeField] Button[] shellTypes;

    private GameObject selected;
    private float ForcePercent;
    private int shellType;
    private int attackType;
    private bool choiceA;


    private void FixedUpdate()
    {
        if (Camera.main.isActiveAndEnabled)
        {
            selected = Camera.main.GetComponent<Interactor>().interactable.gameObject;
        }

        if (!selected.GetComponent<life>().specialAmmo)
        {
            shellTypes[1].interactable = false;
            shellTypes[4].interactable = false;
            shellTypes[5].interactable = false;

        }

        if(shellType == 0)
        {
            attackTypes[0].interactable = false;
            attackTypes[1].interactable = false;
            attackTypes[2].interactable = false;
            attackTypes[3].interactable = false;
        }
    }

    public void LoadShell()
    {
        if(shellType != 0 && attackType != 0)
        {
            ForcePercent = forceSlider.GetComponent<Slider>().value;
            float maxForce = selected.GetComponent<life>().maximumForce;
            float trueForce = maxForce * ForcePercent;
            selected.GetComponent<gunnerScript>().projectile = ShellType[shellType - 1];
            selected.GetComponent<life>().trueForce = trueForce;

            AttackLoader();
        }
    }

    public void AttackLoader()
    {
        foreach (Button shellType in shellTypes)
        {
            shellType.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }

        foreach (Button attackType in attackTypes)
        {
            attackType.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }

        if (attackType == 1)
        {
            selected.GetComponent<gunnerScript>().collisionAttack = true;
            selected.GetComponent<gunnerScript>().AirAttack = false;
            selected.GetComponent<gunnerScript>().aboveGroundAttack = false;
            selected.GetComponent<gunnerScript>().PenetrationAttack = false;
        }
        else if (attackType == 2)
        {
            selected.GetComponent<gunnerScript>().collisionAttack = false;
            selected.GetComponent<gunnerScript>().AirAttack = true;
            selected.GetComponent<gunnerScript>().aboveGroundAttack = false;
            selected.GetComponent<gunnerScript>().PenetrationAttack = false;
        }
        else if (attackType == 3)
        {
            selected.GetComponent<gunnerScript>().collisionAttack = false;
            selected.GetComponent<gunnerScript>().AirAttack = false;
            selected.GetComponent<gunnerScript>().aboveGroundAttack = false;
            selected.GetComponent<gunnerScript>().PenetrationAttack = true;
        }
        else if (attackType == 4)
        {
            selected.GetComponent<gunnerScript>().collisionAttack = false;
            selected.GetComponent<gunnerScript>().AirAttack = false;
            selected.GetComponent<gunnerScript>().aboveGroundAttack = true;
            selected.GetComponent<gunnerScript>().PenetrationAttack = false;
        }

        attackType = 0;
        shellType = 0;
    }
    public void HEShellButton()
    {
        choiceA = false;
        attackTypes[0].interactable = true;
        attackTypes[1].interactable = false;
        attackTypes[2].interactable = true;
        attackTypes[3].interactable = true;
        shellType = 1;
    }

    public void ATShellButton()
    {
        choiceA = false;
        attackTypes[0].interactable = false;
        attackTypes[1].interactable = true;
        attackTypes[2].interactable = false;
        attackTypes[3].interactable = false;
        shellType = 2;
    }

    public void HEATShellButton()
    {
        choiceA = false;
        attackTypes[0].interactable = true;
        attackTypes[1].interactable = false;
        attackTypes[2].interactable = true;
        attackTypes[3].interactable = true;
        shellType = 3;
    }

    public void FlareShellButton()
    {
        choiceA = false;
        attackTypes[0].interactable = false;
        attackTypes[1].interactable = true;
        attackTypes[2].interactable = false;
        attackTypes[3].interactable = false;
        shellType = 4;
    }

    public void IncindieryShellShellButton()
    {
        choiceA = false;
        attackTypes[0].interactable = false;
        attackTypes[1].interactable = true;
        attackTypes[2].interactable = false;
        attackTypes[3].interactable = false;
        shellType = 5;
    }

    public void ClusterShellShellButton()
    {
        choiceA = false;
        attackTypes[0].interactable = false;
        attackTypes[1].interactable = true;
        attackTypes[2].interactable = false;
        attackTypes[3].interactable = false;
        shellType = 6;
    }

    public void ThermobacricShellButton()
    {
        choiceA = false;
        attackTypes[0].interactable = true;
        attackTypes[1].interactable = false;
        attackTypes[2].interactable = true;
        attackTypes[3].interactable = true;
        shellType = 7;
    }

    public void collisionAttack()
    {
        choiceA = true;
        attackType = 1;
    }

    public void airAttack()
    {
        choiceA = true;
        attackType = 2;
    }

    public void PenetrationAttack()
    {
        choiceA = true;
        attackType = 3;
    }

    public void abovegroundAttack()
    {
        choiceA = true;
        attackType = 4;
    }

    public void ClickedButton(GameObject button)
    {

        if (choiceA)
        {
            foreach(Button attackType in attackTypes)
            {
                attackType.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
        }
        else if (!choiceA)
        {
            foreach (Button shellType in shellTypes)
            {
                shellType.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
        }

        button.GetComponent<Image>().color = new Color(0.1f, 0.65f, 0.5f, 1);
    }

}
