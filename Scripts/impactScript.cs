using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class impactScript : MonoBehaviour
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
    [SerializeField] private GameObject FieldOfView;
    [SerializeField] private bool HEshell, ADShell, HEATshell, FlareShell, ClusterShell, IncindiaryShell, ThermoBaricShell;

    public float xpGain;
    public GameObject owner;
    public bool alreadyShot;

    private bool defenceSuccessful;
    private bool AlreadyExploded;
    private bool hit;

    private void Start()
    {
        AlreadyExploded = false;
        hit = false;
        xpGain = 0;
        Explosion.Stop();
        FlameAftermath.Stop();
        expoCloud.Stop();
        Sparks.Play();
        PressurizedCloudTrail.Play();
        InitalSpark.Play();
        InitalSmoke.Play();
        gameObject.GetComponent<MeshRenderer>().enabled = true;
    }

    private void FixedUpdate()
    {

        if (!hit)
        {
            transform.forward = gameObject.GetComponent<Rigidbody>().velocity;
        }

        if (owner.GetComponent<playerScript>().unit.GetComponent<gunnerScript>().AirAttack)
        {
            if (alreadyShot && !AlreadyExploded)
            {
                alreadyShot = false;
                Invoke(nameof(FlareUp), 3 + (owner.GetComponent<playerScript>().unit.GetComponent<life>().trueForce / 25));
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        hit = true;

        if(FieldOfView != null)
        {
            FieldOfView.SetActive(false);
        }

        if (owner.GetComponent<playerScript>().unit.GetComponent<gunnerScript>().PenetrationAttack)
        {
            blastRadius /= 1.5f;
            disableChance *= 2f;
            blastDmg *= 1.5f;
        }
        else if (owner.GetComponent<playerScript>().unit.GetComponent<gunnerScript>().aboveGroundAttack)
        {
            blastRadius *= 1.5f;
            disableChance /= 1.5f;
            blastDmg *= 2.5f;
        }
        else if (owner.GetComponent<playerScript>().unit.GetComponent<gunnerScript>().AirAttack)
        {
            blastRadius /= 2.5f;
            disableChance /= 2.5f;
            blastDmg /= 2.5f;
        }

        if(!IncindiaryShell || !FlareShell || !ADShell)
        {

            Collider[] hits = Physics.OverlapSphere(gameObject.transform.position, blastRadius);
            Collider[] alerted = Physics.OverlapSphere(gameObject.transform.position, blastRadius * 2);

            if(hits != null)
            {
                foreach (Collider hit in hits)
                {
                    if(hit.GetComponent<life>() == true)
                    {

                        if(hit.GetComponent<life>().triangulate == true)
                        {
                            hit.GetComponent<Triangulate>().enabled = true;
                            hit.GetComponent<Triangulate>().target = owner.GetComponent<playerScript>().unit;
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
                        xpGain += 1000 / distanceFromBlast.magnitude;

                        if (!defenceSuccessful)
                        { 

                            hit.GetComponent<life>().armor -= (blastDmg / distanceFromBlast.magnitude) + owner.GetComponent<playerScript>().unit.GetComponent<life>().bonusDMG;
                            hit.GetComponent<life>().driverHealth -= ((blastDmg / distanceFromBlast.magnitude) / hit.GetComponent<life>().armor) + owner.GetComponent<playerScript>().unit.GetComponent<life>().bonusDMG;
                            hit.GetComponent<life>().commanderHealth -= ((blastDmg / distanceFromBlast.magnitude) / hit.GetComponent<life>().armor) + owner.GetComponent<playerScript>().unit.GetComponent<life>().bonusDMG;
                            hit.GetComponent<life>().gunnerHealth -= ((blastDmg / distanceFromBlast.magnitude) / hit.GetComponent<life>().armor) + owner.GetComponent<playerScript>().unit.GetComponent<life>().bonusDMG;
                            hit.GetComponent<life>().engineerHealth -= ((blastDmg / distanceFromBlast.magnitude) / hit.GetComponent<life>().armor) + owner.GetComponent<playerScript>().unit.GetComponent<life>().bonusDMG;


                            int luckFactor = Random.Range(0, 100);
                            float trueChance = luckFactor - distanceFromBlast.magnitude;
                            int canNamesIntRandom = Random.Range(1, 4);

                            if(trueChance >= 100 - disableChance)
                            {
                                if(canNamesIntRandom == 1)
                                {
                                    hit.GetComponent<life>().canDrive = false;
                                }
                                else if(canNamesIntRandom == 2)
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
                        if(hit.GetComponent<Rigidbody>() != null)
                        {
                            hit.GetComponent<Rigidbody>().AddExplosionForce(101, transform.position, blastRadius);
                        }
                    }


                }
            }

            if(alerted != null)
            {
                foreach(Collider unit in alerted)
                {
                    if(unit.GetComponent<life>() != null && unit.CompareTag("Interactable"))
                    {
                        Vector3 distance = gameObject.transform.position - unit.transform.position;

                        if(distance.magnitude < unit.transform.GetComponent<life>().fieldOfVision)
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
        else
        {
            if (ADShell)
            {
                Sparks.Stop();
                PressurizedCloudTrail.Stop();
                InitalSpark.Stop();
                FlameAftermath.Play();
                Explosion.Play();
                expoCloud.Play();
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                gameObject.GetComponent<MeshRenderer>().enabled = true;
            }
            else if(FlareShell)
            {
                destroyObj();
            }
            else if (IncindiaryShell)
            {
                Destroy(gameObject);
            }

        }
    }

    void destroyObj()
    {
        owner.GetComponent<playerScript>().attack();
        owner.GetComponent<playerScript>().xp += xpGain;
        Explosion.Stop();
        expoCloud.Stop();
        FlameAftermath.Stop();
        Destroy(gameObject);
    }

    private void FlareUp()
    {
        if (ClusterShell)
        {
            Vector3 InitailDirection = gameObject.transform.position - owner.GetComponent<playerScript>().unit.GetComponent<gunnerScript>().hitPoint;
            GameObject Bullet = Instantiate(Projectile, new Vector3(gameObject.transform.position.x - 3.0f, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
            GameObject Bullet2 = Instantiate(Projectile, new Vector3(gameObject.transform.position.x + 3.0f, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
            GameObject Bullet3 = Instantiate(Projectile, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 3.0f), Quaternion.identity);
            GameObject Bullet4 = Instantiate(Projectile, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 3.0f), Quaternion.identity);
            GameObject Bullet5 = Instantiate(Projectile, new Vector3(gameObject.transform.position.x - 6.0f, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
            GameObject Bullet6 = Instantiate(Projectile, new Vector3(gameObject.transform.position.x + 6.0f, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
            GameObject Bullet7 = Instantiate(Projectile, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 6.0f), Quaternion.identity);
            GameObject Bullet8 = Instantiate(Projectile, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 6.0f), Quaternion.identity);
            GameObject[] Bullets = new GameObject[] { Bullet, Bullet2, Bullet3, Bullet4, Bullet5, Bullet6, Bullet7, Bullet8 };

            foreach(GameObject bullet in Bullets)
            {
                Vector3 direction = bullet.transform.position - gameObject.transform.position;
                float distance = Vector3.Distance(gameObject.transform.position, owner.GetComponent<playerScript>().unit.GetComponent<gunnerScript>().barrel.transform.position);
                Vector3 velocity = gameObject.GetComponent<Rigidbody>().velocity;
                bullet.GetComponent<impactScript>().AlreadyExploded = true;
                bullet.GetComponent<Rigidbody>().velocity = velocity;
                bullet.GetComponent<Rigidbody>().AddForce(direction * Random.Range(1,3), ForceMode.Impulse);
            }

            AlreadyExploded = true;
            Destroy(gameObject);
        }
        else if (IncindiaryShell)
        {
            Vector3 InitailDirection = gameObject.transform.position - owner.GetComponent<playerScript>().unit.GetComponent<gunnerScript>().hitPoint;
            GameObject Bullet = Instantiate(Projectile, new Vector3(gameObject.transform.position.x - 3.0f, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
            GameObject Bullet2 = Instantiate(Projectile, new Vector3(gameObject.transform.position.x + 3.0f, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
            GameObject Bullet3 = Instantiate(Projectile, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 3.0f), Quaternion.identity);
            GameObject Bullet4 = Instantiate(Projectile, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 3.0f), Quaternion.identity);
            GameObject Bullet5 = Instantiate(Projectile, new Vector3(gameObject.transform.position.x - 6.0f, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
            GameObject Bullet6 = Instantiate(Projectile, new Vector3(gameObject.transform.position.x + 6.0f, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
            GameObject Bullet7 = Instantiate(Projectile, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 6.0f), Quaternion.identity);
            GameObject Bullet8 = Instantiate(Projectile, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 6.0f), Quaternion.identity);
            GameObject[] Bullets = new GameObject[] { Bullet, Bullet2, Bullet3, Bullet4, Bullet5, Bullet6, Bullet7, Bullet8 };

            foreach (GameObject bullet in Bullets)
            {
                Vector3 direction = bullet.transform.position - gameObject.transform.position;
                float distance = Vector3.Distance(gameObject.transform.position, owner.GetComponent<playerScript>().unit.GetComponent<gunnerScript>().barrel.transform.position);
                Vector3 velocity = gameObject.GetComponent<Rigidbody>().velocity;
                bullet.GetComponent<impactScript>().AlreadyExploded = true;
                bullet.GetComponent<Rigidbody>().velocity = velocity;
                bullet.GetComponent<Rigidbody>().AddForce(direction * Random.Range(1, 3), ForceMode.Impulse);
                bullet.GetComponent<Rigidbody>().drag = 1;
            }

            AlreadyExploded = true;
            Destroy(gameObject);
        }
        else if (ADShell)
        {
            Vector3 InitailDirection = gameObject.transform.position - owner.GetComponent<playerScript>().unit.GetComponent<gunnerScript>().hitPoint;
            GameObject Bullet = Instantiate(Projectile, new Vector3(gameObject.transform.position.x - 3.0f, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
            GameObject Bullet2 = Instantiate(Projectile, new Vector3(gameObject.transform.position.x + 3.0f, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
            GameObject Bullet3 = Instantiate(Projectile, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 3.0f), Quaternion.identity);
            GameObject Bullet4 = Instantiate(Projectile, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 3.0f), Quaternion.identity);
            GameObject Bullet5 = Instantiate(Projectile, new Vector3(gameObject.transform.position.x - 6.0f, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
            GameObject Bullet6 = Instantiate(Projectile, new Vector3(gameObject.transform.position.x + 6.0f, gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
            GameObject Bullet7 = Instantiate(Projectile, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + 6.0f), Quaternion.identity);
            GameObject Bullet8 = Instantiate(Projectile, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 6.0f), Quaternion.identity);
            GameObject[] Bullets = new GameObject[] { Bullet, Bullet2, Bullet3, Bullet4, Bullet5, Bullet6, Bullet7, Bullet8 };

            foreach (GameObject bullet in Bullets)
            {
                Vector3 direction = bullet.transform.position - gameObject.transform.position;
                float distance = Vector3.Distance(gameObject.transform.position, owner.GetComponent<playerScript>().unit.GetComponent<gunnerScript>().barrel.transform.position);
                Vector3 velocity = gameObject.GetComponent<Rigidbody>().velocity;
                bullet.GetComponent<impactScript>().AlreadyExploded = true;
                bullet.GetComponent<Rigidbody>().velocity = velocity;
                bullet.GetComponent<Rigidbody>().AddForce(direction * Random.Range(1, 3), ForceMode.Impulse);
            }

            AlreadyExploded = true;
            Destroy(gameObject);

        }
        else if (FlareShell)
        {
            FieldOfView.SetActive(true);
            Invoke(nameof(destroyObj), 5);
        }
    }

}
