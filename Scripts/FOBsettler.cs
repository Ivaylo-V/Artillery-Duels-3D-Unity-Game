using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOBsettler : MonoBehaviour
{
    [SerializeField] GameObject[] RotatableObjects;
    void Start()
    {
        Invoke(nameof(finalPosition), 4);
    }


    private void finalPosition()
    {
        foreach(GameObject obj in RotatableObjects)
        {
            obj.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
