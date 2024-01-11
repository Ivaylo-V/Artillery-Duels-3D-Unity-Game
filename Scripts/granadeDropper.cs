using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class granadeDropper : MonoBehaviour
{
    [SerializeField] private GameObject dropper;
    [SerializeField] private GameObject granade;

    public int shotsFired = 0;

    private void Start()
    {
        shotsFired = 0;
    }

    void Update()
    {
        if (gameObject.transform.parent.gameObject.GetComponent<life>().canDropGranades)
        {
            granadeDrop();
        }

        if (gameObject.transform.parent.gameObject.GetComponent<life>().finishedShooting == true) 
        { 
            gameObject.GetComponent<granadeDropper>().enabled = false; 
        }
    }   

    private void granadeDrop()
    {
        if (Input.GetButtonDown("Fire1") && shotsFired < gameObject.transform.parent.gameObject.GetComponent<life>().maximumShotsPerTurn && gameObject.transform.parent.gameObject.GetComponent<life>().finishedShooting == false)
        {
            shotsFired++;
            gameObject.transform.parent.gameObject.GetComponent<life>().ShotsLeft--;
            Instantiate(granade, dropper.transform.position, Quaternion.identity);

        }

        if(shotsFired >= gameObject.transform.parent.gameObject.GetComponent<life>().maximumShotsPerTurn || gameObject.transform.parent.gameObject.GetComponent<life>().ShotsLeft <= 0)
        {
            gameObject.transform.parent.gameObject.GetComponent<life>().finishedShooting = true;
            shotsFired = 0;
        }
    }
}
 