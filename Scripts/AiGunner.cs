using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiGunner : MonoBehaviour
{
    [SerializeField] GameObject startPosition; //Put barrel end here
    [SerializeField] float MaxForce;
    [SerializeField] GameObject projectile;
    [SerializeField] int TotalShots = 100;

    private float angleY;
    private Vector3 velocity;
    private Vector3 target;
    bool fireForEffect = false;

    public Vector3 ActualHit;

    private void Start()
    {
        StartCoroutine(findTargetRoutine());
    }

    private IEnumerator findTargetRoutine()
    {
        while (true)
        {

            GameObject[] interactables = GameObject.FindGameObjectsWithTag("Interactable");

            foreach (GameObject obj in interactables)
            {

                float distance = Vector3.Distance(gameObject.transform.position, obj.transform.position);

                if (distance <= 200)
                {
                    target = obj.transform.position;
                    Calculations();
                    FireForEffect();
                    yield return new WaitForSeconds(10);

                }
            }
            yield return new WaitForSeconds(10);
        }

    }


    private void FireForEffect()
    {
        if (target != null && TotalShots > 0)
        {
            if(!fireForEffect)
            {
                Debug.Log("Fire For real");
                float diffX = target.x - ActualHit.x;
                float diffY = target.y - ActualHit.y;
                float diffZ = target.z - ActualHit.z;

                if(diffX < 0)
                {
                    Mathf.Abs(diffX);
                }

                if (diffY < 0)
                {
                    Mathf.Abs(diffY);
                }

                if (diffZ < 0)
                {
                    Mathf.Abs(diffZ);
                }

                target = new Vector3(target.x + diffX, target.y + diffY, target.z + diffZ);

                Calculations();

                gameObject.transform.LookAt(target);
                gameObject.transform.rotation = Quaternion.Euler(angleY, gameObject.transform.rotation.y, gameObject.transform.rotation.z);
                fireForEffect = true;
                Shoot();
            }
            else
            {

                Debug.Log("Fire for effect");
                gameObject.transform.LookAt(target);
                gameObject.transform.rotation = Quaternion.Euler(angleY, gameObject.transform.rotation.y, gameObject.transform.rotation.z);
                Shoot();
                fireForEffect = false;
            }
        }

    }

    private void Shoot()
    {
        GameObject firedShell = Instantiate(projectile, startPosition.transform.position, Quaternion.identity);
        firedShell.GetComponent<Rigidbody>().velocity = velocity;
        firedShell.GetComponent<ImpactNoOwner>().alreadyShot = true;
        firedShell.GetComponent<ImpactNoOwner>().owner = gameObject;
        TotalShots--;
    }

    private void Calculations()
    {
        Vector3 displacement = new Vector3(target.x, startPosition.transform.position.y, target.z) - startPosition.transform.position;

        float deltaY = target.y - startPosition.transform.position.y;
        float deltaXY = displacement.magnitude;

        float gravity = Mathf.Abs(Physics.gravity.y);
        float initialForce = Mathf.Clamp(Mathf.Sqrt(gravity * (deltaY + Mathf.Sqrt(Mathf.Pow(deltaY, 2) + Mathf.Pow(deltaXY, 2)))), 0.01f, MaxForce);

        float angle = Mathf.PI / 2f - (0.5f * (Mathf.PI / 2 - (deltaY / deltaXY)));

        Vector3 initialVelocity = Mathf.Cos(angle) * initialForce * displacement.normalized + Mathf.Sin(angle) * initialForce * Vector3.up;

        angleY = angle;
        velocity = initialVelocity;
    }
}
