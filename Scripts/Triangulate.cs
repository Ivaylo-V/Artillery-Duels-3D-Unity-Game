using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangulate : MonoBehaviour
{
    public GameObject target;

    [SerializeField] GameObject FoV;

    private GameObject CamHolder;

    //impact script enables this script and sets target so that the FoV element can triangulate position and reveal enemy.

    private void Start()
    {
        CamHolder = Camera.main.transform.parent.gameObject;
    }
    private void FixedUpdate()
    {
        if (target != null && gameObject.GetComponent<life>().team == GameObject.Find("TurnManager").GetComponent<turnManager>().currentTeam)
        {
            FoV.SetActive(true);
            FoV.transform.position = target.transform.position;

            YesNoBox.Instance.ShowQuestion("Enemy position triangulated. Would you like to go to it?", () =>
            {

                CamHolder.transform.position = target.transform.position;

            }, () =>
            {

                target = null;
                gameObject.GetComponent<Triangulate>().enabled = false;

            });

        }
    }
}
