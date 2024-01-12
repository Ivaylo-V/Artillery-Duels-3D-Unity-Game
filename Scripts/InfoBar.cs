using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoBar : MonoBehaviour
{
    [SerializeField] GameObject BarFull;
    [SerializeField] GameObject BarEmpty;

    public float PercentFull;

    private void FixedUpdate()
    {
        BarFull.transform.localScale = new Vector3(PercentFull, 1, 1);
    }

}
