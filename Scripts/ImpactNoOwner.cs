using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactNoOwner : MonoBehaviour
{

    [SerializeField] private ParticleSystem Explosion;
    [SerializeField] private ParticleSystem Sparks;
    [SerializeField] private ParticleSystem PressurizedCloudTrail;
    [SerializeField] private ParticleSystem expoCloud;
    [SerializeField] private ParticleSystem InitalSmoke;
    [SerializeField] private ParticleSystem InitalSpark;
    [SerializeField] private ParticleSystem FlameAftermath;
    [SerializeField] private float blastRadius = 40;
    [SerializeField] private float blastDmg = 150;
    [SerializeField] private float disableChance = 30;
    [SerializeField] private GameObject Projectile;

    public GameObject owner;
    public bool alreadyShot;

    private bool defenceSuccessful;
    private bool AlreadyExploded;
    private bool hit;

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        owner.GetComponent<AiGunner>().ActualHit = gameObject.transform.position;
        Collider[] hits = Physics.OverlapSphere(gameObject.transform.position, blastRadius);
        Collider[] alerted = Physics.OverlapSphere(gameObject.transform.position, blastRadius * 2);

        if (hits != null)
        {
            foreach (Collider hit in hits)
            {
                if (hit.GetComponent<life>() == true)
                {
                    if (hit.GetComponent<life>().triangulate == true)
                    {
                        hit.GetComponent<Triangulate>().enabled = true;
                        hit.GetComponent<Triangulate>().target = owner.gameObject;
                    }

                    Debug.Log($"{hit.name}");
                    int luck = Random.Range(0, 100);
                    int neededLuck = hit.GetComponent<life>().defenceLvl * 5;

                    if (luck <= neededLuck)
                    {
                        defenceSuccessful = true;
                    }
                    else
                    {
                        defenceSuccessful = false;
                    }

                    Vector3 distanceFromBlast = hit.gameObject.transform.position - gameObject.transform.position;

                    if (!defenceSuccessful)
                    {
                        hit.GetComponent<life>().armor -= (blastDmg / distanceFromBlast.magnitude);
                        hit.GetComponent<life>().driverHealth -= ((blastDmg / distanceFromBlast.magnitude) / hit.GetComponent<life>().armor);
                        hit.GetComponent<life>().commanderHealth -= ((blastDmg / distanceFromBlast.magnitude) / hit.GetComponent<life>().armor);
                        hit.GetComponent<life>().gunnerHealth -= ((blastDmg / distanceFromBlast.magnitude) / hit.GetComponent<life>().armor);
                        hit.GetComponent<life>().engineerHealth -= ((blastDmg / distanceFromBlast.magnitude) / hit.GetComponent<life>().armor);


                        int luckFactor = Random.Range(0, 100);
                        float trueChance = luckFactor - distanceFromBlast.magnitude;
                        int canNamesIntRandom = Random.Range(1, 4);

                        if (trueChance >= 100 - disableChance)
                        {
                            if (canNamesIntRandom == 1)
                            {
                                hit.GetComponent<life>().canDrive = false;
                            }
                            else if (canNamesIntRandom == 2)
                            {
                                hit.GetComponent<life>().canShoot = false;
                            }
                            else if (canNamesIntRandom == 3)
                            {
                                hit.GetComponent<life>().canRepair = false;
                            }
                            else if (canNamesIntRandom == 4)
                            {
                                hit.GetComponent<life>().canCommand = false;
                            }
                        }
                    }
                    else
                    {
                        Debug.Log("Defences did their job");
                    }
                }
                else if (!hit.GetComponent<life>() == true)
                {
                    if (hit.GetComponent<Rigidbody>() != null)
                    {
                        hit.GetComponent<Rigidbody>().AddExplosionForce(101, transform.position, blastRadius);
                    }
                }


            }
        }

        if (alerted != null)
        {
            foreach (Collider unit in alerted)
            {
                if (unit.GetComponent<life>() != null && unit.CompareTag("Interactable"))
                {
                    Vector3 distance = gameObject.transform.position - unit.transform.position;

                    if (distance.magnitude < unit.transform.GetComponent<life>().fieldOfVision)
                    {
                        unit.GetComponent<life>().owner.GetComponent<playerScript>().defence();
                    }
                }
            }
        }

        Sparks.Stop();
        PressurizedCloudTrail.Stop();
        InitalSpark.Stop();
        FlameAftermath.Play();
        Explosion.Play();
        expoCloud.Play();
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        Invoke(nameof(destroyObj), 10);
    }

    void destroyObj()
    {
        Explosion.Stop();
        expoCloud.Stop();
        FlameAftermath.Stop();
        Destroy(gameObject);
    }
}
