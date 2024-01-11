using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadrantManager : MonoBehaviour
{
    [SerializeField] private int[] xMin = new int[] { -1000, -1000, -1000, -1000, -1000, 0, 0, 0, 0, 0, 1000, 1000, 1000, 1000, 1000, 2000, 2000, 2000, 2000, 2000, 3000, 3000, 3000, 3000, 3000 };
    [SerializeField] private int[] yMin = new int[] { 0, 1000, 2000, 3000, 4000, 0, 1000, 2000, 3000, 4000, 0, 1000, 2000, 3000, 4000, 0, 1000, 2000, 3000, 4000, 0, 1000, 2000, 3000, 4000 };
    [SerializeField] Terrain[] quadrantVariants;
    int currentlySpawned;
    int RandomNumber;
    bool ready;

    private void Start()
    {
        currentlySpawned = 0;
        ready = true;
    }

    private void FixedUpdate()
    {
        if (currentlySpawned < 25)
        {
            if (ready)
            {
                RandomNumber = Random.Range(0, 24);
                ready = false;
                Spawner();
            }
        }
        else
        {
            gameObject.GetComponent<QuadrantManager>().enabled = false;
        }
    }

   private void Spawner()
    {
        Terrain instance = Instantiate(quadrantVariants[0], new Vector3(xMin[currentlySpawned], 1, yMin[currentlySpawned]), Quaternion.identity);
        instance.name = $"{instance}{currentlySpawned}";
        currentlySpawned++;
        ready = true;
    }
}
