using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pinToMap : MonoBehaviour
{

    public bool isActivated;
    private void FixedUpdate()
    {
        if (isActivated)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            gameObject.transform.position = new Vector3(Camera.main.transform.position.x - 50, (Camera.main.transform.parent.gameObject.transform.position.y - Camera.main.transform.parent.gameObject.transform.position.y) + 2, Camera.main.transform.position.z + 50);
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
