using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cleanUpSmoke : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(destroyObj), 19);
    }

    private void destroyObj()
    {
        Destroy(gameObject);
    }
}
