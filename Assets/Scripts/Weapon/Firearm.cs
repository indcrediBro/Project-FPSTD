using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Firearm : MonoBehaviour
{

    public Camera cam;
    public GameObject bullet;
    public Transform bulletSpawnPoint;   
    public TextMeshPro ammoDisplay;


    public float range = 100f;
    public float fireRate = 0.1f;
    public float reloadTime = 1f;
    public float bulletLaunchVelocity = 20f;
    public int ammoSize = 20;
    public int reserveSize = 60;

    private int currentAmmo;
    private int currentReserve;

    private bool isReloading = false;
    private bool isFiring = false;

    private void Awake()
    {
        currentAmmo = ammoSize;
        currentReserve = reserveSize;
    }


    private void Update()
    {
        var attemptFire = Input.GetKey(KeyCode.Mouse0);
        var attemptReload = Input.GetKeyDown(KeyCode.R);

        if (isFiring == false && isReloading == false)
        {
            if (attemptReload) { Reload(); }
        
            else if (attemptFire)
            {
                if (currentAmmo > 0) { Fire(); }
                else { Reload(); }
            }
        }
    }

    private void Fire()
    {

        // lock fire
        isFiring = true;

        // calculate direction to aim
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        Vector3 aim;
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, range))
            aim = hit.point;
        else
            aim = ray.GetPoint(range);
        Vector3 direction = aim - bulletSpawnPoint.position;

        // fire bullet
        GameObject newBullet = Instantiate(bullet, bulletSpawnPoint.position, Quaternion.identity);
        newBullet.transform.forward = direction;
        newBullet.GetComponent<Rigidbody>().AddForce(direction.normalized * bulletLaunchVelocity, ForceMode.Impulse);

        // done
        currentAmmo--;
        Invoke("DoneFire", fireRate);

    }

    private void DoneFire()
    {
        isFiring = false;
    }

    private void Reload()
    {
        // can reload?
        var toRefill = ammoSize - currentAmmo;
        if (toRefill <= 0 || currentReserve <= 0)
        {
            return;
        }

        // reloading
        isReloading = true;
        if (currentReserve < toRefill)
        {
            toRefill = currentReserve;
            currentReserve = 0;
        } else
        {
            currentReserve -= toRefill;
        }

        // done
        currentAmmo += toRefill;
        Invoke("DoneReload", reloadTime);
    }

    private void DoneReload()
    {
        isReloading = false;
    }

    private void UpdateText()
    {
        if (ammoDisplay != null)
        {
            ammoDisplay.SetText(currentAmmo + " / " + currentReserve);
        }
    }
}
