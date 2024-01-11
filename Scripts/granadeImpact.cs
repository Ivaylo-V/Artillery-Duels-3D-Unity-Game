using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class granadeImpact : MonoBehaviour
{
    [SerializeField] private ParticleSystem smokeExpo;
    [SerializeField] private ParticleSystem sparkExpo;
    [SerializeField] private GameObject propeller;
    [SerializeField] private GameObject propeller1;
    [SerializeField] private GameObject propeller2;
    [SerializeField] private GameObject propeller3;

    private bool alreadyExploded;

    private void Start()
    {
        smokeExpo.Stop();
        sparkExpo.Stop();
        alreadyExploded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!alreadyExploded)
        {
            alreadyExploded = true;

            Collider[] hits = Physics.OverlapSphere(gameObject.transform.position, 10);

            foreach (Collider hit in hits)
            {
                if (hit.GetComponent<life>() == true)
                {
                    Debug.Log($"{hit}");

                    Vector3 distanceFromBlast = hit.gameObject.transform.position - gameObject.transform.position;

                    int dmg = Random.Range(5, 50);
                    hit.GetComponent<life>().armor -= (dmg / distanceFromBlast.magnitude) / hit.GetComponent<life>().armor;
                    hit.GetComponent<life>().driverHealth -= ((dmg / distanceFromBlast.magnitude) / hit.GetComponent<life>().armor) + Random.Range(0, 25);
                    hit.GetComponent<life>().commanderHealth -= ((dmg / distanceFromBlast.magnitude) / hit.GetComponent<life>().armor) + Random.Range(0, 25);
                    hit.GetComponent<life>().gunnerHealth -= ((dmg / distanceFromBlast.magnitude) / hit.GetComponent<life>().armor) + Random.Range(0, 25);
                    hit.GetComponent<life>().engineerHealth -= ((dmg / distanceFromBlast.magnitude) / hit.GetComponent<life>().armor) + Random.Range(0, 25);
                }
            }

            smokeExpo.Play();
            sparkExpo.Play();
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            propeller.transform.parent = null;
            propeller.GetComponent<Rigidbody>().AddExplosionForce(300, collision.transform.position, 50);
            propeller1.transform.parent = null;
            propeller1.GetComponent<Rigidbody>().AddExplosionForce(300, collision.transform.position, 50);
            propeller2.transform.parent = null;
            propeller2.GetComponent<Rigidbody>().AddExplosionForce(300, collision.transform.position, 30);
            propeller3.transform.parent = null;
            propeller3.GetComponent<Rigidbody>().AddExplosionForce(300, collision.transform.position, 50);
            Invoke(nameof(DestroyNade), 4);
        }
    }

    private void DestroyNade()
    {

        alreadyExploded = false;
        Destroy(propeller1);
        Destroy(propeller2);
        Destroy(propeller3);
        Destroy(propeller);
        Destroy(gameObject);

    }


}
