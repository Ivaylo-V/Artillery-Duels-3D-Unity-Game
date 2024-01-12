using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAirDefence : MonoBehaviour
{
    [SerializeField] GameObject AirDefence;

    private void OnTriggerEnter(Collider other)
    {
        if(other != null)
        {
            if (other.CompareTag("Shell") && other.GetComponent<life>().team != gameObject.transform.parent.GetComponent<life>().team)
            {
                AirDefence.GetComponent<AirDefence>().target = other.gameObject.transform.position;
                AirDefence.GetComponent<AirDefence>().DefenceActive();
            }
        }
    }
}
