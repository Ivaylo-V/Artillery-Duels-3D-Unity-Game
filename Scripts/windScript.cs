using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windScript : MonoBehaviour
{
    [SerializeField] private WindZone south;
    [SerializeField] private WindZone north;
    [SerializeField] private WindZone east;
    [SerializeField] private WindZone west;
    [SerializeField] private WindZone currentWind;
    [SerializeField] private WindZone secondWind;
    [SerializeField] private int WindSpeed;


    private void Start()
    {
        StartCoroutine(windChange());
    }

    GameObject[] AffectedObjects;

    private void FixedUpdate()
    {
        AffectedObjects = GameObject.FindGameObjectsWithTag("Shell");

        foreach(GameObject obj in AffectedObjects)
        {
            Vector3 direction = currentWind.transform.position - obj.transform.position;
            obj.GetComponent<Rigidbody>().AddForce(direction.normalized * WindSpeed, ForceMode.Force);

            if(secondWind != null)
            {
                Vector3 direction2 = secondWind.transform.position - obj.transform.position;
                obj.GetComponent<Rigidbody>().AddForce(direction2.normalized * WindSpeed, ForceMode.Force);
            }
        }
    }

    IEnumerator windChange()
    {
        south.gameObject.SetActive(false);
        north.gameObject.SetActive(false);
        east.gameObject.SetActive(false);
        west.gameObject.SetActive(false);

        int randomTime = Random.Range(50, 200);
        WaitForSeconds wait = new WaitForSeconds(randomTime);
        int randomWind = Random.Range(1, 8);
        WindSpeed = Random.Range(0, 6);

        if (randomWind == 1)
        {
            currentWind = south;
            south.gameObject.SetActive(true);
            secondWind = null;
        }
        else if (randomWind == 2)
        {
            currentWind = north;
            north.gameObject.SetActive(true);
            secondWind = null;
        }
        else if (randomWind == 3)
        {
            currentWind = east;
            east.gameObject.SetActive(true);
            secondWind = null;
        }
        else if (randomWind == 4)
        {
            currentWind = west;
            west.gameObject.SetActive(true);
            secondWind = null;
        }
        else if (randomWind == 5)
        {
            currentWind = south;
            south.gameObject.SetActive(true);
            secondWind = east;
            east.gameObject.SetActive(true);
        }
        else if (randomWind == 6)
        {
            currentWind = north;
            north.gameObject.SetActive(true);
            secondWind = east;
            east.gameObject.SetActive(true);
        }
        else if (randomWind == 7)
        {
            currentWind = south;
            south.gameObject.SetActive(true);
            secondWind = west;
            west.gameObject.SetActive(true);
        }
        else if (randomWind == 8)
        {
            currentWind = north;
            north.gameObject.SetActive(true);
            secondWind = west;
            west.gameObject.SetActive(true);
        }

        yield return wait;

    }
}
