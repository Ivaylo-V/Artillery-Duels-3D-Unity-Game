using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject[] PreLoadedObjects;
    [SerializeField] private GameObject QuadrantManager;
    [SerializeField] private int xPos;
    [SerializeField] private int yPos;
    [SerializeField] private int CurrentlySpawnedAmount;
    [SerializeField] private int SpawnAmount;
    [SerializeField] private int SpawnMin;
    [SerializeField] private int SpawnMax;
    [SerializeField] private int TotallySpawned;

    [SerializeField] private int[] xMin = new int[] { -1000, -1000, -1000, -1000, -1000, 0, 0, 0, 0, 0, 1000, 1000, 1000, 1000, 1000, 2000, 2000, 2000, 2000, 2000, 3000, 3000, 3000, 3000, 3000 };
    [SerializeField] private int[] xMax = new int[] { 0, 0, 0, 0, 0, 1000, 1000, 1000, 1000, 1000, 2000, 2000, 2000, 2000, 2000, 3000, 3000, 3000, 3000, 3000, 4000, 4000, 4000, 4000, 4000 };
    [SerializeField] private int[] yMin = new int[] { 0, 1000, 2000, 3000, 4000, 0, 1000, 2000, 3000, 4000, 0, 1000, 2000, 3000, 4000, 0, 1000, 2000, 3000, 4000, 0, 1000, 2000, 3000, 4000 };
    [SerializeField] private int[] yMax = new int[] { 1000, 2000, 3000, 4000, 5000, 1000, 2000, 3000, 4000, 5000, 1000, 2000, 3000, 4000, 5000, 1000, 2000, 3000, 4000, 5000, 1000, 2000, 3000, 4000, 5000 };
    [SerializeField] private int QuadrantCurrent;
    private bool ready;

    private void Start()
    {
        QuadrantCurrent = 0;
        TotallySpawned = 0;
        ready = true;
    }

    private void FixedUpdate()
    {
        if(QuadrantManager.GetComponent<QuadrantManager>().enabled == false)
        {
            if(QuadrantCurrent < 25)
            {
                if (ready)
                {
                    SpawnAmount = Random.Range(SpawnMin, SpawnMax);
                    ready = false;
                    Spawner();
                }
            }
            else
            {
                gameObject.GetComponent<SpawnerScript>().enabled = false;
            }
        }


    }

    private void Spawner()
    {
        while (CurrentlySpawnedAmount < SpawnAmount)
        {
            xPos = Random.Range(xMin[QuadrantCurrent], xMax[QuadrantCurrent]);
            yPos = Random.Range(yMin[QuadrantCurrent], yMax[QuadrantCurrent]);

            Vector3 Point = new Vector3(xPos, 550f, yPos);
            Ray ray = new Ray(Point, -transform.up);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Ground"))
                {
                    PreLoadedObjects[TotallySpawned].SetActive(true);
                    PreLoadedObjects[TotallySpawned].transform.position = hit.point;
                    TotallySpawned += 1;
                    CurrentlySpawnedAmount += 1;
                }
            }

        }

        if (CurrentlySpawnedAmount == SpawnAmount)
        {
            CurrentlySpawnedAmount = 0;
            QuadrantCurrent++;
            ready = true;
        }
    }
}
