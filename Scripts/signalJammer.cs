using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class signalJammer : MonoBehaviour
{
    public int jamRadius = 50;

    private bool startJamming = true;

    private void FixedUpdate()
    {

        jamRadius = (int)gameObject.GetComponent<life>().fieldOfVision;

        if (startJamming && gameObject.GetComponent<life>().signalJammer == true)
        {
            startJamming = false;
            StartCoroutine(JamFrequencies());
        }
    }

    IEnumerator JamFrequencies()
    {  

         yield return new WaitForSeconds(10);

        Debug.Log("JamAttempt");
        Collider[] detections = Physics.OverlapSphere(gameObject.transform.position, jamRadius);

        foreach (Collider hit in detections)
        {
            if (hit.CompareTag("Drone") && hit.GetComponent<life>().team != gameObject.GetComponent<life>().team)
            {
                Debug.Log("JamDatBitch");
                hit.GetComponent<life>().destroyed = true;
                hit.GetComponent<life>().armor = 0;
                hit.GetComponent<wasdMovement>().endFlight();
                hit.GetComponent<wasdMovement>().enabled = false;
            }

        }

        startJamming = true;
    }
}
