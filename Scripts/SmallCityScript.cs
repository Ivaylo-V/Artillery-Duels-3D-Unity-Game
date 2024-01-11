using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallCityScript : MonoBehaviour
{
    [SerializeField] private LayerMask _interactableLayer;
    public void onInteract()
    {
        YesNoBox.Instance.ShowQuestion("Would you like to capture this landmark?", () =>
        {
            Collider[] CaptureCandidates = Physics.OverlapSphere(transform.position, 100, _interactableLayer);

            if (CaptureCandidates.Length != 0)
            {
                foreach (Collider hit in CaptureCandidates)
                {
                    if (hit.GetComponent<life>() != null)
                    {
                        if (hit.gameObject.GetComponent<life>().team == GameObject.Find("TurnManager").GetComponent<turnManager>().currentTeam && hit.CompareTag("Interactable"))
                        {
                            if (gameObject.GetComponent<life>().team == 0 && !gameObject.GetComponent<life>().destroyed)
                            {
                                hit.GetComponent<life>().owner.gameObject.GetComponent<playerScript>().xp += 1000;
                                Debug.Log("Captured");
                                gameObject.GetComponent<life>().team = GameObject.Find("TurnManager").GetComponent<turnManager>().currentTeam;
                            }
                        }
                    }
                }
            }

        }, () =>
        {

        });
    }
}
