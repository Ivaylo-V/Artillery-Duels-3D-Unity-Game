using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{

    public UnityEvent onInteract;
    public int ID;

    void Start()
    {
        ID = Random.Range(0, 9999);
    }

}
