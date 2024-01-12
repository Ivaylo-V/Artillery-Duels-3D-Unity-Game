using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirDefence : MonoBehaviour
{
    [SerializeField] private GameObject projectile;

    public Vector3 target;

    private Vector3 direction;
    private GameObject DefenceRocket;
    private bool CurrentlyDefending;
    private int RocketsLeft;

    private void Start()
    {
        LoadMoreRockets();
    }

    private void FixedUpdate()
    {
        if (CurrentlyDefending)
        {
            direction = DefenceRocket.transform.position - target;
            DefenceRocket.GetComponent<Rigidbody>().AddForce(direction * 10, ForceMode.Force);
            float distance = Vector3.Distance(target, DefenceRocket.transform.position);

            if (distance <= 15)
            {
                Collider[] hits = Physics.OverlapSphere(DefenceRocket.transform.position, 20);

                foreach(Collider hit in hits)
                {
                    Destroy(hit);
                }
            }
        }
    }

    public void DefenceActive()
    {
        if(RocketsLeft > 0 && DefenceRocket == null)
        {
            DefenceRocket = Instantiate(projectile, gameObject.transform.position, Quaternion.identity);
            DefenceRocket.GetComponent<Rigidbody>().AddForce(Vector3.up * 5, ForceMode.Impulse);
            DefenceRocket.GetComponent<impactScript>().owner = gameObject.GetComponent<life>().owner;

            CurrentlyDefending = true;
            RocketsLeft--;
        }
    }

    private void LoadMoreRockets()
    {
        RocketsLeft = gameObject.transform.parent.GetComponent<life>().AirdefenceLvl * 4;
    }
}
