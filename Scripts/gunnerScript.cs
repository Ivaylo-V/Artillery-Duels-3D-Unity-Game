using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunnerScript : MonoBehaviour
{
    [SerializeField] private LineRenderer linerenderer;
    [SerializeField] private Transform releasePos;
    [SerializeField] private float spreadAmount = 0.5f;

    private float ForceAmount = 100;
    private int linePoints = 25;
    private float timeBetweenPoints = 0.1f;
    private Vector3 target;
    private float TimeToNextBullet;
    private bool ReadyToShoot = true;
    private LayerMask CollisionMask;

    public int shotsFired = 0;
    public GameObject barrel;
    public GameObject projectile;
    public GameObject hitsplat;
    public GameObject mapSplat;
    public GameObject topCam;
    public GameObject MainCamera;
    public GameObject gunnerCam;
    public GameObject FiredShell;
    public bool collisionAttack;
    public bool aboveGroundAttack;
    public bool AirAttack;
    public bool PenetrationAttack;
    public Vector3 hitPoint;


    public void Start()
    {
        MainCamera = GameObject.Find("camHolder/Main Camera");
        topCam = GameObject.Find("mapViewer");
        gunnerCam = GameObject.Find("GunnerCam");
        hitsplat = GameObject.Find("HitSplat");
        mapSplat = GameObject.Find("camHolder/mapSplat");
    }

    private void Awake()
    {
        int CollisionLayer = projectile.gameObject.layer;

        for (int i = 0; i < 32; i++)
        {
            if(!Physics.GetIgnoreLayerCollision(CollisionLayer, i))
            {
                CollisionMask |= 1 << i; //voodoo magic
            }
        }
    }

    private void FixedUpdate()
    {
        ForceAmount = gameObject.GetComponent<life>().trueForce;

        if (gameObject.GetComponent<life>().ShotsLeft == 0)
        {
            shotsFired = 0;
            ReadyToShoot = true;
            gameObject.GetComponent<life>().turnOver = true;
            endShooting();
        }

        if (Input.GetButton("Fire2"))
        {
            endShooting();
        }

        hitsplat.GetComponent<MeshRenderer>().enabled = true;
        DrawProjection();
        PullTrigger();
        gameObject.GetComponent<life>().currentShots = shotsFired;
    }

    private void PullTrigger()
    {
        if (ReadyToShoot && Input.GetButton("Fire1") && gameObject.GetComponent<life>().ShotsLeft > 0)
        {
            ReadyToShoot = false;
            TimeToNextBullet = gameObject.GetComponent<life>().reloadTime;
            Shoot();
        }

    }

    private void Shoot()
    {
        Ray ray = new Ray(barrel.transform.position, barrel.transform.forward);

        RaycastHit hit;


        if (Physics.Raycast(ray, out hit))
        {
            target = ray.GetPoint(ForceAmount);
        }
        else
        {
            target = ray.GetPoint(ForceAmount);
        }

        Vector3 Direction = target - barrel.transform.position;

        spreadAmount -= gameObject.GetComponent<life>().accuracy;
        float SpreadX = Random.Range(-spreadAmount, spreadAmount);
        float SpreadY = Random.Range(-spreadAmount, spreadAmount);

        Vector3 DirectionWithSpread = Direction + new Vector3(SpreadX, SpreadY, 0f);

        FiredShell = Instantiate(projectile, barrel.transform.position, Quaternion.identity);

        FiredShell.transform.forward = Direction;

        FiredShell.GetComponent<Rigidbody>().AddForce(DirectionWithSpread.normalized * ForceAmount, ForceMode.Impulse);

        FiredShell.GetComponent<impactScript>().owner = gameObject.GetComponent<life>().owner;

        FiredShell.GetComponent<impactScript>().alreadyShot = true;

        shotsFired++;
        gameObject.GetComponent<life>().ShotsLeft--;

        if (shotsFired < gameObject.GetComponent<life>().maximumShotsPerTurn)
        {
            Invoke(nameof(ResetAttack), TimeToNextBullet);
        }
        else
        {
            shotsFired = 0;
            ReadyToShoot = true;
            gameObject.GetComponent<life>().turnOver = true;
            endShooting();
        }


    }

    private void ResetAttack()
    {
        ReadyToShoot = true;
    }

    private void endShooting()
    {
        mapSplat.GetComponent<pinToMap>().isActivated = true;
        gunnerCam.transform.SetParent(null);
        gunnerCam.GetComponent<cameraMovement>().enabled = false;
        gunnerCam.GetComponent<Camera>().enabled = false;
        gunnerCam.GetComponent<AudioListener>().enabled = false;
        MainCamera.GetComponent<Camera>().enabled = true;
        MainCamera.GetComponent<AudioListener>().enabled = true;
        MainCamera.GetComponentInParent<moveScreenWithMouse>().enabled = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        hitsplat.GetComponent<MeshRenderer>().enabled = false;
        linerenderer.enabled = false;
        gameObject.GetComponent<gunnerScript>().enabled = false;
    }

    private void DrawProjection()
    {
        linerenderer.enabled = true;
        linerenderer.positionCount = Mathf.CeilToInt(linePoints / timeBetweenPoints) + 1;

        Vector3 startPos = releasePos.position;
        Vector3 startVelocity = ForceAmount * barrel.transform.forward / projectile.GetComponent<Rigidbody>().mass;

        int i = 0;
        linerenderer.SetPosition(i, startPos);

        for (float time = 0; time < linePoints; time += timeBetweenPoints)
        {
            i++;
            Vector3 point = startPos + time * startVelocity;
            point.y = startPos.y + startVelocity.y * time + (Physics.gravity.y / 2f * time * time);

            linerenderer.SetPosition(i, point);

            Vector3 lastPos = linerenderer.GetPosition(i - 1);

            if (Physics.Raycast(lastPos, (point - lastPos).normalized, out RaycastHit hit, (point - lastPos).magnitude, CollisionMask))
            {
                hitPoint = hit.point;
                linerenderer.SetPosition(i, hit.point);
                linerenderer.positionCount = i + 1;
                hitsplat.transform.position = new Vector3(hit.point.x + 50, hit.point.y, hit.point.z + 50);
                return;
            }
        }
    }
}
