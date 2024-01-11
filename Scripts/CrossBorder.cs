using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CrossBorder : MonoBehaviour
{
    [SerializeField] private LayerMask _interactableLayer;
    [SerializeField] private GameObject CheckpointA;
    [SerializeField] private GameObject CheckpointB;
    [SerializeField] private GameObject MapViewerA;
    [SerializeField] private GameObject MapViewerB;

    bool alreadyTreaveled;
    public void onInteract()
    {
        alreadyTreaveled = false;

        YesNoBox.Instance.ShowQuestion("Would you like to travel through checkpoint?", () =>
        {
            Collider[] CaptureCandidates = Physics.OverlapSphere(transform.position, 30, _interactableLayer);


            if (CaptureCandidates.Length != 0)
            {
                foreach (Collider hit in CaptureCandidates)
                {
                    if (hit.GetComponent<life>() != null)
                    {
                        if (hit.gameObject.GetComponent<life>().team == GameObject.Find("TurnManager").GetComponent<turnManager>().currentTeam && hit.CompareTag("Interactable"))
                        {
                            if (!alreadyTreaveled)
                            {
                                float distanceA = Vector3.Distance(hit.transform.position, CheckpointA.transform.position);
                                float distanceB = Vector3.Distance(hit.transform.position, CheckpointB.transform.position);

                                if (distanceA > distanceB)
                                {
                                    RaycastHit HIT;
                                    if (Physics.Raycast(CheckpointA.transform.position, -Vector3.up, out HIT))
                                    {
                                        Debug.Log("Moving to A");
                                        hit.GetComponent<NavMeshAgent>().enabled = false;
                                        hit.transform.position = HIT.point;
                                        MapViewerA.SetActive(true);
                                        MapViewerB.SetActive(false);
                                        GameObject.Find($"{gameObject.name}/Selection").gameObject.GetComponent<MeshRenderer>().enabled = false;
                                        alreadyTreaveled = true;

                                    }
                                }
                                else if(distanceA < distanceB)
                                {
                                    RaycastHit HIT;
                                    if (Physics.Raycast(CheckpointB.transform.position, -Vector3.up, out HIT))
                                    {
                                        Debug.Log("Moving to B");
                                        hit.GetComponent<NavMeshAgent>().enabled = false;
                                        hit.transform.position = HIT.point;
                                        MapViewerA.SetActive(false);
                                        MapViewerB.SetActive(true);
                                        GameObject.Find($"{gameObject.name}/Selection").gameObject.GetComponent<MeshRenderer>().enabled = false;
                                        alreadyTreaveled = true;
                                    }
                                
                                }
                            }

                            hit.GetComponent<NavMeshAgent>().enabled = true;


                        }
                    }
                }
            }

        }, () =>
        {

        });
    }

}
