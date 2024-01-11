using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destruction : MonoBehaviour
{

    [SerializeField] private ParticleSystem smokeExp;
    [SerializeField] private ParticleSystem fire;
    void Update()
    {
        if (gameObject.GetComponent<life>().destroyed == true)
        {
            smokeExp.Play();
            fire.Play();
            Invoke(nameof(cleanUp), 5);
        }

    }
        private void cleanUp()
        {
            smokeExp.Stop();
            fire.Stop();
        }
}
